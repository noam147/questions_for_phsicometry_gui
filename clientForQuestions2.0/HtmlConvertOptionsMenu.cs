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

namespace clientForQuestions2._0
{
    public partial class HtmlConvertOptionsMenu : Form
    {
        private string file_path;
        private bool autoDownload = false;
        private List<dbQuestionParmeters> questions;
        private string finalHtmlContentForFile = "";
        public HtmlConvertOptionsMenu(int test_id)
        {
            questions = new List<dbQuestionParmeters>();
            foreach (afterQuestionParametrs a in TestHistoryFileHandler.get_afterQuestionParametrs_of_test(test_id))
                questions.Add(a.question);
            InitializeComponent();
            explanation_comboBox.SelectedIndex = 0;
        }
        public HtmlConvertOptionsMenu(List<dbQuestionParmeters> questions, bool autoDownload)
        {
            this.questions = questions;
            InitializeComponent();
            explanation_comboBox.SelectedIndex = 0;
            this.autoDownload = autoDownload;
        }
        public HtmlConvertOptionsMenu(List<List<dbQuestionParmeters>> multipleQuestionsfiles)
        {
            string finalSimulation = "";
            string currentChapter = "";
            InitializeComponent();
            explanation_comboBox.SelectedIndex = 0;
            string newPage = "<div style='page-break-after: always;'></div>";
            for (int i =0; i < multipleQuestionsfiles.Count; i++) 
            {
                this.questions = multipleQuestionsfiles[i];
                currentChapter =  get_html();
                finalSimulation += currentChapter + newPage;
            }
            finalHtmlContentForFile = finalSimulation;
            filePath_button_Click(null, null);

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
                saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
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
        private string addNumberToQuestion(string htmlContentOfQuestion,int counter)
        {
            int currIndex = htmlContentOfQuestion.IndexOf("<p>");
            string numberStr = $"<span style='font-size: 1.5em; font-weight: bold;'>{counter}.</span>\t";
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
        private string get_html()
        {
            string html = "";
             html = @"
<style>
    .question-container { page-break-inside: avoid; }
    h1 { font-size: 14px; } /* Reducing the font size for all h1 elements */
    p { font-size: 12px; }  /* Reducing the font size for all paragraph elements */
</style>";
        string html_end = "";

            //expel = 0
            bool isNum = isNum_checkBox.Checked;
            int expl = explanation_comboBox.SelectedIndex;

            int prev_col_id = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                html += $"<div class='question-container'>";
                dbQuestionParmeters a = questions[i];
                
                int curr_col_id = OperationsAndOtherUseful.get_col_id_of_question(a.json_content);
                
                if (curr_col_id != 0 && curr_col_id != prev_col_id)
                {
                    html += OperationsAndOtherUseful.get_string_of_img_col_html(a.json_content);
                    html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: lightgray;\"></div>";
                    html += "<br> <br> ";
                }
                if (isNum)
                {
                    //html += $"<p style=\"font-size: 24px; font-weight: bold; direction: rtl;\">{a.indexOfQuestion + 1}.</p>";
                    if (expl==1)
                        html_end += $"<p style=\"font-size: 24px; font-weight: bold; direction: rtl;\">{i + 1}.</p>";

                }
                if (expl==1)
                {
                    html += OperationsAndOtherUseful.get_string_of_question_and_option_from_json(a, OperationsAndOtherUseful.DO_NOT_MARK);
                    html_end += OperationsAndOtherUseful.get_explanation(a);

                    /*html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                    html += "<br> <br> ";
                    html_end += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                    html_end += "<br> <br> ";*/
                }
                else if(expl==2)
                {
                    html += OperationsAndOtherUseful.get_string_of_question_and_explanation(a, OperationsAndOtherUseful.DO_NOT_MARK);
                   // html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                   // html += "<br> <br> ";
                }
                else if (expl == 0)
                {
                    string currentQuestion = OperationsAndOtherUseful.get_string_of_question_and_option_from_json(a, OperationsAndOtherUseful.DO_NOT_MARK);
                    html += addNumberToQuestion(currentQuestion,i+1);
                    //html += OperationsAndOtherUseful.get_string_of_question_and_option_from_json(a, OperationsAndOtherUseful.DO_NOT_MARK);


                    // html += "<div style=\"top: 50%; left: 0; width: 100vw; height: 3px; background-color: black;\"></div>";
                    //html += "<br> <br> ";
                }
                html += $"</div>";
                prev_col_id = curr_col_id;
            }

            if (expl == 1)
            {
                html += "<p style=\"font-size: 24px; font-weight: bold; direction: rtl;\">הסברים ותשובות:</p>";
               // html += "<br> <div style=\"top: 50%; left: 0; width: 100vw; height: 5px; background-color: lightgray;\"></div><br> <br> " + html_end;
            }

            //for big imgs
            html = html.Replace("height:auto;", "height:500;");
            return html;            
        }

        private void save_button_Click()
        {
            try
            {
                // Write the HTML content to the file
                if(finalHtmlContentForFile == "")
                    {
                    File.WriteAllText(this.file_path, get_html());
                }
                else { File.WriteAllText(this.file_path,finalHtmlContentForFile); }
            }
            catch (Exception ex)
            {
                MessageBox.Show("error");
            }

        }
    }
}
