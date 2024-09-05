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
        private JArray jArray;
        private int m_indexOfCurrQuestion = 0;
        private int m_currAnswer =0;
        public questionsPage(int amount,List<string> listOfTopics)
        {
            jArray = sqlDb.get_n_questions_from_arr_of_categorys(amount, listOfTopics);
            InitializeComponent();
            this.label1.Text = "";
            this.seeAnswerButton.Visible = false;
            Task.Run(() => InitializeWebView2());
        }
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

        private void OnCoreWebView2InitializationCompleted(object sender, EventArgs e)
        {
            if (webView21.CoreWebView2 != null)
            {
                this.m_currAnswer = sqlDb.get_num_of_correct_answer(this.jArray[this.m_indexOfCurrQuestion]);
                string htmlContent = sqlDb.get_string_of_question_and_option_from_json(this.jArray[this.m_indexOfCurrQuestion]);
                // Load the HTML content into WebView2
                webView21.NavigateToString(htmlContent);
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void afterAnswerQuestion()
        {
            string htmlContent = sqlDb.get_string_of_question_and_explanation(this.jArray[m_indexOfCurrQuestion]);
            // Load the HTML content into WebView2
            webView21.NavigateToString(htmlContent);
            this.m_indexOfCurrQuestion++;
            this.seeAnswerButton.Visible = true;
            this.answer1Button.Visible = false;
            this.answer2Button.Visible = false;
            this.answer3Button.Visible = false;
            this.answer4Button.Visible = false;
        }
        private void seeAnswerButton_Click(object sender, EventArgs e)
        {
            if (webView21.CoreWebView2 != null)
            {
                    OnCoreWebView2InitializationCompleted(sender, e);
                this.seeAnswerButton.Visible=false;
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
            if(this.m_currAnswer == 1)
            {
                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion();
        }

        private void answer2Button_Click(object sender, EventArgs e)
        {
            if (this.m_currAnswer == 2)
            {
                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion();
        }

        private void answer3Button_Click(object sender, EventArgs e)
        {
            if (this.m_currAnswer == 3)
            {
                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion();
        }

        private void answer4Button_Click(object sender, EventArgs e)
        {
            if (this.m_currAnswer == 4)
            {

                answerCorrect();
            }
            else
            {
                answerinCorrect();
            }
            afterAnswerQuestion();
        }
    }
}
