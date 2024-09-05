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
        private List<string> topicsList = new List<string>();
        public menuPage()
        {
            InitializeComponent();
            this.topicsLabel.Text = "";
            this.continueButton.Enabled = false;
        }

        private void hestabrotButton_Click(object sender, EventArgs e)
        {
            topicButton_Click(sender);
        }
        private void topicButton_Click(object sender)
        {
            Button b = (Button)sender;
            string topic = b.Text;
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
            topicsList.Add(b.Text);
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
            topicButton_Click(sender);
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
            topicButton_Click(sender);
        }
    }
}
