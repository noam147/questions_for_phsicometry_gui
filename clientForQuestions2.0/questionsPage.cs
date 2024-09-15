using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Runtime.CompilerServices;
using System.Reflection;


namespace clientForQuestions2._0
{
    public partial class questionsPage : Form
    {
        private WebView2 webView21;
        private WebView2 webView2_col;
        private List<dbQuestionParmeters> m_questionDetails;
        //for summrize:
        private List<afterQuestionParametrs> m_afterQuestionParametrs = new List<afterQuestionParametrs>();

        private int m_maxQuestions;
        private int m_rightQuestions = 0;
        private int m_questionCounter = 1;
        private int m_indexOfCurrQuestion = 0;
        private int m_currAnswer = 0;

        private int width_screen;
        private int height_screen;
        private int w_buttonsPlace = 170;

        private bool isUserDoNotGetFeedBack;
        private int col_id = 0;

        private int secondsTookForCurrq = 0;
        private int timeElapsed = 0; // to get the time per question when isUserDoNotGetFeedBack == true
        private System.Timers.Timer m_aTimer;
        private questionsDifficultyLevel m_aDifficultyLevels;

        private List<Button> m_buttonList = new List<Button>();


        private int timePerQ = 0; // 0 means "stoper" (no time limit), positive value means "timer" (count backwards to 0)
        public questionsPage(int amount, List<string> listOfTopics, bool isQSkip, int timePerQ, questionsDifficultyLevel difficultyLevel)
        {
            m_aDifficultyLevels = difficultyLevel;
            InitializeComponent();
            setAnswerButtonsToNormalcolor();

            width_screen = this.ClientSize.Width;
            height_screen = this.ClientSize.Height;
            this.isUserDoNotGetFeedBack = isQSkip;
           
            this.timePerQ = timePerQ;

            updateAtStart(amount, listOfTopics);

            if (this.isUserDoNotGetFeedBack)
                this.timePerQ = timePerQ * m_questionDetails.Count;

            rewriteTimer();

            //PositionNextQuestionButton();
            this.Resize += MainForm_Resize;
            Thread thread = new Thread(() =>
            {
                InitializeWebView21();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void whenDoNotGetFeedBack()
        {
            this.timer.Visible = true;
            //here we will init *all* the answer after questions without userchoice
            for(int i =0; i <this.m_questionDetails.Count;i++)
            {
                //the defult is skipped question - will be updated as the user clicks on the option buttons
                afterQuestionParametrs af = new afterQuestionParametrs { indexOfQuestion = i, question = m_questionDetails[i], timeForAnswer = 0, userAnswer = OperationsAndOtherUseful.SKIPPED_Q };
                this.m_afterQuestionParametrs.Add(af);
            }
            updateToNextButtonQuestion(0);
        }

        public questionsPage(int collection_id, List<int> questions, int tpq)
        {
            //when is text
            InitializeComponent();
            setAnswerButtonsToNormalcolor();
            width_screen = this.ClientSize.Width;
            height_screen = this.ClientSize.Height;
            this.isUserDoNotGetFeedBack = true;//in text user does not get immdiate feedback



            updateAtStartCol(questions);
            this.col_id = collection_id;

            createButtons(questions.Count);
            displayButtons();

            this.timePerQ = tpq * m_questionDetails.Count;
            rewriteTimer();


            //PositionNextQuestionButton();
            this.Resize += MainForm_Resize;

            this.Size = new System.Drawing.Size(2 * width_screen - w_buttonsPlace, height_screen);

            Thread thread = new Thread(() =>
            {
                InitializeWebView2_col();
                InitializeWebView21();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();

        }

        private void createButtons(int amountOfQuestions)
        {
            for (int i = 0; i < amountOfQuestions; i++)
            {

                Button btn = new Button
                {
                    Text = $"{i + 1}",
                    Width = 30,
                    Height = 30,
                    Location = new System.Drawing.Point(110 + i * 55, 30), // Adjust spacing
                    Enabled = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold) // make the text BOLD
                };
                //at start user didnt answer anything
                btn.BackColor = Color.Gray;
                btn.Click += swichQuestionButton_Click;
                m_buttonList.Add(btn);
            }
            if (m_buttonList.Count != 0)
            {
                //Button_Click(0);//index of first questin
            }
        }
        private void displayButtons()
        {
            for (int i = 0; i < m_buttonList.Count; i++)
            {
                Button btn = m_buttonList[i];
                btn.BringToFront();
                Controls.Add(btn);
            }
        }


        private void rewriteTimer()
        {
            this.timer.Text = OperationsAndOtherUseful.get_time_mmss_fromseconds(timePerQ);
        }

        private void writeQuestionToLogFile()
        {
            if (m_questionDetails == null)
            {
                LogFileHandler.writeIntoFile("Error: questionDetails == null - related to db!");
                return;
            }
            string s = "";
            for (int i = 0; i < m_questionDetails.Count; i++)
            {
                s += m_questionDetails[i].questionId + ", ";
            }
            s = s.Substring(0, s.Length - 2);
            LogFileHandler.writeIntoFile("Questions id are: " + s);
        }

        private void updateAtStart(int amount, List<string> listOfTopics)
        {
            //wait until webview is init
            this.answer1Button.Enabled = false;
            this.answer2Button.Enabled = false;
            this.answer3Button.Enabled = false;
            this.answer4Button.Enabled = false;
            //questionDetails = sqlDb.get_n_questions_from_arr_of_categorys(amount, listOfTopics);
            m_questionDetails = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(amount, listOfTopics, m_aDifficultyLevels);
            writeQuestionToLogFile();
            m_maxQuestions = m_questionDetails.Count;//if amount is bigger that questions avelible
            writeQuestionToLogFile();
            this.isUserRightLabel.Text = "";
            this.nextQuestionButton.Visible = false;
            updateLabelAnswers();

            if (this.isUserDoNotGetFeedBack)
            {
                createButtons(m_questionDetails.Count);
                displayButtons();
            }
        }

        private void updateAtStartCol(List<int> q_ids)
        {
            //wait until webview is init
            this.answer1Button.Enabled = false;
            this.answer2Button.Enabled = false;
            this.answer3Button.Enabled = false;
            this.answer4Button.Enabled = false;
            m_questionDetails = new List<dbQuestionParmeters>();
            foreach (int id in q_ids)
            {
                m_questionDetails.Add(sqlDb.get_question_based_on_id(id));
            }
            m_maxQuestions = m_questionDetails.Count;//if amount is bigger that questions avelible
            writeQuestionToLogFile();

            this.isUserRightLabel.Text = "";
            this.nextQuestionButton.Visible = false;


            updateLabelAnswers();
        }


        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.secondsTookForCurrq++;
            try
            {
                // Ensure the label is updated on the UI thread
                this.timer.Invoke((MethodInvoker)delegate
                {
                    this.timer.Text = OperationsAndOtherUseful.get_time_mmss_fromseconds(Math.Abs(timePerQ - secondsTookForCurrq));

                    if (secondsTookForCurrq >= timePerQ && timePerQ != 0) // check if time ran out
                    {
                        if (isUserDoNotGetFeedBack) // if time ran out && navigate questions, go to summary
                        {
                            var s = new summrizePage(this.m_afterQuestionParametrs);
                            s.Show();
                            this.Close();
                            return;
                        }
                        answerinnotAnswered();
                        afterAnswerQuestion(OperationsAndOtherUseful.SKIPPED_Q); // if time ran out, it counts as he didn't answer.
                    }
                });
            }
            catch (Exception ex) { }




        }
        private void SetTimer()
        {
            //src= https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-8.0
            // Create a timer with a one second interval.
            m_aTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            m_aTimer.Elapsed += OnTimedEvent;
            m_aTimer.AutoReset = true;
            m_aTimer.Enabled = true;
        }

        private void whenFinishInitWebView()
        {
            this.answer1Button.Enabled = true;
            this.answer2Button.Enabled = true;
            this.answer3Button.Enabled = true;
            this.answer4Button.Enabled = true;
            SetTimer();//start the timer here
            if (isUserDoNotGetFeedBack)
            {
                whenDoNotGetFeedBack();
                //let the user pass through questions
            }
        }
        //func is not intersting - just prepering html displayer
        private async void InitializeWebView21()
        {
            // Ensure that UI updates are made on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView21));
                return;
            }
            // Dispose of any existing instance before creating a new one
            if (webView21 != null)
            {
                webView21.Dispose();
                webView21 = null; // Clear reference
            }


            // Initialize a new instance of WebView2
            if (this.isUserDoNotGetFeedBack)
            {
                webView21 = new WebView2
                {
                    Location = new Point(0, 70),
                    Size = new Size(width_screen - w_buttonsPlace, height_screen)
                };
            }
            else
            {
                webView21 = new WebView2
                {
                    Location = new Point(0, 0),
                    Size = new Size(width_screen - w_buttonsPlace, height_screen)
                };

            }

            webView21.CoreWebView2InitializationCompleted += OnCoreWebView21InitializationCompleted;

            int maxRetries = 10;
            int retryCount = 0;
            bool initialized = false;

            while (!initialized && retryCount < maxRetries)
            {
                try
                {
                    retryCount++;

                    // Attempt to initialize WebView2 runtime
                    var task = webView21.EnsureCoreWebView2Async(null);
                    await task; // Await the task to ensure it runs on the UI thread

                    // Check if the task has completed successfully
                    if (task.IsCompleted && webView21.CoreWebView2 != null)
                    {
                        initialized = true; // Exit loop if initialization is successful
                    }
                    else if (task.IsFaulted)
                    {
                        // Extract exception details for better debugging
                        var exceptionMessage = task.Exception != null
                            ? string.Join("\n", task.Exception.InnerExceptions.Select(ex => ex.Message))
                            : "Unknown error initializing WebView2.";

                        MessageBox.Show($"Attempt {retryCount} failed: {exceptionMessage}",
                            "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Add delay between retries
                    await Task.Delay(500); // Wait before retrying
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during WebView2 initialization attempt {retryCount}: {ex.Message}",
                        "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Wait before next retry
                    await Task.Delay(500); // Wait for 2 seconds
                }
            }

            if (initialized)
            {
                // Safely add the control to the form on the main thread
                try
                {
                    whenFinishInitWebView();
                    Controls.Add(webView21);
                    webView21.SendToBack(); // Send WebView2 to the back
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
        }

        //func is not intersting - just prepering html displayer
        private async void InitializeWebView2_col()
        {
            // Ensure that UI updates are made on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView2_col));
                return;
            }
            // Dispose of any existing instance before creating a new one
            if (webView2_col != null)
            {
                webView2_col.Dispose();
                webView2_col = null; // Clear reference
            }


            // Initialize a new instance of WebView2
            webView2_col = new WebView2
            {
                Location = new Point(width_screen-10, 0),
                Size = new Size(width_screen - w_buttonsPlace, height_screen)
            };

            webView2_col.CoreWebView2InitializationCompleted += OnCoreWebView2_colInitializationCompleted;

            int maxRetries = 10;
            int retryCount = 0;
            bool initialized = false;

            while (!initialized && retryCount < maxRetries)
            {
                try
                {
                    retryCount++;

                    // Attempt to initialize WebView2 runtime
                    var task = webView2_col.EnsureCoreWebView2Async(null);
                    await task; // Await the task to ensure it runs on the UI thread

                    // Check if the task has completed successfully
                    if (task.IsCompleted && webView2_col.CoreWebView2 != null)
                    {
                        initialized = true; // Exit loop if initialization is successful
                    }
                    else if (task.IsFaulted)
                    {
                        // Extract exception details for better debugging
                        var exceptionMessage = task.Exception != null
                            ? string.Join("\n", task.Exception.InnerExceptions.Select(ex => ex.Message))
                            : "Unknown error initializing webView2_col.";

                        MessageBox.Show($"Attempt {retryCount} failed: {exceptionMessage}",
                            "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Add delay between retries
                    await Task.Delay(500); // Wait before retrying
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during webView2_col initialization attempt {retryCount}: {ex.Message}",
                        "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Wait before next retry
                    await Task.Delay(500); // Wait for 2 seconds
                }
            }

            if (initialized)
            {
                // Safely add the control to the form on the main thread
                try
                {
                    Controls.Add(webView2_col);
                    webView2_col.SendToBack(); // Send WebView2 to the back
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
        }

        //sender and e are not in use
        private void OnCoreWebView21InitializationCompleted(object sender, EventArgs e)
        {
            webTaker.OnCoreWebView21InitializationCompleted(webView21, this.m_questionDetails[this.m_indexOfCurrQuestion]);
            return;
        }

        //sender and e are not in use
        private void OnCoreWebView2_colInitializationCompleted(object sender, EventArgs e)
        {
            webTaker.OnCoreWebView2_colInitializationCompleted(webView2_col, this.m_questionDetails[0]);
            return;
        }

        private void updateLabelAnswers()
        {
            if (!isUserDoNotGetFeedBack) { this.answersTrackLabel.Text = $"תשובות נכונות: {m_rightQuestions}/{m_maxQuestions}"; }
            else { this.answersTrackLabel.Text = ""; }
            this.questionTrackLabel.Text = $"שאלה נוכחית: {m_questionCounter}/{m_maxQuestions}";
        }

        private void stopTestButtonClick(object sender, EventArgs e)
        {
            if (this.m_afterQuestionParametrs.Count == 0)
            {
                //if user didnt answer questions at all - direct him to the menu
                var mp = new menuPage();
                mp.Show();
                this.Close();
                return;
            }
            var s = new summrizePage(this.m_afterQuestionParametrs);
            s.Show();
            this.Close();
        }

        private void afterAnswerQuestion(int answer)
        {
            this.nextQuestionButton.BackColor = Color.White;
            if (this.m_buttonList.Count != 0)
            {
                m_buttonList[m_indexOfCurrQuestion].BackColor = Color.Yellow;
            }
           
            this.stopTestButton.Text = "סיום התרגול וסיכום";
            //this func happens after the user clicked on an answer
            // dont stop the timer if you navigate qs
            if (!isUserDoNotGetFeedBack)
                this.m_aTimer.Stop();
            
            //func check if answer is true and return explantion 

            //for summrize page get data on user choice and time
            afterQuestionParametrs after_questionParametrs = new afterQuestionParametrs();
            after_questionParametrs.userAnswer = answer;//we have the right answer in question details
            after_questionParametrs.question = m_questionDetails[m_indexOfCurrQuestion];
            after_questionParametrs.timeForAnswer = this.secondsTookForCurrq;
            after_questionParametrs.indexOfQuestion = this.m_indexOfCurrQuestion;//we alresy incresed the curr quesiton index


            //check if there isnt alredy a question forms of that and if there is - update
            bool isExsist = false;
            for (int i = 0; i < m_afterQuestionParametrs.Count; i++)
            {
                if (m_afterQuestionParametrs[i].indexOfQuestion == this.m_indexOfCurrQuestion)
                {
                    // Retrieve the element, modify it, and then assign it back, add time to current 42
                    var parameter = m_afterQuestionParametrs[i];      // Retrieve the element
                    parameter.userAnswer = answer;                    // Modify the property
                    m_afterQuestionParametrs[i] = parameter;          // Assign it back to the list
                    isExsist = true;
                    break;
                }
            }
            if(!isExsist)
            {
                m_afterQuestionParametrs.Add(after_questionParametrs);
            }
            

            if (this.m_questionDetails[m_indexOfCurrQuestion].rightAnswer == answer && !this.isUserDoNotGetFeedBack)
            {
                this.m_rightQuestions++;
            }
            updateLabelAnswers();

            //after user submit an answer give to him a feedback
            if (!isUserDoNotGetFeedBack)
            {
                string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_explanation(this.m_questionDetails[m_indexOfCurrQuestion], answer);
                webView21.NavigateToString(htmlContent);

                //check if this the last question
                this.m_indexOfCurrQuestion++;

            }

            if (isUserDoNotGetFeedBack)
            {
                this.answer1Button.Visible = true;
                this.answer2Button.Visible = true;
                this.answer3Button.Visible = true;
                this.answer4Button.Visible = true;
                //nextQuestionButtonClick(null, null);
                //return;

                setAnswerButtonsToNormalcolor();
                markUserAnswerInYellow(this.m_indexOfCurrQuestion); //the curr q

                if ((m_buttonList.Any() && m_buttonList.All(b => b.BackColor == Color.Yellow)))
                {
                    this.nextQuestionButton.Text = "סיכום";
                    this.nextQuestionButton.BackColor = System.Drawing.Color.Yellow;
                }

                // if it is the last question and not all the qs are answered, dont show the next button
                if (m_indexOfCurrQuestion == this.m_questionDetails.Count - 1 && !m_buttonList.All(b => b.BackColor == Color.Yellow))
                    this.nextQuestionButton.Visible = false;
                else
                    this.nextQuestionButton.Visible = true;

            }


            if (m_indexOfCurrQuestion == this.m_questionDetails.Count)//if questions end
            {
                this.nextQuestionButton.Text = "סיכום";
                this.nextQuestionButton.BackColor = System.Drawing.Color.Yellow;
            }
            //wait until user clicks on the continue button to display another q 

            if (!isUserDoNotGetFeedBack)
            {
                this.nextQuestionButton.Visible = true;

                this.answer1Button.Visible = false;
                this.answer2Button.Visible = false;
                this.answer3Button.Visible = false;
                this.answer4Button.Visible = false;
            }
        }

        private void nextQuestionButtonClick(object sender, EventArgs e)
        {
            m_questionCounter++;


            if (!isUserDoNotGetFeedBack)
            {
                rewriteTimer();
            }
            else
            {
                for (int i = 0; i < m_afterQuestionParametrs.Count; i++)
                {
                    if (m_afterQuestionParametrs[i].indexOfQuestion == this.m_indexOfCurrQuestion)
                    {
                        // Retrieve the element, modify it, and then assign it back, add time to current
                        var parameter = m_afterQuestionParametrs[i];                 // Retrieve the element
                        parameter.timeForAnswer += secondsTookForCurrq - timeElapsed; // add the time since the user entered the q
                        m_afterQuestionParametrs[i] = parameter;                     // Assign it back to the list
                        break;
                    }
                }

                timeElapsed = secondsTookForCurrq; // save the time when the user left the curr q and enter a new one
            }
            this.updateLabelAnswers();

            if (isUserDoNotGetFeedBack)
            {
                this.m_indexOfCurrQuestion++;
            }

            //if questions end, check if all q are answered
            if ( (m_indexOfCurrQuestion == this.m_questionDetails.Count && !isUserDoNotGetFeedBack) || (m_buttonList.Any() && m_buttonList.All(b => b.BackColor == Color.Yellow && isUserDoNotGetFeedBack) ) )
            {
                if(sender == null)
                {
                    //if this is not a real click and just we want to move on but its the end
                    this.nextQuestionButton.Text = "סיכום";
                    this.nextQuestionButton.Visible = true;
                    this.nextQuestionButton.BackColor = Color.Yellow;
                    return;
                }

                var s = new summrizePage(this.m_afterQuestionParametrs);
                s.Show();
                this.Close();
                return;
            }

            if (isUserDoNotGetFeedBack)
            {
                swichQuestionButton_Click(m_indexOfCurrQuestion);
                return;
            }
            if (webView21.CoreWebView2 != null)
            {
                OnCoreWebView21InitializationCompleted(sender, e);

                this.nextQuestionButton.Visible = false;
                this.isUserRightLabel.Text = "";
                this.answer1Button.Visible = true;
                this.answer2Button.Visible = true;
                this.answer3Button.Visible = true;
                this.answer4Button.Visible = true;

                if (!isUserDoNotGetFeedBack)
                {
                    this.secondsTookForCurrq = 0;
                    this.m_aTimer.Start();
                }
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void answerCorrect()
        {
            //when answer correct display a msg
            this.isUserRightLabel.Text = ":) נכון ";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.Green;
        }
        private void answerinCorrect()
        {
            this.isUserRightLabel.Text = ":( לא נכון ";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.Red;
        }
        private void answerinnotAnswered()
        {
            this.isUserRightLabel.Text = "נגמר הזמן :(";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.DarkGray;
        }
        private void answerButton_Click(object sender, EventArgs e)
        {
            int clicked_answer = int.Parse((((Button)sender).Text.ToString()[((Button)sender).Text.ToString().Length - 1]).ToString());

            if (!isUserDoNotGetFeedBack)
            {
                if (this.m_questionDetails[this.m_indexOfCurrQuestion].rightAnswer == clicked_answer)
                {
                    answerCorrect();
                }
                else
                {
                    answerinCorrect();
                }
            }

            afterAnswerQuestion(clicked_answer);
        }


        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Reposition the button on form resize
            //PositionNextQuestionButton();
        }

        private void PositionNextQuestionButton()
        {
            return;
            // Position the button on the far right with a fixed height, and optional margin
            int rightMargin = 10; // Adjust as needed
            int topMargin = 50;   // Adjust as needed
            this.isUserRightLabel.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin + this.nextQuestionButton.Height + 20);
            nextQuestionButton.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin);
        }

        private void questionsPage_Load(object sender, EventArgs e)
        {

        }

        private void isUserRightLabel_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //from here - relevant to when user doesnt want a feedback - so we give him the possebility to navigate throgh questions
        private void swichQuestionButton_Click(object sender, EventArgs e)
        {

            int index = int.Parse(((Button)sender).Text) -1;
            swichQuestionButton_Click(index);
          

        }
        private void swichQuestionButton_Click(int indexOfQuestion)
        {
            setAnswerButtonsToNormalcolor();
            setButtonsToNormalSize();
            updateToNextButtonQuestion(indexOfQuestion);

            for (int i = 0; i < m_afterQuestionParametrs.Count; i++)
            {
                if (m_afterQuestionParametrs[i].indexOfQuestion == this.m_indexOfCurrQuestion)
                {
                    // Retrieve the element, modify it, and then assign it back, add time to current
                    var parameter = m_afterQuestionParametrs[i];                 // Retrieve the element
                    parameter.timeForAnswer += secondsTookForCurrq - timeElapsed; // add the time since the user entered the q
                    m_afterQuestionParametrs[i] = parameter;                     // Assign it back to the list
                    break;
                }
            }

            timeElapsed = secondsTookForCurrq; // save the time when the user left the curr q and enter a new one

            this.answer1Button.Visible = true;
            this.answer2Button.Visible = true;
            this.answer3Button.Visible = true;
            this.answer4Button.Visible = true;
            this.m_indexOfCurrQuestion = indexOfQuestion;



            //need to also mark the current user answer!
            string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_option_from_json(this.m_questionDetails[indexOfQuestion], OperationsAndOtherUseful.DO_NOT_MARK);
            this.webView21.NavigateToString(htmlContent);
            markUserAnswerInYellow(indexOfQuestion);

            if ((m_buttonList.Any() && m_buttonList.All(b => b.BackColor == Color.Yellow)))
            {
                this.nextQuestionButton.Visible = true;
                this.nextQuestionButton.Text = "סיכום";
                this.nextQuestionButton.BackColor = System.Drawing.Color.Yellow;
            }
            else
                this.nextQuestionButton.Visible = false;
        }
        private void markUserAnswerInYellow(int index)
        {
            int userAnswer = -1;
            for (int i = 0; i < this.m_afterQuestionParametrs.Count; i++)
            {
                if (m_afterQuestionParametrs[i].indexOfQuestion == index)
                {
                    userAnswer = m_afterQuestionParametrs[i].userAnswer;
                    break;
                }
            }
            if (userAnswer == 1)
                answer1Button.BackColor = Color.Yellow;
            if (userAnswer == 2)
                answer2Button.BackColor = Color.Yellow;
            if (userAnswer == 3)
                answer3Button.BackColor = Color.Yellow;
            if (userAnswer == 4)
                answer4Button.BackColor = Color.Yellow;
        }
        private void setAnswerButtonsToNormalcolor()
        {
            this.answer1Button.BackColor = Color.White;
            this.answer2Button.BackColor = Color.White;
            this.answer3Button.BackColor = Color.White;
            this.answer4Button.BackColor = Color.White;
        }
        private void setButtonsToNormalSize()
        {
            for(int i =0; i <this.m_buttonList.Count; i++)
            {
                m_buttonList[i].Width = 30;
                m_buttonList[i].Height = 30;
            }
        }
        private void updateToNextButtonQuestion(int index)
        {
            setButtonsToNormalSize();
            try
            {
                m_buttonList[index].Width = 30 + 10;
                m_buttonList[index].Height = 30 +10;
            }
            catch (Exception e) { }
        }
    }
}
