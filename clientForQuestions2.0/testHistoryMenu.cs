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


        public testHistoryMenu()
        {
            InitializeComponent();
        }

        private void downloadButton_Click(int test_id)
        {
            HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu(test_id);
            n.Show();
        }

        private void LoadData()
        {
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
                    if (qp.userAnswer == -1 || qp.userAnswer == OperationsAndOtherUseful.SKIPPED_Q)
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

            if (history_dataGridView.Rows.Count == 0)
            {
                history_dataGridView.Visible = false;
                emptyHistory_label.Visible = true;
            }
            else
            {
                history_dataGridView.Visible = true;
                emptyHistory_label.Visible = false;
            }

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
                                      MessageBoxIcon.Question);
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

            sortContextMenu = new ContextMenuStrip();

            // Add sorting options to the context menu for the custom column
            sortContextMenu.Items.Add("מיון לפי מספר השאלות בתרגול", null, (s, e) => SetSortOption("שאלות"));
            sortContextMenu.Items.Add("מיון לפי מספר התשובות הנכונות", null, (s, e) => SetSortOption("נכונות"));
            sortContextMenu.Items.Add("מיון לפי אחוז התשובות הנכונות", null, (s, e) => SetSortOption("אחוז"));
        }





        private void testHistoryMenu_Load(object sender, EventArgs e)
        {
            this.resetHistory_button.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - this.resetHistory_button.Size.Width - 10, this.resetHistory_button.Location.Y); 
            this.emptyHistory_label.Location = new System.Drawing.Point((int) (Screen.PrimaryScreen.WorkingArea.Width - this.emptyHistory_label.Size.Width) / 2, this.emptyHistory_label.Location.Y);

            // Handle ColumnHeaderMouseClick for opening sorting options
            history_dataGridView.ColumnHeaderMouseClick += history_dataGridView_ColumnHeaderMouseClick;

            history_dataGridView.Location = new System.Drawing.Point(0, TOP_EMPTY_SPACE);
            history_dataGridView.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - TOP_EMPTY_SPACE);
            history_dataGridView.Visible = true;
            history_dataGridView.AutoSize = true;

            SetupContextMenu();

            LoadData();

            //lessons_dataGridView.Columns["לקח"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            history_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            history_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            history_dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            history_dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);

            history_dataGridView.Visible = true;
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

                    summrizePage s = new summrizePage(TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id), test_id, 0);
                    s.Show();
                    this.Close();
                }
            }
        }

    }
}
