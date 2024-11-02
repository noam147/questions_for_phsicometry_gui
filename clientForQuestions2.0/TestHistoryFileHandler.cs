using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.Text.RegularExpressions;

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
        public string type;
        public int id;
        public string name; // when in TestWithChapters: used for chapter's name
        public bool isMarked;
    }

    public struct TestWithChapters
    {
        public List<Test> chapters;
        public string date;
        public string type;
        public int id;
        public string name;
        public bool isMarked;
    }

    internal class TestHistoryFileHandler
    {
        public static string MARKED_TRUE = "★";
        public static string MARKED_FALSE = "☆"; //✰

        static string file_path = AppDomain.CurrentDomain.BaseDirectory + "test_history_file.db";
        public static string connectionString = $"Data Source={file_path};Version=3;";
        public static int WITHOUT_SETTING = -1;
        public static int EXSIST = 1;
        public static int NOT_EXSIST = 0;

        public static int CHAPTER_PARTITION_Q_ID = -42; // id of 'question' that is between two chapters
        public static string CHAPTER_PARTITION_TestInfo = "&PARTITION&"; // id of 'question' that is between two chapters

        public static void openFile()
        {
            try
            {
                string file_path = AppDomain.CurrentDomain.BaseDirectory + "test_history_file.db";

                // Specify the database file path
                string dbPath = file_path;

                // Create a new SQLite database connection
                using (SQLiteConnection connection = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    connection.Open();

                    // SQL command to create a table
                    string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS TestsHistoryData (
                        TestId INT,
                        TestName TEXT,
                        TestInfo TEXT,
                        TestType TEXT,
                        TestIsMarked BOOLEAN,
                        Date TEXT,
                        QuestionId INT,
                        IndexOfQuestion INT,
                        UserAnswer INT,
                        TimeForQuestion INT,
                        QuestionIsMarked BOOLEAN,
                        QuestionLesson TEXT
                    )";

                    using (SQLiteCommand command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                        //LogFileHandler.writeIntoFile("Table 'TestsHistoryData' created successfully.");
                    }
                }
            }
            catch (IOException ex)
            {
                LogFileHandler.writeIntoFile($"File TestsHistoryData already exists: {ex.Message}");
            }
        }

        public static void deleteFile()
        {
            try
            {
                if (File.Exists(file_path))
                {
                    File.Delete(file_path);
                }
            }
            catch (IOException e)
            {
            }

        }

        public static int get_next_test_id()
        {
            int highestTestId = 0; // Variable to store the highest TestId

            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to get the highest TestId
                    string selectQuery = "SELECT MAX(TestId) AS HighestTestId FROM TestsHistoryData;";

                    using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                    {
                        object result = command.ExecuteScalar(); // Execute the query and get the result

                        if (result != DBNull.Value)
                        {
                            highestTestId = Convert.ToInt32(result); // Convert result to int
                            Console.WriteLine($"The highest TestId is: {highestTestId}");
                        }
                        else
                        {
                            Console.WriteLine("No TestId found in the table.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to get next test_id: {ex.Message}");
            }

            return highestTestId + 1;
        }
        public static void save_afterQuestionParametrs_to_test_history(List<afterQuestionParametrs> m_afterQuestionParametrs, int test_id, string test_type)
        {
            
            // SQL query to insert values into the table
            string insertValuesQuery = @"
                        INSERT INTO TestsHistoryData (TestId, TestType, TestIsMarked, Date, QuestionId, IndexOfQuestion, UserAnswer, TimeForQuestion,QuestionIsMarked, QuestionLesson,TestName,TestInfo) 
                        VALUES (@TestId, @TestType, @TestIsMarked, @Date, @QuestionId,  @IndexOfQuestion, @UserAnswer, @TimeForQuestion, @QuestionIsMarked, @QuestionLesson,@TestName,@TestInfo)";

            string date = DateTime.Now.ToString();

            foreach (afterQuestionParametrs paramers in m_afterQuestionParametrs)
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // Insert some values into the table
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertValuesQuery, connection))
                    {
                        // Define the parameters for the query
                        insertCommand.Parameters.AddWithValue("@TestId", test_id);
                        insertCommand.Parameters.AddWithValue("@TestType", test_type); // TODO
                        insertCommand.Parameters.AddWithValue("@TestIsMarked", false);
                        insertCommand.Parameters.AddWithValue("@Date", date);
                        insertCommand.Parameters.AddWithValue("@QuestionId", paramers.question.questionId);
                        insertCommand.Parameters.AddWithValue("@IndexOfQuestion", paramers.indexOfQuestion);
                        insertCommand.Parameters.AddWithValue("@UserAnswer", paramers.userAnswer);
                        insertCommand.Parameters.AddWithValue("@TimeForQuestion", paramers.timeForAnswer);
                        insertCommand.Parameters.AddWithValue("@QuestionIsMarked", false);
                        if (paramers.lesson == null)
                            insertCommand.Parameters.AddWithValue("@QuestionLesson", "");
                        else
                            insertCommand.Parameters.AddWithValue("@QuestionLesson", paramers.lesson);
                        insertCommand.Parameters.AddWithValue("@TestName", $"תרגול #{test_id}");
                        insertCommand.Parameters.AddWithValue("@TestInfo", "");
                        // Execute the insert command
                        insertCommand.ExecuteNonQuery();
                    }

                }
            }
        }

        /// <summary>
        /// This function gets a list of chapters, so each chapter is a list of afterQuestionParametrs.
        /// Before each chapter, the function saves a 'fake question' (not a real question but a partition), with the QuestionId = TestHistoryFileHandler.CHAPTER_PARTITION_Q_ID.
        /// The TestInfo of the 'fake question' determains the name of the chapter.
        /// </summary>
        public static void save_multiple_chapters_to_test_history(List<List<afterQuestionParametrs>> chapters, int test_id, string test_type, List<string> chapter_names)
        {

            // SQL query to insert values into the table
            string insertValuesQuery = @"
                        INSERT INTO TestsHistoryData (TestId, TestType, TestIsMarked, Date, QuestionId, IndexOfQuestion, UserAnswer, TimeForQuestion,QuestionIsMarked, QuestionLesson,TestName,TestInfo) 
                        VALUES (@TestId, @TestType, @TestIsMarked, @Date, @QuestionId,  @IndexOfQuestion, @UserAnswer, @TimeForQuestion, @QuestionIsMarked, @QuestionLesson,@TestName,@TestInfo)";

            string date = DateTime.Now.ToString();

            int indexOfQuestion = 0;
            for (int i = 0; i < chapters.Count; i++)
            {
                List<afterQuestionParametrs> chapter_qs = chapters[i];
                // save 'fake question'
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    using (SQLiteCommand insertCommand = new SQLiteCommand(insertValuesQuery, connection))
                    {
                        // Define the parameters for the query
                        insertCommand.Parameters.AddWithValue("@TestId", test_id);
                        insertCommand.Parameters.AddWithValue("@TestType", test_type);
                        insertCommand.Parameters.AddWithValue("@TestIsMarked", false);
                        insertCommand.Parameters.AddWithValue("@Date", date);
                        insertCommand.Parameters.AddWithValue("@QuestionId", TestHistoryFileHandler.CHAPTER_PARTITION_Q_ID);
                        insertCommand.Parameters.AddWithValue("@IndexOfQuestion", indexOfQuestion);
                        insertCommand.Parameters.AddWithValue("@UserAnswer", OperationsAndOtherUseful.SKIPPED_Q);
                        insertCommand.Parameters.AddWithValue("@TimeForQuestion", -1);
                        insertCommand.Parameters.AddWithValue("@QuestionIsMarked", false);
                        insertCommand.Parameters.AddWithValue("@QuestionLesson", "");
                        insertCommand.Parameters.AddWithValue("@TestName", $"תרגול #{test_id}");
                        insertCommand.Parameters.AddWithValue("@TestInfo", CHAPTER_PARTITION_TestInfo + chapter_names[i] + CHAPTER_PARTITION_TestInfo);
                        // Execute the insert command
                        insertCommand.ExecuteNonQuery();
                    }

                }

                indexOfQuestion++;

                foreach (afterQuestionParametrs paramers in chapter_qs)
                {
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        // Insert some values into the table
                        using (SQLiteCommand insertCommand = new SQLiteCommand(insertValuesQuery, connection))
                        {
                            // Define the parameters for the query
                            insertCommand.Parameters.AddWithValue("@TestId", test_id);
                            insertCommand.Parameters.AddWithValue("@TestType", test_type); // TODO
                            insertCommand.Parameters.AddWithValue("@TestIsMarked", false);
                            insertCommand.Parameters.AddWithValue("@Date", date);
                            insertCommand.Parameters.AddWithValue("@QuestionId", paramers.question.questionId);
                            insertCommand.Parameters.AddWithValue("@IndexOfQuestion", indexOfQuestion);
                            insertCommand.Parameters.AddWithValue("@UserAnswer", paramers.userAnswer);
                            insertCommand.Parameters.AddWithValue("@TimeForQuestion", paramers.timeForAnswer);
                            insertCommand.Parameters.AddWithValue("@QuestionIsMarked", false);
                            insertCommand.Parameters.AddWithValue("@TestName", $"תרגול #{test_id}");
                            insertCommand.Parameters.AddWithValue("@TestInfo", "");
                            if (paramers.lesson == null)
                                insertCommand.Parameters.AddWithValue("@QuestionLesson", "");
                            else
                                insertCommand.Parameters.AddWithValue("@QuestionLesson", paramers.lesson);

                            // Execute the insert command
                            insertCommand.ExecuteNonQuery();
                        }

                    }

                    indexOfQuestion++;
                }
            }
        }

        private static List<Test> getResultFromQuery(string selectQuery)
        {
            DataTable dataTable = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //LogFileHandler.writeIntoFile("Database connection opened.");



                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // Check if the reader has rows
                        if (reader.HasRows)
                        {

                            // Load the data into the DataTable
                            dataTable.Load(reader);
                            //LogFileHandler.writeIntoFile("Data loaded into DataTable.");
                        }
                        else
                        {
                            LogFileHandler.writeIntoFile("No rows found in the query.");
                        }
                    }
                }
            }

            //LogFileHandler.writeIntoFile($"{dataTable.Rows.Count}");
            // Grouping and iterating over the DataTable rows
            var testIdGroups = new Dictionary<int, List<DataRow>>();

            // Grouping by TestId
            foreach (DataRow row in dataTable.Rows)
            {
                int testId = (int)row["TestId"];
                if (!testIdGroups.ContainsKey(testId))
                {
                    testIdGroups[testId] = new List<DataRow>();
                }
                testIdGroups[testId].Add(row);
            }

            // taking all of the tests in the file
            List<Test> testHistory = new List<Test>();
            // Iterating through each group
            foreach (var group in testIdGroups)
            {
                Test test = new Test();

                // getting all the after Question Parameters
                List<afterQuestionParametrs> m_afterQuestionParameters = new List<afterQuestionParametrs>();

                //LogFileHandler.writeIntoFile($"Records for TestId: {group.Key}");
                foreach (var row in group.Value)
                {
                    afterQuestionParametrs m_afterQuestionParameter = new afterQuestionParametrs();
                    if ((int)row["QuestionId"] == CHAPTER_PARTITION_Q_ID)
                    {
                        dbQuestionParmeters q_of_partition = new dbQuestionParmeters();
                        q_of_partition.questionId = CHAPTER_PARTITION_Q_ID;
                        m_afterQuestionParameter.question = q_of_partition;
                    }
                    else
                        m_afterQuestionParameter.question = sqlDb.get_question_based_on_id((int)row["QuestionId"]); // getting the q from its id
                    m_afterQuestionParameter.userAnswer = (int)row["UserAnswer"];
                    m_afterQuestionParameter.timeForAnswer = (int)row["TimeForQuestion"];
                    m_afterQuestionParameter.indexOfQuestion = (int)row["IndexOfQuestion"];
                    m_afterQuestionParameter.lesson = (string)row["QuestionLesson"];
                    m_afterQuestionParameter.isMarked = (bool)row["QuestionIsMarked"];
                    m_afterQuestionParameter.info = (string)row["TestInfo"];

                    m_afterQuestionParameters.Add(m_afterQuestionParameter);

                    // Access each column by name
                    //LogFileHandler.writeIntoFile($"  TestId: {group.Key}, TestType: {row["TestType"]}, Date: {row["Date"]}, QuestionId: {row["QuestionId"]}, IndexOfQuestion: {row["IndexOfQuestion"]}, UserAnswer: {row["UserAnswer"]}, TimeForQuestion: {row["TimeForQuestion"]}, QuestionLesson: {row["QuestionLesson"]}");
                }
                test.m_afterQuestionParametrs = m_afterQuestionParameters;
                test.date = (string)group.Value[0]["Date"];
                test.type = (string)group.Value[0]["TestType"];
                test.id = (int)group.Value[0]["TestId"];
                test.isMarked = (bool)group.Value[0]["TestIsMarked"];
                test.name = (string)group.Value[0]["TestName"];
                testHistory.Add(test);

            }

            return testHistory;
        }

        public static bool is_test_with_chapters(int test_id)
        {
            string selectQuery = @"
                SELECT * 
                FROM TestsHistoryData 
                WHERE TestId = " + test_id +
" ORDER BY IndexOfQuestion ASC;";

            Test whole_test = getResultFromQuery(selectQuery)[0];

            // if test have chapters 
            return whole_test.m_afterQuestionParametrs[0].question.questionId == CHAPTER_PARTITION_Q_ID;
        }

        public static TestWithChapters get_test_with_chapters(int test_id)
        {
            string selectQuery = @"
                SELECT * 
                FROM TestsHistoryData 
                WHERE TestId = " + test_id +
    " ORDER BY IndexOfQuestion ASC;";

            Test whole_test = getResultFromQuery(selectQuery)[0];

            // if test doesnt have chapters, return empty TestWithChapters
            if (!(whole_test.m_afterQuestionParametrs[0].question.questionId == CHAPTER_PARTITION_Q_ID))
                return new TestWithChapters();
            
            TestWithChapters new_test = new TestWithChapters();
            new_test.chapters = new List<Test>();
            new_test.id = whole_test.id;
            new_test.date = whole_test.date;
            new_test.name = whole_test.name;
            new_test.isMarked = whole_test.isMarked;
            new_test.type = whole_test.type;
            int indexOfQuestion = 0;
            foreach (afterQuestionParametrs q in whole_test.m_afterQuestionParametrs)
            {
                // if this is a start of a new chapter
                if (q.question.questionId == CHAPTER_PARTITION_Q_ID)
                {
                    indexOfQuestion = 0;

                    Test chapter = new Test();
                    chapter.type = whole_test.type;
                    chapter.date = whole_test.date;
                    chapter.isMarked = whole_test.isMarked;
                    chapter.id = whole_test.id;

                    var match = Regex.Match(q.info, $"{CHAPTER_PARTITION_TestInfo}(.*?){CHAPTER_PARTITION_TestInfo}");

                    chapter.name = match.Groups[1].Value;
                    chapter.m_afterQuestionParametrs = new List<afterQuestionParametrs>();
                    new_test.chapters.Add(chapter);
                }
                else
                {
                    afterQuestionParametrs new_q = q;
                    //new_q.indexOfQuestion = indexOfQuestion;
                    // add q to the last chapter
                    new_test.chapters[new_test.chapters.Count - 1].m_afterQuestionParametrs.Add(new_q);
                    indexOfQuestion++;
                }
            }

            return new_test;
        }

        public static List<Test> get_questions_with_lesson()
        {
            //LogFileHandler.writeIntoFile("get_questions_with_lesson");
            // SQL command to select all data ordered by TestId and IndexOfQuestion
            string selectQuery = @"
                SELECT *
                FROM TestsHistoryData
                where QuestionLesson != ''  
                ORDER BY TestId DESC, IndexOfQuestion ASC ;";
            return getResultFromQuery(selectQuery);
        }
        public static List<Test> get_test_history()
        {
            //LogFileHandler.writeIntoFile("get_test_history");
            // SQL command to select all data ordered by TestId and IndexOfQuestion
            string selectQuery = @"
                SELECT *
                FROM TestsHistoryData
                ORDER BY TestId DESC, IndexOfQuestion ASC;";
            return getResultFromQuery(selectQuery);
        }

        public static void rename_test(int test_id,string newName)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    //LogFileHandler.writeIntoFile("Database connection opened.");

                    // SQL command to update the TestLesson
                    string updateQuery = @"
                UPDATE TestsHistoryData 
                SET TestName = @TestName 
                WHERE TestId = @TestId;";

                    using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TestId", test_id);
                        command.Parameters.AddWithValue("@TestName", newName);

                        int rowsAffected = command.ExecuteNonQuery(); // Execute the update command
                        if (rowsAffected > 0)
                        {
                            //LogFileHandler.writeIntoFile($"Updated TestLesson for TestId {test_id} and IndexOfQuestion {q_index}: '{lesson}'");
                        }
                        else
                        {
                            LogFileHandler.writeIntoFile("No rows updated. Check if TestId exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFileHandler.writeIntoFile($"An error occurred: {ex.Message}");
            }
        }


        public static void edit_lesson_in_test_history(string lesson, int test_id, int q_index)
        {
            // TODO edit the "lesson" of the question in a specific test_id
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    //LogFileHandler.writeIntoFile("Database connection opened.");

                    // SQL command to update the TestLesson
                    string updateQuery = @"
                UPDATE TestsHistoryData 
                SET QuestionLesson = @QuestionLesson 
                WHERE TestId = @TestId AND IndexOfQuestion = @IndexOfQuestion;";

                    using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                    {
                        // Adding parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@QuestionLesson", lesson);
                        command.Parameters.AddWithValue("@TestId", test_id);
                        command.Parameters.AddWithValue("@IndexOfQuestion", q_index);

                        int rowsAffected = command.ExecuteNonQuery(); // Execute the update command
                        if (rowsAffected > 0)
                        {
                            //LogFileHandler.writeIntoFile($"Updated TestLesson for TestId {test_id} and IndexOfQuestion {q_index}: '{lesson}'");
                        }
                        else
                        {
                            LogFileHandler.writeIntoFile("No rows updated. Check if TestId and IndexOfQuestion exist.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFileHandler.writeIntoFile($"An error occurred: {ex.Message}");
            }

        }

        public static List<List<string>> get_lessons_of_test_with_chapters_in_order(int test_id)
        {
            TestWithChapters result = get_test_with_chapters(test_id);
            List<List<string>> lessons = new List<List<string>>();
            foreach (Test chapter in result.chapters) 
            {
                lessons.Add(new List<string>());

                foreach (afterQuestionParametrs currParameters in chapter.m_afterQuestionParametrs)
                {
                    string currLesson = currParameters.lesson.Replace("clientForQuestions2._0.afterQuestionParametrs", "");
                    lessons[lessons.Count - 1].Add(currLesson);
                }
            }
            return lessons;
            /*List<String> lessons = new List<String>();
            // TODO get lessons of a test, occording to the order of the questions indexes
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Database connection opened.");

                    // SQL command to select lessons for a specific TestId ordered by IndexOfQuestion
                    string selectQuery = @"
                SELECT QuestionLesson 
                FROM TestsHistoryData 
                WHERE TestId = @TestId 
                ORDER BY IndexOfQuestion ASC;";

                    using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                    {
                        // Adding parameter to the query
                        command.Parameters.AddWithValue("@TestId", test_id);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            // Check if there are any lessons returned
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lessons.Add(reader.GetString(0)); // Add the lesson to the list 
                                }
                            }
                            else
                            {
                                LogFileHandler.writeIntoFile($"No lessons found for TestId {test_id}.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogFileHandler.writeIntoFile($"An error occurred: {ex.Message}");
            }

            return lessons;*/
        }

        public static List<string> get_lessons_of_test_in_order(int test_id)
        {

            string selectQuery = @"
                SELECT * 
                FROM TestsHistoryData 
                WHERE TestId = " + test_id +
                " ORDER BY IndexOfQuestion ASC;";

            List<Test> result = getResultFromQuery(selectQuery);
            List<string> lessons = new List<string>();
            foreach (Test test in result)
            {
                foreach (afterQuestionParametrs currParameters in test.m_afterQuestionParametrs)
                {
                    string currLesson = currParameters.lesson.Replace("clientForQuestions2._0.afterQuestionParametrs", "");
                    lessons.Add(currLesson);
                }
            }
            return lessons;
        }

            public static List<afterQuestionParametrs> get_afterQuestionParametrs_of_test(int test_id)
        {

            string selectQuery = @"
                SELECT * 
                FROM TestsHistoryData 
                WHERE TestId = " + $"{test_id}" +
                " ORDER BY IndexOfQuestion ASC;";

            List<Test> result = getResultFromQuery(selectQuery);

            return result[0].m_afterQuestionParametrs;
        }

        public static List<dbQuestionParmeters> get_dbQuestionParmeters_of_test(int test_id)
        {
            List<dbQuestionParmeters> qs = new List<dbQuestionParmeters>();
            foreach (afterQuestionParametrs q in get_afterQuestionParametrs_of_test(test_id))
                qs.Add(q.question);

            return qs;
        }

        public static List<List<afterQuestionParametrs>> get_afterQuestionParametrs_of_test_with_chapters(int test_id)
        {
            TestWithChapters result = get_test_with_chapters(test_id);
            List<List<afterQuestionParametrs>> chapters = new List<List<afterQuestionParametrs>>();

            foreach (Test chapter in result.chapters)
                chapters.Add(chapter.m_afterQuestionParametrs);
            return chapters;
        }

        public static List<List<dbQuestionParmeters>> get_dbQuestionParmeters_of_test_with_chapters(int test_id)
        {
            List<List<dbQuestionParmeters>> chapters = new List<List<dbQuestionParmeters>>();

            foreach (List<afterQuestionParametrs> qs in get_afterQuestionParametrs_of_test_with_chapters(test_id))
            {
                chapters.Add(new List<dbQuestionParmeters>());
                foreach (afterQuestionParametrs q in qs)
                    chapters[chapters.Count - 1].Add(q.question);
            }

            return chapters;
        }

        public static string get_type_of_test(int test_id)
        {
            string selectQuery = @"
                SELECT * 
                FROM TestsHistoryData 
                WHERE TestId = " + $"{test_id};";

            List<Test> result = getResultFromQuery(selectQuery);

            return result[0].type;
        }

        public static string get_name_of_test(int test_id)
        {
            string selectQuery = @"
                SELECT *
                FROM TestsHistoryData 
                WHERE TestId = " + $"{test_id};";

            List<Test> result = getResultFromQuery(selectQuery);

            return result[0].name;
        }

        public static void delete_test_history()
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to delete all data from the table
                    string deleteQuery = "DELETE FROM TestsHistoryData;";

                    using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.ExecuteNonQuery(); // Execute the delete command
                    }
                }
            }
            catch (Exception ex)
            {
            }        
        }

        public static void delete_test_by_id(int test_id)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    // SQL command to delete all data from the table
                    string deleteQuery = $"DELETE FROM TestsHistoryData WHERE TestId = {test_id};";

                    using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.ExecuteNonQuery(); // Execute the delete command
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static bool get_test_isMarked(int test_id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //LogFileHandler.writeIntoFile("Database connection opened.");

                // SQL command to update the TestLesson
                string selectQuery = @"
                SELECT TestIsMarked
                FROM TestsHistoryData 
                WHERE TestId = " + $"{test_id}";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (bool)reader["TestIsMarked"];
                        }
                    }
                }
            }
            return false;
        }

        public static bool get_question_isMarked(int test_id, int q_index)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //LogFileHandler.writeIntoFile("Database connection opened.");

                // SQL command to update the TestLesson
                string selectQuery = @"
                SELECT QuestionIsMarked
                FROM TestsHistoryData 
                WHERE TestId = " + $"{test_id} AND IndexOfQuestion = {q_index};";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return (bool)reader["QuestionIsMarked"];
                        }
                    }
                }
            }
            return false;
        }

        public static void set_test_isMarked(bool isMarked, int test_id)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //LogFileHandler.writeIntoFile("Database connection opened.");

                // SQL command to update the TestLesson
                string updateQuery = $"UPDATE TestsHistoryData " +
                $"SET TestIsMarked = {isMarked} " +
                $"WHERE TestId = {test_id};";

                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.ExecuteScalar();
                }
            }
        }

        public static void set_question_isMarked(bool isMarked, int test_id, int q_index)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //LogFileHandler.writeIntoFile("Database connection opened.");

                // SQL command to update the TestLesson
                string updateQuery = $"UPDATE TestsHistoryData " +
                $"SET QuestionIsMarked = {isMarked} " +
                $"WHERE TestId = {test_id} AND IndexOfQuestion = {q_index};";

                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.ExecuteScalar();
                }
            }
        }

        public static void set_question_UserAnswer(int userAnswer, int test_id, int q_index)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                //LogFileHandler.writeIntoFile("Database connection opened.");

                // SQL command to update the TestLesson
                string updateQuery = $"UPDATE TestsHistoryData " +
                $"SET UserAnswer = {userAnswer} " +
                $"WHERE TestId = {test_id} AND IndexOfQuestion = {q_index};";

                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.ExecuteScalar();
                }
            }
        }

        public static DataTable get_lessons_from_history_for_DataGridView()
        {
            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(TestHistoryFileHandler.connectionString))
            {
                connection.Open();
                string query = @"SELECT QuestionIsMarked,TestId,QuestionId,QuestionLesson,Date,IndexOfQuestion
                    FROM TestsHistoryData
                    WHERE QuestionLesson <> ''
                    ORDER BY TestId DESC, IndexOfQuestion DESC;"; // Replace 'YourTable' with your actual table name
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, connection);
                
                dataAdapter.Fill(table);
            }
            table.Columns["QuestionIsMarked"].ColumnName = "מועדפים";
            table.Columns["TestId"].ColumnName = "מס' תרגול";
            table.Columns["Date"].ColumnName = "תאריך";
            table.Columns["QuestionId"].ColumnName = "מס' מזהה שאלה";
            table.Columns["QuestionLesson"].ColumnName = "לקח";

            //// convert data_type of 'מועדפים' from bool to string
            // Get the old column
            DataColumn oldColumn = table.Columns["מועדפים"];
            // Create a new column with the same name but with a DataType of string
            DataColumn newColumn = new DataColumn("מועדפים2", typeof(string));
            // Add the new column to the DataTable (placing it right after the old column for clarity)
            int oldColumnIndex = oldColumn.Ordinal;
            table.Columns.Add(newColumn);
            newColumn.SetOrdinal(oldColumnIndex);
            // Copy the data from the old column to the new column (as strings)
            foreach (DataRow row in table.Rows)
                if (row[oldColumn] != DBNull.Value)
                    row[newColumn] = row[oldColumn].ToString();
            // Remove the old column
            table.Columns.Remove(oldColumn);
            table.Columns["מועדפים2"].ColumnName = "מועדפים";

            return table;
        }

        public static DataTable get_history_for_DataGridView()
        {
            DataTable table = new DataTable();

            using (SQLiteConnection connection = new SQLiteConnection(TestHistoryFileHandler.connectionString))
            {
                connection.Open();
                string query = @"SELECT TestIsMarked,TestId,TestName,TestType,Date
                    FROM TestsHistoryData
                    WHERE IndexOfQuestion = 0
                    ORDER BY TestId DESC;"; // Replace 'YourTable' with your actual table name
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, connection);

                dataAdapter.Fill(table);
            }

            table.Columns["TestId"].ColumnName = "מס' תרגול";
            table.Columns["TestName"].ColumnName = "שם התרגול";
            table.Columns["Date"].ColumnName = "תאריך";
            table.Columns["TestType"].ColumnName = "סוג תרגול";
            table.Columns["TestIsMarked"].ColumnName = "מועדפים";

            //// convert data_type of 'שאלות מועדפות' from bool to string
            // Get the old column
            DataColumn oldColumn = table.Columns["מועדפים"];
            // Create a new column with the same name but with a DataType of string
            DataColumn newColumn = new DataColumn("מועדפים2", typeof(string));
            // Add the new column to the DataTable (placing it right after the old column for clarity)
            int oldColumnIndex = oldColumn.Ordinal;
            table.Columns.Add(newColumn);
            newColumn.SetOrdinal(oldColumnIndex);
            // Copy the data from the old column to the new column (as strings)
            foreach (DataRow row in table.Rows)
                if (row[oldColumn] != DBNull.Value)
                    row[newColumn] = row[oldColumn].ToString();
            // Remove the old column
            table.Columns.Remove(oldColumn);
            table.Columns["מועדפים2"].ColumnName = "מועדפים";

            return table;
        }

        public static List<int> get_list_of_all_q_ids_in_history()
        {
            List<int> q_ids = new List<int>();
            string selectQuery = @"
                SELECT QuestionId 
                FROM TestsHistoryData;";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) // Loop through each row in the result set
                    {
                        int id = reader.GetInt32(0); // Get the integer value from the first column (index 0)

                        if (!q_ids.Contains(id))
                            q_ids.Add(id); // Add the ID to the list if it's not already present
                    }

                }
            }

            return q_ids;
        }
    }
}
