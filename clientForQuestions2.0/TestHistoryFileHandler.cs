using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientForQuestions2._0
{
    public struct tirgulParameters
    {
        public List<Dictionary<string, int>> all_q;
        public string date;
        public int id;
    }

    public struct Test
    {
        public List<afterQuestionParametrs> m_afterQuestionParametrs;
        public string date;
    }

    internal class TestHistoryFileHandler
    {
        private static string fileName = "testHistoryFile.history";
        public static int WITHOUT_SETTING = -1;
        public static int EXSIST = 1;
        public static int NOT_EXSIST = 0;
        private static string filePath = Environment.CurrentDirectory + "/" + fileName;
        public static void openFile()
        {
            try
            {
                Console.WriteLine(filePath);
                // Create a new file, throw an exception if it exists
                using (FileStream fs = new FileStream(filePath, FileMode.CreateNew))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        writer.WriteLine("[]"); // start an empty array
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File already exists: {ex.Message}");
            }
        }

        public static void save_afterQuestionParametrs_to_test_history(List<afterQuestionParametrs> m_afterQuestionParametrs)
        {
            // list of 
            List<Dictionary<string, int>> all_q = new List<Dictionary<string, int>>();

            
            foreach (afterQuestionParametrs paramers in m_afterQuestionParametrs)
            {
                Dictionary<string, int> current_q_parameters = new Dictionary<string, int>();
                current_q_parameters.Add("questionId", paramers.question.questionId); // to save storage, we save only the id of the question
                current_q_parameters.Add("userAnswer", paramers.userAnswer);
                current_q_parameters.Add("timeForAnswer", paramers.timeForAnswer);
                current_q_parameters.Add("indexOfQuestion", paramers.indexOfQuestion);

                all_q.Add(current_q_parameters); // add single 'question' to a list of all the questions in the test
            }

            tirgulParameters tirgulParameters = new tirgulParameters();
            tirgulParameters.all_q = all_q; // set all_q
            tirgulParameters.date = DateTime.Now.ToString(); // set current date & time
            //tirgulParameters.id = 0; // TODO, maybe we can do an id?
            
            // appending the tirgulParameters to the file
            // adding from the start so the last tests would be at the start

            JToken json = JToken.FromObject(tirgulParameters);

            // Read the existing JSON array from the file
            string file_content = File.ReadAllText(filePath);
            JArray jArray = JArray.Parse(file_content); ;
            // Append the new JToken to the JArray to the start
            jArray.Insert(0, json);
            // Serialize the updated JArray back to a JSON string
            string updatedJson = jArray.ToString(Newtonsoft.Json.Formatting.Indented);
            // Write the updated JSON back to the file
            File.WriteAllText(filePath, updatedJson);
        }

        public static List<Test> get_test_history()
        {
            // getting the data from the file
            string file_content = File.ReadAllText(filePath);
            JArray jArray = JArray.Parse(file_content); ;

            // taking all of the tests in the file
            List<Test> testHistory = new List<Test>();
            foreach (JToken jToken in jArray)
            {
                Test test = new Test();

                // getting all the after Question Parameters
                List<afterQuestionParametrs> m_afterQuestionParameters = new List<afterQuestionParametrs>();
                foreach (JToken jToken2 in (JArray) jToken["all_q"])
                {
                    afterQuestionParametrs m_afterQuestionParameter = new afterQuestionParametrs();
                    m_afterQuestionParameter.question = sqlDb.get_question_based_on_id((int)jToken2["questionId"]); // getting the q from its id
                    m_afterQuestionParameter.userAnswer = (int) jToken2["userAnswer"];
                    m_afterQuestionParameter.timeForAnswer = (int)jToken2["timeForAnswer"];
                    m_afterQuestionParameter.indexOfQuestion = (int)jToken2["indexOfQuestion"];

                    m_afterQuestionParameters.Add(m_afterQuestionParameter);
                }

                test.m_afterQuestionParametrs = m_afterQuestionParameters;
                test.date = (string)jToken["date"];

                testHistory.Add(test);
            }

            return testHistory;
        }

        public static void delete_test_history()
        {
            File.WriteAllText(filePath, "[]"); // reset the array
        }
    }
}
