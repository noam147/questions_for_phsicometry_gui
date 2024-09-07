using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Runtime.CompilerServices;
namespace clientForQuestions2._0
{
    public struct afterQuestionParametrs
    {
        public dbQuestionParmeters question;
        public int userAnswer;
        public int timeForAnswer;
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
        static string file_path = "C:\\Users\\Magshimim\\Downloads\\wifi\\jsons_decrypt\\kidum_jsons.db";
        //static string file_path = "kidum_jsons.db";
        static string connectionString = $"Data Source={file_path};Version=3;";


        private static List<dbQuestionParmeters> doQuery(string query)
        {
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
                        return null;
                    }
                }
            }
            for(int i = 0;i<jsonArray.Count;i++)
            {
                dbQuestionParmeters dbParmeters = new dbQuestionParmeters();
                dbParmeters.json_content = jsonArray[i];
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
            if(questionParameters != null)
                return questionParameters[0];
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

        private static string get_string_of_img_html(JToken json)
        {
            if(json == null)
            {
                return "";
            }
            if(!json.HasValues)
            {
                return "";
            }
            string img_path = "https://lmsapi.kidum-me.com/storage/";
            string file_path = img_path + json["file_path"].ToString();
            string fullImg = $"<img src=\"{file_path}\" alt=\"Question Image\" style=\"max-width:100%; height:auto;\">";
            return fullImg;
        }

        public static string addNumberToQuestions(string q1,string q2,string q3,string q4)
        {
            q1 = "<p>(1)\t" + q1.Substring(3,q1.Length-3);
            q2 = "<p>(2)\t" + q2.Substring(3, q2.Length - 3);
            q3 = "<p>(3)\t" + q3.Substring(3, q3.Length - 3);
            q4 = "<p>(4)\t" + q4.Substring(3, q4.Length - 3);
            return q1 + q2 + q3 + q4;
        }
            public static string get_string_of_question_and_option_from_json(JToken json)
        {
            string question = json["question"].ToString();
            string option1 = json["options"][0]["text"].ToString();
            string option2 = json["options"][1]["text"].ToString();
            string option3 = json["options"][2]["text"].ToString();
            string option4 = json["options"][3]["text"].ToString();

            string allOptions = addNumberToQuestions(option1, option2, option3, option4);
            return question + get_string_of_img_html(json["image"]) + "<br><br>" + allOptions;
        }
        
        
        private static string getFinalStringOfOptions(string option1,string option2, string option3, string option4,int wrongUserChoice)
        {
            if(wrongUserChoice == 1)
            {
                option1 = $"<div style = \"background-color: red;display: inline-block;\">{option1}</div><br>";
            }
            if (wrongUserChoice == 2)
            {
                option2 = $"<div style = \"background-color: red;display: inline-block;\">{option2}</div><br>";
            }
            if (wrongUserChoice == 3)
            {
                option3 = $"<div style = \"background-color: red;display: inline-block;\">{option3}</div><br>";
            }
            if (wrongUserChoice == 4)
            {
                option4 = $"<div style = \"background-color: red;display: inline-block;\">{option4}</div><br>";
            }
            return option1 + option2 + option3 + option4;
        }
        public static string get_string_of_question_and_option_from_json(JToken json, int answer)
        {
            string question = json["question"].ToString();
            string option1 = json["options"][0]["text"].ToString();
            string option2 = json["options"][1]["text"].ToString();
            string option3 = json["options"][2]["text"].ToString();
            string option4 = json["options"][3]["text"].ToString();
            string final = addNumberToQuestions(option1, option2, option3, option4);
            option1 = final.Substring(0, final.IndexOf("<p>(2)"));
            final = final.Substring(final.IndexOf("<p>(2)"), final.Length - option1.Length);
            option2 = final.Substring(0, final.IndexOf("<p>(3)"));
            final = final.Substring(final.IndexOf("<p>(3)"), final.Length - option2.Length);
            option3 = final.Substring(0, final.IndexOf("<p>(4)"));
            final = final.Substring(final.IndexOf("<p>(4)"), final.Length - option3.Length);
            option4 = final;
            int option1tf = (int)json["options"][0]["is_correct"];
            int option2tf = (int)json["options"][1]["is_correct"];
            int option3tf = (int)json["options"][2]["is_correct"];
            int option4tf = (int)json["options"][3]["is_correct"];
            string startOfMsg = question + get_string_of_img_html(json["image"]) + "<br><br>";
            string[] options = { option1, option2, option3, option4 }; // Assuming you have these defined
            int[] optionFlags = { option1tf, option2tf, option3tf, option4tf };
            //option[n]tf = 1 - this is the real true answer
            for (int i = 1; i < 5; i++)
            {
                if (optionFlags[i - 1] == 1)
                {
                    options[i - 1] = $"<div style = \"background-color: cyan;display: inline-block;\">{options[i - 1]}</div><br>";
                    if (answer != i)
                    {
                        //if the user were wrong
                        return startOfMsg + getFinalStringOfOptions(options[0], options[1], options[2], options[3], answer);
                    }
                    //if the user got right
                    return startOfMsg + options[0] + options[1] + options[2] + options[3];
                }
            }
            return "";
        }
        
        public static string get_string_of_question_and_explanation(JToken json,int clientanswer)
        {
            string answer = json["solving_explanation"].ToString();
            var img = json["explanation_image"];
            string line = "<div style=\"top: 50%; left: 0; width: 100vw; height: 1px; background-color: lightgray;\"></div>\r\n<br>explanation:";
            return get_string_of_question_and_option_from_json(json,clientanswer) +line +answer+ get_string_of_img_html(img);
            
        }
        private static int get_num_of_correct_answer(JToken json)
        {
            int option1 = (int)json["options"][0]["is_correct"];
            int option2 = (int)json["options"][1]["is_correct"];
            int option3 = (int)json["options"][2]["is_correct"];
            int option4 = (int)json["options"][3]["is_correct"];
            if(option1 == 1)
            {
                return 1;
            }
            if(option2 == 1)
            {
                return 2;
            }
            if (option3 == 1)
                return 3;
            return 4;
        }
    }
        
    
}