using System;
using System.IO;
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

        public menuPage()
        {
            InitializeComponent();
            //this.rjButtons22.buttonStyle = RJButtons2.ButtonStyle.GradientRounded;
            //this.rjButtons22.BackColor = Color.Blue;
            LogFileHandler.writeIntoFile("logged on");
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void normalQuestions_Clicked(object sender, EventArgs e)
        {
            LogFileHandler.writeIntoFile("Opened new questions menu");
            //get the questions here
            normalQuestionsMenu c = new normalQuestionsMenu();

            c.Show();
            this.Close();
        }

        private void collectionsQuestions_Clicked(object sender, EventArgs e)
        {
            LogFileHandler.writeIntoFile("Opened new collections questions menu");
            //get the questions here
            collectionsQuestionsMenu t = new collectionsQuestionsMenu();

            t.Show();
            this.Close();
        }

        private void menuPage_Load_1(object sender, EventArgs e)
        {

        }

        private void questionbyIdButton_Click(object sender, EventArgs e)
        {
            int id = -1;
            try
            {
                id = int.Parse(this.questionByIDtextBox.Text);
            }
            catch
            {
                return;
            }
            //init the question
            List<afterQuestionParametrs> specificquestion = new List<afterQuestionParametrs>();
            afterQuestionParametrs q = new afterQuestionParametrs();
            q.timeForAnswer = OperationsAndOtherUseful.QUESTION_THAT_DID_NOT_ANSWERED; ;
            q.question = sqlDb.get_question_based_on_id(id);
            if (q.question.questionId == 0)
            {
                LogFileHandler.writeIntoFile($"try to accsess id that is not exsist, id: {id}");
                return;
            }
            LogFileHandler.writeIntoFile($"got into specic question by id. id: {id}");
            q.userAnswer = OperationsAndOtherUseful.SKIPPED_Q;

            specificquestion.Add(q);
            summrizePage s = new summrizePage(specificquestion);
            s.Show();
            this.Close();

        }

        private void chaptersQuestions_Click(object sender, EventArgs e)
        {
            LogFileHandler.writeIntoFile("Opened new chapters questions menu");
            //get the questions here
            chaptersQuestionsMenu t = new chaptersQuestionsMenu();

            t.Show();
            this.Close();
        }
    }
}
