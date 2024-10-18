using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class chaptersQuestionsMenu : Form
    {
        private String chosen_chapter = "";

        public chaptersQuestionsMenu()
        {
            InitializeComponent();
        }

        private void timePerQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.timePerQPicker.Enabled = ((CheckBox)sender).Checked;
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            menuPage c = new menuPage();

            c.Show();
            this.Close();
        }

        private void chapterButton_Click(object sender, EventArgs e)
        {
            this.continueButton.Enabled = true;
            this.chosen_chapter = ((Button)sender).Text.ToString();

            this.chapterButton1.BackColor = Color.White;
            this.chapterButton2.BackColor = Color.White;
            this.chapterButton3.BackColor = Color.White;

            ((Button)sender).BackColor = Color.LightBlue; // show which category is chosen
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            List<dbQuestionParmeters> questions;
            switch (chosen_chapter) {
                case "חשיבה מילולית":
                    questions = OperationsAndOtherUseful.sendChapter_hebrew_Questions(); 
                    break;
                case "חשיבה כמותית":
                    questions = OperationsAndOtherUseful.sendChapter_math_Questions();
                    break;
                case "אנגלית":
                    questions = OperationsAndOtherUseful.sendChapter_english_Questions(); 
                    break;
                default:
                    return; // no chapter is selected
            }

            QuestionsToPdf.Questions2Pdf(questions);

            questionsPage c;
            if (this.timePerQPicker.Enabled)
                c = new questionsPage(questions, timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second);
            else
                c = new questionsPage(questions, 20 * 60);

            c.Show();
            this.Close();
        }
    }
}
