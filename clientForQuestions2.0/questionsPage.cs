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

        private int m_indexOfCurrQuestion = 0;
        private int m_currAnswer =0;
        public questionsPage(int amount,List<string> listOfTopics)
        {
            InitializeComponent();
            updateAtStart(amount, listOfTopics);
      
            PositionNextQuestionButton();
            this.Resize += MainForm_Resize;
            Task.Run(() => InitializeWebView2());
        }
        private void updateAtStart(int amount, List<string> listOfTopics)
        {
            questionDetails = sqlDb.get_n_questions_from_arr_of_categorys(amount, listOfTopics);
            m_maxQuestions = questionDetails.Count;//if amount is bigger that questions avelible
            this.label1.Text = "";
            this.nextQuestionButton.Visible = false;
            updateLabelAnswers();
        }

        //func is not intersting - just prepering html displayer
        private void InitializeWebView2()
        {
            // Ensure that UI updates are made on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView2));
                return;
            }

            webView21 = new WebView2
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(webView21);

            // Event handler for when CoreWebView2 initialization is complete
            webView21.CoreWebView2InitializationCompleted += OnCoreWebView2InitializationCompleted;

            try
            {
                // Initialize WebView2 runtime
                webView21.EnsureCoreWebView2Async(null).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        MessageBox.Show($"Error initializing WebView2: {task.Exception.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView2: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //sender and e are not in use
        private void OnCoreWebView2InitializationCompleted(object sender, EventArgs e)
        {
            //update html content in here
            if (webView21.CoreWebView2 != null)
            {
                this.m_currAnswer = this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer;
                string htmlContent = sqlDb.get_string_of_question_and_option_from_json(this.questionDetails[this.m_indexOfCurrQuestion].json_content);
                // Load the HTML content into WebView2
                webView21.NavigateToString(htmlContent);
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateLabelAnswers()
        {
            this.answersTrackLabel.Text = $"{m_rightQuestions}/{m_maxQuestions} correct";
        }
        private void afterAnswerQuestion(int answer)
        {
            //this func happens after the user clicked on an answer
            //func check if answer os true and return explantion 

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
            string htmlContent = sqlDb.get_string_of_question_and_explanation(this.questionDetails[m_indexOfCurrQuestion].json_content,answer);
            // Load the HTML content into WebView2
            webView21.NavigateToString(htmlContent);
            this.m_indexOfCurrQuestion++;
            if (m_indexOfCurrQuestion == this.questionDetails.Count)
            {
                this.nextQuestionButton.Text = "summrize";
                this.nextQuestionButton.BackColor = System.Drawing.Color.Red;
            }
            this.nextQuestionButton.Visible = true;
            this.answer1Button.Visible = false;
            this.answer2Button.Visible = false;
            this.answer3Button.Visible = false;
            this.answer4Button.Visible = false;
        }
        private void nextQuestionButtonClick(object sender, EventArgs e)
        {
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
                this.label1.Text = "";
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
            this.label1.Text = "correct :)";
            this.label1.ForeColor = System.Drawing.Color.Green;
        }
        private void answerinCorrect()
        {
            this.label1.Text = "incorrect :(";
            this.label1.ForeColor = System.Drawing.Color.Red;
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
            //PositionOptionsButton();
        }

        private void PositionNextQuestionButton()
        {
            // Position the button on the far right with a fixed height, and optional margin
            int rightMargin = 10; // Adjust as needed
            int topMargin = 50;   // Adjust as needed
            this.label1.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin+this.nextQuestionButton.Height+20);
            nextQuestionButton.Location = new Point(this.ClientSize.Width - nextQuestionButton.Width - rightMargin, topMargin);
        }



    }
}
