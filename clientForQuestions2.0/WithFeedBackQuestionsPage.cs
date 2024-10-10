using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json.Linq;
using System.Timers;



namespace clientForQuestions2._0
{
    public partial class WithFeedBackQuestionsPage : Form
    {
        private WebView2 webView21;
        private List<dbQuestionParmeters> m_questionDetails;
        //for summrize:
        private List<afterQuestionParametrs> m_afterQuestionParametrs = new List<afterQuestionParametrs>();
        private int m_maxQuestions;
        private int m_rightQuestions = 0;
        private int m_indexOfCurrQuestion = 0;
        private int width_screen;
        private int height_screen;
        private int w_buttonsPlace = 170;
        private int h_buttonsQuestionsPlace = 70; // for the buttons of the questions when isUserDoNotGetFeedBack
        private int Q_BUTTON_SIZE = 30;
        private int Q_CHOSEN_BUTTON_ADD_SIZE = 10;

        private int m_timeElapsed = 0;
        private int m_secondsTookForCurrq = 0;
        private System.Timers.Timer m_aTimer;
        private int secondsUntilTimerResets = 0;
        private questionsDifficultyLevel m_aDifficultyLevels;

        private bool isUserDoNotGetFeedBack;
        public WithFeedBackQuestionsPage()
        {
            //for desgner useage
            InitializeComponent();
        }

        public WithFeedBackQuestionsPage(int amount, List<string> listOfTopics, bool isQSkip, int secondsUntilTimerResets, questionsDifficultyLevel difficultyLevel)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.width_screen = this.ClientSize.Width;
            this.height_screen = this.ClientSize.Height;

            this.secondsUntilTimerResets = secondsUntilTimerResets;
            this.isUserDoNotGetFeedBack = isQSkip;
            this.m_aDifficultyLevels = difficultyLevel;
            updateAtStartOfNormalExrecize(amount,listOfTopics);
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
            InitializeWebView21();
        }
        private void updateLabelAnswers()
        {

                this.answersTrackLabel.Text = $"תשובות נכונות: {m_rightQuestions}/{m_maxQuestions}";
                this.questionTrackLabel.Text = $"שאלה נוכחית: {m_indexOfCurrQuestion+1}/{m_maxQuestions}";
        }
        private void OnCoreWebView21InitializationCompleted(object sender, EventArgs e)
        {
            webTaker.OnCoreWebView21InitializationCompleted(webView21, this.m_questionDetails[this.m_indexOfCurrQuestion]);
            return;
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
            webView21 = new WebView2
            {
                Location = new Point(w_buttonsPlace, h_buttonsQuestionsPlace),
                Size = new Size(width_screen - w_buttonsPlace, height_screen - h_buttonsQuestionsPlace)
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
                    Controls.Add(webView21);
                    //webView21.SendToBack(); // Send WebView2 to the back
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

        private void whenFinishInitWebView()
        {
            this.answer1Button.Enabled = true;
            this.answer2Button.Enabled = true;
            this.answer3Button.Enabled = true;
            this.answer4Button.Enabled = true;
            SetTimer();//start the timer here
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
        private void answerInNotAnswered()
        {
            this.isUserRightLabel.Text = "נגמר הזמן :(";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.DarkGray;
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.m_secondsTookForCurrq++;
            try
            {
                // Ensure the label is updated on the UI thread
                this.timer.Invoke((MethodInvoker)delegate
                {
                    this.timer.Text = OperationsAndOtherUseful.get_time_mmss_fromseconds(Math.Abs(secondsUntilTimerResets - m_secondsTookForCurrq));
                    if (this.secondsUntilTimerResets != OperationsAndOtherUseful.WITHOUT_TIMER)
                    {
                        actionsWithTimer();
                    }
                });
            }
            catch (Exception ex) { }
        }
        protected virtual void actionsWithTimer()
        {
            if(m_secondsTookForCurrq >= secondsUntilTimerResets && secondsUntilTimerResets != 0)
            {
                answerInNotAnswered();
                afterAnswerQuestion(OperationsAndOtherUseful.SKIPPED_Q);
            }
        }
       private void rewriteTimer()
        {
           /* if(secondsUntilTimerResets == OperationsAndOtherUseful.WITHOUT_TIMER)
            {
                return;
            }*/
            this.timer.Text = OperationsAndOtherUseful.get_time_mmss_fromseconds(m_secondsTookForCurrq);
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
            if(s != "")
            {
                s = s.Substring(0, s.Length - 2);
            }
            
            LogFileHandler.writeIntoFile("Questions id are: " + s);
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

            if (this.m_afterQuestionParametrs.Count == 0)
            {
                //if user didnt answer questions at all - direct him to the menu
                var mp = new menuPage();
                mp.Show();
                this.Close();
                return;
            }
            disposedWebViews();
            goToSummrizePage();
        }
        protected void goToSummrizePage()
        {
            var s = new summrizePage(this.m_afterQuestionParametrs, 0); // TODO edit 0 to test_id
            s.Show();
            this.Close();
        }

        private void nextQuestionButtonClick(object sender, EventArgs e)
        {
            //if questions end, check if all q are answered
            if (m_indexOfCurrQuestion == this.m_questionDetails.Count)
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
                var s = new summrizePage(this.m_afterQuestionParametrs, 0); // TODO edit 0 to test_id
                s.Show();
                this.Close();
                return;
            }

           // m_indexOfCurrQuestion++;

            rewriteTimer();

                

                m_timeElapsed = m_secondsTookForCurrq; // save the time when the user left the curr q and enter a new one
            

            this.updateLabelAnswers();

            if (webView21.CoreWebView2 != null)
            {
                OnCoreWebView21InitializationCompleted(sender, e);

                this.continueToQuestionButton.Visible = false;
                this.isUserRightLabel.Text = "";
                this.answer1Button.Visible = true;
                this.answer2Button.Visible = true;
                this.answer3Button.Visible = true;
                this.answer4Button.Visible = true;


                    this.m_secondsTookForCurrq = 0;
                    this.m_aTimer.Start();
                
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual bool checkIfUserAlreadyAnswerOnQuestionAndChangeItsAnswer(int answer)
        {
            /*
              bool isExsist = false;
            for (int i = 0; i < m_afterQuestionParametrs.Count; i++)
            {
                if (m_afterQuestionParametrs[i].indexOfQuestion == this.m_indexOfCurrQuestion)
                {
                    // Retrieve the element, modify it, and then assign it back, add time to current 42
                    var parameter = m_afterQuestionParametrs[i];      // Retrieve the element
                    parameter.userAnswer = answer;                    // Modify the property
                    m_afterQuestionParametrs[i] = parameter;          // Assign it back to the list
                    return true;
                }
            }*/
            //this is a func to be overrided
            return false;
        }
        protected void afterAnswerQuestion(int answer)
        {
            this.continueToQuestionButton.BackColor = Color.White;
            this.stopTestButton.Text = "סיום התרגול וסיכום";
            //this func happens after the user clicked on an answer
            this.m_aTimer.Stop();

            //func check if answer is true and return explantion 

            //for summrize page get data on user choice and time
            afterQuestionParametrs after_questionParametrs = new afterQuestionParametrs();
            after_questionParametrs.userAnswer = answer;//we have the right answer in question details
            after_questionParametrs.question = m_questionDetails[m_indexOfCurrQuestion];
            after_questionParametrs.timeForAnswer = this.m_secondsTookForCurrq;
            after_questionParametrs.indexOfQuestion = this.m_indexOfCurrQuestion;//we alresy incresed the curr quesiton index


            //check if there isnt alredy a question forms of that and if there is - update
           
            if (!checkIfUserAlreadyAnswerOnQuestionAndChangeItsAnswer(answer))
            {
                m_afterQuestionParametrs.Add(after_questionParametrs);
            }

            //the old question details:
            if (this.m_questionDetails[m_indexOfCurrQuestion].rightAnswer == answer)
            {
                this.m_rightQuestions++;
            }
            updateLabelAnswers();

            //after user submit an answer give to him a feedback

                string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_explanation(this.m_questionDetails[m_indexOfCurrQuestion], answer);
                webView21.NavigateToString(htmlContent);

                //check if this the last question
                this.m_indexOfCurrQuestion++;

            




            if (m_indexOfCurrQuestion == this.m_questionDetails.Count)//if questions end
            {
                this.continueToQuestionButton.Text = "סיכום";
                this.continueToQuestionButton.BackColor = System.Drawing.Color.Yellow;
            }
            //wait until user clicks on the continue button to display another q 

          
                this.continueToQuestionButton.Visible = true;

                this.answer1Button.Visible = false;
                this.answer2Button.Visible = false;
                this.answer3Button.Visible = false;
                this.answer4Button.Visible = false;
            
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
                    answerIncorrect();
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
        private void answerIncorrect()
        {
            this.isUserRightLabel.Text = ":( לא נכון ";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.Red;
        }
        private void answerinnotAnswered()
        {
            this.isUserRightLabel.Text = "נגמר הזמן :(";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.DarkGray;
        }


       
        protected void disposedWebViews()
        {
            if (webView21 != null)
            {
                this.webView21.Dispose();
                this.webView21 = null;
            }
        }




        }
    }
