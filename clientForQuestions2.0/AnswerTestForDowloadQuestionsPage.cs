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
        private TestWithChapters chapters = new TestWithChapters();
        private List<(TextBox, Label)> answer_boxes;
        private int ANSWER_BOXES_SHOWN = 10;
        private int MARGIN_BETWEEN_ANSWER_BOXES = 50;
        private int indexOfFirstQuestion = 0;
        private int indexOfCurrChapter = 0;

        public AnswerTestForDowloadQuestionsPage(int test_id)
        {
            this.test_id = test_id;
            InitializeComponent();

            if (TestHistoryFileHandler.is_test_with_chapters(test_id))
                at_start_chapters();
            else
                questions = TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id);
            create_answer_boxes();
            displayAnswerBoxes();
        }

        private void at_start_chapters()
        {
            chapters = TestHistoryFileHandler.get_test_with_chapters(test_id);

            questions = chapters.chapters[indexOfCurrChapter].m_afterQuestionParametrs;
            //for (int i = 0; i < questions.Count; i++)
            //{
            //    afterQuestionParametrs q = questions[i];
            //    q.indexOfQuestion = i;
            //    questions[i] = q;
            //}

            chapters_comboBox.Visible = true;
            foreach (Test chapter in chapters.chapters)
            {
                chapters_comboBox.Items.Add(chapter.name);
            }
            
            chapters_comboBox.SelectedItem = chapters_comboBox.Items[indexOfCurrChapter];

            //MessageBox.Show($"n of chaps {chapters.chapters.Count}");
            //foreach (Test chapter in chapters.chapters)
            //    MessageBox.Show($"chap {chapter.name} no of q {chapter.m_afterQuestionParametrs.Count}");

        }

        private int get_number_of_question(int n_chapter, int index_question)
        {
            int num_of_previous_qs = 0;
            for (int i = 0; i < n_chapter; i++)
            {
                num_of_previous_qs += chapters.chapters[i].m_afterQuestionParametrs.Count + 1;
            }

            return index_question - num_of_previous_qs - 1;
        }

        private void create_answer_boxes()
        {
            answer_boxes = new List<(TextBox, Label)>();

            foreach (afterQuestionParametrs q in questions)
            {
                int i = get_number_of_question(indexOfCurrChapter, q.indexOfQuestion);
                // Set up the label
                Label label = new Label
                {
                    Text = $"{i+1}.",
                    Location = new System.Drawing.Point(MARGIN_BETWEEN_ANSWER_BOXES * 2 + (i % ANSWER_BOXES_SHOWN) * MARGIN_BETWEEN_ANSWER_BOXES, 50), // Position the label
                    AutoSize = true,
                    Visible = false,
                };
                this.Controls.Add(label);

                string text = "";
                if (q.userAnswer != OperationsAndOtherUseful.SKIPPED_Q)
                    text = $"{q.userAnswer}";

                // Set up the TextBox control
                TextBox textBox = new TextBox
                {
                    Location = new System.Drawing.Point(label.Location.X, label.Height + label.Location.Y + 5), // Position the TextBox
                    Name = $"{i+1}",
                    Visible = false,
                    Enabled = true,
                    Text = text,
                    Width = 25
                };

                // Subscribe to ValueChanged event
                textBox.TextChanged += TextBox_TextChanged;
                textBox.KeyDown += textBox_KeyDown;
                textBox.Enter += textBox_Enter;

                this.Controls.Add(textBox);

                answer_boxes.Add((textBox, label));
            }

            previousQuestionsButton.Location = new Point(MARGIN_BETWEEN_ANSWER_BOXES, answer_boxes[0].Item2.Location.Y);
            nextQuestionsButton.Location = new Point((ANSWER_BOXES_SHOWN + 2) * MARGIN_BETWEEN_ANSWER_BOXES, answer_boxes[0].Item2.Location.Y);

        }
        private void delete_answer_boxes()
        {
            if (answer_boxes == null)
                return;
            foreach ( (TextBox textBox, Label label) in answer_boxes) {
                this.Controls.Remove(textBox);
                this.Controls.Remove(label);
            }
            answer_boxes = new List<(TextBox, Label)>();
        }
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = ((TextBox)sender);

            int question_num = 0;
            try
            {
                question_num = int.Parse(textBox.Name);
            }
            catch { }

            // Allow only 0-4 as inputs, and empty str if the user just deleted their answer
            if (!(new List<string> { "0", "1", "2", "3", "4" }).Contains(textBox.Text))
            {
                if (textBox.Text.Length == 1)
                {
                    int previous_answer = questions[question_num - 1].userAnswer != OperationsAndOtherUseful.SKIPPED_Q ? questions[question_num - 1].userAnswer : 0;
                    textBox.Text = previous_answer == 0 ? "" : $"{previous_answer}";
                }
                else if (textBox.Text.Length == 2)
                {
                    int previous_answer = questions[question_num - 1].userAnswer != OperationsAndOtherUseful.SKIPPED_Q ? questions[question_num - 1].userAnswer : 0;
                    string current_answer = textBox.Text.EndsWith($"{previous_answer}") ? textBox.Text[0].ToString() : textBox.Text[1].ToString();
                    // if new answer isn't legall
                    if (!(new List<string> { "0", "1", "2", "3", "4" }).Contains(current_answer))
                    {
                        textBox.Text = $"{previous_answer}";
                    }
                    else
                        textBox.Text = current_answer;
                }
            }

            int userAnswer;
            if (textBox.Text == "")
                userAnswer = 0;
            else
                userAnswer = int.Parse(textBox.Text);

            afterQuestionParametrs q = questions[question_num - 1];
            if (userAnswer == 0)
                userAnswer = OperationsAndOtherUseful.SKIPPED_Q;
            q.userAnswer = userAnswer;
            questions[question_num - 1] = q;

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

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            int question_num = 0;
            try
            {
                question_num = int.Parse(((TextBox)sender).Name);
            }
            catch { }

            // Check if the Enter key was pressed
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Right)
            {
                if (question_num != answer_boxes.Count)
                {
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

                e.SuppressKeyPress = true; // Prevents the 'ding' sound on Enter
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (question_num != 1)
                {
                    if ((question_num-1) % ANSWER_BOXES_SHOWN == 0)
                    {
                        displayAnswerBoxes(question_num - 1 - ANSWER_BOXES_SHOWN, question_num - 1);
                    }
                    // Move focus to the next TextBox when the value changes
                    answer_boxes[question_num-2].Item1.Focus();

                    e.SuppressKeyPress = true; // Prevents the default action (if any)
                }
            }
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll(); // Selects all text when the TextBox gains focus
        }

        private void displayAnswerBoxes()
        {
            displayAnswerBoxes(this.indexOfFirstQuestion, this.indexOfFirstQuestion + ANSWER_BOXES_SHOWN);
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

            for (int i = startIndex; i < Math.Min(endIndex, answer_boxes.Count); i++)
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
            if (chapters.chapters == null || chapters.chapters.Count == 0)
                save_answers();
            else
                save_answers_of_chapters();

            MessageBox.Show("התשובות התעדכנו");

            this.Close();
        }

        private void save_answers()
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


            // saving
            for (int i = 0; i < questions.Count; i++)
            {
                afterQuestionParametrs q = questions[i];
                if (!(new List<int> { 1, 2, 3, 4, OperationsAndOtherUseful.SKIPPED_Q }).Contains(q.userAnswer))
                    q.userAnswer = OperationsAndOtherUseful.SKIPPED_Q;
                TestHistoryFileHandler.set_question_UserAnswer(q.userAnswer, test_id, q.indexOfQuestion);
            }

        }

        private void save_answers_of_chapters()
        {

            for (int i = 0; i < chapters.chapters[indexOfCurrChapter].m_afterQuestionParametrs.Count; i++)
            {
                afterQuestionParametrs q = chapters.chapters[indexOfCurrChapter].m_afterQuestionParametrs[i];
                q.userAnswer = questions[i].userAnswer;
                chapters.chapters[indexOfCurrChapter].m_afterQuestionParametrs[i] = q;
            }

            foreach (Test chapter in chapters.chapters)
                for (int i = 0; i < chapter.m_afterQuestionParametrs.Count; i++)
                {
                    afterQuestionParametrs q = chapter.m_afterQuestionParametrs[i];
                    if (!(new List<int> { 1, 2, 3, 4, OperationsAndOtherUseful.SKIPPED_Q }).Contains(q.userAnswer))
                        q.userAnswer = OperationsAndOtherUseful.SKIPPED_Q;
                    TestHistoryFileHandler.set_question_UserAnswer(q.userAnswer, test_id, q.indexOfQuestion);
                }
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

        private void chapters_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int new_chapter_index = chapters_comboBox.SelectedIndex;
            if (new_chapter_index == indexOfCurrChapter)
                return;

            for (int i = 0; i < chapters.chapters[indexOfCurrChapter].m_afterQuestionParametrs.Count; i++)
            {
                afterQuestionParametrs q = chapters.chapters[indexOfCurrChapter].m_afterQuestionParametrs[i];
                q.userAnswer = questions[i].userAnswer;
                chapters.chapters[indexOfCurrChapter].m_afterQuestionParametrs[i] = q;
            }


            questions = chapters.chapters[new_chapter_index].m_afterQuestionParametrs;
            //for (int i = 0; i < questions.Count; i++)
            //{
            //    afterQuestionParametrs q = questions[i];
            //    q.indexOfQuestion = i;
            //    questions[i] = q;
            //}

            this.indexOfFirstQuestion = 0;
            indexOfCurrChapter = new_chapter_index;

            delete_answer_boxes();
            create_answer_boxes();
            displayAnswerBoxes();
        }
    }
}
