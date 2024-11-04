using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientForQuestions2._0
{
    
    internal class IdsToFile
    {
        private static string folderName = "sinulationsFiles";
        private static string folderPath = Environment.CurrentDirectory + "/" + folderName;
        public static void  openDir()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine("Folder created.");
            }
        }
        public static void addChapterFile(int numOfChapter, List<dbQuestionParmeters> questionParmeters)
        {
            string fileName = $"chapter{numOfChapter}.txt";
            string textIntoFile = "";
            foreach(dbQuestionParmeters question in questionParmeters)
            {
                textIntoFile += question.questionId + " ";
            }
            string filePath = folderPath + "/" + fileName;
            writeIntoFile(textIntoFile, filePath);

        }

        private static void writeIntoFile(string text,string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath, append: false))
            {
                sw.WriteLine(text);
            }
        }

        public static List<dbQuestionParmeters> getQuestionsFromFile(int numOfChapter)
        {
            string filePath = folderPath + "/" + $"chapter{numOfChapter}.txt";
            string fileContent = File.ReadAllText(filePath);
            List<string> ids = fileContent.Contains("\n") ? new List<string>(fileContent.Split('\n')) : new List<string>(fileContent.Split(' '));
            List < dbQuestionParmeters > questions = new List<dbQuestionParmeters >();
            foreach (string id in ids)
            {
                int integetId = -1;
                try
                {
                    integetId = int.Parse(id);
                }
                catch (Exception ex) { continue; }
               dbQuestionParmeters curr =  sqlDb.get_question_based_on_id(integetId);
                if(curr.questionId == integetId)//if q with this id exsist
                {
                    questions.Add(curr);
                }
            }
            return questions;
        }
    }
}
