using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace clientForQuestions2._0
{
    public partial class summrizePage : Form
    {
        List<Button> m_buttonList = new List<Button>();

        List<List<afterQuestionParametrs>> m_chapters;
        private WebView2 webView21;
        private WebView2 webView2_col;

        private int h_statsPlace = 40;
        private int h_questionsPlace = 70;
        private int w_lessonsPlace = 120; // for Lekachim (לקחים)
        private int width_webView;
        private int height_webView;
        private int Q_BUTTON_SIZE = 30;
        private int Q_CHOSEN_BUTTON_ADD_SIZE = 10;

        private List<List<String>> lessons_list = new List<List<String>>();

        private int col_id = 0;
        private int test_id = OperationsAndOtherUseful.NOT_A_REAL_TEST_ID;
        private int indexQuestion = -1;
        private int indexChapter = -1;
        private int m_currentIndexOfFirstButton = 0;

        public summrizePage(List<afterQuestionParametrs> questions, int test_id, int indexQuestion2display)
        {
            this.indexQuestion = indexQuestion2display;
            this.indexChapter = 0;
            this.m_chapters = new List<List<afterQuestionParametrs>>();
            this.m_chapters.Add( questions );
            this.test_id = test_id;
            
            orgnizeQuestions();
            InitializeComponent();

            // not showing lessons if it is not a real test
            if (this.test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
            {
                w_lessonsPlace = 0;
                this.lessons_label.Visible = false;
                this.lessons_richTextBox.Visible = false;
            }
            else
            {
                lessons_list.Add(TestHistoryFileHandler.get_lessons_of_test_in_order(this.test_id));
                if (lessons_list[indexChapter].Count == 0)
                {
                    lessons_list.Add(new List<string>());
                    for (int i = 0; i < m_chapters[indexChapter].Count; i++)
                        lessons_list[0].Add("");
                }
            }

            ToolTip copy_q_id_toolTip = new ToolTip();
            copy_q_id_toolTip.SetToolTip(curr_q_id, "Click to copy id to clipboard");
            copy_q_id_toolTip.AutoPopDelay = 5000;   // Duration the tooltip will remain visible (5 seconds)
            copy_q_id_toolTip.InitialDelay = 0;      // Delay before the tooltip appears (0 ms for instant display)
            copy_q_id_toolTip.ReshowDelay = 0;       // Delay between when the cursor moves from one tooltip to another (0 ms for instant display)
            copy_q_id_toolTip.ShowAlways = true;
            curr_q_id.Cursor = Cursors.Hand;

            // full screen
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            try
            {
                if (((JArray)m_chapters[indexChapter][indexQuestion].question.json_content["collections"]).Count != 0)
                {
                    col_id = (int)m_chapters[indexChapter][indexQuestion].question.json_content["collections"][0]["id"];
                }
            }
            catch (Exception ex) { }



            width_webView = (int)(Screen.PrimaryScreen.WorkingArea.Width - w_lessonsPlace) / 2;
            height_webView = Screen.PrimaryScreen.WorkingArea.Height - this.h_questionsPlace - h_statsPlace - OperationsAndOtherUseful.MARGIN_OF_HEIGHT;


            this.timeTookForQLabel.Text = "";
            this.timeTookForQLabel.Visible = false;
            displayStatsOfWholeChapter();

            createButtons(m_chapters[indexChapter]);
            displayButtons();
            //this.Resize += Form_Resize;
            this.Load += Form_Load;
            //Form_Resize(this, EventArgs.Empty);

        }

        /// <summary>
        /// FOR CHAPTERS
        /// This function gets a list of chapters, so each chapter is a list of afterQuestionParametrs.
        /// The 'indexQuestion2display' refers to the IndexOfQuestion variable
        /// </summary>
        public summrizePage(List<List<afterQuestionParametrs>> chapters, int test_id, int indexQuestion2display, List<string> names_of_chapters)
        {
            m_chapters = chapters;
            (this.indexChapter, this.indexQuestion) = get_number_of_question_and_index_chapter_by_index_of_question(indexQuestion2display);

            this.test_id = test_id;
            orgnizeQuestions();
            InitializeComponent();
            // not showing lessons if it is not a real test
            if (this.test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
            {
                w_lessonsPlace = 0;
                this.lessons_label.Visible = false;
                this.lessons_richTextBox.Visible = false;
            }
            else
            {
                lessons_list = TestHistoryFileHandler.get_lessons_of_test_with_chapters_in_order(this.test_id);
                for (int i=0; i < lessons_list.Count; i++)
                    if (lessons_list[i].Count == 0)
                        for (int j = 0; j < m_chapters[i].Count; j++)
                            lessons_list[i].Add("");
            }

            chapters_comboBox.Visible = true;
            foreach (string name in names_of_chapters)
            {
                chapters_comboBox.Items.Add(name);
            }

            chapters_comboBox.SelectedItem = chapters_comboBox.Items[this.indexChapter];

            ToolTip copy_q_id_toolTip = new ToolTip();
            copy_q_id_toolTip.SetToolTip(curr_q_id, "Click to copy id to clipboard");
            copy_q_id_toolTip.AutoPopDelay = 5000;   // Duration the tooltip will remain visible (5 seconds)
            copy_q_id_toolTip.InitialDelay = 0;      // Delay before the tooltip appears (0 ms for instant display)
            copy_q_id_toolTip.ReshowDelay = 0;       // Delay between when the cursor moves from one tooltip to another (0 ms for instant display)
            copy_q_id_toolTip.ShowAlways = true;
            curr_q_id.Cursor = Cursors.Hand;


            // full screen
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            try
            {
                if (((JArray)m_chapters[indexChapter][indexQuestion].question.json_content["collections"]).Count != 0)
                {
                    col_id = (int)m_chapters[indexChapter][indexQuestion].question.json_content["collections"][0]["id"];
                }
            }
            catch (Exception ex) { }

            width_webView = (int)(Screen.PrimaryScreen.WorkingArea.Width - w_lessonsPlace) / 2;
            height_webView = Screen.PrimaryScreen.WorkingArea.Height - this.h_questionsPlace - h_statsPlace - OperationsAndOtherUseful.MARGIN_OF_HEIGHT;


            this.timeTookForQLabel.Text = "";
            this.timeTookForQLabel.Visible = false;
            displayStatsOfWholeChapter();

            createButtons(m_chapters[indexChapter]);
            displayButtons();
            //this.Resize += Form_Resize;
            this.Load += Form_Load;
            //Form_Resize(this, EventArgs.Empty);

        }

        // returns (index of chapter, number of question)
        private (int, int) get_number_of_question_and_index_chapter_by_index_of_question(int index_of_question)
        {
            int num_of_previous_qs = 0;
            for (int i = 0; i < m_chapters.Count; i++)
            {
                num_of_previous_qs += m_chapters[i].Count + 1;
                if (num_of_previous_qs >= index_of_question)
                {
                    return (i, m_chapters[i].Count + index_of_question - num_of_previous_qs);
                }
            }

            return (-1, -1);
        }

        // returns (index of chapter, index of question)
        private int get_index_of_question_by_number_of_question_and_index_chapter(int index_chapter, int number_of_question)
        {
            if (m_chapters.Count == 1)
                return number_of_question;

            int num_of_previous_qs = 0;
            for (int i = 0; i < index_chapter; i++)
            {
                num_of_previous_qs += m_chapters[i].Count + 1;
            }

            return num_of_previous_qs + number_of_question + 1;
        }

        private async Task WaitForHandleAndInitializeAsync()
        {
            // Wait until the form's handle is created
            while (!this.IsHandleCreated)
            {
                await Task.Delay(100); // Asynchronous delay
            }

            // Perform initialization after the handle is created
            InitializeWebView2_col();
            InitializeWebView21();
        }


        private async void Form_Load(object sender, EventArgs e)
        {
            // Call the asynchronous method to wait and initialize
            await WaitForHandleAndInitializeAsync();
        }

        private void orgnizeQuestions()
        {
            foreach (List<afterQuestionParametrs> questions in m_chapters)
                questions.Sort((q1, q2) => q1.indexOfQuestion.CompareTo(q2.indexOfQuestion));
        }
        private void displayStatsOfWholeChapter()
        {
            int sum_time = 0;
            float sum_difficultLevel = 0;
            int corr_c = 0;

            foreach (afterQuestionParametrs qp in m_chapters[indexChapter])
            {
                sum_time += qp.timeForAnswer;

                sum_difficultLevel += (float) qp.question.json_content["difficulty_level"];

                if (qp.userAnswer == -1 || qp.userAnswer == OperationsAndOtherUseful.SKIPPED_Q)
                    continue;
                if (((JArray)qp.question.json_content["options"]).Count != 0)
                    if ((int)qp.question.json_content["options"][qp.userAnswer - 1]["is_correct"] == 1)
                        corr_c++;
                    else
                    if (((JArray)qp.question.json_content["option_images"]).Count != 0)
                        if ((int)qp.question.json_content["option_images"][qp.userAnswer - 1]["is_correct"] == 1)
                            corr_c++;
            }


            this.total_time.Text = $"זמן כולל: {OperationsAndOtherUseful.get_time_mmss_fromseconds(sum_time)}";
            this.avrage_time.Text = $"זמן ממוצע לשאלה: {OperationsAndOtherUseful.get_time_mmss_fromseconds(sum_time / m_chapters[this.indexChapter].Count)}";
            this.avrage_difficultyLevel.Text = $"רמת קושי ממוצעת לשאלה: {Math.Round(sum_difficultLevel / m_chapters[this.indexChapter].Count, 1)}";
            this.correct_answers.Text = $"תשובות נכונות: {corr_c}/{m_chapters[this.indexChapter].Count}";
            if (test_id != OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
                this.test_id_label.Text = $"test id: {this.test_id}";
            else
                this.test_id_label.Visible = false;
        }

        // to detect arrow keys preesed
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    LogFileHandler.writeIntoFile($"clicked left in summarize, trying to enter q no. {this.indexQuestion} from q no. {this.indexQuestion + 1}");
                    if (this.indexQuestion > 0 && this.indexQuestion < this.m_chapters[indexChapter].Count)
                    {
                        if ((this.indexQuestion + 1) % 10 == 1)
                            displayButtons(this.indexQuestion - 10, this.indexQuestion);

                        Button_Click(this.indexQuestion - 1);
                    }
                    return true;
                case Keys.Right:
                    LogFileHandler.writeIntoFile($"clicked right in summarize, trying to enter q no. {this.indexQuestion + 2} from q no. {this.indexQuestion + 1}");
                    if (this.indexQuestion >= 0 && this.indexQuestion < this.m_chapters[indexChapter].Count - 1)
                    {
                        if ((this.indexQuestion + 1) % 10 == 0)
                            displayButtons(this.indexQuestion + 1, this.indexQuestion + 11);

                        Button_Click(this.indexQuestion + 1);
                    }
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        //private void Form_Resize(object sender, EventArgs e)
        //{
        //    return;


        //    if (webView2pdf != null)
        //    {
        //        if(col_id == 0)
        //            webView2pdf.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - h_questionsPlace); // Adjust height based on form size
        //        else
        //            webView2pdf.Size = new Size((int) this.ClientSize.Width / 2, this.ClientSize.Height - h_questionsPlace); // Adjust height based on form size
        //    }
        //    if (webView2_col != null && col_id != 0)
        //    {
        //        Location = new Point((int)this.ClientSize.Width / 2, h_questionsPlace + h_statsPlace); // Adjust Y coordinate to leave space for buttons

        //        webView2_col.Size = new Size((int)this.ClientSize.Width / 2, this.ClientSize.Height - h_questionsPlace); // Adjust height based on form size

        //    }
        //    this.button1.Location = new Point(this.ClientSize.Width - button1.Width - 40, 20);
        //}
        private void enableButtons()
        {
            for(int i =0; i <m_buttonList.Count;i++)
            {
                m_buttonList[i].Enabled = true;
            }
        }

        private void displayCol()
        {
            dbQuestionParmeters q = this.m_chapters[this.indexChapter][this.indexQuestion].question;
            JArray collection = (JArray)q.json_content["collections"];
            if (collection == null || collection.Count == 0)
            {
                this.col_id = 0;
                hideCol();
            }
            else
            {
                int q_col_id = (int)collection[0]["id"];
                if (this.col_id == q_col_id) // this is the same collection, we dont need to display it again
                    return;

                this.col_id = q_col_id;
                showCol(q);
            }
        }

        private void hideCol()
        {
            //to remove previous html content
            webTaker.OnCoreWebView2_colDeleteContent(webView2_col);
        }

        private void showCol(dbQuestionParmeters q)
        {
            //this.webView2pdf.Size = new Size(width_screen, height_screen - h_questionsPlace - h_statsPlace); // Adjust size to fit below the buttons

            webTaker.OnCoreWebView2_colInitializationCompleted(webView2_col, q);
        }
        private void InitializeWebView21()
        {
            // Use InvokeRequired check to ensure all UI operations are marshaled back to the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView21));
                return;
            }

            // Initialize the WebView2 control
            webView21 = new WebView2
            {
                Location = new Point(w_lessonsPlace, h_questionsPlace + h_statsPlace), // Adjust Y coordinate to leave space for buttons
                Size = new Size(width_webView, height_webView), // Adjust size to fit below the buttons
            };

            webView21.CoreWebView2InitializationCompleted += OnCoreWebView21InitializationCompleted;

            // Attempt to initialize WebView2 runtime
            try
            {
                // Since WebView2 should be initialized on STA, but controls added on the UI thread, check once more:
                Invoke((MethodInvoker)(async () =>
                {
                    int maxRetries = 20;
                    int retryCount = 0;
                    bool initialized = false;

                    while (!initialized && retryCount < maxRetries)
                    {
                        try
                        {
                            retryCount++;

                            // Initialize WebView2 runtime asynchronously
                            await webView21.EnsureCoreWebView2Async(null);

                            // Check if WebView2 was initialized successfully
                            if (webView21.CoreWebView2 != null)
                            {
                                initialized = true; // Exit loop if initialization is successful
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log error details
                            Debug.WriteLine($"WebView2 initialization attempt {retryCount} failed: {ex}");
                            MessageBox.Show($"Attempt {retryCount} failed: {ex.Message}",
                                "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Add delay between retries
                        await Task.Delay(500); // Wait before retrying
                    }

                    if (initialized)
                    {
                        try
                        {
                            Controls.Add(webView21);
                            webView21.NavigationCompleted +=webTaker.webView_NavigationCompleted;
                            webView21.SendToBack(); // Ensure WebView2 is on back of other controls
                            if (this.m_buttonList.Count != 0)
                            {
                                Button_Click(this.indexQuestion); // First index after init
                                this.lessons_richTextBox.Enabled = true;
                            }
                            enableButtons();
                        }
                        catch (Exception addEx)
                        {
                            MessageBox.Show($"Error adding WebView2 to the form: {addEx.Message}",
                                "Add Control Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to initialize WebView2 after multiple attempts.",
                            "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during WebView2 initialization: {ex.Message}",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeWebView2_col()
        {
            // Use InvokeRequired check to ensure all UI operations are marshaled back to the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView2_col));
                return;
            }

            // Initialize the WebView2 control
            webView2_col = new WebView2
            {
                Location = new Point(width_webView + w_lessonsPlace, h_questionsPlace + h_statsPlace), // Adjust Y coordinate to leave space for buttons
                Size = new Size(width_webView, height_webView), // Adjust size to fit below the buttons
                //Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            webView2_col.CoreWebView2InitializationCompleted += OnCoreWebView2_colInitializationCompleted;

            // Attempt to initialize webView2_col runtime
            try
            {
                // Since webView2_col should be initialized on STA, but controls added on the UI thread, check once more:
                Invoke((MethodInvoker)(async () =>
                {
                    int maxRetries = 20;
                    int retryCount = 0;
                    bool initialized = false;

                    while (!initialized && retryCount < maxRetries)
                    {
                        try
                        {
                            retryCount++;

                            // Initialize webView2_col runtime asynchronously
                            await webView2_col.EnsureCoreWebView2Async(null);

                            // Check if WebView2 was initialized successfully
                            if (webView2_col.CoreWebView2 != null)
                            {
                                initialized = true; // Exit loop if initialization is successful
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log error details
                            Debug.WriteLine($"webView2_col initialization attempt {retryCount} failed: {ex}");
                            MessageBox.Show($"Attempt {retryCount} failed: {ex.Message}",
                                "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        // Add delay between retries
                        await Task.Delay(500); // Wait before retrying
                    }

                    if (initialized)
                    {
                        try
                        {
                            Controls.Add(webView2_col);
                            webView2_col.NavigationCompleted += webTaker.webView_NavigationCompleted;
                            webView2_col.SendToBack(); // Ensure webView2_col is on back of other controls
                        }
                        catch (Exception addEx)
                        {
                            MessageBox.Show($"Error adding webView2_col to the form: {addEx.Message}",
                                "Add Control Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to initialize webView2_col after multiple attempts.",
                            "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during webView2_col initialization: {ex.Message}",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnCoreWebView21InitializationCompleted(object sender, EventArgs e)
        {

            webTaker.OnCoreWebView21InitializationCompleted(webView21,new dbQuestionParmeters());
            return;
        }

        private void OnCoreWebView2_colInitializationCompleted(object sender, EventArgs e)
        {
            if (col_id != 0)
                webTaker.OnCoreWebView2_colInitializationCompleted(webView2_col, m_chapters[indexChapter][indexQuestion].question);
            return;
        }

        private void deleteButtons()
        {
            if (m_buttonList == null)
                return;
            foreach (Button btn in m_buttonList)
                this.Controls.Remove(btn);
            m_buttonList = new List<Button>();
        }

        private void createButtons(List<afterQuestionParametrs> currQuestionRight)
        {
            for(int i = 0;i<currQuestionRight.Count;i++) 
            {

                Button btn = new Button
                {
                    Text = $"{i + 1}",
                    Width = this.Q_BUTTON_SIZE,
                    Height = this.Q_BUTTON_SIZE,
                    Location = new System.Drawing.Point(230+(i%10)*40, this.Q_BUTTON_SIZE), // Adjust spacing
                    Enabled = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold) // make the text BOLD
            };
                btn.Click += Button_Click;
                //if answer was correct
                if (currQuestionRight[i].userAnswer == OperationsAndOtherUseful.SKIPPED_Q)
                    btn.BackColor = Color.Gray;
                else
                {
                    if (currQuestionRight[i].question.rightAnswer == currQuestionRight[i].userAnswer)
                        btn.BackColor = Color.LightGreen;
                    else
                        btn.BackColor = Color.Red;
                }
                m_buttonList.Add(btn);
                Controls.Add(btn);
            }
            if (m_buttonList.Count != 0) 
            {
                //Button_Click(0);//index of first questin
            }
        }

        private void unvisibleButtonsFromButtonList()
        {
            for (int i = 0; i < m_buttonList.Count; i++)
            {
                m_buttonList[i].Visible = false;
            }
        }
        private void displayButtons(int startIndex, int endIndex)
        {
            if (startIndex < 0)
                return;
            m_currentIndexOfFirstButton = startIndex;
            unvisibleButtonsFromButtonList();

            // hide the nextQuestionsButton if there are no next questions
            if (endIndex >= m_buttonList.Count - 1)
            {
                nextQuestionsButton.Visible = false;
                endIndex = m_buttonList.Count;
            }
            else
                nextQuestionsButton.Visible = true;
            // hide the previousQuestionsButton if there are no previous questions
            if (startIndex == 0)
                previousQuestionsButton.Visible = false;
            else
                previousQuestionsButton.Visible = true;

            for (int i = startIndex; i < endIndex; i++)
            {
                Button btn = m_buttonList[i];
                btn.Visible = true;
                btn.BringToFront();
                //
            }
        }
        private void displayButtons()
        {
            displayButtons(((int)this.indexQuestion / 10) * 10, ((int)this.indexQuestion / 10) * 10 + 10);
        }
  
        private void Button_Click(object sender, EventArgs e)
        {
/*<<<<<<< HEAD
            //previous button clicked:
            m_buttonList[this.indexQuestion].BackColor = m_buttonList[this.indexQuestion].ForeColor; // change the last clicked button back to normal
            m_buttonList[this.indexQuestion].ForeColor = System.Drawing.Color.Black; // change the last clicked button back to normal
            //update to next button:
            this.indexQuestion = (int.Parse(((Button)sender).Text)-1);
            Button_Click();
        }


        private void Button_Click()
=======*/
            Button_Click((int.Parse(((Button)sender).Text)-1));
        }

        private void Button_Click(int new_indexQuestion)
//>>>>>>> 417be51f8b0ed105be95d0ddf17446bb40720811
        {
            if (new_indexQuestion < 0 || new_indexQuestion >= this.m_chapters[indexChapter].Count)
                return;

            if (this.indexQuestion >= 0 && this.indexQuestion < this.m_chapters[indexChapter].Count && m_buttonList[this.indexQuestion].Width != Q_BUTTON_SIZE)
            {
                // setting the previous question button to normal size again
                m_buttonList[this.indexQuestion].Width = Q_BUTTON_SIZE;
                m_buttonList[this.indexQuestion].Height = Q_BUTTON_SIZE;
                m_buttonList[this.indexQuestion].Location = new System.Drawing.Point(m_buttonList[this.indexQuestion].Location.X + ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2), m_buttonList[this.indexQuestion].Location.Y + ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2));
                //m_buttonList[this.indexQuestion].BackColor = m_buttonList[this.indexQuestion].ForeColor; // change the last clicked button back to normal
                //m_buttonList[this.indexQuestion].ForeColor = System.Drawing.Color.Black; // change the last clicked button back to normal
            }

            this.indexQuestion = new_indexQuestion;

            // setting the current question button size to be bigger
            m_buttonList[this.indexQuestion].Width = Q_BUTTON_SIZE + Q_CHOSEN_BUTTON_ADD_SIZE;
            m_buttonList[this.indexQuestion].Height = Q_BUTTON_SIZE + Q_CHOSEN_BUTTON_ADD_SIZE;
            m_buttonList[this.indexQuestion].Location = new System.Drawing.Point(m_buttonList[this.indexQuestion].Location.X - ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2), m_buttonList[this.indexQuestion].Location.Y - ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2));
            //m_buttonList[this.indexQuestion].ForeColor = m_buttonList[this.indexQuestion].BackColor; // change ForeColor to green/red 
            //m_buttonList[this.indexQuestion].BackColor = System.Drawing.Color.Cyan; // change BackColor to cyan to highlight the current question

            //here we display the question and answer based on the index
            string toDisplay = OperationsAndOtherUseful.get_string_of_question_and_explanation(this.m_chapters[this.indexChapter][this.indexQuestion].question, this.m_chapters[this.indexChapter][this.indexQuestion].userAnswer);
            displayCol();

            // display the lesson of the new question
            display_lesson_of_current_question();


            int secondsTook = this.m_chapters[this.indexChapter][this.indexQuestion].timeForAnswer;
            updateQuestionTimerText(secondsTook);
            updateStats();
            this.webView21.NavigateToString(toDisplay);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            menuPage m = new menuPage();
            m.Show();
            this.Close();
        }
        private void updateQuestionTimerText(int seconds)
        {
            if(seconds == OperationsAndOtherUseful.QUESTION_THAT_DID_NOT_ANSWERED)
            {
                this.timeTookForQLabel.Text = "";
                this.timeTookForQLabel.Visible = false;
                return;
            }
            else
            {
                this.timeTookForQLabel.Text = $"זמן לשאלה: {OperationsAndOtherUseful.get_time_mmss_fromseconds(seconds)}";
                this.timeTookForQLabel.Visible = true;
            }

        }
        private void updateStats()
        {
            dbQuestionParmeters c = this.m_chapters[this.indexChapter][this.indexQuestion].question;
            this.category_of_q.Text = $"נושא: {c.category}";
            this.diffic_level.Text = $"רמת קושי: {c.json_content["difficulty_level"].ToString()}";
            this.curr_q.Text = $"שאלה: {this.indexQuestion + 1}/{this.m_chapters[this.indexChapter].Count}";
            this.curr_q_id.Text = $"id: {c.questionId}";

            if (col_id != 0)
            {
                this.curr_col_id.Text = $"collection id: {col_id}";
                this.curr_col_id.Visible = true;

                this.diffic_level_col.Text = $"רמת הקושי של קטע הקריאה או התרשים: {c.json_content["collections"][0]["difficulty_level"].ToString()}";
                this.diffic_level_col.Visible = true;
            }
            else
            {
                this.curr_col_id.Text = $"";
                this.curr_col_id.Visible = false;

                this.diffic_level_col.Text = $"";
                this.diffic_level_col.Visible = false;
            }
        }

        private void nextQuestionsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_chapters[this.indexChapter].Count - 10; i += 10)
            {
                if (m_currentIndexOfFirstButton == i)
                {
                    displayButtons(i + 10, i + 20);
                    Button_Click(i + 10);
                    return;
                }
            }
        }

        private void previousQuestionsButton_Click(object sender, EventArgs e)
        {
            int maxQuestions = this.m_chapters[this.indexChapter].Count;

            for (int i = 10; i < maxQuestions; i += 10)
            {
                if (m_currentIndexOfFirstButton == i)
                {
                    displayButtons(i - 10, i);
                    Button_Click(i - 10);
                    return;
                }
            }
            return;
        }

        private void curr_q_id_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            ToolTip toolTip = new ToolTip();
            if (clickedLabel != null && clickedLabel.Text.Length > 4)
            {
                // Copy the label text to the clipboard
                Clipboard.SetText(clickedLabel.Text.Substring(4));

                toolTip.Show("Id copied!", curr_q_id, curr_q_id.Width / 2, -20, 2000);
            }
        }

        private void summrizePage_Load(object sender, EventArgs e)
        {

        }

        private void display_lesson_of_current_question()
        {
            if (this.test_id != OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
                this.lessons_richTextBox.Text = lessons_list[this.indexChapter][indexQuestion];
        }

        private void lessons_richTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID || this.indexQuestion < 0)
                return;
            string lesson = this.lessons_richTextBox.Text;

            // dont save if it didnt change
            if (lesson == lessons_list[this.indexChapter][indexQuestion])
                return;

            if (lesson.Replace("\n", "").Replace(" ", "").Length == 0) // if str only contains "\n" and " ", it is not valid
            {
                // edit to an empty lesson, TODO maybe not delete the current lesson but rather leave it as it is
                TestHistoryFileHandler.edit_lesson_in_test_history("", this.test_id, get_index_of_question_by_number_of_question_and_index_chapter(indexChapter, indexQuestion)); // TODO add tirgul_id where 0
            }
            else
            {
                // TODO add tirgul_id
                TestHistoryFileHandler.edit_lesson_in_test_history(lesson, this.test_id, get_index_of_question_by_number_of_question_and_index_chapter(indexChapter, indexQuestion)); // TODO add tirgul_id where 0
            }

            lessons_list[this.indexChapter][indexQuestion] = lesson;
        }

        private void chapters_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int new_chapter_index = chapters_comboBox.SelectedIndex;
            if (new_chapter_index == this.indexChapter)
                return;

            this.indexQuestion = 0;
            this.indexChapter = new_chapter_index;
            m_currentIndexOfFirstButton = 0;

            this.timeTookForQLabel.Text = "";
            this.timeTookForQLabel.Visible = false;

            deleteButtons();
            createButtons(m_chapters[this.indexChapter]);
            displayButtons();
            Button_Click(0);
            displayStatsOfWholeChapter();
        }

    }
}
