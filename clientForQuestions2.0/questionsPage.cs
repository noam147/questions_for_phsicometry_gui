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


namespace clientForQuestions2._0
{
    public partial class questionsPage : Form
    {
        private WebView2 webView21;
        private List<dbQuestionParmeters> questionDetails;
        //for summrize:
        private List<afterQuestionParametrs> m_afterQuestionParametrs = new List<afterQuestionParametrs>();

        private int m_maxQuestions;
        private int m_rightQuestions = 0;
        private int m_questionCounter = 1;
        private int m_indexOfCurrQuestion = 0;
        private int m_currAnswer =0;

        private bool isQSkip;

        private int secondsTookForCurrq = 0;
        private System.Timers.Timer m_aTimer;
        public questionsPage(int amount,List<string> listOfTopics, bool isQSkip)
        {
            InitializeComponent();
            updateAtStart(amount, listOfTopics);
            this.isQSkip = isQSkip;
            //PositionNextQuestionButton();
            this.Resize += MainForm_Resize;
            Thread thread = new Thread(() =>
            {
                InitializeWebView2();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        private void writeQuestionToLogFile()
        {
            if(questionDetails == null)
            {
                LogFileHandler.writeIntoFile("Error: questionDetails == null - related to db!");
                return;
            }
            string s = "";
            for(int i =0;i < questionDetails.Count;i++)
            {
                s += questionDetails[i].questionId +", ";
            }
            s = s.Substring(0, s.Length - 2);
            LogFileHandler.writeIntoFile("Questions id are: "+s);
        }
        private void updateAtStart(int amount, List<string> listOfTopics)
        {
            //wait until webview is init
            this.answer1Button.Enabled = false;
            this.answer2Button.Enabled = false;
            this.answer3Button.Enabled = false;
            this.answer4Button.Enabled = false;
            questionDetails = sqlDb.get_n_questions_from_arr_of_categorys(amount, listOfTopics);
            //questionDetails = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(amount, listOfTopics, 10);
            writeQuestionToLogFile();
            m_maxQuestions = questionDetails.Count;//if amount is bigger that questions avelible
            this.isUserRightLabel.Text = "";
            this.nextQuestionButton.Visible = false;
            updateLabelAnswers();
        }
        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            this.secondsTookForCurrq++;
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
        }
        //func is not intersting - just prepering html displayer
        private async void InitializeWebView2()
        {
            // Ensure that UI updates are made on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView2));
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
                Location = new Point(0, 0),
                Size = new Size(this.ClientSize.Width-170, this.ClientSize.Height)
            };

            webView21.CoreWebView2InitializationCompleted += OnCoreWebView2InitializationCompleted;

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

        //sender and e are not in use
        private void OnCoreWebView2InitializationCompleted(object sender, EventArgs e)
        {
            webTaker.OnCoreWebView2InitializationCompleted(webView21,this.questionDetails[this.m_indexOfCurrQuestion]);
            return;
        }
        private void updateLabelAnswers()
        {
            if (!isQSkip) { this.answersTrackLabel.Text = $"{m_rightQuestions}/{m_maxQuestions} correct"; }
            else { this.answersTrackLabel.Text = "";  }
            this.questionTrackLabel.Text = $"Current question is: {m_questionCounter}/{m_maxQuestions}";
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
            this.stopTestButton.Text = "stop test and summarize";
            //this func happens after the user clicked on an answer
            this.m_aTimer.Stop();
            //func check if answer is true and return explantion 

            //for summrize page get data on user choice and time
            afterQuestionParametrs after_questionParametrs = new afterQuestionParametrs();
            after_questionParametrs.userAnswer = answer;//we have the right answer in question details
            after_questionParametrs.question = questionDetails[m_indexOfCurrQuestion];
            after_questionParametrs.timeForAnswer = this.secondsTookForCurrq;//TO-DO
            m_afterQuestionParametrs.Add(after_questionParametrs);

            if (this.questionDetails[m_indexOfCurrQuestion].rightAnswer == answer)
            {
                this.m_rightQuestions++;
            }
            updateLabelAnswers();

            //after user submit an answer give to him a feedback
            if (!isQSkip)
            {
                string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_explanation(this.questionDetails[m_indexOfCurrQuestion], answer);
                webView21.NavigateToString(htmlContent);
            }
            //check if this the last question
            this.m_indexOfCurrQuestion++;

            if (isQSkip)
            {
                this.answer1Button.Visible = false;
                this.answer2Button.Visible = false;
                this.answer3Button.Visible = false;
                this.answer4Button.Visible = false;
                nextQuestionButtonClick(null, null);
                return;
            }

            if (m_indexOfCurrQuestion == this.questionDetails.Count)//if questions end
            {
                this.nextQuestionButton.Text = "summarize";
                this.nextQuestionButton.BackColor = System.Drawing.Color.Yellow;
            }
            //wait until user clicks on the continue button to display another q 
            this.nextQuestionButton.Visible = true;
            this.answer1Button.Visible = false;
            this.answer2Button.Visible = false;
            this.answer3Button.Visible = false;
            this.answer4Button.Visible = false;
        }
        private void nextQuestionButtonClick(object sender, EventArgs e)
        {
            m_questionCounter++;
            this.updateLabelAnswers();
            //if questions end
            if (m_indexOfCurrQuestion == this.questionDetails.Count)
            {
                var s = new summrizePage(this.m_afterQuestionParametrs);
                s.Show();
                this.Close();
                return;
            }
            if (webView21.CoreWebView2 != null)
            {
                OnCoreWebView2InitializationCompleted(sender, e);
                
                this.nextQuestionButton.Visible=false;
                this.isUserRightLabel.Text = "";
                this.answer1Button.Visible = true;
                this.answer2Button.Visible = true;
                this.answer3Button.Visible = true;
                this.answer4Button.Visible = true;

                this.secondsTookForCurrq = 0;
                this.m_aTimer.Start();
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void answerCorrect()
        {
            //when answer correct display a msg
            this.isUserRightLabel.Text = "correct :)";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.Green;
        }
        private void answerinCorrect()
        {
            this.isUserRightLabel.Text = "incorrect :(";
            this.isUserRightLabel.ForeColor = System.Drawing.Color.Red;
        }
        private void answer1Button_Click(object sender, EventArgs e)
        {
            if (!isQSkip)
            {
                if (this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 1)
                {
                    answerCorrect();
                }
                else
                {
                    answerinCorrect();
                }
            }
            afterAnswerQuestion(1);
        }

        private void answer2Button_Click(object sender, EventArgs e)
        {
            if (!isQSkip)
            {
                if (this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 2)
                {
                    answerCorrect();
                }
                else
                {
                    answerinCorrect();
                }
            }
            afterAnswerQuestion(2);
        }

        private void answer3Button_Click(object sender, EventArgs e)
        {
            if (!isQSkip)
            {
                if (this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 3)
                {
                    answerCorrect();
                }
                else
                {
                    answerinCorrect();
                }
            }
            afterAnswerQuestion(3);
        }

        private void answer4Button_Click(object sender, EventArgs e)
        {
            if (!isQSkip)
            {
                if (this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 4)
                {
                    answerCorrect();
                }
                else
                {
                    answerinCorrect();
                }
            }
            afterAnswerQuestion(4);
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
            this.isUserRightLabel.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin+this.nextQuestionButton.Height+20);
            nextQuestionButton.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin);
        }

        private void questionsPage_Load(object sender, EventArgs e)
        {

        }

        private void isUserRightLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
