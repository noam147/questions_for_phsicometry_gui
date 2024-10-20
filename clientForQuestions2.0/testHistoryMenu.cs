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

namespace clientForQuestions2._0
{
    public partial class testHistoryMenu : Form
    {
        //private List<Test> tests;
        //private List<Button> m_button_list = new List<Button>();
        //private List<Button> m_download_button_list = new List<Button>();

        //private int m_currentIndexOfFirstButton = 0;
        private int EXRECISEC_SHOWN_PER_CLICK = 5;
        private int TOP_EMPTY_SPACE = 50;
        public testHistoryMenu()
        {
            InitializeComponent();
            //tests = TestHistoryFileHandler.get_test_history();

            //createButtons();
            //displayButtons(0, EXRECISEC_SHOWN_PER_CLICK);
        }

        // create buttons
        //private void createButtons()
        //{
        //    // if empty history
        //    if (tests == null || tests.Count == 0)
        //        emptyHistory_label.Visible = true;
        //    else
        //        emptyHistory_label.Visible = false;

        //    m_download_button_list = new List<Button>();
        //    m_button_list = new List<Button>();
        //    for (int i = 0; i < tests.Count; i++)
        //    {
        //        Button btn = new Button
        //        {
        //            Text = $"{tests[i].id}. {tests[i].date}",
        //            AutoSize = true,
        //            Location = new System.Drawing.Point(100, 100 + (i % EXRECISEC_SHOWN_PER_CLICK) * 40), // Adjust spacing
        //            Enabled = true,
        //            Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold) // make the text BOLD
        //        };
        //        Button download_btn = new Button
        //        {
        //            Text = "🡇",
        //            Size = new Size(50, 40),
        //            Location = new System.Drawing.Point(50, 100 + (i % EXRECISEC_SHOWN_PER_CLICK) * 40), // Adjust spacing
        //            Enabled = true,
        //            BackColor = Color.Yellow,
        //            Name = $"{tests[i].id}",
        //            Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Underline) // make the text BOLD
        //        };
        //        btn.Click += Button_Click;
        //        download_btn.Click += downloadButton_Click;

        //        m_button_list.Add(btn);
        //        m_download_button_list.Add(download_btn);

        //        Controls.Add(btn);
        //        Controls.Add(download_btn);
        //    }
        //}

        //private void deleteButtons()
        //{
        //    foreach(Button button in m_button_list)
        //    {
        //        this.Controls.Remove(button);
        //    }
        //    foreach (Button button in m_download_button_list)
        //    {
        //        this.Controls.Remove(button);
        //    }
        //}

        //private void downloadButton_Click(object sender, EventArgs e)
        //{
        //    int test_id = Int32.Parse(((Button)sender).Name);
        //    HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu(test_id);
        //    n.Show();
        //}
        private void downloadButton_Click(int test_id)
        {
            HtmlConvertOptionsMenu n = new HtmlConvertOptionsMenu(test_id);
            n.Show();
        }

        //private void Button_Click(object sender, EventArgs e)
        //{
        //    string name = (string)((Button)sender).Text;
        //    foreach (Test test in tests)
        //    {
        //        if ($"{test.id}. {test.date}" == name) // if the date on the button is the same of the test
        //        {
        //            summrizePage t = new summrizePage(test.m_afterQuestionParametrs, test.id, 0);
        //            t.Show();
        //            this.Close();
        //            return;
        //        }
        //    }
        //}

        private void LoadData()
        {
            try
            {
                // Bind the data to the DataGridView
                DataTable dataTable = TestHistoryFileHandler.get_history_for_DataGridView();
                history_dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

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
            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.Font = new Font("Arial", 25, FontStyle.Regular);  // Font settings
            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.ForeColor = Color.Yellow;                        // Text color (foreground)
            history_dataGridView.Columns["מועדפים"].DefaultCellStyle.SelectionForeColor = Color.Yellow;
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

            history_dataGridView.Columns["הורדה"].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", history_dataGridView.DefaultCellStyle.Font.Size, System.Drawing.FontStyle.Underline);
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
            //tests = TestHistoryFileHandler.get_test_history();
            //deleteButtons();
            //createButtons();
            //nextExreciseButton.Visible = false;
            //previousExreciseButton.Visible = false;
            LoadData();
        }

        //private void nextExreciseButton_Click(object sender, EventArgs e)
        //{

        //    displayButtons(m_currentIndexOfFirstButton + EXRECISEC_SHOWN_PER_CLICK, m_currentIndexOfFirstButton + EXRECISEC_SHOWN_PER_CLICK*2);
        //}
        //private void unvisibleButtonsFromButtonList()
        //{
        //    for (int i = 0; i < m_button_list.Count; i++)
        //    {
        //        m_button_list[i].Visible = false;
        //        m_download_button_list[i].Visible = false;
        //    }
        //}
        //private void displayButtons(int startIndex, int endIndex)
        //{
        //    if (startIndex < 0)
        //        return;
        //    m_currentIndexOfFirstButton = startIndex;
        //    unvisibleButtonsFromButtonList();

        //    // hide the nextQuestionsButton if there are no next questions
        //    if (endIndex >= m_button_list.Count)
        //    {
        //        nextExreciseButton.Visible = false;
        //        endIndex = m_button_list.Count;
        //    }
        //    else
        //        nextExreciseButton.Visible = true;
        //    // hide the previousQuestionsButton if there are no previous questions
        //    if (startIndex == 0)
        //        previousExreciseButton.Visible = false;
        //    else
        //        previousExreciseButton.Visible = true;

        //    for (int i = startIndex; i < endIndex; i++)
        //    {
        //        Button btn = m_button_list[i];
        //        btn.Visible = true;
        //        btn.BringToFront();
        //        btn = m_download_button_list[i];
        //        btn.Visible = true;
        //        btn.BringToFront();
        //        //
        //    }
        //}

        //private void previousExreciseButton_Click(object sender, EventArgs e)
        //{
        //    displayButtons(m_currentIndexOfFirstButton - EXRECISEC_SHOWN_PER_CLICK, m_currentIndexOfFirstButton);
        //}

        private void emptyHistory_label_Click(object sender, EventArgs e)
        {

        }

        private void testHistoryMenu_Load(object sender, EventArgs e)
        {
            this.resetHistory_button.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - this.resetHistory_button.Size.Width - 10, this.resetHistory_button.Location.Y); 
            this.emptyHistory_label.Location = new System.Drawing.Point((int) (Screen.PrimaryScreen.WorkingArea.Width - this.emptyHistory_label.Size.Width) / 2, this.emptyHistory_label.Location.Y);

            history_dataGridView.Location = new System.Drawing.Point(0, TOP_EMPTY_SPACE);
            history_dataGridView.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - TOP_EMPTY_SPACE);
            //lessons_dataGridView.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 0);
            history_dataGridView.Visible = true;
            history_dataGridView.AutoSize = true;

            LoadData();

            //lessons_dataGridView.Columns["לקח"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            history_dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            history_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            history_dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            history_dataGridView.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
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
