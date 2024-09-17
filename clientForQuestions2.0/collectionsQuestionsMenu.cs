using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class collectionsQuestionsMenu : Form
    {
        Random random = new Random();
        string chosen_text = "";

        public collectionsQuestionsMenu()
        {
            InitializeComponent();
        }

        private void colButton_Click(object sender, EventArgs e)
        {
            this.continueButton.Enabled = true;
            this.chosen_text = ((Button)sender).Text.ToString();

            this.colButton1.BackColor = Color.White;
            this.colButton2.BackColor = Color.White;
            this.colButton3.BackColor = Color.White;

            ((Button)sender).BackColor = Color.LightBlue; // show which category is chosen
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            List<int> colIds = OperationsAndOtherUseful.title2colIds[this.chosen_text];

            int col_id = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
            List<int> questions = OperationsAndOtherUseful.colId2qIds[col_id]; // get the questions of the collection

            questionsPage c;
            //get the questions here

            if (this.timePerQPicker.Enabled)
                c = new questionsPage(col_id, questions, timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second); // CHANGE!!!!!42
            else
                c = new questionsPage(col_id, questions, 0); // CHANGE!!!!!42

            c.Show();
            this.Close();
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            menuPage c = new menuPage();

            c.Show();
            this.Close();
        }
        private void timePerQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.timePerQPicker.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
