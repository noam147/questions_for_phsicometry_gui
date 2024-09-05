using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace clientForQuestions2._0
{
    internal class sqlDb
    {
        static string file_path = "C:\\Users\\Magshimim\\Downloads\\wifi\\jsons_decrypt\\kidum_jsons.db";
        static string connectionString = $"Data Source={file_path};Version=3;";


        private static JArray doQuery(string query)
        {
            JArray jsonArray = new JArray();

            // Create a connection to the SQLite database
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Create a command to execute the query
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    try
                    {
                        // Open the connection
                        connection.Open();

                        // Execute the command and read the results
                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the results and display them
                            while (reader.Read())
                            {
                                JObject jsonObject = JObject.Parse($"{reader["json_question"]}");
                                var check = jsonObject["data"][0];
                                JObject realJsonObject = JObject.Parse(check.ToString());
                                jsonArray.Add(realJsonObject);

                                // Access the data using column names or indices
                                //Console.WriteLine($"{reader["Username"]}, {reader["Email"]}"); // Replace with your column names
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle any errors that occur
                        Console.WriteLine($"An error occurred: {ex.Message}");
                        return null;
                    }
                }
            }
            return jsonArray;
        }
        public static JArray get_all_data()
        {  
            string query = "SELECT * FROM questions"; // Replace 'Users' with your actual table name
            return doQuery(query);
        }
        public static JArray get_n_questions_from_specofic_category(int n,string category)
        {
            string query = $"SELECT * FROM questions where question_type = \'{category}\' ORDER BY RANDOM() limit {n}";
            return doQuery(query);
        }
        public static JArray get_n_questions_from_arr_of_categorys(int n, List<string> categories)
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
            public static string get_string_of_question_and_option_from_json(JToken json)
        {
            string question = json["question"].ToString();
            string option1 = json["options"][0]["text"].ToString();
            string option2 = json["options"][1]["text"].ToString();
            string option3 = json["options"][2]["text"].ToString();
            string option4 = json["options"][3]["text"].ToString();
            return question + get_string_of_img_html(json["image"]) + "<br><br>" + option1 + option2 + option3 + option4;

        }
        public static string get_string_of_question_and_explanation(JToken json)
        {
            string answer = json["solving_explanation"].ToString();
            var img = json["explanation_image"];
            //string line = "<div style=\"position: fixed; left: 50%; top: 0; height: 100vh; width: 1px; background-color: lightgray;\"></div>\r\n";
            //string line = "<div style=\"position: fixed; left: 50%; top: 50%; height: 1px; width: 100vw; background-color: lightgray; transform: rotate(90deg);\"></div>\r\n";
            string line = "<div style=\"top: 50%; left: 0; width: 100vw; height: 1px; background-color: lightgray;\"></div>\r\n<br>explanation:";
            return get_string_of_question_and_option_from_json(json) +line +answer+ get_string_of_img_html(img);
            
        }
        public static int get_num_of_correct_answer(JToken json)
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