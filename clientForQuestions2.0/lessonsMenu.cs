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
    public partial class lessonsMenu : Form
    {
        private int TOP_EMPTY_SPACE = 75;
        public lessonsMenu()
        {
            InitializeComponent();


        }

        private void backToMainMenu_button_Click(object sender, EventArgs e)
        {
            menuPage m = new menuPage();
            m.Show();
            this.Close();
        }

        private void LoadData()
        {
            // Bind the data to the DataGridView
            DataTable dataTable = TestHistoryFileHandler.get_lessons_from_history_for_DataGridView();

            dataTable.Columns.Add("נושא", typeof(string));
            dataTable.Columns.Add("רמת קושי", typeof(float));

            foreach (DataRow row in dataTable.Rows)
            {
                dbQuestionParmeters question = sqlDb.get_question_based_on_id(Int32.Parse(row["מס' מזהה שאלה"].ToString()));
                row["נושא"] = question.category;
                row["רמת קושי"] = question.json_content["difficulty_level"];
            }


            lessons_dataGridView.DataSource = dataTable;
            lessons_dataGridView.Columns["IndexOfQuestion"].Visible = false;
            //InsertLineBreaksBasedOnWidth();


            if (lessons_dataGridView.Rows.Count == 0)
            {
                lessons_dataGridView.Visible = false;
                emptyLessons_label.Visible = true;
            }
            else
            {
                lessons_dataGridView.Visible = true;
                emptyLessons_label.Visible = false;
            }
            lessons_dataGridView.Columns["מועדפים"].DefaultCellStyle.Font = new Font("Arial", 35, FontStyle.Regular);  // Font settings
            lessons_dataGridView.Columns["מועדפים"].DefaultCellStyle.ForeColor = Color.Yellow;                         // Text color (foreground)
            lessons_dataGridView.Columns["מועדפים"].DefaultCellStyle.BackColor = Color.Black;                          // Text color (background)
            lessons_dataGridView.Columns["מועדפים"].DefaultCellStyle.SelectionForeColor = Color.Yellow;
            lessons_dataGridView.Columns["מועדפים"].DefaultCellStyle.SelectionBackColor = Color.Black;
            foreach (DataGridViewRow row in lessons_dataGridView.Rows)
            {
                // Check if the column exists in the DataTable
                if (lessons_dataGridView.Columns.Contains("מועדפים"))
                {
                    // Access the value in the column 'isMarked' for each row
                    DataGridViewCell cell = row.Cells["מועדפים"];
                    if (cell.Value.ToString() == "True")
                        cell.Value = TestHistoryFileHandler.MARKED_TRUE;
                    else
                        cell.Value = TestHistoryFileHandler.MARKED_FALSE;
                }
            }
        }

        // enter the test when clicked the lesson
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a row was clicked (not a header or out of bounds)
            if (e.RowIndex >= 0)
            {
                // Get the clicked row
                DataGridViewRow clickedRow = lessons_dataGridView.Rows[e.RowIndex];
                int index_of_question = Int32.Parse(clickedRow.Cells["IndexOfQuestion"].Value.ToString());
                int test_id = Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString());
                if (e.ColumnIndex == lessons_dataGridView.Columns["מועדפים"].Index)
                {
                    bool isMarked = TestHistoryFileHandler.get_question_isMarked(test_id, index_of_question);
                    
                    TestHistoryFileHandler.set_question_isMarked(!isMarked, test_id, index_of_question);
                    if (isMarked)
                        lessons_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = TestHistoryFileHandler.MARKED_FALSE;
                    else
                        lessons_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = TestHistoryFileHandler.MARKED_TRUE;
                    return;
                }

                // check if the user is sure to leave the test
                DialogResult result = MessageBox.Show("?האם ברצונך להציג שאלה זו",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                if (result == DialogResult.No) // the user isn't sure
                    return;

                summrizePage s = new summrizePage(TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id), test_id, index_of_question);
                s.Show();
                this.Close();
            }

        }

        private void lessonsMenu_Load(object sender, EventArgs e)
        {
            this.emptyLessons_label.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.WorkingArea.Width - this.emptyLessons_label.Size.Width) / 2, this.emptyLessons_label.Location.Y);


            lessons_dataGridView.Location = new System.Drawing.Point(0, TOP_EMPTY_SPACE);
            lessons_dataGridView.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - TOP_EMPTY_SPACE);
            //lessons_dataGridView.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 0);
            lessons_dataGridView.Visible = true;
            lessons_dataGridView.AutoSize = true;

            LoadData();

            //lessons_dataGridView.Columns["לקח"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            lessons_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            lessons_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set a fixed width for the column you want to allow line breaks
            //lessons_dataGridView.Columns["מס' תרגול"].Width = 100;  // Adjust width as needed
            //lessons_dataGridView.Columns["תאריך"].Width = 100;  // Adjust width as needed
            //lessons_dataGridView.Columns["מס' מזהה שאלה"].Width = 100;  // Adjust width as needed
            lessons_dataGridView.Columns["לקח"].Width = Screen.PrimaryScreen.WorkingArea.Width - 1000;  // Adjust width as needed

            lessons_dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            lessons_dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
        }
    }
}
