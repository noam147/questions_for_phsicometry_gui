using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
        public summrizePage(List<afterQuestionParametrs> questions)
        {
            this.m_questions = questions;
            InitializeComponent();
            createButtons(questions);
            displayButtons();
            Thread thread = new Thread(() =>
            {
                InitializeWebView2();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            this.Resize += Form_Resize;
            Form_Resize(this, EventArgs.Empty);
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            if (webView21 != null)
            {
                webView21.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 70); // Adjust height based on form size
            }
            this.button1.Location = new Point(this.ClientSize.Width - button1.Width - 40, 20);
        }

        private async void InitializeWebView2()
        {
            // Ensure that UI updates are made on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView2));
                return;
            }

            webView21 = new WebView2
            {
                Location = new Point(0, 70), // Adjust Y coordinate to leave space for buttons
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 70), // Adjust size to fit below the buttons
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            
            webView21.CoreWebView2InitializationCompleted += OnCoreWebView2InitializationCompleted;

            int maxRetries = 5;
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

                    // Check if the task has completed and without faults
                    if (task.IsCompleted && task.Exception == null)
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
                    await Task.Delay(2000); // Wait for 2 seconds before retrying
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during WebView2 initialization attempt {retryCount}: {ex.Message}",
                        "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Wait before next retry
                    await Task.Delay(2000); // Wait for 2 seconds
                }
            }

            if (initialized)
            {
                // Safely add the control to the form on the main thread
                try
                {
                    Controls.Add(webView21);
                    webView21.SendToBack(); // Ensure WebView2 is on back of other controls
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



        private void OnCoreWebView2InitializationCompleted(object sender, EventArgs e)
        {

            webTaker.OnCoreWebView2InitializationCompleted(webView21,null);
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
                    Location = new System.Drawing.Point(10+i*55, 30), // Adjust spacing
                    
                };
                btn.Click += Button_Click;
                //if answer was correct
                if (currQuestionRight[i].question.rightAnswer == currQuestionRight[i].userAnswer)
                {
                    btn.BackColor = Color.Cyan;
                }
                else
                {
                    btn.BackColor = Color.Red;
                }
                m_buttonList.Add(btn);
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
            Button b = (Button)sender;
            int indexQuestion = (int.Parse(b.Text)-1);

            string toDisplay = sqlDb.get_string_of_question_and_explanation(this.m_questions[indexQuestion].question.json_content, this.m_questions[indexQuestion].userAnswer);
            this.webView21.NavigateToString(toDisplay);
            //display the question with html explanation etc and user choice
            //maybe even keep track on the user choice
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuPage m = new menuPage();
            m.Show();
            this.Close();
        }
    }
}
