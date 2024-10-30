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
            for (int i = 0; i < this.englishAmount.Value; i++)
            {
                currQuestions = OperationsAndOtherUseful.sendChapter_english_Questions();
                finalSimulation.Add(currQuestions);
            }
            for (int i = 0; i < this.mathAmount.Value; i++)
            {
                currQuestions = OperationsAndOtherUseful.sendChapter_math_Questions();
                finalSimulation.Add(currQuestions);
            }
            for (int i = 0; i < this.hebrewAmount.Value; i++)
            {
                currQuestions = OperationsAndOtherUseful.sendChapter_hebrew_Questions();
                finalSimulation.Add(currQuestions);
            }
            HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu(finalSimulation, "סימולציה להורדה");
            //HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu();
            try { n.Show(); }
            catch (Exception ex) { }
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            menuPage c = new menuPage();

            c.Show();
            this.Close();
        }
    }
}