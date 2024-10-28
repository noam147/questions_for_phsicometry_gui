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
    public partial class WithoutFeedbackQuestions : BaseQuestionsPage
    {
        private int Q_BUTTON_SIZE = 30;
        private int Q_CHOSEN_BUTTON_ADD_SIZE = 10;
        private List<Button> m_buttonList = new List<Button>();
        private int m_currentIndexOfFirstButton = 0;
        public WithoutFeedbackQuestions() : base()
        {
            InitializeComponent();
        }
        public WithoutFeedbackQuestions(int amount, List<string> listOfTopics, int timePerQ, questionsDifficultyLevel difficultyLevel, string test_type)
           : base(amount, listOfTopics, true, timePerQ != OperationsAndOtherUseful.STOPER ? timePerQ * amount : OperationsAndOtherUseful.STOPER, difficultyLevel, test_type)
        {

        }
        protected override void updateAtStartOfNormalExrecizeWithoutFeedBackActions()
        {
            InitializeComponent();

            createButtons();
            displayButtons();
        }
        private void whenDoNotGetFeedBack()
        {
            this.timerLabel.Visible = true;
            //here we will init all the answer after questions without userchoice
            for (int i = 0; i < this.m_questionDetails.Count; i++)
            {
                //the defult is skipped question - will be updated as the user clicks on the option buttons
                afterQuestionParametrs af = new afterQuestionParametrs { indexOfQuestion = i, question = m_questionDetails[i], timeForAnswer = 0, userAnswer = OperationsAndOtherUseful.SKIPPED_Q };
                this.m_afterQuestionParametrs.Add(af);
            }
            updateToNextButtonQuestion(0);
        }
        protected override void whenFinishInitWebView()
        {
            base.whenFinishInitWebView();
            for (int i = 0; i < m_buttonList.Count; i++)
               m_buttonList[i].Enabled = true;
            whenDoNotGetFeedBack();

        }
        protected override void updateLabelAnswers()
        {
            this.answersTrackLabel.Text = "";
            this.questionTrackLabel.Text = $"שאלות שנענו: {m_buttonList.Count(b => b.BackColor == Color.Yellow)}/{m_maxQuestions}";
        }
        protected override void notGetFeedBackUseerActionsAfterAnswer()
        {
            if (this.m_buttonList.Count != 0)
            {
                m_buttonList[m_indexOfCurrQuestion].BackColor = Color.Yellow;
            }
                this.answer1Button.Visible = true;
            this.answer2Button.Visible = true;
            this.answer3Button.Visible = true;
            this.answer4Button.Visible = true;
            //nextQuestionButtonClick(null, null);
            //return;

            setAnswerButtonsToNormalcolor();
            markUserAnswerInLightBlue(this.m_indexOfCurrQuestion); //the curr q

            if ((m_buttonList.Any() && m_buttonList.All(b => b.BackColor == Color.Yellow)))
            {
                this.continueToQuestionButton.Text = "סיכום";
                this.continueToQuestionButton.BackColor = System.Drawing.Color.Yellow;
            }

            // if it is the last question and not all the qs are answered, dont show the next button
            if (m_indexOfCurrQuestion == this.m_questionDetails.Count - 1 && !m_buttonList.All(b => b.BackColor == Color.Yellow))
                this.continueToQuestionButton.Visible = false;
            else
                this.continueToQuestionButton.Visible = true;
        }
        protected override bool checkIfQuestionsEnded() 
        {
            return base.checkIfQuestionsEnded() || (m_buttonList.Any() && m_buttonList.All(b => b.BackColor == Color.Yellow));
        }
        private void unvisibleButtonsFromButtonList()
        {
            for (int i = 0; i < m_buttonList.Count; i++)
            {
                m_buttonList[i].Visible = false;
            }
        }

        private void createButtons()
        {
            for (int i = 0; i < m_questionDetails.Count; i++)
            {

                Button btn = new Button
                {
                    Text = $"{i + 1}",
                    Width = this.Q_BUTTON_SIZE,
                    Height = this.Q_BUTTON_SIZE,
                    Location = new System.Drawing.Point(140 + (i % 10) * 45, this.Q_BUTTON_SIZE), // Adjust spacing
                    Enabled = true,
                    Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold) // make the text BOLD
                };
                //at start user didnt answer anything
                btn.BackColor = Color.Gray;
                btn.Click += swichQuestionButton_Click;
                m_buttonList.Add(btn);
                Controls.Add(btn);
            }

        }
        private void swichQuestionButton_Click(object sender, EventArgs e)
        {

            int index = int.Parse(((Button)sender).Text) - 1;
            swichQuestionButton_Click(index);
        }
        private void setButtonsToNormalSize()
        {
            for (int i = 0; i < this.m_buttonList.Count; i++)
            {
                if (m_buttonList[i].Width != Q_BUTTON_SIZE)
                    m_buttonList[i].Location = new System.Drawing.Point(m_buttonList[i].Location.X + ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2), m_buttonList[i].Location.Y + ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2));

                m_buttonList[i].Width = Q_BUTTON_SIZE;
                m_buttonList[i].Height = Q_BUTTON_SIZE;


            }
        }
        private void nextQuestionsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_questionDetails.Count - 10; i += 10)
            {
                if (m_currentIndexOfFirstButton == i)
                {
                    displayButtons(i + 10, i + 20);
                    swichQuestionButton_Click(i + 10);
                    return;
                }
            }
        }
        private void previousQuestionsButton_Click(object sender, EventArgs e)
        {
            int maxQuestions = this.m_questionDetails.Count;

            for (int i = 10; i < maxQuestions; i += 10)
            {
                if (m_currentIndexOfFirstButton == i)
                {
                    displayButtons(i - 10, i);
                    swichQuestionButton_Click(i - 10);
                    return;
                }
            }
            return;
        }
        private void displayButtons(int startIndex, int endIndex)
        {
            m_currentIndexOfFirstButton = startIndex;
            unvisibleButtonsFromButtonList();

            // hide the nextQuestionsButton if there are no next questions
            if (endIndex >= m_buttonList.Count - 1)
            {
                nextQuestionsButton.Visible = false;
                endIndex = m_buttonList.Count;
            }
            else
                nextQuestionsButton.Visible = true;
            // hide the previousQuestionsButton if there are no previous questions
            if (startIndex == 0)
                previousQuestionsButton.Visible = false;
            else
                previousQuestionsButton.Visible = true;


            for (int i = startIndex; i < endIndex; i++)
            {
                Button btn = m_buttonList[i];
                btn.Visible = true;
                btn.BringToFront();
                //
            }
        }
        private void displayButtons()
        {
            displayButtons(0, 10);
        }

        private void updateToNextButtonQuestion(int index)
        {
            if (this.m_currentIndexOfFirstButton + 10 <= index)
            {
                displayButtons(this.m_currentIndexOfFirstButton + 10, this.m_currentIndexOfFirstButton + 20);
            }
            setButtonsToNormalSize();
            try
            {
                m_buttonList[index].Width = Q_BUTTON_SIZE + Q_CHOSEN_BUTTON_ADD_SIZE;
                m_buttonList[index].Height = Q_BUTTON_SIZE + Q_CHOSEN_BUTTON_ADD_SIZE;

                m_buttonList[index].Location = new System.Drawing.Point(m_buttonList[index].Location.X - ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2), m_buttonList[index].Location.Y - ((int)Q_CHOSEN_BUTTON_ADD_SIZE / 2));
            }
            catch (Exception e) { }
        }
        private void markUserAnswerInLightBlue(int index)
        {
            int userAnswer = -1;
            for (int i = 0; i < this.m_afterQuestionParametrs.Count; i++)
            {
                if (m_afterQuestionParametrs[i].indexOfQuestion == index)
                {
                    userAnswer = m_afterQuestionParametrs[i].userAnswer;
                    break;
                }
            }
            if (userAnswer == 1)
                answer1Button.BackColor = Color.LightBlue;
            if (userAnswer == 2)
                answer2Button.BackColor = Color.LightBlue;
            if (userAnswer == 3)
                answer3Button.BackColor = Color.LightBlue;
            if (userAnswer == 4)
                answer4Button.BackColor = Color.LightBlue;
        }
        protected override void userNotGetFeedBackWhenClickContinueActions()
        {
            this.m_indexOfCurrQuestion++;
            swichQuestionButton_Click(m_indexOfCurrQuestion);
        }
        private void swichQuestionButton_Click(int indexOfQuestion)
        {
            //displayCol(indexOfQuestion);
            base.setAnswerButtonsToNormalcolor();
            setButtonsToNormalSize();
            updateToNextButtonQuestion(indexOfQuestion);

            for (int i = 0; i <m_afterQuestionParametrs.Count; i++)
            {
                if (m_afterQuestionParametrs[i].indexOfQuestion == this.m_indexOfCurrQuestion)
                {
                    // Retrieve the element, modify it, and then assign it back, add time to current
                    var parameter = m_afterQuestionParametrs[i];                 // Retrieve the element
                    parameter.timeForAnswer += secondsTookForCurrq - timeElapsed; // add the time since the user entered the q
                    m_afterQuestionParametrs[i] = parameter;                     // Assign it back to the list
                    break;
                }
            }

            timeElapsed = secondsTookForCurrq; // save the time when the user left the curr q and enter a new one

            this.answer1Button.Visible = true;
            this.answer2Button.Visible = true;
            this.answer3Button.Visible = true;
            this.answer4Button.Visible = true;
            this.m_indexOfCurrQuestion = indexOfQuestion;



            //need to also mark the current user answer!
            string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_option_from_json(this.m_questionDetails[indexOfQuestion], OperationsAndOtherUseful.DO_NOT_MARK);
            this.webView21.NavigateToString(htmlContent);
            markUserAnswerInLightBlue(indexOfQuestion);

            if ((m_buttonList.Any() && m_buttonList.All(b => b.BackColor == Color.Yellow)))
            {
                this.continueToQuestionButton.Visible = true;
                this.continueToQuestionButton.Text = "סיכום";
                this.continueToQuestionButton.BackColor = System.Drawing.Color.Yellow;
            }
            else
                this.continueToQuestionButton.Visible = false;
        }

    }


}
