using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientForQuestions2._0
{
    internal class QuestionsToPdf
    {
        private static int amountOfQuestions = 20;
        private static bool isGraphAdded = false;
        private static int counter = 0;
        private static string htmlContent = "";
        private static string fileName = "quesions.html";
        private static string filePath = Environment.CurrentDirectory + "/" + fileName;
        public static void Questions2Pdf(List<dbQuestionParmeters> questions)
        {
            return;
            string htmlcontent = "";
            foreach (var question in questions) 
            {
                htmlContent += $"<h1>{counter + 1}.<h1>";
                htmlcontent += question.json_content["question"];
                htmlcontent += question.json_content[""];
            }
            writeIntoFile(htmlcontent);

        }
        public static void addGraph(string htmlContentOfGraph)
        {
            if(isGraphAdded)
            {
                return;
            }
            htmlContentOfGraph = htmlContentOfGraph.Replace("height:auto;", "height:500;");
            htmlContent += htmlContentOfGraph + "<br>";
        }
        public static void addQuestion(string html)
        {
            counter++;
            htmlContent += $"<h1>{counter}.</h1>";
            htmlContent += html + "<br>";
            if(counter == amountOfQuestions) 
            {
                writeIntoFile(htmlContent);
            }
        }
        public static void writeIntoFile(string htmlcontent)
        {
            ClearFileContent();
            using (StreamWriter sw = new StreamWriter(filePath, append: true))
            {
                sw.WriteLine(htmlcontent);
            }
        }
        public static void ClearFileContent()
        {
            try
            {
                // Open the file with FileMode.Truncate to clear its content
                using (FileStream fs = new FileStream(filePath, FileMode.Truncate))
                {
                    // File content is now cleared
                }
            }
            catch { }
        }

    }
}
