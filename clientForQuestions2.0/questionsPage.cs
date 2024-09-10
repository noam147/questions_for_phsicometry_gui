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
        public questionsPage(int amount,List<string> listOfTopics)
        {
            InitializeComponent();
            updateAtStart(amount, listOfTopics);
      
            PositionNextQuestionButton();
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
            questionDetails = sqlDb.get_n_questions_from_arr_of_categorys(amount, listOfTopics);
            writeQuestionToLogFile();
            m_maxQuestions = questionDetails.Count;//if amount is bigger that questions avelible
            this.isUserRightLabel.Text = "";
            this.nextQuestionButton.Visible = false;
            updateLabelAnswers();
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
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height)
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
            webTaker.OnCoreWebView2InitializationCompleted(webView21,this.questionDetails[this.m_indexOfCurrQuestion].json_content);
            return;
        }
        private void updateLabelAnswers()
        {
            this.answersTrackLabel.Text = $"{m_rightQuestions}/{m_maxQuestions} correct";
            this.questionTrackLabel.Text = $"Current question is: {m_questionCounter}";
        }
        private void afterAnswerQuestion(int answer)
        {
            //this func happens after the user clicked on an answer

            //func check if answer is true and return explantion 

            //for summrize page get data on user choice and time
            afterQuestionParametrs after_questionParametrs = new afterQuestionParametrs();
            after_questionParametrs.userAnswer = answer;//we have the right answer in question details
            after_questionParametrs.question = questionDetails[m_indexOfCurrQuestion];
            after_questionParametrs.timeForAnswer = -1;//TO-DO
            m_afterQuestionParametrs.Add(after_questionParametrs);

            if (this.questionDetails[m_indexOfCurrQuestion].rightAnswer == answer)
            {
                this.m_rightQuestions++;
            }
            updateLabelAnswers();

            //after user submit an answer give to him a feedback
            string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_explanation(this.questionDetails[m_indexOfCurrQuestion].json_content,answer);
            webView21.NavigateToString(htmlContent);

            //check if this the last question
            this.m_indexOfCurrQuestion++;
            if (m_indexOfCurrQuestion == this.questionDetails.Count)//if questions end
            {
                this.nextQuestionButton.Text = "summrize";
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
            if(this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 1)
            {
                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion(1);
        }

        private void answer2Button_Click(object sender, EventArgs e)
        {
            if (this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 2)
            {
                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion(2);
        }

        private void answer3Button_Click(object sender, EventArgs e)
        {
            if (this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 3)
            {
                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion(3);
        }

        private void answer4Button_Click(object sender, EventArgs e)
        {
            if (this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer == 4)
            {
                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion(4);
        }



        private void MainForm_Resize(object sender, EventArgs e)
        {
            // Reposition the button on form resize
            PositionNextQuestionButton();
        }

        private void PositionNextQuestionButton()
        {
            // Position the button on the far right with a fixed height, and optional margin
            int rightMargin = 10; // Adjust as needed
            int topMargin = 50;   // Adjust as needed
            this.isUserRightLabel.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin+this.nextQuestionButton.Height+20);
            nextQuestionButton.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin);
        }



    }
}
