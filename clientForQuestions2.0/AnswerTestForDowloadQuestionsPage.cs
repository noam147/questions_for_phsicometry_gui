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
    public partial class AnswerTestForDowloadQuestionsPage : Form
    {
        private int test_id;

        private List<afterQuestionParametrs> questions;
        private List<(TextBox, Label)> answer_boxes;
        private int ANSWER_BOXES_SHOWN = 10;
        private int MARGIN_BETWEEN_ANSWER_BOXES = 50;
        private int indexOfFirstQuestion = 0;

        public AnswerTestForDowloadQuestionsPage(int test_id)
        {
            this.test_id = test_id;
            questions = TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id);
            InitializeComponent();
            create_answer_boxes();
            displayAnswerBoxes();
        }
        private void create_answer_boxes()
        {
            answer_boxes = new List<(TextBox, Label)>();

            foreach (afterQuestionParametrs q in questions)
            {
                int i = q.indexOfQuestion;
                // Set up the label
                Label label = new Label
                {
                    Text = $"{i+1}.",
                    Location = new System.Drawing.Point(MARGIN_BETWEEN_ANSWER_BOXES * 2 + (i % ANSWER_BOXES_SHOWN) * MARGIN_BETWEEN_ANSWER_BOXES, 50), // Position the label
                    AutoSize = true
                };
                this.Controls.Add(label);

                string text = "";
                if (q.userAnswer != OperationsAndOtherUseful.SKIPPED_Q)
                    text = $"{q.userAnswer + 1}";

                // Set up the TextBox control
                TextBox textBox = new TextBox
                {
                    Location = new System.Drawing.Point(label.Location.X, label.Height + label.Location.Y + 5), // Position the TextBox
                    Name = $"{i + 1}",
                    Visible = false,
                    Enabled = true,
                    Text = text,
                    Width = 25
                };

                // Subscribe to ValueChanged event
                textBox.TextChanged += TextBox_TextChanged;

                this.Controls.Add(textBox);

                answer_boxes.Add((textBox, label));
            }

            previousQuestionsButton.Location = new Point(MARGIN_BETWEEN_ANSWER_BOXES, answer_boxes[0].Item2.Location.Y);
            nextQuestionsButton.Location = new Point((ANSWER_BOXES_SHOWN + 2) * MARGIN_BETWEEN_ANSWER_BOXES, answer_boxes[0].Item2.Location.Y);

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = ((TextBox)sender);

            // the user just deleted their answer
            if (textBox.Text == "")
                return;

            // Allow only 0-4 as inputs
            if (!(new List<string> { "0", "1", "2", "3", "4" }).Contains(textBox.Text))
            {
                textBox.Text = textBox.Text[0].ToString();
                if (!(new List<string> { "0", "1", "2", "3", "4" }).Contains(textBox.Text))
                    textBox.Text = "";
                return;
            }

            int question_num = 0;
            try
            {
                question_num = int.Parse(textBox.Name);
            }
            catch {  }

            if (question_num != answer_boxes.Count) { 
                if (question_num % ANSWER_BOXES_SHOWN == 0)
                {
                    displayAnswerBoxes(question_num, question_num + ANSWER_BOXES_SHOWN);
                }
                else
                {
                    // Move focus to the next TextBox when the value changes
                    answer_boxes[question_num].Item1.Focus();
                }
            }

        }

        private void displayAnswerBoxes()
        {
            displayAnswerBoxes(((int)this.indexOfFirstQuestion / ANSWER_BOXES_SHOWN) * ANSWER_BOXES_SHOWN, ((int)this.indexOfFirstQuestion / ANSWER_BOXES_SHOWN) * ANSWER_BOXES_SHOWN + ANSWER_BOXES_SHOWN);
        }
        private void displayAnswerBoxes(int startIndex, int endIndex)
        {
            if (startIndex < 0)
                return;
            this.indexOfFirstQuestion = startIndex;
            unvisibleButtonsFromAnswerBoxes();

            // hide the nextQuestionsButton if there are no next questions
            if (endIndex >= answer_boxes.Count - 1)
            {
                nextQuestionsButton.Visible = false;
                endIndex = answer_boxes.Count;
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
                (TextBox textBox, Label label) = answer_boxes[i];
                textBox.Visible = true;
                label.Visible = true;
                textBox.BringToFront();
                label.BringToFront();
            }

            // Move focus to the next TextBox when the value changes
            answer_boxes[this.indexOfFirstQuestion].Item1.Focus();

        }

        private void unvisibleButtonsFromAnswerBoxes()
        {
            foreach ((TextBox textBox, Label label) in answer_boxes)
            {
                textBox.Visible = false;
                label.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<int> not_filled_qs = new List<int>(); // a list for questions without text
            foreach ((TextBox textBox, Label label) in answer_boxes)
                if (!(new List<string> { "0", "1", "2", "3", "4" }).Contains(textBox.Text))
                    not_filled_qs.Add(int.Parse(textBox.Name));

            // if not all answers are filled, ask the user if he is sure he wants to continue
            if (not_filled_qs.Count > 0)
            {
                string q_nums = "";
                for (int i = 0; i < Math.Min(not_filled_qs.Count, 5); i++)
                    q_nums += $"{not_filled_qs[i]}, ";
                q_nums = q_nums.Substring(0, q_nums.Length - 2);
                if (not_filled_qs.Count > 5)
                    q_nums += ", ועוד...";

                // check if the user is sure to reset history
                DialogResult result = MessageBox.Show("האם אתה בטוח שברצונך להגיש את התשובות?\nשאלות " + q_nums + " עדיין לא מולאו",
                                          "Confirmation",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Question);
                if (result == DialogResult.No) // the user isn't sure
                    return;
            }

            foreach ((TextBox textBox, Label label) in answer_boxes)
            {
                int indexOfQuestion = int.Parse(textBox.Name) - 1;

                int userAnswer;
                if (!(new List<string> { "0", "1", "2", "3", "4" }).Contains(textBox.Text))
                    userAnswer = OperationsAndOtherUseful.SKIPPED_Q;
                else if (textBox.Text == "0")
                    userAnswer = OperationsAndOtherUseful.SKIPPED_Q;
                else
                    userAnswer = int.Parse(textBox.Text);

                TestHistoryFileHandler.set_question_UserAnswer(userAnswer, test_id, indexOfQuestion);
            }

            MessageBox.Show("התשובות התעדכנו");

            this.Close();
        }

        private void nextQuestionsButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < answer_boxes.Count - ANSWER_BOXES_SHOWN; i += ANSWER_BOXES_SHOWN)
            {
                if (indexOfFirstQuestion == i)
                {
                    displayAnswerBoxes(i + ANSWER_BOXES_SHOWN, i + ANSWER_BOXES_SHOWN * 2);
                    return;
                }
            }
        }

        private void previousQuestionsButton_Click(object sender, EventArgs e)
        {
            int maxQuestions = this.answer_boxes.Count;

            for (int i = ANSWER_BOXES_SHOWN; i < maxQuestions; i += ANSWER_BOXES_SHOWN)
            {
                if (indexOfFirstQuestion == i)
                {
                    displayAnswerBoxes(i - ANSWER_BOXES_SHOWN, i);
                    return;
                }
            }
            return;
        }
    }
}
