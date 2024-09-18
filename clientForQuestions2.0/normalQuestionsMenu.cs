using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    
    public struct questionsDifficultyLevel
    {
        public decimal minlevel;
        public decimal maxLevel;
    }
    public partial class normalQuestionsMenu : Form
    {
        questionsDifficultyLevel difficultyLevels = new questionsDifficultyLevel();
        private List<Button> m_buttonsList = new List<Button>();
        private List<string> topicsList = new List<string>();

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
            unVisibleDifficulyLevelItems();
            
            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.amountOfQuestionNumericUpDown.Value = 5;
            this.continueButton.Enabled = false;

            this.difficultyLevels.minlevel = OperationsAndOtherUseful.MIN_LEVEL;
            this.difficultyLevels.maxLevel = OperationsAndOtherUseful.MAX_LEVEL;

            this.difficulyLevelMinVal.Maximum = OperationsAndOtherUseful.MAX_LEVEL;
            this.difficulyLevelMinVal.Minimum = OperationsAndOtherUseful.MIN_LEVEL;
            this.difficulyLevelMaxVal.Maximum = OperationsAndOtherUseful.MAX_LEVEL;
            this.difficulyLevelMaxVal.Minimum = OperationsAndOtherUseful.MIN_LEVEL;
            initByPreviousSettingsOfUser();
        }
        private void initByPreviousSettingsOfUser()
        {
            Settings settings = SettingsFileHandler.getSettingsFromFile();
            if(settings.isExsist == SettingsFileHandler.NOT_EXSIST)
            {
                return;
            }
            if(settings.minLevel != SettingsFileHandler.WITHOUT_SETTING)
            {
                this.dificultLevelcheckBox.Checked = true;
                this.difficulyLevelMinVal.Value = settings.minLevel;
            }
            if (settings.maxLevel != SettingsFileHandler.WITHOUT_SETTING)
            {
                this.dificultLevelcheckBox.Checked = true;
                this.difficulyLevelMaxVal.Value = settings.maxLevel;
            }
            if (settings.amount != SettingsFileHandler.WITHOUT_SETTING)
            {
                this.amountOfQuestionNumericUpDown.Value = settings.amount;
            }
            if (settings.minuets != SettingsFileHandler.WITHOUT_SETTING)
            {
                this.timePerQPicker.Value = new DateTime(2000, 1, 1, 0, settings.minuets, 0);
                if (settings.seconds != SettingsFileHandler.WITHOUT_SETTING)
                {
                    this.timePerQPicker.Value = new DateTime(2000, 1, 1, 0, settings.minuets, settings.seconds);
                }
            }
            else
            {
                this.timePerQCheckbox.Checked = false;
            }
            if (settings.withoutFeedback == 0)
            {
                this.skipFeedBackCheckBox.Checked = false;
            }
            if (settings.withoutFeedback == 1)
            {

                this.skipFeedBackCheckBox.Checked = true;
            }
            //iterate on settings as a dict:
            foreach (FieldInfo field in typeof(Settings).GetFields())
            {
                string name = field.Name;
                object value = field.GetValue(settings);
            }

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
            string topic_clicked = ((Button)sender).Text.ToString();
            if (topic_clicked.StartsWith("▼"))
                topic_clicked = topic_clicked.Substring(2);
            List<string> topics = OperationsAndOtherUseful.topicsdict[topic_clicked];
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

        private void updateSettingsForNextMenu()
        {
            
            int amount1 = (int)this.amountOfQuestionNumericUpDown.Value;
            int maxLevel;
            int minLevel;
            if(this.dificultLevelcheckBox.Checked)
            {
                maxLevel = (int)this.difficulyLevelMaxVal.Value;
                minLevel = (int)this.difficulyLevelMinVal.Value;
            }
            else
            {
                maxLevel = SettingsFileHandler.WITHOUT_SETTING;
                minLevel = SettingsFileHandler.WITHOUT_SETTING;
            }
            int seconds;
            int minutes;
            if(this.timePerQCheckbox.Checked)
            {
                minutes = this.timePerQPicker.Value.Minute;
                seconds = (int)this.timePerQPicker.Value.Second;
            }
            else
            {
                seconds = SettingsFileHandler.WITHOUT_SETTING;
                minutes = SettingsFileHandler.WITHOUT_SETTING;
            }
            int withoutFeedBack;
            if(this.skipFeedBackCheckBox.Checked)
            {
                withoutFeedBack = 1;
            }
            else
            { withoutFeedBack = 0; }
            Settings settings = new Settings{amount = amount1,seconds=seconds,minuets=minutes,withoutFeedback=withoutFeedBack,maxLevel=maxLevel,minLevel=minLevel,isExsist=1};
            SettingsFileHandler.writeSettingsIntoFile(settings);
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
            if(amount > 99)
            {
                amount = 99;
            }
            LogFileHandler.writeIntoFile("Opened new questions page");

            questionsPage c;
            //get the questions here

            if (this.timePerQPicker.Enabled)
                c = new questionsPage(amount, this.topicsList, this.skipFeedBackCheckBox.Checked, timePerQPicker.Value.Minute*60 + timePerQPicker.Value.Second,difficultyLevels); // CHANGE!!!!!42
            else
                c = new questionsPage(amount, this.topicsList, this.skipFeedBackCheckBox.Checked, 0,difficultyLevels); // CHANGE!!!!!42
            updateSettingsForNextMenu();
            c.Show();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(this.dificultLevelcheckBox.Checked)
            {
                visibleDifficulyLevelItems();
            }
            else
            {
                unVisibleDifficulyLevelItems();
            }
        }
        private void unVisibleDifficulyLevelItems()
        {
            this.difficulyLevelMaxVal.Visible = false;
            this.difficulyLevelMinVal.Visible = false;
            this.difficulyLevelMaxValLabel.Visible = false;
            this.difficulyLevelMinValLabel.Visible = false;
        }
        private void visibleDifficulyLevelItems()
        {
            this.difficulyLevelMaxVal.Visible = true;
            this.difficulyLevelMinVal.Visible = true;
            this.difficulyLevelMaxValLabel.Visible = true;
            this.difficulyLevelMinValLabel.Visible = true;
        }

        private void difficulyLevelMinVal_ValueChanged(object sender, EventArgs e)
        {
            this.difficultyLevels.minlevel = (decimal)this.difficulyLevelMinVal.Value;
            this.difficulyLevelMaxVal.Minimum = this.difficultyLevels.minlevel;

        }

        private void difficulyLevelMaxVal_ValueChanged(object sender, EventArgs e)
        {
            this.difficultyLevels.maxLevel = (decimal)this.difficulyLevelMaxVal.Value;
            this.difficulyLevelMinVal.Maximum = this.difficultyLevels.maxLevel;
        }

        private void timePerQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.timePerQPicker.Enabled = ((CheckBox)sender).Checked;
        }
    }
}
