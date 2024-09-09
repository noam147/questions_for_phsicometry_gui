using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class menuPage : Form
    {
        private List<Button> buttons = new List<Button>();
        private List<string> topicsList = new List<string>();
        public menuPage()
        {
            InitializeComponent();
            buttons = Controls.OfType<Button>()
                       .Where(b => b.Name.StartsWith("themeBase") &&
                                   int.TryParse(b.Name.Substring(9), out int n) &&
                                   n >= 1 && n <= 43)
                       .ToList();
            for(int i =0; i < buttons.Count; i++)
            {
                buttons[i].BackColor = Color.White;
            }
            LogFileHandler.writeIntoFile("logged on");
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.StartPosition = FormStartPosition.CenterScreen;

            this.amountOfQuestionTextBox.Text = "5";
            this.topicsLabel.Text = "";
            this.continueButton.Enabled = false;
        }

        private void hestabrotButton_Click(object sender, EventArgs e)
        {
            topicButton_Click(((Button)sender).Text);
        }
        private void topicButton_Click(string topic)
        {
 
            for (int i = 0; i < topicsList.Count; i++)
            {
                if (topicsList[i] == topic)
                {
                    topicsList.RemoveAt(i);
                    if (topicsList.Count == 0)
                    {
                        this.continueButton.Enabled = false;
                    }
                    updateTopicLabel();
                    return;
                }
            }
            this.continueButton.Enabled = true;
            topicsList.Add(topic);
            updateTopicLabel();
        }
        private void updateTopicLabel()
        {
            this.topicsLabel.Text = "";
            for (int i = 0; i < topicsList.Count; i++)
            {
                this.topicsLabel.Text += topicsList[i] + "\n";
            }
        }

        private void analogyotButton_Click(object sender, EventArgs e)
        {
            topicButton_Click(((Button)sender).Text);
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            int amount = 0;
            try
            {
                amount = int.Parse(this.amountOfQuestionTextBox.Text);
            }
            catch (Exception ex)
            {
                return;
            }

            LogFileHandler.writeIntoFile("Opened new questions page");
            //get the questions here
            questionsPage c = new questionsPage(amount,this.topicsList);
            
            c.Show();
            this.Close();
        }

        private void menuPage_Load(object sender, EventArgs e)
        {

        }

        private void hociotButton_Click(object sender, EventArgs e)
        {
            topicButton_Click(((Button)sender).Text);
        }

        private void englishTopicButton_Click(object sender, EventArgs e)
        {
            //topicButton_Click("Restatements");
            //topicButton_Click("Sentence Completions");
            topicButton_Click("Reading Comprehension");
        }
    }
}
