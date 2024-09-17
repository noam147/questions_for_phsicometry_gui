using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class chaptersQuestionsMenu : Form
    {
        Random random = new Random();

        private String chosen_chapter = "";

        public chaptersQuestionsMenu()
        {
            InitializeComponent();
        }

        private void timePerQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.timePerQPicker.Enabled = ((CheckBox)sender).Checked;
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            menuPage c = new menuPage();

            c.Show();
            this.Close();
        }

        private void chapterButton_Click(object sender, EventArgs e)
        {
            this.continueButton.Enabled = true;
            this.chosen_chapter = ((Button)sender).Text.ToString();

            this.chapterButton1.BackColor = Color.White;
            this.chapterButton2.BackColor = Color.White;
            this.chapterButton3.BackColor = Color.White;

            ((Button)sender).BackColor = Color.LightBlue; // show which category is chosen
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            switch(chosen_chapter) {
                case "חשיבה מילולית":
                    sendChapter1_Questions(); 
                    break;
                case "חשיבה כמותית":
                    sendChapter2_Questions();
                    break;
                case "אנגלית":
                    sendChapter3_Questions(); 
                    break;
                default:
                    return; // no chapter is selected
            }
        }

        // חשיבה מילולית
        private void sendChapter1_Questions()
        {
            //get אנלוגיות
            List<dbQuestionParmeters> anlog_qs = sqlDb.get_n_questions_from_specofic_category(6, "אנלוגיות"); // qs of normal qs
            anlog_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get הבנה והסקה
            List<dbQuestionParmeters> havana_qs = sqlDb.get_n_questions_from_arr_of_categorys(11, OperationsAndOtherUseful.topicsdict["הבנה והסקה"]); // qs of normal qs
            havana_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get קטע קריאה
            List<int> colIds = OperationsAndOtherUseful.title2colIds["קטע קריאה פרקים"]; // get all ids of קטע קריאה
            int col_id = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
                // qs of קטע קריאה
            List<dbQuestionParmeters> col_qs = new List<dbQuestionParmeters>();
            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id])
            {
                col_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }
            anlog_qs.AddRange(havana_qs);
            anlog_qs.AddRange(col_qs);

            questionsPage c;
            if (this.timePerQPicker.Enabled)
                c = new questionsPage(anlog_qs, timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second);
            else
                c = new questionsPage(anlog_qs, 20 * 60);

            c.Show();
            this.Close();

        }

        // חשיבה כמותית
        private void sendChapter2_Questions()
        {
            List<String> topics = OperationsAndOtherUseful.topicsdict["חשיבה כמותית"];

            List<int> colIds = OperationsAndOtherUseful.title2colIds["הסקה מתרשים"]; // get all ids of tarshim
            int col_id = colIds[random.Next(colIds.Count)]; // choose rand collection of the category

            // qs of tarshim
            List<dbQuestionParmeters> col_qs = new List<dbQuestionParmeters>();
            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id])
            {
                col_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }

            List<dbQuestionParmeters> normal_qs = sqlDb.get_n_questions_from_arr_of_categorys(20 - col_qs.Count, topics); // qs of normal qs

            // sort by "difficulty_level", from easiest to hardest
            normal_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // difficulty_level of the tarshim to determine if it would be at the start, middle or end. 
            float col_diffLvl = (float) col_qs[0].json_content["collections"][0]["difficulty_level"];

            List<dbQuestionParmeters> questions = new List<dbQuestionParmeters>();

            // add to start
            if (col_diffLvl >= 0 && col_diffLvl <= 4)
            {
                col_qs.AddRange(normal_qs);
                questions = col_qs;
            }
            // add to middle
            if (col_diffLvl > 4 && col_diffLvl <= 7)
            {
                normal_qs.InsertRange(8, col_qs); //after the 8th element
                questions = normal_qs;
            }
            // add to end
            if (col_diffLvl > 7 && col_diffLvl <= 10)
            {
                normal_qs.AddRange(col_qs);
                questions = normal_qs;
            }

            questionsPage c;
            if (this.timePerQPicker.Enabled)
                c = new questionsPage(questions, timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second);
            else
                c = new questionsPage(questions, 20 * 60);

            c.Show();
            this.Close();
        }

        //אנגלית
        private void sendChapter3_Questions()
        {
            //get Sentence Completions
            List<dbQuestionParmeters> Sentence_Completions_qs = sqlDb.get_n_questions_from_specofic_category(8, "Sentence Completions"); // qs of normal qs
            Sentence_Completions_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get Restatements
            List<dbQuestionParmeters> Restatements_qs = sqlDb.get_n_questions_from_specofic_category(4, "Restatements"); // qs of normal qs
            Restatements_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get קטע קריאה
            List<int> colIds = OperationsAndOtherUseful.title2colIds["Reading Comprehension"]; // get all ids of קטע קריאה
            int col_id1 = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
            int col_id2 = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
            while (col_id1 == col_id2) // so there will be different texts
                col_id2 = colIds[random.Next(colIds.Count)]; // choose rand collection of the category

            // qs of קטע קריאה
            List<dbQuestionParmeters> col1_qs = new List<dbQuestionParmeters>();
            List<dbQuestionParmeters> col2_qs = new List<dbQuestionParmeters>();

            LogFileHandler.writeIntoFile($"try to accsess col ids, id1: {col_id1}; id2: {col_id2}");

            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id1])
            {
                col1_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }
            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id2]) // TODO the easier text first
            {
                col2_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }


            Sentence_Completions_qs.AddRange(Restatements_qs);

            // if col2 is harder than col1
            if ((float)col2_qs[0].json_content["collections"][0]["difficulty_level"] > (float)col1_qs[0].json_content["collections"][0]["difficulty_level"])
            {
                for (int i = 0; i < col1_qs.Count; i++)
                    col1_qs[i].json_content["collections"][0]["cover"] += "<p style=\"font-size: 18px; font-style: italic;\">Text I</p>";

                for (int i = 0; i < col2_qs.Count; i++)
                    col2_qs[i].json_content["collections"][0]["cover"] += "<p style=\"font-size: 18px; font-style: italic;\">Text II</p>";

                Sentence_Completions_qs.AddRange(col1_qs);
                Sentence_Completions_qs.AddRange(col2_qs);
            }
            // if col1 is harder than col2
            else
            {
                for (int i = 0; i < col2_qs.Count; i++)
                    col2_qs[i].json_content["collections"][0]["cover"] += "<p style=\"font-size: 18px; font-style: italic;\">Text I</p>";

                for (int i = 0; i < col1_qs.Count; i++)
                    col1_qs[i].json_content["collections"][0]["cover"] += "<p style=\"font-size: 18px; font-style: italic;\">Text II</p>";

                Sentence_Completions_qs.AddRange(col2_qs);
                Sentence_Completions_qs.AddRange(col1_qs);
            }


            questionsPage c;
            if (this.timePerQPicker.Enabled)
                c = new questionsPage(Sentence_Completions_qs, timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second);
            else
                c = new questionsPage(Sentence_Completions_qs, 20 * 60);

            c.Show();
            this.Close();

        }

    }
}
