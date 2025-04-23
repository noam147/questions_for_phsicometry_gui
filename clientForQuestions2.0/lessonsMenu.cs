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
        private ContextMenuStrip contextMenu; // Context menu for options about the test
        private static int NOT_A_REAL_ROW_INDEX = -2;
        private int selected_row_index = NOT_A_REAL_ROW_INDEX; // for contextMenu 

        public lessonsMenu()
        {
            InitializeComponent();

            // for info labels:
            this.i_toolTip.SetToolTip(this.i_form, @"לחיצה על שורה תציג את השאלה -

לחיצה על הכוכב בטור ""מועדפים"" תוסיף את התרגול למועדפים, או תסיר אותו אם הוא כבר במועדפים -");

            SetupContextMenu();
        }

        private void SetupContextMenu()
        {
            void open_question_by_selected_row_index()
            {
                if (selected_row_index == NOT_A_REAL_ROW_INDEX)
                    return;

                DataGridViewRow clickedRow = lessons_dataGridView.Rows[this.selected_row_index];

                if (TestHistoryFileHandler.is_test_with_chapters(Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString())))
                {
                    TestWithChapters testWithChapters = TestHistoryFileHandler.get_test_with_chapters(Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString()));
                    List<List<afterQuestionParametrs>> chapters = new List<List<afterQuestionParametrs>>();
                    List<string> names_of_chapters = new List<string>();
                    foreach (Test chap in testWithChapters.chapters)
                    {
                        chapters.Add(chap.m_afterQuestionParametrs);
                        names_of_chapters.Add(chap.name);
                    }

                    summrizePage s_ = new summrizePage(chapters, Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString()), Int32.Parse(clickedRow.Cells["IndexOfQuestion"].Value.ToString()), names_of_chapters);
                    s_.Show();
                    this.Close();

                }
                else
                {
                    summrizePage s = new summrizePage(TestHistoryFileHandler.get_afterQuestionParametrs_of_test(Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString())), Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString()), Int32.Parse(clickedRow.Cells["IndexOfQuestion"].Value.ToString()));
                    s.Show();
                    this.Close();

                }


            }

            void delete_lesson_by_selected_row_index()
            {
                if (selected_row_index == NOT_A_REAL_ROW_INDEX)
                    return;

                DataGridViewRow clickedRow = lessons_dataGridView.Rows[this.selected_row_index];

                // check if the user is sure to leave the test
                DialogResult result = MessageBox.Show("?האם אתה בטוח שברצונך למחוק לקח זה",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);
                if (result == DialogResult.No) // the user isn't sure
                    return;


                TestHistoryFileHandler.edit_lesson_in_test_history("", Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString()), Int32.Parse(clickedRow.Cells["IndexOfQuestion"].Value.ToString())); // TODO
                selected_row_index = NOT_A_REAL_ROW_INDEX;

                LoadData();
            }

            void edit_lesson_by_selected_row_index()
            {
                if (selected_row_index == NOT_A_REAL_ROW_INDEX)
                    return;

                DataGridViewRow clickedRow = lessons_dataGridView.Rows[this.selected_row_index];

                string new_lesson = showInputForm();
                if (new_lesson == null || new_lesson.Replace("\n", "").Replace(" ", "").Length == 0)
                {
                    selected_row_index = NOT_A_REAL_ROW_INDEX;
                    return;
                }

                TestHistoryFileHandler.edit_lesson_in_test_history(new_lesson, Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString()), Int32.Parse(clickedRow.Cells["IndexOfQuestion"].Value.ToString())); // TODO

                selected_row_index = NOT_A_REAL_ROW_INDEX;

                LoadData();
            }

            void cencel_option()
            {
                selected_row_index = NOT_A_REAL_ROW_INDEX;
            }

            // opens a form to enter an input (a string)
            string showInputForm()
            {

                // Create the form and controls within the function
                Form form = new Form()
                {
                    Text = "",
                    Width = 300,
                    Height = 250,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                form.StartPosition = FormStartPosition.CenterScreen;
                Label label = new Label() { Left = 10, Top = 10, Text = "הקלד לקח:", Width = 260 };
                RichTextBox textBox = new RichTextBox() { Left = 10, Top = 30, Width = 260, Height = 150 };
                Button okButton = new Button() { Text = "שמירה", Left = 10, Width = 70, Top = 180, DialogResult = DialogResult.OK };
                Button cancelButton = new Button() { Text = "ביטול", Left = 200, Width = 70, Top = 180, DialogResult = DialogResult.Cancel };

                textBox.Text = lessons_dataGridView.Rows[this.selected_row_index].Cells["לקח"].Value.ToString();
                textBox.RightToLeft = RightToLeft.Yes;

                label.RightToLeft = RightToLeft.Yes;

                // Set button functionality
                okButton.Click += (sender, e) => { form.DialogResult = DialogResult.OK; };
                cancelButton.Click += (sender, e) => { form.DialogResult = DialogResult.Cancel; };

                // Add controls to form
                form.Controls.Add(label);
                form.Controls.Add(textBox);
                form.Controls.Add(okButton);
                form.Controls.Add(cancelButton);

                // Set Accept and Cancel buttons
                form.AcceptButton = okButton;
                form.CancelButton = cancelButton;

                // Show form as dialog and return text if OK was pressed
                return form.ShowDialog() == DialogResult.OK ? textBox.Text : null;
            }


            contextMenu = new ContextMenuStrip();

            // Add sorting options to the context menu 
            contextMenu.Items.Add("הצגת השאלה", null, (s, e) => open_question_by_selected_row_index());
            contextMenu.Items.Add("הוספת השאלה למועדפים", null, (s, e) => mark_test(selected_row_index));
            contextMenu.Items.Add("מחיקת הלקח מההיסטוריה", null, (s, e) => delete_lesson_by_selected_row_index());
            contextMenu.Items.Add("עריכת הלקח", null, (s, e) => edit_lesson_by_selected_row_index());
            contextMenu.Items.Add("ביטול", SystemIcons.Error.ToBitmap(), (s, e) => cencel_option());
        }

        private void mark_test(int row_index)
        {
            if (row_index == NOT_A_REAL_ROW_INDEX)
                return;

            int test_id = (int)lessons_dataGridView.Rows[row_index].Cells["מס' תרגול"].Value;
            int index_of_question = (int)lessons_dataGridView.Rows[row_index].Cells["IndexOfQuestion"].Value;
            bool isMarked = TestHistoryFileHandler.get_question_isMarked(test_id, index_of_question);

            TestHistoryFileHandler.set_question_isMarked(!isMarked, test_id, index_of_question);
            if (isMarked)
                lessons_dataGridView.Rows[row_index].Cells["מועדפים"].Value = TestHistoryFileHandler.MARKED_FALSE;
            else
                lessons_dataGridView.Rows[row_index].Cells["מועדפים"].Value = TestHistoryFileHandler.MARKED_TRUE;
            return;
        }

        private void backToMainMenu_button_Click(object sender, EventArgs e)
        {
            menuPage m = new menuPage();
            m.Show();
            this.Close();
        }

        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

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

            this.Cursor = Cursors.Default;
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
                    mark_test(e.RowIndex);
                    return;
                }

                // check if the user is sure to leave the test
                DialogResult result = MessageBox.Show("?האם ברצונך להציג שאלה זו",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                if (result == DialogResult.No) // the user isn't sure
                    return;


                if (TestHistoryFileHandler.is_test_with_chapters(test_id))
                {
                    TestWithChapters testWithChapters = TestHistoryFileHandler.get_test_with_chapters(test_id);
                    List<List<afterQuestionParametrs>> chapters = new List<List<afterQuestionParametrs>>();
                    List<string> names_of_chapters = new List<string>();
                    foreach (Test chap in testWithChapters.chapters)
                    {
                        chapters.Add(chap.m_afterQuestionParametrs);
                        names_of_chapters.Add(chap.name);
                    }

                    summrizePage s_ = new summrizePage(chapters, test_id, index_of_question, names_of_chapters);
                    s_.Show();
                    this.Close();

                }
                else
                {
                    summrizePage s = new summrizePage(TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id), test_id, index_of_question);
                    s.Show();
                    this.Close();

                }
            }

        }

        private void lessonsMenu_Load(object sender, EventArgs e)
        {
            this.emptyLessons_label.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.WorkingArea.Width - this.emptyLessons_label.Size.Width) / 2, this.emptyLessons_label.Location.Y);
            this.titleOfPage.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.WorkingArea.Width - this.titleOfPage.Size.Width) / 2, this.titleOfPage.Location.Y);


            lessons_dataGridView.Location = new System.Drawing.Point(0, TOP_EMPTY_SPACE);
            lessons_dataGridView.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - TOP_EMPTY_SPACE);
            //lessons_dataGridView.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 0);
            lessons_dataGridView.Visible = true;
            lessons_dataGridView.AutoSize = true;

            LoadData();

            //lessons_dataGridView.Columns["לקח"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            lessons_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Set a fixed width for the column you want to allow line breaks
            lessons_dataGridView.Columns["מס' תרגול"].Width =  100 * Screen.PrimaryScreen.WorkingArea.Width / 1300;  // Adjust width as needed
            lessons_dataGridView.Columns["תאריך"].Width = 200 * Screen.PrimaryScreen.WorkingArea.Width / 1300;  // Adjust width as needed
            lessons_dataGridView.Columns["מס' מזהה שאלה"].Width = 100 * Screen.PrimaryScreen.WorkingArea.Width / 1300;  // Adjust width as needed
            lessons_dataGridView.Columns["מועדפים"].Width = 100 * Screen.PrimaryScreen.WorkingArea.Width / 1300;  // Adjust width as needed
            lessons_dataGridView.Columns["נושא"].Width = 300 * Screen.PrimaryScreen.WorkingArea.Width / 1300;  // Adjust width as needed
            lessons_dataGridView.Columns["רמת קושי"].Width = 100 * Screen.PrimaryScreen.WorkingArea.Width / 1300;  // Adjust width as needed

            lessons_dataGridView.Columns["לקח"].Width = 400 * Screen.PrimaryScreen.WorkingArea.Width / 1300;  // Adjust width as needed

            lessons_dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            lessons_dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            lessons_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            lessons_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void lessons_dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex >= 0)
            {
                // Get the clicked row
                DataGridViewRow clickedRow = lessons_dataGridView.Rows[e.RowIndex];
                if (e.ColumnIndex == lessons_dataGridView.Columns["מועדפים"].Index)
                {
                    return;
                }
                else
                {
                    selected_row_index = e.RowIndex;
                    int test_id = (int)lessons_dataGridView.Rows[selected_row_index].Cells["מס' תרגול"].Value;
                    int index_of_question = (int)lessons_dataGridView.Rows[selected_row_index].Cells["IndexOfQuestion"].Value;
                    bool isMarked = TestHistoryFileHandler.get_question_isMarked(test_id, index_of_question);
                    if (isMarked)
                        contextMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text == "הוספת השאלה למועדפים" || item.Text == "הסרת השאלה מהמועדפים").Text = "הסרת השאלה מהמועדפים";
                    else
                        contextMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text == "הוספת השאלה למועדפים" || item.Text == "הסרת השאלה מהמועדפים").Text = "הוספת השאלה למועדפים";

                    contextMenu.Show(Cursor.Position);
                }

            }
        }
    }
}
