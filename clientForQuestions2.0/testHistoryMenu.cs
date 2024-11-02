using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using iText.Html2pdf;
using iText.Kernel.Pdf;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace clientForQuestions2._0
{
    public partial class testHistoryMenu : Form
    {
        private class CustomSortComparer : System.Collections.IComparer
        {
            private string selectedSortOption;
            private static int sortAscending = 1;
            public CustomSortComparer(SortOrder sortOrder, string selectedSortOption)
            {
                this.selectedSortOption = selectedSortOption;
                if (sortOrder == SortOrder.Descending)
                {
                    sortAscending = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortAscending = 1;
                }
            }
            private int get_numerator(string details)
            {
                return int.Parse(details.Split(' ')[0].Split('/')[1].ToString());
            }
            private int get_denominator(string details)
            {
                return int.Parse(details.Split(' ')[0].Split('/')[0].ToString());
            }

            private int get_percentage(string details)
            {
                return int.Parse(details.Split(' ')[1].Replace("%","").ToString());
            }
            public int Compare(object x, object y)
            {
                string data1 = ((DataGridViewRow)x).Cells["מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות"].Value.ToString();
                string data2 = ((DataGridViewRow)y).Cells["מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות"].Value.ToString();
                int result = 0;
                switch (selectedSortOption)
                {
                    case "שאלות":
                        result = get_numerator(data1) - get_numerator(data2);
                        break;
                    case "נכונות":
                        result = get_denominator(data1) - get_denominator(data2);
                        break;
                    case "אחוז":
                        result = get_percentage(data1) - get_percentage(data2);
                        break;
                    default:
                        break;
                }

                return result * sortAscending;
            }
        }

        private int TOP_EMPTY_SPACE = 65;

        private string lastSelectedSortOption = ""; // by what option to sort
        private bool sortAscending = true; // for sorting
        private ContextMenuStrip sortContextMenu; // Context menu for sorting

        private ContextMenuStrip contextMenu; // Context menu for options about the test
        private int selected_test_id = OperationsAndOtherUseful.NOT_A_REAL_TEST_ID; // for contextMenu 

        public testHistoryMenu()
        {
            InitializeComponent();

            // for info labels:
            this.i_toolTip.SetToolTip(this.i_form, @"לחיצה על שורה תציג את התרגול -

לחיצה על הכוכב בטור ""מועדפים"" תוסיף את התרגול למועדפים, או תסיר אותו אם הוא כבר במועדפים -

לחיצה על סמל ההורדה תציג תפריט אפשרויות להורדת התרגול -

לחיצה ימנית בעזרת העכבר על שורה של תרגול תפתח תפריט אפשרויות -");
            
        }

        private void downloadButton_Click(int test_id)
        {
            HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu(test_id);
            n.Show();
        }

        private void LoadData()
        {
            this.Cursor = Cursors.WaitCursor;

            history_dataGridView.Rows.Clear();
            history_dataGridView.Columns.Clear();
            history_dataGridView.Refresh();

            // Bind the data to the DataGridView
            DataTable dataTable = TestHistoryFileHandler.get_history_for_DataGridView();


            //STATS//
            dataTable.Columns.Add("מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות", typeof(string));
            dataTable.Columns.Add("זמן התרגול", typeof(string));
            dataTable.Columns.Add("הורדה", typeof(string));

           

            foreach (DataRow row in dataTable.Rows)
            {
                int test_id = Int32.Parse(row["מס' תרגול"].ToString());

                List<afterQuestionParametrs> questions = TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id);
                int count_questions = questions.Count;
                int count_right_answers = 0;
                int sum_time = 0;

                foreach (afterQuestionParametrs qp in questions)
                {
                    sum_time += qp.timeForAnswer;
                    if (qp.userAnswer == -1 || qp.userAnswer == OperationsAndOtherUseful.SKIPPED_Q || qp.question.questionId == TestHistoryFileHandler.CHAPTER_PARTITION_Q_ID)
                        continue;
                    if (((JArray)qp.question.json_content["options"]).Count != 0)
                        if ((int)qp.question.json_content["options"][qp.userAnswer - 1]["is_correct"] == 1)
                            count_right_answers++;
                        else
                        if (((JArray)qp.question.json_content["option_images"]).Count != 0)
                            if ((int)qp.question.json_content["option_images"][qp.userAnswer - 1]["is_correct"] == 1)
                                count_right_answers++;
                }
                row["מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות"] = $"{count_right_answers}/{count_questions} {(int)count_right_answers * 100 / count_questions}%";
                row["זמן התרגול"] = OperationsAndOtherUseful.get_time_mmss_fromseconds(sum_time);
                row["הורדה"] = "🡇";
                
            }

            // Add columns from DataTable
            foreach (DataColumn column in dataTable.Columns)
            {
                // Create a new DataGridViewColumn
                DataGridViewColumn dataGridViewColumn = new DataGridViewTextBoxColumn
                {
                    HeaderText = column.ColumnName, // Set header text
                    Name = column.ColumnName, // Optional: set a name for the column
                    DataPropertyName = column.ColumnName // Optional: if using data binding in the future
                };

                // Add the column to the DataGridView
                history_dataGridView.Columns.Add(dataGridViewColumn);
            }

            // Add rows from DataTable
            foreach (DataRow row in dataTable.Rows)
            {
                // Create a new DataGridViewRow
                int rowIndex = history_dataGridView.Rows.Add();

                // Fill the row with data
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    history_dataGridView.Rows[rowIndex].Cells[i].Value = row[i];
                }
            }

            history_dataGridView.Columns["מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות"].SortMode = DataGridViewColumnSortMode.Programmatic;
            history_dataGridView.Columns["הורדה"].SortMode = DataGridViewColumnSortMode.NotSortable;

            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.Font = new Font("Arial", 35, FontStyle.Regular);  // Font settings
            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.ForeColor = Color.Yellow;                         // Text color (foreground)
            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.BackColor = Color.Black;                          // Text color (background)
            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.SelectionForeColor = Color.Yellow;
            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.SelectionBackColor = Color.Black;

            foreach (DataGridViewRow row in history_dataGridView.Rows)
            {
                // Check if the column exists in the DataTable
                if (history_dataGridView.Columns.Contains("מועדפים"))
                {
                    // Access the value in the column 'isMarked' for each row
                    DataGridViewCell cell = row.Cells["מועדפים"];
                    if (cell.Value.ToString() == "True")
                        cell.Value = TestHistoryFileHandler.MARKED_TRUE;
                    else
                        cell.Value = TestHistoryFileHandler.MARKED_FALSE;
                }
            }
            
            history_dataGridView.Columns["הורדה"].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 25, System.Drawing.FontStyle.Underline);
            history_dataGridView.Columns["הורדה"].DefaultCellStyle.ForeColor = System.Drawing.Color.Blue;
            history_dataGridView.Columns["הורדה"].DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Blue;
            history_dataGridView.Columns["הורדה"].DefaultCellStyle.BackColor = System.Drawing.Color.Honeydew;
            history_dataGridView.Columns["הורדה"].DefaultCellStyle.SelectionBackColor = System.Drawing.Color.Honeydew;

            history_dataGridView.Columns["מועדפים"].Width = 100;  // Adjust width as needed
            history_dataGridView.Columns["הורדה"].Width = 100;  // Adjust width as needed

            //lessons_dataGridView.Columns["לקח"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            history_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            history_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            history_dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            history_dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            if (history_dataGridView.RowCount == 0)
            {
                history_dataGridView.Visible = false;
                emptyHistory_label.Visible = true;
            }
            else
            {
                history_dataGridView.Visible = true;
                emptyHistory_label.Visible = false;
            }

            this.Cursor = Cursors.Default;
        }

        private void backToMainMenu_button_Click(object sender, EventArgs e)
        {
            menuPage m = new menuPage();
            m.Show();
            this.Close();
        }

        private void resetHistory_button_Click(object sender, EventArgs e)
        {
            // no history to reset
            if (history_dataGridView.RowCount == 0)
                return;
            
            // check if the user is sure to reset history
            DialogResult result = MessageBox.Show("?האם אתה בטוח שאתה רוצה לאפס את היסטורית התרגולים",
                                      "Confirmation",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Warning);
            if (result == DialogResult.No) // the user isn't sure
                return;


            TestHistoryFileHandler.delete_test_history();
            LoadData();
        }

        private void history_dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // If the clicked column is the custom column, show the context menu
            if (history_dataGridView.Columns[e.ColumnIndex].Name == "מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות")
            {
                // Show the context menu at the mouse position
                sortContextMenu.Show(Cursor.Position);
            }
            else
            {
                // remove the SortGlyphDirection from this column if another column is being sorted
                history_dataGridView.Columns["מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות"].HeaderCell.SortGlyphDirection = SortOrder.None;
            }
        }

        private void SetupContextMenu()
        {
            void SetSortOption(string option)
            {
                // Find the index of the custom column to sort
                if (!string.IsNullOrEmpty(option))
                {
                    if (lastSelectedSortOption == option)
                        sortAscending = !sortAscending;
                    else
                        sortAscending = true;
                    if (sortAscending)
                    {
                        history_dataGridView.Sort(new CustomSortComparer(SortOrder.Ascending, option));
                        history_dataGridView.Columns["מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות"].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                    }
                    else if (!sortAscending)
                    {
                        history_dataGridView.Sort(new CustomSortComparer(SortOrder.Descending, option));
                        history_dataGridView.Columns["מספר התשובות הנכונות מתוך מספר השאלות ואחוז התשובות הנכונות"].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                    }

                    lastSelectedSortOption = option;
                }

            }

            void open_test_by_selected_id()
            {
                if (selected_test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
                    return;

                if(TestHistoryFileHandler.is_test_with_chapters(selected_test_id))
                {
                    TestWithChapters testWithChapters = TestHistoryFileHandler.get_test_with_chapters(selected_test_id);
                    List<List<afterQuestionParametrs>> chapters = new List<List<afterQuestionParametrs>>();
                    List<string> names_of_chapters = new List<string>();
                    foreach (Test chap in testWithChapters.chapters)
                    {
                        chapters.Add(chap.m_afterQuestionParametrs);
                        names_of_chapters.Add(chap.name);
                    }

                    summrizePage s_ = new summrizePage(chapters, selected_test_id, 1, names_of_chapters);
                    s_.Show();
                    this.Close();

                }
                else
                {
                    summrizePage s = new summrizePage(TestHistoryFileHandler.get_afterQuestionParametrs_of_test(selected_test_id), selected_test_id, 0);
                    s.Show();
                    this.Close();

                }


            }

            void redo_test_by_selected_id()
            {
                if (selected_test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
                    return;

                // check if the user is sure to leave the test
                DialogResult result = MessageBox.Show("?האם ברצונך לעשות תרגול חוזר",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                if (result == DialogResult.No) // the user isn't sure
                    return;

                bool isQSkip = false;
                string type = TestHistoryFileHandler.get_type_of_test(selected_test_id);

                if (type.Contains("\n"))
                    type = type.Substring(0, type.IndexOf('\n'));

                if (type != "תרגול רגיל")
                    isQSkip = true;

                questionsPage n = new questionsPage(TestHistoryFileHandler.get_dbQuestionParmeters_of_test(selected_test_id), isQSkip, 0, type + $"\n(תרגול #{selected_test_id} חוזר)");
                n.Show();
                this.Close();
            }

            void delete_test_by_selected_id()
            {
                if (selected_test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
                    return;

                // check if the user is sure to leave the test
                DialogResult result = MessageBox.Show("?האם אתה בטוח שברצונך למחוק תרגול זה",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);
                if (result == DialogResult.No) // the user isn't sure
                    return;


                TestHistoryFileHandler.delete_test_by_id(selected_test_id);
                selected_test_id = OperationsAndOtherUseful.NOT_A_REAL_TEST_ID;

                LoadData();
            }

            void answer_test_for_download_by_selected_id()
            {
                if (selected_test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
                    return;

                contextMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text == "מילוי תשובות של תרגול להורדה").Enabled = false;

                AnswerTestForDowloadQuestionsPage a = new AnswerTestForDowloadQuestionsPage(selected_test_id);
                a.Show();

                selected_test_id = OperationsAndOtherUseful.NOT_A_REAL_TEST_ID;
            }
            void rename_test_by_selected_id()
            {
                if (selected_test_id == OperationsAndOtherUseful.NOT_A_REAL_TEST_ID)
                    return;

                string new_name = showInputForm();
                if (new_name == null)
                {
                    selected_test_id = OperationsAndOtherUseful.NOT_A_REAL_TEST_ID;
                    return;
                }
                TestHistoryFileHandler.rename_test(selected_test_id, new_name);
                selected_test_id = OperationsAndOtherUseful.NOT_A_REAL_TEST_ID;

                LoadData();
            }
            void cencel_option()
            {
                selected_test_id = OperationsAndOtherUseful.NOT_A_REAL_TEST_ID;
            }

            // opens a form to enter an input (a string)
            string showInputForm()
            {

                // Create the form and controls within the function
                Form form = new Form()
                {
                    Text = "",
                    Width = 300,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };
                form.StartPosition = FormStartPosition.CenterScreen;
                Label label = new Label() { Left = 10, Top = 10, Text = "הקלד שם חדש לתרגול:", Width = 260 };
                TextBox textBox = new TextBox() { Left = 10, Top = 30, Width = 260 };
                Button okButton = new Button() { Text = "שמירה", Left = 10, Width = 70, Top = 60, DialogResult = DialogResult.OK };
                Button cancelButton = new Button() { Text = "ביטול", Left = 200, Width = 70, Top = 60, DialogResult = DialogResult.Cancel };

                textBox.Text = TestHistoryFileHandler.get_name_of_test(selected_test_id);
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
            contextMenu.Items.Add("הצגת התרגול", null, (s, e) => open_test_by_selected_id());
            contextMenu.Items.Add("תרגול חוזר", null, (s, e) => redo_test_by_selected_id());
            contextMenu.Items.Add("מחיקת התרגול מההיסטוריה", null, (s, e) => delete_test_by_selected_id());
            contextMenu.Items.Add("מילוי תשובות של תרגול להורדה", null, (s, e) => answer_test_for_download_by_selected_id());
            contextMenu.Items.Add("שינוי שם התרגול", null,(s,e) => rename_test_by_selected_id());
            contextMenu.Items.Add("ביטול", SystemIcons.Error.ToBitmap(), (s, e) => cencel_option());

            contextMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text == "מילוי תשובות של תרגול להורדה").Enabled = false;

            sortContextMenu = new ContextMenuStrip();

            // Add sorting options to the context menu for the custom column
            sortContextMenu.Items.Add("מיון לפי מספר השאלות בתרגול", null, (s, e) => SetSortOption("שאלות"));
            sortContextMenu.Items.Add("מיון לפי מספר התשובות הנכונות", null, (s, e) => SetSortOption("נכונות"));
            sortContextMenu.Items.Add("מיון לפי אחוז התשובות הנכונות", null, (s, e) => SetSortOption("אחוז"));
        }





        private void testHistoryMenu_Load(object sender, EventArgs e)
        {
            this.resetHistory_button.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - this.resetHistory_button.Size.Width - 10, this.resetHistory_button.Location.Y);
            this.refresh_button.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - this.resetHistory_button.Size.Width * 2 - 40, this.refresh_button.Location.Y);

            this.emptyHistory_label.Location = new System.Drawing.Point((int) (Screen.PrimaryScreen.WorkingArea.Width - this.emptyHistory_label.Size.Width) / 2, this.emptyHistory_label.Location.Y);
            this.titleOfPage.Location = new System.Drawing.Point((int)(Screen.PrimaryScreen.WorkingArea.Width - this.titleOfPage.Size.Width) / 2, this.titleOfPage.Location.Y);
            // Handle ColumnHeaderMouseClick for opening sorting options
            history_dataGridView.ColumnHeaderMouseClick += history_dataGridView_ColumnHeaderMouseClick;

            history_dataGridView.Location = new System.Drawing.Point(0, TOP_EMPTY_SPACE);
            history_dataGridView.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - TOP_EMPTY_SPACE);
            history_dataGridView.Visible = true;
            history_dataGridView.AutoSize = true;

            SetupContextMenu();

            LoadData();
        }

        private void history_dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a row was clicked (not a header or out of bounds)
            if (e.RowIndex >= 0)
            {
                // Get the clicked row
                DataGridViewRow clickedRow = history_dataGridView.Rows[e.RowIndex];
                int test_id = Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString());
                if (e.ColumnIndex == history_dataGridView.Columns["מועדפים"].Index)
                {
                    bool isMarked = TestHistoryFileHandler.get_test_isMarked(test_id);

                    TestHistoryFileHandler.set_test_isMarked(!isMarked, test_id);
                    if (isMarked)
                        history_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = TestHistoryFileHandler.MARKED_FALSE;
                    else
                        history_dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = TestHistoryFileHandler.MARKED_TRUE;
                    return;
                }
                else if (e.ColumnIndex == history_dataGridView.Columns["הורדה"].Index)
                {
                    downloadButton_Click(test_id);
                }
                else
                {
                    // check if the user is sure to leave the test
                    DialogResult result = MessageBox.Show("?האם ברצונך להציג תרגול זה",
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

                        summrizePage s_ = new summrizePage(chapters, test_id, 1, names_of_chapters);
                        s_.Show();
                        this.Close();

                    }
                    else
                    {
                        summrizePage s = new summrizePage(TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id), test_id, 0);
                        s.Show();
                        this.Close();

                    }
                }
            }
        }

        private void history_dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.RowIndex >= 0)
            {
                // Get the clicked row
                DataGridViewRow clickedRow = history_dataGridView.Rows[e.RowIndex];
                if (e.ColumnIndex == history_dataGridView.Columns["מועדפים"].Index)
                {
                    return;
                }
                else if (e.ColumnIndex == history_dataGridView.Columns["הורדה"].Index)
                {
                    return;
                }
                else
                {
                    selected_test_id = Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString());

                    contextMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text == "מילוי תשובות של תרגול להורדה").Enabled = history_dataGridView.Rows[e.RowIndex].Cells[history_dataGridView.Columns["סוג תרגול"].Index].Value.ToString().Contains("להורדה");
                    contextMenu.Items.OfType<ToolStripMenuItem>().FirstOrDefault(item => item.Text == "תרגול חוזר").Enabled = !TestHistoryFileHandler.is_test_with_chapters(selected_test_id);

                    contextMenu.Show(Cursor.Position);
                }
         
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
