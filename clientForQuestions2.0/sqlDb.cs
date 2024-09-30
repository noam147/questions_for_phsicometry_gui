using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Newtonsoft.Json.Linq;
namespace clientForQuestions2._0
{
    public struct afterQuestionParametrs
    {
        public dbQuestionParmeters question;
        public int userAnswer;
        public int timeForAnswer;
        public int indexOfQuestion;
    }
    public struct dbQuestionParmeters
    {
        public JToken json_content;
        public int questionId;
        public string category;
        public int rightAnswer;  
    }
    internal class sqlDb
    {
        static string file_path = AppDomain.CurrentDomain.BaseDirectory + "kidum_jsons.db";
        static string connectionString = $"Data Source={file_path};Version=3;";


        private static List<dbQuestionParmeters> doQuery(string query)
        {
            //func content is not intresting
            //input sql query
            //output list of questions details
            string includeJsLibs = "<head> <script src=\"https://polyfill.io/v3/polyfill.min.js?features=es6\"></script>\r\n  <script id=\"MathJax-script\" async src=\"https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js\"></script></head>";
            List<dbQuestionParmeters> dbQuestions = new List<dbQuestionParmeters>();
            JArray jsonArray = new JArray();
            List<string> categories = new List<string>(); 
            List<int> ids = new List<int>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        connection.Open();
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the results and display them
                            while (reader.Read())
                            {
                                JObject jsonObject = JObject.Parse($"{reader["json_question"]}");
                                var check = jsonObject["data"][0];
                                JObject realJsonObject = JObject.Parse(check.ToString());
                                jsonArray.Add(realJsonObject);
                                ids.Add(int.Parse(reader["question_id"].ToString()));
                                categories.Add(reader["question_type"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        LogFileHandler.writeIntoFile("error occurred with db query - check if file json.db exsist");
                        return null;
                    }
                }
            }
            for(int i = 0;i<jsonArray.Count;i++)
            {
                dbQuestionParmeters dbParmeters = new dbQuestionParmeters();
                dbParmeters.json_content = jsonArray[i];
                dbParmeters.json_content["question"] = includeJsLibs + dbParmeters.json_content["question"];
                dbParmeters.category = categories[i];
                dbParmeters.questionId = ids[i];
                dbParmeters.rightAnswer = get_num_of_correct_answer(jsonArray[i]);
                dbQuestions.Add(dbParmeters);
            }
            return dbQuestions;
        }
        public static List<dbQuestionParmeters> get_all_data()
        {  
            string query = "SELECT * FROM questions"; 
            return doQuery(query);
        }
        public static dbQuestionParmeters get_question_based_on_id(int questionId)
        {
            string query = $"SELECT * FROM questions where question_id = \'{questionId}\'"; 
            var questionParameters = doQuery(query);
            
            if(questionParameters != null && questionParameters.Count != 0)
                return questionParameters[0];

            //this will return a something with id = 0 - that cant happend in real q
            return new dbQuestionParmeters();
            
            
        }
        public static List<dbQuestionParmeters> get_n_questions_from_specofic_category(int n,string category)
        {
            string query = $"SELECT * FROM questions where question_type = \'{category}\' ORDER BY RANDOM() limit {n}";
            return doQuery(query);
        }
        public static List<dbQuestionParmeters> get_n_questions_from_arr_of_categorys(int n, List<string> categories)
        {
            string add = "";
            for(int i = 0;i<categories.Count;i++)
            {
                add += "\'"+categories[i] + "\',";
            }
            add = add.Substring(0, add.Length-1);
            string query = $"SELECT * FROM questions WHERE question_type IN ({add}) ORDER BY RANDOM() LIMIT {n}";
            
            return doQuery(query);
        }
        public static List<dbQuestionParmeters> get_n_questions_from_arr_of_categorysWithDiffcultyLevel(int n, List<string> categories,questionsDifficultyLevel difficulty)
        {
            string add = "";
            for (int i = 0; i < categories.Count; i++)
            {
                add += "\'" + categories[i] + "\',";
            }
            add = add.Substring(0, add.Length - 1);
            string query = $@"
    SELECT * 
    FROM questions 
    WHERE question_type IN ({add}) 
      AND JSON_EXTRACT(json_question, '$.data[0].difficulty_level') >= {difficulty.minlevel} 
      AND JSON_EXTRACT(json_question, '$.data[0].difficulty_level') <= {difficulty.maxLevel} 
    ORDER BY RANDOM() 
    LIMIT {n}";

            return doQuery(query);
        }
        private static int get_num_of_correct_answer(JToken json)
        {
            int option1;
            int option2;
            int option3;
            int option4;
            try
            {
                option1 = (int)json["options"][0]["is_correct"];
                option2 = (int)json["options"][1]["is_correct"];
                option3 = (int)json["options"][2]["is_correct"];
                option4 = (int)json["options"][3]["is_correct"];
            }
            catch (Exception e)
            {
                return 1;
            }
            if (option1 == 1)
            {
                return 1;
            }
            if (option2 == 1)
            {
                return 2;
            }
            if (option3 == 1)
                return 3;
            return 4;
        }

    }
        
    
}