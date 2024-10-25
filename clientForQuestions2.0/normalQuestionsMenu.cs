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

            // for info labels:
            this.i_toolTip.SetToolTip(this.i_theme, @"לחצו על שם הנושא כדי לכלול אותו בתרגול -

לחצו על ""בחירת כל הנושאים"" כדי ששאלות התרגול ייבחרו מכל הנושאים -

לחצו על כותרות של נושאים כדי לבחור את כל התת-נושאים שלהן -
(לדוג': 'חשיבה כמותית', הבנה והסקה')");
            this.i_toolTip.SetToolTip(this.i_skipFeedBack, @",כאשר מסומן, אחרי שעונים על שאלה לא מתקבל משוב לגבי התשובה שלך (האם צדקת, מהי התשובה הנכונה וכו')
ובנוסף, ניתן לעבור בין שאלות במהלך התרגול עד להגשתו, וזמן התרגול יהיה כולל לכל השאלות");
            this.i_toolTip.SetToolTip(this.i_timePerQ, @"כאשר מסומן, תיבת הטקטס מתחת לתיבת הסימון מגדירה את כמות הזמן הקצובה לכל שאלה

כאשר התרגול הוא ללא משוב מיידי על תשובות, הזמן הכולל של התרגול *
יהיה כמות הזמן הקצובה לכל שאלה כפול מספר השאלות");
            this.i_toolTip.SetToolTip(this.i_withAlreadyAnsweredQs, @"כאשר מסומן, התרגול יכלול שאלות מתרגולים קודמים, אחרת לא יהיו בתרגול שאלות שכבר עשית");
            this.i_toolTip.SetToolTip(this.i_dificultLevel, @":כאשר מסומן, ניתן לבחור טווח רמות הקושי של השאלות בתרגול, לפי
מינימום - רמת הקושי המינימלית של השאלות בתרגול
מקסימום - רמת הקושי המקסימלית של השאלות בתרגול");


            Dictionary<Control, (float, float, float, float)> controlLayout = new Dictionary<Control, (float, float, float, float)>();

            // Store each control's original position and size
            foreach (Control ctrl in this.Controls)
            {
                controlLayout[ctrl] = (ctrl.Location.X * Screen.PrimaryScreen.WorkingArea.Width / this.ClientSize.Width, ctrl.Location.Y * Screen.PrimaryScreen.WorkingArea.Height / this.ClientSize.Height, ctrl.Size.Width * Screen.PrimaryScreen.WorkingArea.Width / this.ClientSize.Width, ctrl.Size.Height * Screen.PrimaryScreen.WorkingArea.Height / this.ClientSize.Height);
            }

            this.WindowState = FormWindowState.Maximized;
            //this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;

            foreach (Control ctrl in this.Controls)
            {
                var (x,y,w,h) = controlLayout[ctrl];
                ctrl.Location = new Point((int) (x), (int) (y));
                ctrl.Size = new Size(ctrl.Size.Width, (int) (h));
            }

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
        private void resetSettings()
        {
            // topics
            updatebuttons();
            topicsList = new List<string>();

            // timePerQ
            timePerQCheckbox.Checked = true;
            timePerQCheckbox_CheckedChanged(timePerQCheckbox, null);
            timePerQPicker.Value = new System.DateTime(2000, 1, 1, 0, 1, 0, 0);

            // difficultyLevel
            dificultLevelcheckBox.Checked = false;
            unVisibleDifficulyLevelItems();
            this.difficultyLevels.minlevel = OperationsAndOtherUseful.MIN_LEVEL;
            this.difficultyLevels.maxLevel = OperationsAndOtherUseful.MAX_LEVEL;

            this.difficulyLevelMinVal.Maximum = OperationsAndOtherUseful.MAX_LEVEL;
            this.difficulyLevelMinVal.Minimum = OperationsAndOtherUseful.MIN_LEVEL;
            this.difficulyLevelMaxVal.Maximum = OperationsAndOtherUseful.MAX_LEVEL;
            this.difficulyLevelMaxVal.Minimum = OperationsAndOtherUseful.MIN_LEVEL;

            this.difficulyLevelMinVal.Value = OperationsAndOtherUseful.MIN_LEVEL;
            this.difficulyLevelMaxVal.Value = OperationsAndOtherUseful.MAX_LEVEL;

            // with_already_answered_qs_checkBox
            with_already_answered_qs_checkBox.Checked = true;

            // amountOfQuestionNumericUpDown
            this.amountOfQuestionNumericUpDown.Value = 5;

            // skipFeedBackCheckBox
            skipFeedBackCheckBox.Checked = false;

            // continueButton
            this.continueButton.Enabled = false;
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
            if (settings.withAlreadyAnsweredQs == 0)
            {
                this.with_already_answered_qs_checkBox.Checked = false;
            }
            if (settings.withAlreadyAnsweredQs == 1)
            {

                this.with_already_answered_qs_checkBox.Checked = true;
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
            int withAlreadyAnsweredQs;
            if (this.with_already_answered_qs_checkBox.Checked)
            {
                withAlreadyAnsweredQs = 1;
            }
            else
            { withAlreadyAnsweredQs = 0; }
            Settings settings = new Settings{amount = amount1,seconds=seconds,minuets=minutes,withoutFeedback=withoutFeedBack,maxLevel=maxLevel,minLevel=minLevel,isExsist=1,withAlreadyAnsweredQs=withAlreadyAnsweredQs };
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
            int timeUntilTimeReset = timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second;
            if (!this.timePerQCheckbox.Checked)
                timeUntilTimeReset = OperationsAndOtherUseful.STOPER;

            /*if (!this.skipFeedBackCheckBox.Checked)
            {
                updateSettingsForNextMenu();

                BaseQuestionsPage n = new BaseQuestionsPage(amount, this.topicsList, this.skipFeedBackCheckBox.Checked,timeUntilTimeReset, difficultyLevels);
                n.Show();
                this.Close();
                return;
            }*/
            /*else
            {
                updateSettingsForNextMenu();
                WithoutFeedbackQuestions n = new WithoutFeedbackQuestions(amount, this.topicsList, timeUntilTimeReset, difficultyLevels);
                n.Show();
                this.Close();
                return;
            }*/

            string test_type = "";
            if (this.skipFeedBackCheckBox.Checked)
                test_type = "תרגול רגיל ללא משוב";
            else
                test_type = "תרגול רגיל";

            bool with_already_answered_qs = with_already_answered_qs_checkBox.Checked;

            List<dbQuestionParmeters> questions = new List<dbQuestionParmeters>();
            if (with_already_answered_qs)
                questions = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(amount, this.topicsList, difficultyLevels);

            else
                questions = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel_Without_arr_of_q_ids(amount, this.topicsList, difficultyLevels, TestHistoryFileHandler.get_list_of_all_q_ids_in_history());
            
            // no questions
            if (questions.Count == 0)
            {
                if (with_already_answered_qs)
                    MessageBox.Show("לא ניתן לפתוח תרגול, אין שאלות");
                else
                    MessageBox.Show("לא ניתן לפתוח תרגול, אין שאלות בנושאים אלה שעוד לא עשית");

                return;
            }

            LogFileHandler.writeIntoFile("Opened new questions page");

            questionsPage c;
            //get the questions here

            c = new questionsPage(questions, this.skipFeedBackCheckBox.Checked, timeUntilTimeReset, test_type); // CHANGE!!!!!42
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

        private void resetButton_Click(object sender, EventArgs e)
        {
            resetSettings();
        }

        private void amountOfQuestionText_Click(object sender, EventArgs e)
        {

        }

        private void i_timePerQ_Click(object sender, EventArgs e)
        {

        }
    }
}
