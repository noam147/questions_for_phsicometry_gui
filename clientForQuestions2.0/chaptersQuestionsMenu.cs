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

            // for info labels:
            this.i_toolTip.SetToolTip(this.i_simulationDownload, @"הורדת סימולציה בעלת 6 פרקים: שני פרקים מכל סוג פרק");
            this.i_toolTip.SetToolTip(this.i_downloadChapter, @"הורדת הפרק הנבחר");
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
            this.downloadChapterButton.Enabled = true;
            this.chosen_chapter = ((Button)sender).Text.ToString();

            this.chapterButton_hebrew.BackColor = Color.White;
            this.chapterButton_math.BackColor = Color.White;
            this.chapterButton_english.BackColor = Color.White;

            ((Button)sender).BackColor = Color.LightBlue; // show which category is chosen
        }
        private List<dbQuestionParmeters> getQuestionsBasedOnChapter()
        {
            List<dbQuestionParmeters> questions;
            switch (chosen_chapter)
            {
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
                    return null; // no chapter is selected
            }
            return questions;
        }
        private void continueButton_Click(object sender, EventArgs e)
        {
            List<dbQuestionParmeters> questions = getQuestionsBasedOnChapter();
            if (questions == null)
            {
                return;
            }

            QuestionsToPdf.Questions2Pdf(questions);

            string test_type = "";
            if (this.chapterButton_hebrew.BackColor == Color.LightBlue)
                test_type = "פרק חשיבה מילולית";
            if (this.chapterButton_math.BackColor == Color.LightBlue)
                test_type = "פרק חשיבה כמותית";
            if (this.chapterButton_english.BackColor == Color.LightBlue)
                test_type = "פרק אנגלית";


            questionsPage c;
            if (this.timePerQPicker.Enabled)
                c = new questionsPage(questions, timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second, test_type);
            else
                c = new questionsPage(questions, 20 * 60, test_type);

            c.Show();
            this.Close();
        }

        private void downloadExreciseButton_Click(object sender, EventArgs e)
        {
            List<dbQuestionParmeters> questions = getQuestionsBasedOnChapter();
            if (questions == null)
            {
                return;
            }
            HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu(questions,true);
            n.Show();
        }

        private void simulationDownloadButton_Click(object sender, EventArgs e)
        {
            List<List<dbQuestionParmeters>> finalSimulation = new List<List<dbQuestionParmeters>>();
            List<dbQuestionParmeters> currQuestions;
            currQuestions = OperationsAndOtherUseful.sendChapter_english_Questions();
            finalSimulation.Add(currQuestions);
            currQuestions = OperationsAndOtherUseful.sendChapter_english_Questions();
            finalSimulation.Add(currQuestions);
            
            currQuestions = OperationsAndOtherUseful.sendChapter_math_Questions();
            finalSimulation.Add(currQuestions);
            currQuestions = OperationsAndOtherUseful.sendChapter_math_Questions();
            finalSimulation.Add(currQuestions);
            
            currQuestions = OperationsAndOtherUseful.sendChapter_hebrew_Questions();
            finalSimulation.Add(currQuestions);
            currQuestions = OperationsAndOtherUseful.sendChapter_hebrew_Questions();
            finalSimulation.Add(currQuestions);
            HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu(finalSimulation);
            //HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu();
            try { n.Show(); }
            catch(Exception ex) { }
            
        }
    }
}
