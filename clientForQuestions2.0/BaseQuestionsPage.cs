using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json.Linq;

namespace clientForQuestions2._0
{
   
    public partial class BaseQuestionsPage : Form
    {
        protected WebView2 webView21;
        private WebView2 webView2_col;
        protected List<dbQuestionParmeters> m_questionDetails;
        //for summrize:
        protected List<afterQuestionParametrs> m_afterQuestionParametrs = new List<afterQuestionParametrs>();

        protected int m_maxQuestions;
        private int m_rightQuestions = 0;
        private int m_questionCounter = 1;
        protected int m_indexOfCurrQuestion = 0;

        private int width_webView; // w of each webview
        private int height_webView;// h of each webview
        private int w_buttonsPlace = 170;
        private int h_buttonsQuestionsPlace = 70; // for the buttons of the questions when isUserDoNotGetFeedBack
        private int Q_BUTTON_SIZE = 30;
        private int Q_CHOSEN_BUTTON_ADD_SIZE = 10;
        private string test_type = "";

        private bool isUserDoNotGetFeedBack;
        //private int col_id = 0;
        //private bool isWithCol = false; // if the excersize has a collection

        protected int secondsTookForCurrq = 0;
        protected int timeElapsed = 0; // to get the time per question when isUserDoNotGetFeedBack == true
                                     //private System.Timers.Timer m_aTimer;
        private questionsDifficultyLevel m_aDifficultyLevels;

        //private List<Button> m_buttonList = new List<Button>();
        //private int m_currentIndexOfFirstButton = 0;


        private int timePerQ = 0; // 0 means "stoper" (no time limit), positive value means "timer" (count backwards to 0)

        public BaseQuestionsPage()
        {
            InitializeComponent();
        }
        //normalQuestions
        public BaseQuestionsPage(int amount, List<string> listOfTopics, bool isQSkip, int timePerQ, questionsDifficultyLevel difficultyLevel, string test_type)
        {
            this.test_type = test_type;
            //isWithCol = false;
            this.isUserDoNotGetFeedBack = isQSkip;

            if (!isUserDoNotGetFeedBack)
                h_buttonsQuestionsPlace = OperationsAndOtherUseful.MARGIN_OF_HEIGHT; // this is here and not later in the function to set height_screen in atStart()

            m_aDifficultyLevels = difficultyLevel;
            atStart();

            /*if (!isUserDoNotGetFeedBack)
            {
                this.nextQuestionsButton.Visible = false;
                this.previousQuestionsButton.Visible = false;
            }*/


            this.timePerQ = timePerQ;

            //when normal exrecize
            updateAtStartOfNormalExrecize(amount, listOfTopics);

            if (this.isUserDoNotGetFeedBack)
                this.timePerQ = timePerQ * m_questionDetails.Count;

            rewriteTimer();




            InitializeWebView21();


        }
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
            webView21 = new WebView2
            {
                Location = new Point(w_buttonsPlace, h_buttonsQuestionsPlace),
                Size = new Size(width_webView, height_webView)
            };


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
                    webView21.NavigationCompleted += webTaker.webView_NavigationCompleted;
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.secondsTookForCurrq++;
            try
            {
                // Ensure the label is updated on the UI thread
                this.timerLabel.Invoke((MethodInvoker)delegate
                {
                    this.timerLabel.Text = OperationsAndOtherUseful.get_time_mmss_fromseconds(Math.Abs(timePerQ - secondsTookForCurrq));

                    if (secondsTookForCurrq >= timePerQ && timePerQ != 0) // check if time ran out
                    {
                        if (isUserDoNotGetFeedBack) // if time ran out && navigate questions, go to summary
                        {
                            disposedWebViews();
                            timer1.Stop();
                            MessageBox.Show("נגמר הזמן :(");
                            int test_id = TestHistoryFileHandler.get_next_test_id();

                            TestHistoryFileHandler.save_afterQuestionParametrs_to_test_history(m_afterQuestionParametrs, test_id, test_type);

                            var s = new summrizePage(this.m_afterQuestionParametrs, test_id, 0); // TODO edit 0 to test_id
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
            ////src= https://learn.microsoft.com/en-us/dotnet/api/system.timers.timer?view=net-8.0
            //// Create a timer with a one second interval.
            //m_aTimer = new System.Timers.Timer(1000);
            //// Hook up the Elapsed event for the timer. 
            //m_aTimer.Elapsed += OnTimedEvent;
            //m_aTimer.AutoReset = true;
            //m_aTimer.Enabled = true;

            timer1.Interval = 1000; // 1000 ms = 1 second
            timer1.Tick += Timer_Tick;
            timer1.Start();

        }
        protected virtual void whenFinishInitWebView()
        {
            this.answer1Button.Enabled = true;
            this.answer2Button.Enabled = true;
            this.answer3Button.Enabled = true;
            this.answer4Button.Enabled = true;

            //for (int i = 0; i < m_buttonList.Count; i++)
             //   m_buttonList[i].Enabled = true;


            SetTimer();//start the timer here
            /*if (isUserDoNotGetFeedBack)
            {
                whenDoNotGetFeedBack();
                //let the user pass through questions
            }*/
        }
        private void updateAtStartOfNormalExrecize(int amount, List<string> listOfTopics)
        {
            //wait until webview is init

            m_questionDetails = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(amount, listOfTopics, m_aDifficultyLevels);
            writeQuestionToLogFile();
            m_maxQuestions = m_questionDetails.Count;//if amount is bigger that questions avelible
            writeQuestionToLogFile();
            this.isUserRightLabel.Text = "";
            this.continueToQuestionButton.Visible = false;
            updateLabelAnswers();
            updateAtStartOfNormalExrecizeWithoutFeedBackActions();
            /*if (this.isUserDoNotGetFeedBack)
            {
                createButtons();
                displayAnswerBoxes();
            }*/
        }
       protected virtual void updateAtStartOfNormalExrecizeWithoutFeedBackActions()
        {
            return;
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
        private void atStart()
        {
            InitializeComponent();
            setAnswerButtonsToNormalcolor();

            this.answer1Button.Enabled = false;
            this.answer2Button.Enabled = false;
            this.answer3Button.Enabled = false;
            this.answer4Button.Enabled = false;

            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            /*if (isWithCol)
                width_webView = (int)(Screen.PrimaryScreen.WorkingArea.Width - this.w_buttonsPlace) / 2;
            else*/
                width_webView = Screen.PrimaryScreen.WorkingArea.Width - this.w_buttonsPlace;

            height_webView = Screen.PrimaryScreen.WorkingArea.Height - this.h_buttonsQuestionsPlace - OperationsAndOtherUseful.MARGIN_OF_HEIGHT;

            //this.Resize += MainForm_Resize;
        }
        protected void setAnswerButtonsToNormalcolor()
        {
            this.answer1Button.BackColor = Color.White;
            this.answer2Button.BackColor = Color.White;
            this.answer3Button.BackColor = Color.White;
            this.answer4Button.BackColor = Color.White;
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
        protected virtual void notGetFeedBackUseerActionsAfterAnswer()
        {
            return;
        }
        private void afterAnswerQuestion(int answer)
        {
            this.continueToQuestionButton.BackColor = Color.White;
            /*if (this.m_buttonList.Count != 0)
            {

                m_buttonList[m_indexOfCurrQuestion].BackColor = Color.Yellow;
            }*/

            this.stopTestButton.Text = "סיום התרגול וסיכום";
            //this func happens after the user clicked on an answer
            // dont stop the timer if you navigate qs
            if (!isUserDoNotGetFeedBack)
                this.timer1.Stop();

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
            if (!isExsist)
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

            notGetFeedBackUseerActionsAfterAnswer();//override


            if (m_indexOfCurrQuestion == this.m_questionDetails.Count)//if questions end
            {
                this.continueToQuestionButton.Text = "סיכום";
                this.continueToQuestionButton.BackColor = System.Drawing.Color.Yellow;
            }
            //wait until user clicks on the continue button to display another q 

            if (!isUserDoNotGetFeedBack)
            {
                this.continueToQuestionButton.Visible = true;

                this.answer1Button.Visible = false;
                this.answer2Button.Visible = false;
                this.answer3Button.Visible = false;
                this.answer4Button.Visible = false;
            }
        }
        protected virtual void updateLabelAnswers()
        {
            if (!isUserDoNotGetFeedBack)
            {
                this.answersTrackLabel.Text = $"תשובות נכונות: {m_rightQuestions}/{m_maxQuestions}";
                this.questionTrackLabel.Text = $"שאלה נוכחית: {m_questionCounter}/{m_maxQuestions}";
            }
            /*else
            {
                this.answersTrackLabel.Text = "";
                this.questionTrackLabel.Text = $"שאלות שנענו: {m_buttonList.Count(b => b.BackColor == Color.Yellow)}/{m_maxQuestions}";
            }*/
        }
        private void rewriteTimer()
        {
            this.timerLabel.Text = OperationsAndOtherUseful.get_time_mmss_fromseconds(timePerQ);
        }
        protected virtual bool checkIfQuestionsEnded()
        {
            return m_indexOfCurrQuestion == this.m_questionDetails.Count && !isUserDoNotGetFeedBack;
        }
        private void nextQuestionButtonClick(object sender, EventArgs e)
        {
            if (!isUserDoNotGetFeedBack)
            {
                rewriteTimer();
            }
            else // MIGHT BE A PROBLEM, IN swichQuestionButton_Click() THIS PROCESS IS ALSO DONE, AND THIS FUNCTION CALLS swichQuestionButton_Click() #42 #todo #fix #from_the_bipper_to_the_sea_cna'an_will_be_free
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

            //if questions end, check if all q are answered
            if (checkIfQuestionsEnded())
            {
                if (sender == null)
                {
                    //if this is not a real click and just we want to move on but its the end
                    this.continueToQuestionButton.Text = "סיכום";
                    this.continueToQuestionButton.Visible = true;
                    this.continueToQuestionButton.BackColor = Color.Yellow;
                    return;
                }
                disposedWebViews();
                int test_id = TestHistoryFileHandler.get_next_test_id();

                TestHistoryFileHandler.save_afterQuestionParametrs_to_test_history(m_afterQuestionParametrs, test_id, test_type);

                var s = new summrizePage(this.m_afterQuestionParametrs, test_id, 0); // TODO edit 0 to test_id
                s.Show();
                this.Close();
                return;
            }


            m_questionCounter++;

            this.updateLabelAnswers();

            if (isUserDoNotGetFeedBack)
            {
                userNotGetFeedBackWhenClickContinueActions();//override
                return;
            }
            

            if (webView21.CoreWebView2 != null)
            {
                OnCoreWebView21InitializationCompleted(sender, e);

                this.continueToQuestionButton.Visible = false;
                this.isUserRightLabel.Text = "";
                this.answer1Button.Visible = true;
                this.answer2Button.Visible = true;
                this.answer3Button.Visible = true;
                this.answer4Button.Visible = true;

                if (!isUserDoNotGetFeedBack)
                {
                    this.secondsTookForCurrq = 0;
                    this.timer1.Start();
                }
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        protected virtual void userNotGetFeedBackWhenClickContinueActions()
        {

        }
        private void disposedWebViews()
        {
            if (webView21 != null)
            {
                this.webView21.Dispose();
                this.webView21 = null;
            }

            /*if (this.webView2_col != null)
            {
                this.webView2_col.Dispose();
                this.webView2_col = null;
            }*/
        }
        private void OnCoreWebView21InitializationCompleted(object sender, EventArgs e)
        {
            webTaker.OnCoreWebView21InitializationCompleted(webView21, this.m_questionDetails[this.m_indexOfCurrQuestion]);
            return;
        }

        private void stopTestButtonClick(object sender, EventArgs e)
        {
            // check if the user is sure to leave the test
            DialogResult result = MessageBox.Show("?האם אתה בטוח שאתה רוצה לצאת מהתרגול",
                                      "Confirmation",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);
            if (result == DialogResult.No) // the user isn't sure
                return;

            disposedWebViews();

            if (this.m_afterQuestionParametrs.Count == 0)
            {
                //if user didnt answer questions at all - direct him to the menu
                var mp = new menuPage();
                mp.Show();
                this.Close();
                return;
            }
            int test_id = TestHistoryFileHandler.get_next_test_id();
            TestHistoryFileHandler.save_afterQuestionParametrs_to_test_history(m_afterQuestionParametrs, test_id, test_type);

            var s = new summrizePage(this.m_afterQuestionParametrs, test_id, 0); // TODO edit 0 to test_id
            s.Show();
            this.Close();
        }
    }
}
