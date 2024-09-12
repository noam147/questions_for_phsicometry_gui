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
    public partial class normalQuestionsMenu : Form
    {
        private List<Button> m_buttonsList = new List<Button>();
        private List<string> topicsList = new List<string>();
        private Dictionary<string, List<string>> topicsdict = new Dictionary<string, List<string>>
        {
            {

                "בחירת כל הנושאים", new List<string>
                {
                    "אותיות וספרות",
                    "פעולות מומצאות",
                    "הצבת תשובות",
                    "הצבת מספרים",
                    "דמיון צורות",
                    "אנליטית",
                    "תלת-ממד",
                    "פיתגורס",
                    "שטחים",
                    "קווים וזוויות",
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים",
                    "צירופים",
                    "הסתברות",
                    "חלוקה",
                    "תנועה",
                    "הספק",
                    "ממוצע",
                    "כלליות 1",
                    "כלליות 2",
                    "אחוזים",
                    "יחס",
                    "טווחים",
                    "התנהגות אלגברית",
                    "חיובי שלילי וערך מוחלט",
                    "שלמים",
                    "משוואות",
                    "ביטויים",
                    "אי-שוויון",
                    "עצרת",
                    "אנלוגיות",
                    "שאלות פסקה",
                    "השלמת משפטים מילולי",
                    "עריכת ניסוי",
                    "משמעות מילולית",
                    "כללים וסידורים",
                    "Restatements",
                    "אוצר-מילים",
                    "Sentence Completions"
                }
            },
            {
                "חשיבה כמותית", new List<string>
                {
                    "אותיות וספרות",
                    "פעולות מומצאות",
                    "הצבת תשובות",
                    "הצבת מספרים",
                    "דמיון צורות",
                    "אנליטית",
                    "תלת-ממד",
                    "פיתגורס",
                    "שטחים",
                    "קווים וזוויות",
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים",
                    "צירופים",
                    "הסתברות",
                    "חלוקה",
                    "תנועה",
                    "הספק",
                    "ממוצע",
                    "כלליות 1",
                    "כלליות 2",
                    "אחוזים",
                    "יחס",
                    "טווחים",
                    "התנהגות אלגברית",
                    "חיובי שלילי וערך מוחלט",
                    "שלמים",
                    "משוואות",
                    "ביטויים",
                    "אי-שוויון",
                    "עצרת"
                }
            },
            {
                "חשיבה מילולית", new List<string>
                {
                    "אנלוגיות",
                    "שאלות פסקה",
                    "השלמת משפטים מילולי",
                    "עריכת ניסוי",
                    "משמעות מילולית",
                    "כללים וסידורים"
                }
            },
            {
                "אנגלית", new List<string>
                {
                    "Restatements",
                    "אוצר-מילים",
                    "Sentence Completions"
                }
            },
            {
                "הצבה", new List<string>
                {
                    "הצבת תשובות",
                    "הצבת מספרים"
                }
            },
            {
                "▼ גיאומטריה", new List<string>
                {
                    "דמיון צורות",
                    "אנליטית",
                    "תלת-ממד",
                    "פיתגורס",
                    "שטחים",
                    "קווים וזוויות",
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים"
                }
            },
            {
                "▼ מצולעים", new List<string>
                {
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים"
                }
            },
            {
                "▼ בעיות מילוליות", new List<string>
                {
                    "צירופים",
                    "הסתברות",
                    "חלוקה",
                    "תנועה",
                    "הספק",
                    "ממוצע",
                    "כלליות 1",
                    "כלליות 2",
                    "אחוזים",
                    "יחס",
                    "טווחים"
                }
            },
            {
                "▼ התנהגות אלגברית", new List<string>
                {
                    "התנהגות אלגברית",
                    "חיובי שלילי וערך מוחלט",
                    "שלמים"
                }
            },
            {
                "▼ משוואות", new List<string>
                {
                    "משוואות",
                    "ביטויים",
                    "אי-שוויון",
                    "עצרת"
                }
            },
            {
                "▼ הבנה והסקה", new List<string>
                {
                    "שאלות פסקה",
                    "השלמת משפטים מילולי",
                    "עריכת ניסוי",
                    "משמעות מילולית",
                    "כללים וסידורים"
                }
            },
            {
                "▼ אוצר מילים", new List<string>
                {
                    "אוצר-מילים",
                    "Sentence Completions"
                }
            }
        };

        Dictionary<string, string> themeToNameDict = new Dictionary<string, string>
        {
            { "הסקה מתרשים", "themeBase1" },
            { "אותיות וספרות", "themeBase2" },
            { "פעולות מומצאות", "themeBase3" },
            { "הצבה", "themeBase4" },
            { "הצבת תשובות", "themeBase4" },
            { "הצבת מספרים", "themeBase4" },
            { "דמיון צורות", "themeBase5" },
            { "אנליטית", "themeBase6" },
            { "תלת-ממד", "themeBase7" },
            { "פיתגורס", "themeBase8" },
            { "שטחים", "themeBase9" },
            { "קווים וזוויות", "themeBase10" },
            { "מצולעים", "themeBase11" },
            { "מרובעים", "themeBase12" },
            { "מעגלים", "themeBase13" },
            { "משולשים", "themeBase14" },
            { "צירופים", "themeBase15" },
            { "הסתברות", "themeBase16" },
            { "חלוקה", "themeBase17" },
            { "תנועה", "themeBase18" },
            { "הספק", "themeBase19" },
            { "ממוצע", "themeBase20" },
            { "כלליות 1", "themeBase21" },
            { "כלליות 2", "themeBase22" },
            { "אחוזים", "themeBase23" },
            { "יחס", "themeBase24" },
            { "טווחים", "themeBase25" },
            { "התנהגות אלגברית", "themeBase26" },
            { "חיובי שלילי וערך מוחלט", "themeBase27" },
            { "שלמים", "themeBase28" },
            { "משוואות", "themeBase29" },
            { "ביטויים", "themeBase30" },
            { "אי-שוויון", "themeBase31" },
            { "עצרת", "themeBase32" },
            { "אנלוגיות", "themeBase33" },
            { "קטע קריאה", "themeBase34" },
            { "שאלות פסקה", "themeBase35" },
            { "השלמת משפטים מילולי", "themeBase36" },
            { "עריכת ניסוי", "themeBase37" },
            { "משמעות מילולית", "themeBase38" },
            { "כללים וסידורים", "themeBase39" },
            { "Restatements", "themeBase40" },
            { "Reading Comprehension", "themeBase41" },
            { "אוצר-מילים", "themeBase42" },
            { "Sentence Completions", "themeBase43" }
        };


        public normalQuestionsMenu()
        {
            InitializeComponent();
            updatebuttons();
            LogFileHandler.writeIntoFile("logged on");
            //this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.StartPosition = FormStartPosition.CenterScreen;

            this.amountOfQuestionNumericUpDown.Value = 5;
            this.continueButton.Enabled = false;
        }

        private void back2MainMenuButton_Click(object sender, EventArgs e)
        {
            menuPage c = new menuPage();

            c.Show();
            this.Close();
        }

        private void updatebuttons()
        {
            m_buttonsList = Controls.OfType<Button>()
                            .Where(b => b.Name.StartsWith("themeBase") &&
                                        int.TryParse(b.Name.Substring(9), out int n) &&
                                        n >= 1 && n <= 43)
                            .ToList();

            // Optional: Display button names in the console to verify
            foreach (var button in m_buttonsList)
            {
                button.BackColor = Color.White;
            }
        }

        private void topicButton_Click(string topic)
        {
            Button myButton = this.Controls.Find(themeToNameDict[topic], true).FirstOrDefault() as Button;
            if (topicsList.Contains(topic))
            {
                topicsList.Remove(topic);
                if (myButton != null)
                {
                    myButton.BackColor = Color.White;
                }
            }
            else
            {
                this.continueButton.Enabled = true;
                topicsList.Add(topic);
                if (myButton != null)
                {
                    myButton.BackColor = Color.LightBlue;
                }
            }

            if (topicsList.Count == 0)
            {
                this.continueButton.Enabled = false;
            }

        }

        private void themebase_Click(object sender, EventArgs e)
        {
            topicButton_Click(((Button)sender).Text);
        }

        private void theme_Click(object sender, EventArgs e)

        {
            List<string> topics = topicsdict[((Button)sender).Text];
            bool allSelected = true;
            foreach (string topic in topics)
            {
                if (!topicsList.Contains(topic))
                {
                    allSelected = false;
                    topicButton_Click(topic);
                }
            }
            if (!allSelected) { return; }

            foreach (string topic in topics)
            {
                topicButton_Click(topic);
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            int amount = 0;
            try
            {
                amount = (int) this.amountOfQuestionNumericUpDown.Value;
            }
            catch (Exception ex)
            {
                return;
            }

            LogFileHandler.writeIntoFile("Opened new questions page");

            questionsPage c;
            //get the questions here
            if (this.timePerQPicker.Enabled)
                c = new questionsPage(amount, this.topicsList, this.isQSkip_checkBox.Checked, timePerQPicker.Value.Minute*60 + timePerQPicker.Value.Second); // CHANGE!!!!!42
            else
                c = new questionsPage(amount, this.topicsList, this.isQSkip_checkBox.Checked, 0); // CHANGE!!!!!42

            c.Show();
            this.Close();
        }

        private void normalQuestionsMenu_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.dificultLevelcheckBox.Checked)
            {
                this.difficulyLevelUpAndDown.Enabled = true;
            }
            else
            {
                this.difficulyLevelUpAndDown.Value = this.difficulyLevelUpAndDown.Minimum;
                this.difficulyLevelUpAndDown.Enabled = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void timePerQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.timePerQPicker.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
