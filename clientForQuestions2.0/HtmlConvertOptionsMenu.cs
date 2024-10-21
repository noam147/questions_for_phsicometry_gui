using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;


namespace clientForQuestions2._0
{
    public partial class HtmlConvertOptionsMenu : Form
    {
        private string file_path;
        private bool autoDownload = false;
        private List<dbQuestionParmeters> questions;
        private string finalHtmlContentForFile = "";
        private string htmlContentOfAnswers = "<head> <style>body { font-weight: normal !important; }</style></head><body dir=\"rtl\">";

        private List<int> listOfPreviousQuestionsId = new List<int>();
        public HtmlConvertOptionsMenu(int test_id)
        {
            questions = new List<dbQuestionParmeters>();
            foreach (afterQuestionParametrs a in TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id))
                questions.Add(a.question);
            InitializeComponent();
            explanation_comboBox.SelectedIndex = 0;
        }
        

        public HtmlConvertOptionsMenu()
        {
            string finalSimulation = "";
            string currentChapter = "";
            InitializeComponent();
            this.questions = new List<dbQuestionParmeters>();
            explanation_comboBox.SelectedIndex = 0;
            string newPage = "<div style='page-break-after: always;'></div>";
            for (int i =0; i < 5;i++)
            {
                var multipleChaptersQuestions = OperationsAndOtherUseful.sendChapter_math_Questions_without_graph(this.listOfPreviousQuestionsId);
                foreach(var question in multipleChaptersQuestions)
                {
                    this.questions.Add(question);
                    this.listOfPreviousQuestionsId.Add(question.questionId);
                }
                this.htmlContentOfAnswers += "<br><br>" + "כמותי " + (i+1)+":\n";//add lines to separate diffrentchapters
                currentChapter = get_html(true);
                questions.Clear();
                finalSimulation += $"<h4>test number {i + 1}</h4>";
                finalSimulation += currentChapter + newPage;
            }
            finalHtmlContentForFile = finalSimulation;
            filePath_button_Click(null, null);

        }

        public HtmlConvertOptionsMenu(List<dbQuestionParmeters> questions, bool autoDownload)
        {
            this.questions = questions;
            InitializeComponent();
            explanation_comboBox.SelectedIndex = 0;
            this.autoDownload = autoDownload;
        }
        private string getGeneralCategory(string category)
        {
            if(category == "אנלוגיות")
            {
                return "מילולי";
            }
            if(category == "Sentence Completions")
            {
                return "אנגלית";
            }
            return "כמותי";
        }
        private void action_when_get_list_of_chapters(List<List<dbQuestionParmeters>> multipleQuestionsfiles)
        {
            string finalSimulation = "";
            string currentChapter = "";
            InitializeComponent();
            explanation_comboBox.SelectedIndex = 0;
            string newPage = "<div style='page-break-after: always;'></div>";
            for (int i = 0; i < multipleQuestionsfiles.Count; i++)
            {
                this.questions = multipleQuestionsfiles[i];
                string currentCategory = questions[0].category;
                string generalCategory = getGeneralCategory(currentCategory);
                this.htmlContentOfAnswers += "<br><br>" + generalCategory + ":\n";//add lines to separate diffrentchapters
                currentChapter = get_html(true);
                
                finalSimulation += currentChapter + newPage;
            }
            finalHtmlContentForFile = finalSimulation;
            filePath_button_Click(null, null);

        }
        public HtmlConvertOptionsMenu(List<List<dbQuestionParmeters>> multipleQuestionsfiles)
        {
            action_when_get_list_of_chapters(multipleQuestionsfiles);

        }
        private void HtmlConvertOptionsMenu_Load(object sender, EventArgs e)
        {

        }

        private void filePath_button_Click(object sender, EventArgs e)
        {

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Title = "Save Output File";
                saveFileDialog.Filter = "Html Files (*.html)|*.html";
                saveFileDialog.DefaultExt = "html"; // Default file extension
                saveFileDialog.FileName = $"test";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Initial directory

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // The user selected a file location
                    this.file_path = saveFileDialog.FileName;

                    save_button_Click();
                    MessageBox.Show($"File save to: '{this.file_path}'");
                    this.Close();
                }
            }
        }
        private string addNumberToQuestion(string htmlContentOfQuestion, int counter,int questionId)
        {
            int currIndex = htmlContentOfQuestion.IndexOf("<p>");
            string numberStr = $"qId = {questionId}<br><span style='font-size: 1.5em; font-weight: bold;'>{counter}.</span>\t";

            if (currIndex == -1)
            {
                currIndex = htmlContentOfQuestion.IndexOf("<p ");
                int secondsIndex = htmlContentOfQuestion.IndexOf(">");//find the closing tag of p
                htmlContentOfQuestion = $"<p>(" + numberStr + ")\t" + htmlContentOfQuestion.Substring(secondsIndex + 1, htmlContentOfQuestion.Length - (secondsIndex + 1));
            }
            else
            {
                htmlContentOfQuestion = htmlContentOfQuestion.Substring(0, currIndex + 3) + numberStr + htmlContentOfQuestion.Substring(currIndex + 3);
            }
            return htmlContentOfQuestion;
        }
        private string addNumberToQuestion(string htmlContentOfQuestion,int counter)
        {
            return addNumberToQuestion(htmlContentOfQuestion, counter, -1);
        }
        private void add_html_content_to_answers(int index,int answer)
        {
            this.htmlContentOfAnswers += "("+(index+1)+ ")." + " " + answer + "\n";
        }
        private string get_html(bool isNum)
        {
            //isnum: i have no idea
            //expel: i have no idea
            string html = "";
             html = @"
<style>
    .question-container { page-break-inside: avoid; }
    h1 { font-size: 14px; } /* Reducing the font size for all h1 elements */
    p { font-size: 12px; }  /* Reducing the font size for all paragraph elements */
    mjx-mspace[linebreak=""newline""] { display: block; height: 0; }
</style>";
            string html_end = html;

            //expel = 0
            //bool isNum = isNum_checkBox.Checked;
            int selected_explanationsOption_index = explanation_comboBox.SelectedIndex; // index of the selected option

            int prev_col_id = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                html += $"<div class='question-container'>";
                html_end += $"<div class='question-container'>";

                dbQuestionParmeters a = questions[i];
                add_html_content_to_answers(i, a.rightAnswer);
                int curr_col_id = OperationsAndOtherUseful.get_col_id_of_question(a.json_content);
                
                if (curr_col_id != 0 && curr_col_id != prev_col_id)
                {
                    html += OperationsAndOtherUseful.get_string_of_img_col_html(a.json_content);
                    //html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: lightgray;\"></div>";
                }
                string currentQuestion = "";
                string currentQuestion_end = "";

                if (selected_explanationsOption_index==1)
                {
                    currentQuestion += OperationsAndOtherUseful.get_string_of_question_and_option_from_json(a, OperationsAndOtherUseful.DO_NOT_MARK);
                    currentQuestion_end += OperationsAndOtherUseful.get_explanation(a);

                    /*html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                    html += "<br> <br> ";
                    html_end += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                    html_end += "<br> <br> ";*/
                }
                else if(selected_explanationsOption_index==2)
                {
                    currentQuestion += OperationsAndOtherUseful.get_string_of_question_and_explanation(a, OperationsAndOtherUseful.DO_NOT_MARK);
                   // html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                   // html += "<br> <br> ";
                }
                else if (selected_explanationsOption_index == 0)
                {
                    currentQuestion = OperationsAndOtherUseful.get_string_of_question_and_option_from_json(a, OperationsAndOtherUseful.DO_NOT_MARK);
                    //html += OperationsAndOtherUseful.get_string_of_question_and_option_from_json(a, OperationsAndOtherUseful.DO_NOT_MARK);


                    // html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                    //html += "<br> <br> ";
                }

                if (isNum)
                {
                    html += addNumberToQuestion(currentQuestion, i + 1, a.questionId); ;
                    if (selected_explanationsOption_index == 1)
                        html_end += addNumberToQuestion(currentQuestion_end, i + 1, a.questionId); ;
                }
                else
                {
                    html += currentQuestion;
                    html_end += currentQuestion_end;
                }

                html += "<br><br></div>";
                html_end += "<br><br></div>";

                prev_col_id = curr_col_id;
            }

            if (selected_explanationsOption_index == 1)
            {
                html += "<div style='page-break-after: always;'></div><p style=\"font-size: 24px; font-weight: bold; direction: rtl;\">הסברים ותשובות:</p>";
                html += "<br> <div style=\"top: 50%; left: 0; width: 100vw; height: 5px; background-color: lightgray;\"></div><br> <br> " + html_end;
            }

            //for big imgs
            html = html.Replace("height:auto;", "height:500;");

            // fix the problem with newlines in math ml
            html = fix_newline_mathml(html);

            return html;            
        }

        public static string fix_newline_mathml(string html)
        {
            // the mathml newline
            string new_line_mathml = "linebreak=\"newline\"";
            string new_html = html;

            if (new_html.Contains(new_line_mathml))
            {
                // get the whole mathml newline element (e.g.: '<mspace linebreak="newline"></mspace>')
                int i = new_html.IndexOf(new_line_mathml) - 1;
                string start_element = "";
                while (i >= 0 && new_html[i + 1] != '<')
                {
                    start_element = new_html[i] + start_element;
                    i--;
                }

                int count = 0;
                i = new_html.IndexOf(new_line_mathml) + new_line_mathml.Length;
                string close_element = "";
                while (i >= 0 && count != 2)
                {
                    close_element = close_element + new_html[i];
                    if (new_html[i] == '>')
                        count++;
                    i++;
                }

                if (close_element.Contains(start_element.Replace("<", "").Replace(">", "").Replace(" ", "").Replace("\n", "")))
                    new_line_mathml = start_element + new_line_mathml + close_element;
                else // if the element is <mspace linebreak="newline"> and not <mspace linebreak="newline"></mspace>
                    new_line_mathml = start_element + new_line_mathml + close_element.Substring(0, close_element.IndexOf(">")+1);

                LogFileHandler.writeIntoFile(new_line_mathml);
                List<string> openTags = new List<string>();
                List<string> closeTags = new List<string>();
                i = new_html.IndexOf(new_line_mathml) - 1;

                List<string> already_closedTags = new List<string>();

                // Loop to find all opening tags up to the <mjx-container> or the <math>
                while (true)
                {
                    if (new_html[i] == '>')
                    {
                        // Extract the opening tag
                        string openString = "";

                        // Read backwards to get the entire opening tag
                        while (i >= 0 && new_html[i] != '<')
                        {
                            openString = new_html[i] + openString;
                            i--;
                        }
                        openString = "<" + openString;

                        // if it is another element in the tree that is closed closed, ignore it
                        if (openString.StartsWith("</"))
                        {
                            already_closedTags.Add(openString);
                            continue;
                        }

                        // if there are options (aka src="...", style="..." ...) ignore them for the close element string
                        string without_options = openString.Substring(0, openString.IndexOf(" ") + 1) + ">";
                        if (without_options.Length < 2)
                            without_options = openString;

                        // Generate the corresponding closing tag for the close string
                        string closeString = without_options.Replace("<", "</");

                        // if it is another element in the tree that is closed closed, ignore it
                        if (already_closedTags.Contains(closeString))
                        {
                            already_closedTags.Remove(closeString);
                            continue;
                        }

                        // add to the lists
                        openTags.Add(openString);
                        closeTags.Add(closeString);

                        // Break if we find the closing tag for <mjx-container> or <math>
                        if (openString.Contains("mjx-container") || openString.Contains("math"))
                            break;
                    }
                    i--;
                    // Check to prevent index out of bounds
                    if (i < 0) break;
                }

                closeTags.Reverse();
                openTags.Reverse();

                // Replace the <mspace> with the closing tags, <br>, and reopening tags
                new_html = new_html.Substring(0, new_html.IndexOf(new_line_mathml))
                    + string.Join("", closeTags) // Close tags in reverse order
                    + "<br>"
                    + string.Join("", openTags) // Open tags in normal order
                    + new_html.Substring(new_html.IndexOf(new_line_mathml) + new_line_mathml.Length);
            }


            // to edit all new_lines
            if (new_html == html)
                return new_html;
            return fix_newline_mathml(new_html);
        }

        private void save_button_Click()
        {
            try
            {
                // Write the HTML content to the file
                if(finalHtmlContentForFile == "")
                    {
                    File.WriteAllText(this.file_path, get_html(this.isNum_checkBox.Checked));
                }
                else { File.WriteAllText(this.file_path,finalHtmlContentForFile); }
                string answers_filePath = this.file_path.Insert(this.file_path.LastIndexOf(".html"), "_answers");
                File.WriteAllText(answers_filePath,this.htmlContentOfAnswers);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"error {ex}");
            }

        }
    }
}
