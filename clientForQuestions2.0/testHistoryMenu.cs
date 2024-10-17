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

namespace clientForQuestions2._0
{
    public partial class testHistoryMenu : Form
    {
        private List<Test> tests;
        private List<Button> m_button_list = new List<Button>();
        private int m_currentIndexOfFirstButton = 0;
        private int EXRECISEC_SHOWN_PER_CLICK = 5;
        public testHistoryMenu()
        {
            InitializeComponent();
            tests = TestHistoryFileHandler.get_test_history();
            createButtons();
            displayButtons(0, EXRECISEC_SHOWN_PER_CLICK);

            

            //LoadData();
        }

        // create buttons
        private void createButtons()
        {
            // if empty history
            if (tests == null || tests.Count == 0)
                emptyHistory_label.Visible = true;
            else
                emptyHistory_label.Visible = false;

            m_button_list = new List<Button>();
            for (int i = 0; i < tests.Count; i++)
            {
                Button btn = new Button
                {
                    Text = $"{tests[i].id}. {tests[i].date}",
                    AutoSize = true,
                    Location = new System.Drawing.Point(100, 100 + (i % EXRECISEC_SHOWN_PER_CLICK) * 40), // Adjust spacing
                    Enabled = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold) // make the text BOLD
                };
                btn.Click += Button_Click;

                m_button_list.Add(btn);
                Controls.Add(btn);
            }
        }

        private void deleteButtons()
        {
            foreach(Button button in m_button_list)
            {
                this.Controls.Remove(button);
            }
        }


        private void Button_Click(object sender, EventArgs e)
        {
            string name = (string)((Button)sender).Text;
            foreach (Test test in tests)
            {
                if ($"{test.id}. {test.date}" == name) // if the date on the button is the same of the test
                {
                    summrizePage t = new summrizePage(test.m_afterQuestionParametrs, test.id, 0);
                    t.Show();
                    this.Close();
                    return;
                }
            }
        }

        private void LoadData()
        {
            using (SQLiteConnection connection = new SQLiteConnection(TestHistoryFileHandler.connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM TestsHistoryData"; // Replace 'YourTable' with your actual table name
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, connection);

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    // Bind the data to the DataGridView
                    dataGridView1_ZMANI.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
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
            if (tests.Count == 0)
                return;
            
            // check if the user is sure to reset history
            DialogResult result = MessageBox.Show("?האם אתה בטוח שאתה רוצה לאפס את היסטורית התרגולים",
                                      "Confirmation",
                                      MessageBoxButtons.YesNo,
                                      MessageBoxIcon.Question);
            if (result == DialogResult.No) // the user isn't sure
                return;


            TestHistoryFileHandler.delete_test_history();
            tests = TestHistoryFileHandler.get_test_history();
            deleteButtons();
            createButtons();
            nextExreciseButton.Visible = false;
            previousExreciseButton.Visible = false;
        }

        private void nextExreciseButton_Click(object sender, EventArgs e)
        {

            displayButtons(m_currentIndexOfFirstButton + EXRECISEC_SHOWN_PER_CLICK, m_currentIndexOfFirstButton + EXRECISEC_SHOWN_PER_CLICK*2);
        }
        private void unvisibleButtonsFromButtonList()
        {
            for (int i = 0; i < m_button_list.Count; i++)
            {
                m_button_list[i].Visible = false;
            }
        }
        private void displayButtons(int startIndex, int endIndex)
        {
            if (startIndex < 0)
                return;
            m_currentIndexOfFirstButton = startIndex;
            unvisibleButtonsFromButtonList();

            // hide the nextQuestionsButton if there are no next questions
            if (endIndex >= m_button_list.Count)
            {
                nextExreciseButton.Visible = false;
                endIndex = m_button_list.Count;
            }
            else
                nextExreciseButton.Visible = true;
            // hide the previousQuestionsButton if there are no previous questions
            if (startIndex == 0)
                previousExreciseButton.Visible = false;
            else
                previousExreciseButton.Visible = true;

            for (int i = startIndex; i < endIndex; i++)
            {
                Button btn = m_button_list[i];
                btn.Visible = true;
                btn.BringToFront();
                //
            }
        }

        private void previousExreciseButton_Click(object sender, EventArgs e)
        {
            displayButtons(m_currentIndexOfFirstButton - EXRECISEC_SHOWN_PER_CLICK, m_currentIndexOfFirstButton);
        }

        private void emptyHistory_label_Click(object sender, EventArgs e)
        {

        }

        private void testHistoryMenu_Load(object sender, EventArgs e)
        {
            this.resetHistory_button.Location = new System.Drawing.Point(Screen.PrimaryScreen.WorkingArea.Width - this.resetHistory_button.Size.Width - 10, this.resetHistory_button.Location.Y); 
            this.emptyHistory_label.Location = new System.Drawing.Point((int) (Screen.PrimaryScreen.WorkingArea.Width - this.emptyHistory_label.Size.Width) / 2, this.emptyHistory_label.Location.Y);
        }
    }
}
