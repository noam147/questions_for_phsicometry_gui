using System;
using System.IO;

//this should be final!
namespace clientForQuestions2._0
{
    internal class LogFileHandler
    {
        private static string fileName = "logFile.log";
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
        public static void writeIntoFile(string text)
        {
            string formattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            using (StreamWriter sw = new StreamWriter(filePath, append: true))
            {
                sw.WriteLine(formattedTime+": "+text);
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
