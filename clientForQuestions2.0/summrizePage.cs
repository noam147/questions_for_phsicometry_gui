using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace clientForQuestions2._0
{
    public partial class summrizePage : Form
    {
        List<Button> m_buttonList= new List<Button>();
        List<afterQuestionParametrs> m_questions;
        private WebView2 webView21;
        private WebView2 webView2_col;
        private bool isFinishInit = false;

        private int h_statsPlace = 40;
        private int h_questionsPlace = 70;
        private int w_screen;
        private int h_screen;

        private int col_id = 0;
        private int indexQuestion = 0;

        public summrizePage(List<afterQuestionParametrs> questions)
        {
            this.m_questions = questions;
            InitializeComponent();

            if (((JArray)questions[0].question.json_content["collections"]).Count != 0)
            {
                col_id = (int) questions[0].question.json_content["collections"][0]["id"];
            }

            w_screen = this.ClientSize.Width;
            h_screen = this.ClientSize.Height;
            if (col_id != 0)
                this.Size = new Size(w_screen * 2, h_screen);

            this.timeTookForQLabel.Text = "";
            displayTotalAvrageTime();

            createButtons(questions);
            displayButtons();

            Thread thread = new Thread(() =>
            {
                while (!this.IsHandleCreated)
                {
                    // Introduce a small delay to avoid tight looping
                    Thread.Sleep(100);
                }
                if (col_id != 0)
                {
                    InitializeWebView2_col();
                }
                InitializeWebView21();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            this.Resize += Form_Resize;
            Form_Resize(this, EventArgs.Empty);

        }

        private void displayTotalAvrageTime()
        {
            int time = 0;
            foreach(afterQuestionParametrs qp in m_questions)
            {
                time += qp.timeForAnswer;
            }

            this.total_time.Text = $"זמן כולל: {OperationsAndOtherUseful.get_time_mmss_fromseconds(time)}";
            this.avrage_time.Text = $"זמן ממוצע לשאלה: {OperationsAndOtherUseful.get_time_mmss_fromseconds(time / m_questions.Count)}";
            int corr_c = 0;
            foreach (afterQuestionParametrs qp in m_questions)
            {
                if (qp.userAnswer == -1 || qp.userAnswer == OperationsAndOtherUseful.SKIPPED_Q)
                    continue;
                if (((JArray)qp.question.json_content["options"]).Count != 0)
                    if ((int) qp.question.json_content["options"][qp.userAnswer-1]["is_correct"] == 1)
                            corr_c++;
                else
                    if (((JArray)qp.question.json_content["option_images"]).Count != 0)
                        if ((int)qp.question.json_content["option_images"][qp.userAnswer-1]["is_correct"] == 1)
                            corr_c++;
            }
            this.correct_answers.Text = $"תשובות נכונות: {corr_c}/{m_questions.Count}";

        }

        private void Form_Resize(object sender, EventArgs e)
        {
            return;
            if (webView21 != null)
            {
                if(col_id == 0)
                    webView21.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - h_questionsPlace); // Adjust height based on form size
                else
                    webView21.Size = new Size((int) this.ClientSize.Width / 2, this.ClientSize.Height - h_questionsPlace); // Adjust height based on form size
            }
            if (webView2_col != null && col_id != 0)
            {
                Location = new Point((int)this.ClientSize.Width / 2, h_questionsPlace + h_statsPlace); // Adjust Y coordinate to leave space for buttons

                webView2_col.Size = new Size((int)this.ClientSize.Width / 2, this.ClientSize.Height - h_questionsPlace); // Adjust height based on form size
            
            }
            this.button1.Location = new Point(this.ClientSize.Width - button1.Width - 40, 20);
        }
        private void enableButtons()
        {
            for(int i =0; i <m_buttonList.Count;i++)
            {
                m_buttonList[i].Enabled = true;
            }
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
                Location = new Point(0, h_questionsPlace + h_statsPlace), // Adjust Y coordinate to leave space for buttons
                Size = new Size(w_screen, h_screen - h_questionsPlace - h_statsPlace), // Adjust size to fit below the buttons
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
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
                            webView21.SendToBack(); // Ensure WebView2 is on back of other controls
                            if (this.m_buttonList.Count != 0)
                            {
                                Button_Click(); // First index after init
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
                Location = new Point(w_screen, h_questionsPlace + h_statsPlace), // Adjust Y coordinate to leave space for buttons
                Size = new Size(w_screen, h_screen - h_questionsPlace - h_statsPlace), // Adjust size to fit below the buttons
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
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

            webTaker.OnCoreWebView2_colInitializationCompleted(webView2_col, m_questions[0].question);
            return;
        }
        private void createButtons(List<afterQuestionParametrs> currQuestionRight)
        {
            for(int i = 0;i<currQuestionRight.Count;i++) 
            {

                Button btn = new Button
                {
                    Text = $"{i + 1}",
                    Width = 30,
                    Height = 30,
                    Location = new System.Drawing.Point(110+i*55, 30), // Adjust spacing
                    Enabled = false,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold) // make the text BOLD
            };
                btn.Click += Button_Click;
                //if answer was correct
                if (currQuestionRight[i].userAnswer == OperationsAndOtherUseful.SKIPPED_Q)
                    btn.BackColor = Color.Gray;
                else
                {
                    if (currQuestionRight[i].question.rightAnswer == currQuestionRight[i].userAnswer)
                        btn.BackColor = Color.Green;
                    else
                        btn.BackColor = Color.Red;
                }
                m_buttonList.Add(btn);
            }
            if (m_buttonList.Count != 0) 
            {
                //Button_Click(0);//index of first questin
            }
        }
        private void displayButtons()
        {
            for(int i = 0; i<m_buttonList.Count;i++) 
            {
                Button btn = m_buttonList[i];
                btn.BringToFront();
                Controls.Add(btn);
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            m_buttonList[this.indexQuestion].BackColor = m_buttonList[this.indexQuestion].ForeColor; // change the last clicked button back to normal
            m_buttonList[this.indexQuestion].ForeColor = System.Drawing.Color.Black; // change the last clicked button back to normal

            this.indexQuestion = (int.Parse(((Button)sender).Text)-1);
            Button_Click();
        }
        private void Button_Click()
        {
            m_buttonList[this.indexQuestion].ForeColor = m_buttonList[this.indexQuestion].BackColor; // change ForeColor to green/red 
            m_buttonList[this.indexQuestion].BackColor = System.Drawing.Color.Cyan; // change BackColor to cyan to highlight the current question

            //here we display the question and answer based on the index
            string toDisplay = OperationsAndOtherUseful.get_string_of_question_and_explanation(this.m_questions[this.indexQuestion].question, this.m_questions[this.indexQuestion].userAnswer);
            int secondsTook = this.m_questions[this.indexQuestion].timeForAnswer;
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
                return;
            }
            else
            {
                this.timeTookForQLabel.Text = $"זמן לשאלה: {OperationsAndOtherUseful.get_time_mmss_fromseconds(seconds)}";
            }

        }
        private void updateStats()
        {
            dbQuestionParmeters c = this.m_questions[this.indexQuestion].question;
            this.category_of_q.Text = $"נושא: {c.category}";
            this.diffic_level.Text = $"רמת קושי: {c.json_content["difficulty_level"].ToString()}";
            this.curr_q.Text = $"שאלה: {this.indexQuestion + 1}/{this.m_questions.Count}";
            this.curr_q_id.Text = $"id: {c.questionId}";
        }
        private void timeTookForQLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void summrizePage_Load(object sender, EventArgs e)
        {

        }

        private void correct_answers_Click(object sender, EventArgs e)
        {

        }
    }
}
