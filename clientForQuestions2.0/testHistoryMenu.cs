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
    public partial class testHistoryMenu : Form
    {
        private List<Test> tests;
        private List<Button> m_button_list = new List<Button>();
        public testHistoryMenu()
        {
            InitializeComponent();
            tests = TestHistoryFileHandler.get_test_history();
            createButtons();
        }

        // create buttons
        private void createButtons()
        {
            for (int i = 0; i < tests.Count; i++)
            {
                Button btn = new Button
                {
                    Text = tests[i].date,
                    AutoSize = true,
                    Location = new System.Drawing.Point(10, 10 + (i % 10) * 40), // Adjust spacing
                    Enabled = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold) // make the text BOLD
                };
                btn.Click += Button_Click;

                m_button_list.Add(btn);
                Controls.Add(btn);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            string name = (string)((Button)sender).Text;
            foreach (Test test in tests)
            {
                if (test.date == name) // if the date on the button is the same of the test
                {
                    summrizePage t = new summrizePage(test.m_afterQuestionParametrs);
                    t.Show();
                    this.Close();
                    return;
                }
            }
        }
    }
}
