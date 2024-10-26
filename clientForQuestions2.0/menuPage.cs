using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

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
            maximize_screen();
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

        private void questionbyIdButton_Click(object sender, EventArgs e)
        {
            this.error_qById_label.Text = "";

            int id = -1;
            try
            {
                id = int.Parse(this.questionByIDtextBox.Text);
            }
            catch
            {
                this.error_qById_label.Text = "המספר המזהה אינו תקין";

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
                this.error_qById_label.Text = "המספר המזהה אינו קיים";

                return;
            }
            LogFileHandler.writeIntoFile($"got into specic question by id. id: {id}");
            q.userAnswer = OperationsAndOtherUseful.SKIPPED_Q;

            specificquestion.Add(q);
            summrizePage s = new summrizePage(specificquestion, OperationsAndOtherUseful.NOT_A_REAL_TEST_ID, 0);
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

        private void menuPage_Load(object sender, EventArgs e)
        {

        }

        private void collectionbyIdButton_Click(object sender, EventArgs e)
        {
            this.error_colById_label.Text = "";

            int col_id = -1;
            try
            {
                col_id = int.Parse(this.collectionbyIdtextBox.Text);
            }
            catch
            {
                this.error_colById_label.Text = "המספר המזהה אינו תקין";
                return;
            }
            if (!OperationsAndOtherUseful.title2colIds.Values.Any(list => list.Contains(col_id)))
            {
                this.error_colById_label.Text = "המספר המזהה אינו קיים";
                return;
            }


            //init the question
            List<int> q_ids = OperationsAndOtherUseful.colId2qIds[col_id];
            List<afterQuestionParametrs> questions = new List<afterQuestionParametrs>();

            for (int i = 0; i < q_ids.Count; i++)
            {
                afterQuestionParametrs question = new afterQuestionParametrs();
                question.question = sqlDb.get_question_based_on_id(q_ids[i]);
                question.userAnswer = OperationsAndOtherUseful.SKIPPED_Q;
                question.timeForAnswer = OperationsAndOtherUseful.QUESTION_THAT_DID_NOT_ANSWERED;
                question.indexOfQuestion = i;

                if (question.question.questionId == 0)
                {
                    LogFileHandler.writeIntoFile($"try to accsess collection id that has invalid questions, id: {col_id}, previous question id: {q_ids[i]}");
                    this.error_colById_label.Text = "חלה שגיאה באתחול התצוגה, שאלה לא תקינה במאגר";

                    return;
                }

                questions.Add(question);
            }
            LogFileHandler.writeIntoFile($"got into specic collection by id. id: {col_id}");

            summrizePage s = new summrizePage(questions, OperationsAndOtherUseful.NOT_A_REAL_TEST_ID, 0);
            s.Show();
            this.Close();
        }

        // when enter is pressed go to the question
        private void questionByIDtextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.questionbyIdButton_Click(null, null);
        }

        // when enter is pressed go to the collection
        private void collectionbyIdtextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.collectionbyIdButton_Click(null, null);
        }

        private void testHistoryButton_Click(object sender, EventArgs e)
        {
            LogFileHandler.writeIntoFile("Opened new test history menu");
            //get the questions here
            testHistoryMenu t = new testHistoryMenu();

            t.Show();
            this.Close();
        }

        private void lessons_button_Click(object sender, EventArgs e)
        {
            LogFileHandler.writeIntoFile("Opened new lessons menu");
            //get the questions here
            lessonsMenu t = new lessonsMenu();

            t.Show();
            this.Close();
        }

        private void maximize_screen()
        {
            Dictionary<Control, (float, float, float, float)> controlLayout = new Dictionary<Control, (float, float, float, float)>();

            // Store each control's original position and size
            foreach (Control ctrl in this.Controls)
            {
                controlLayout[ctrl] = (ctrl.Location.X * Screen.PrimaryScreen.WorkingArea.Width / this.ClientSize.Width, ctrl.Location.Y * Screen.PrimaryScreen.WorkingArea.Height / this.ClientSize.Height, ctrl.Size.Width * Screen.PrimaryScreen.WorkingArea.Width / this.ClientSize.Width, ctrl.Size.Height * Screen.PrimaryScreen.WorkingArea.Height / this.ClientSize.Height);
            }

            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            foreach (Control ctrl in this.Controls)
            {
                var (x, y, w, h) = controlLayout[ctrl];
                ctrl.Location = new Point((int)(x), (int)(y));
                ctrl.Size = new Size((int) (w / 1.5), (int)(h));
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
