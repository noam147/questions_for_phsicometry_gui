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
        private int MAX_CHARS_IN_LINE = 50;
        public lessonsMenu()
        {
            InitializeComponent();
            lessons_dataGridView.Location = new System.Drawing.Point(0, 200);
            lessons_dataGridView.MaximumSize = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 200);
            //lessons_dataGridView.Size = new System.Drawing.Size(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height - 0);
            lessons_dataGridView.Visible = true;
            lessons_dataGridView.AutoSize = true;
            LoadData();
            foreach (DataGridViewColumn col in lessons_dataGridView.Columns)
            {
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
            lessons_dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            lessons_dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;


        }

        private void backToMainMenu_button_Click(object sender, EventArgs e)
        {
            menuPage m = new menuPage();
            m.Show();
            this.Close();
        }

        private void LoadData()
        {
            try
            {
                // Bind the data to the DataGridView
                DataTable dataTable = TestHistoryFileHandler.get_lessons_from_history();
                lessons_dataGridView.DataSource = dataTable;
                lessons_dataGridView.Columns["IndexOfQuestion"].Visible = false;
                InsertLineBreaksBasedOnWidth();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // enter the test when clicked the lesson
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if a row was clicked (not a header or out of bounds)
            if (e.RowIndex >= 0)
            {
                // check if the user is sure to leave the test
                DialogResult result = MessageBox.Show("?האם אתה ברצונך להציג שאלה זו",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                if (result == DialogResult.No) // the user isn't sure
                    return;

                // Get the clicked row
                DataGridViewRow clickedRow = lessons_dataGridView.Rows[e.RowIndex];

                // Example: Retrieve the values from the clicked row
                int index_of_question = Int32.Parse(clickedRow.Cells["IndexOfQuestion"].Value.ToString());
                int test_id = Int32.Parse(clickedRow.Cells["מס' תרגול"].Value.ToString());

                summrizePage s = new summrizePage(TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id), test_id, index_of_question);
                s.Show();
                this.Close();
            }

        }


        // Method to insert line breaks based on the width of the 'Description' column
        private void InsertLineBreaksBasedOnWidth()
        {
            // Iterate through each row
            foreach (DataGridViewRow row in lessons_dataGridView.Rows)
            {
                if (row == null || row.Cells["לקח"].Value == null)
                    continue;

                // Insert line breaks into the text based on the available width
                string wrappedText = InsertLineBreaks(row.Cells["לקח"].Value.ToString(), MAX_CHARS_IN_LINE);

                // Update the cell value with the wrapped text
                row.Cells["לקח"].Value = wrappedText;
            }
        }

        // Method to insert line breaks based on a max number of characters per line
        private string InsertLineBreaks(string text, int maxCharsInLine)
        {
            if (text.Length <= maxCharsInLine)
            {
                return text; // No need to insert line breaks
            }

            StringBuilder result = new StringBuilder();

            // Insert line breaks every maxCharsInLine characters
            for (int i = 0; i < text.Length; i += maxCharsInLine)
            {
                if (i + maxCharsInLine > text.Length)
                {
                    result.Append(text.Substring(i)); // Add the remainder of the text
                }
                else
                {
                    result.AppendLine(text.Substring(i, maxCharsInLine)); // Insert a line break
                }
            }

            return result.ToString();
        }

    }
}
