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
            string query = $"SELECT * FROM questions where question_type = \'{category}\' limit {n}";
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
            string query = $"SELECT * FROM questions WHERE question_type IN ({add}) LIMIT {n}";
            
            return doQuery(query);
        }
    }
        
    
}