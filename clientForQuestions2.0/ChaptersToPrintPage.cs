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
    public partial class ChaptersToPrintPage : Form
    {
        public ChaptersToPrintPage()
        {
            InitializeComponent();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void simulationDownloadButton_Click(object sender, EventArgs e)
        {
            List<List<dbQuestionParmeters>> finalSimulation = new List<List<dbQuestionParmeters>>();
            List<dbQuestionParmeters> currQuestions;

            List<int> without_q_ids = with_already_answered_qs_checkBox.Checked ? new List<int>() : TestHistoryFileHandler.get_list_of_all_q_ids_in_history();

            for (int i = 0; i < this.hebrewAmount.Value; i++)
            {
                if (hebrewTextsCheckBox.Checked)
                {
                    currQuestions = OperationsAndOtherUseful.sendChapter_hebrew_Questions(without_q_ids);
                }
                else { currQuestions = OperationsAndOtherUseful.sendChapter_hebrew_Questions_withoutText(without_q_ids); }
                finalSimulation.Add(currQuestions);
            }
            for (int i = 0; i < this.mathAmount.Value; i++)
            {
                if (this.mathGraphCheckBox.Checked)
                { currQuestions = OperationsAndOtherUseful.sendChapter_math_Questions(without_q_ids); }
                else { currQuestions = OperationsAndOtherUseful.sendChapter_math_Questions_without_graph(without_q_ids); }

                finalSimulation.Add(currQuestions);
            }
            for (int i = 0; i < this.englishAmount.Value; i++)
            {
                if(this.englishTextsCheckBox.Checked)
                {
                    currQuestions = OperationsAndOtherUseful.sendChapter_english_Questions(without_q_ids);
                }
                else { currQuestions = OperationsAndOtherUseful.sendChapter_english_Questions_withoutTexts(without_q_ids); }
                finalSimulation.Add(currQuestions);
            }



            // shuffle if random order is chosen
            if (isRand_checkBox.Checked)
            {
                Random rng = new Random();
                for (int i = finalSimulation.Count - 1; i > 0; i--)
                {
                    int j = rng.Next(i + 1); // Random index from 0 to i
                    (finalSimulation[i], finalSimulation[j]) = (finalSimulation[j], finalSimulation[i]); // Swap elements
                }
            }

            HtmlConvertOptionsMenu h = new HtmlConvertOptionsMenu(finalSimulation, "סימולציה להורדה");
            //HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu();
            try { h.Show(); }
            catch (Exception ex) { }
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            menuPage c = new menuPage();

            c.Show();
            this.Close();
        }

        private void fullSimulation_button_Click(object sender, EventArgs e)
        {
            // choose a chapter type without a pylot
            Random rng = new Random();
            int chapter_without_pylot = rng.Next(3);

            // get the qs
            List<List<dbQuestionParmeters>> finalSimulation = new List<List<dbQuestionParmeters>>();
            List<dbQuestionParmeters> currQuestions;

            for (int i = 0; i < (chapter_without_pylot == 0 ? 2 : 3); i++)
            {
                currQuestions = OperationsAndOtherUseful.sendChapter_hebrew_Questions();
                finalSimulation.Add(currQuestions);
            }
            for (int i = 0; i < (chapter_without_pylot == 1 ? 2 : 3); i++)
            {
                currQuestions = OperationsAndOtherUseful.sendChapter_math_Questions();
                finalSimulation.Add(currQuestions);
            }
            for (int i = 0; i < (chapter_without_pylot == 2 ? 2 : 3); i++)
            {
                currQuestions = OperationsAndOtherUseful.sendChapter_english_Questions();
                finalSimulation.Add(currQuestions);
            }


            //shuffle chapters
            for (int i = finalSimulation.Count - 1; i > 0; i--)
            {
                int j = rng.Next(i + 1); // Random index from 0 to i
                (finalSimulation[i], finalSimulation[j]) = (finalSimulation[j], finalSimulation[i]); // Swap elements
            }

            HtmlConvertOptionsMenu h = new HtmlConvertOptionsMenu(finalSimulation, "סימולציה מלאה להורדה");
            //HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu();
            try { h.Show(); }
            catch (Exception ex) { }
        }
    }
}