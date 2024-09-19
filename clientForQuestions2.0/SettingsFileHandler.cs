using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientForQuestions2._0
{
    public struct Settings
    {
        public int isExsist;//1 - yes;2 - no
        public int withoutFeedback;
        public int minLevel;
        public int maxLevel;
        public int amount;
        public int minuets;
        public int seconds;
    }

    internal class SettingsFileHandler
    {
        private static string fileName = "settingsFile.setting";
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
                    // Optional: Write to the file here if needed
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File already exists: {ex.Message}");
            }
        }
        public static void writeSettingsIntoFile(Settings settingsForQuestions)
        {
            ClearFileContent();//add new settings
            //if usesr dicide to save the settings
            string final = "";
            Dictionary<string, int> settingsDict = new Dictionary<string, int>();

            settingsDict.Add("MinLevel", settingsForQuestions.minLevel); 
            settingsDict.Add("MaxLevel", settingsForQuestions.maxLevel); 
            settingsDict.Add("amount", settingsForQuestions.amount); 
            settingsDict.Add("minuets", settingsForQuestions.minuets);
            settingsDict.Add("hours", settingsForQuestions.seconds);
            settingsDict.Add("isExsist", settingsForQuestions.isExsist);
            settingsDict.Add("withoutFeedback", settingsForQuestions.withoutFeedback);
            JToken json = JToken.FromObject(settingsDict);
            final = json.ToString();
            using (StreamWriter sw = new StreamWriter(filePath, append: true))
            {
                sw.WriteLine(final);
            }
        }
        public static Settings getSettingsFromFile()
        {
            string fileContent = File.ReadAllText(filePath);
            Settings settings = new Settings();
            JToken json;
            try
            {
                json = JToken.Parse(fileContent);
            }
            catch
            {
                settings.isExsist = NOT_EXSIST;
                return settings;
            }
            settings.isExsist = EXSIST;
            try
            {
                settings.minLevel = (int)json["MinLevel"];
                settings.maxLevel = (int)json["MaxLevel"];
                settings.amount = (int)json["amount"];
                settings.seconds = (int)json["hours"];
                settings.minuets = (int)json["minuets"];
                settings.withoutFeedback = (int)json["withoutFeedback"];
            }
            catch
            {
                settings.isExsist=NOT_EXSIST;
                return settings;
            }
            return settings;
           
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
