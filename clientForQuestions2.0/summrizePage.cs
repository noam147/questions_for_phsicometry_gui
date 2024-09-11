using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        private bool isFinishInit = false;
        public summrizePage(List<afterQuestionParametrs> questions)
        {
            this.m_questions = questions;
            InitializeComponent();
            this.timeTookForQLabel.Text = "";
            Thread thread = new Thread(() =>
            {
                while (!this.IsHandleCreated)
                {
                    // Introduce a small delay to avoid tight looping
                    Thread.Sleep(100);
                }
                InitializeWebView2();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            this.Resize += Form_Resize;
            Form_Resize(this, EventArgs.Empty);
            createButtons(questions);
            displayButtons();
        }
        private void Form_Resize(object sender, EventArgs e)
        {
            if (webView21 != null)
            {
                webView21.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 70); // Adjust height based on form size
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
        private void InitializeWebView2()
        {
            // Use InvokeRequired check to ensure all UI operations are marshaled back to the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView2));
                return;
            }

            // Initialize the WebView2 control
            webView21 = new WebView2
            {
                Location = new Point(0, 70), // Adjust Y coordinate to leave space for buttons
                Size = new Size(this.ClientSize.Width, this.ClientSize.Height - 70), // Adjust size to fit below the buttons
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            webView21.CoreWebView2InitializationCompleted += OnCoreWebView2InitializationCompleted;

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
                                Button_Click(0); // First index after init
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



        private void OnCoreWebView2InitializationCompleted(object sender, EventArgs e)
        {

            webTaker.OnCoreWebView2InitializationCompleted(webView21,new dbQuestionParmeters());
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
                    Enabled = false,
                    
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
            if(m_buttonList.Count != 0) 
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
            Button b = (Button)sender;
            int indexQuestion = (int.Parse(b.Text)-1);
            Button_Click(indexQuestion);
        }
        private void Button_Click(int questionIndex)
        {
            //here we display the question and answer based on the index
            string toDisplay = OperationsAndOtherUseful.get_string_of_question_and_explanation(this.m_questions[questionIndex].question, this.m_questions[questionIndex].userAnswer);
            int secondsTook = this.m_questions[questionIndex].timeForAnswer;
            updateQuestionTimerText(secondsTook);
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
                this.timeTookForQLabel.Text = $"Time took for question: {seconds}S";
            }

        }
        private void timeTookForQLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
