using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace clientForQuestions2._0
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            LogFileHandler.openFile();
            SettingsFileHandler.openFile();

            TestHistoryFileHandler.openFile();
            OperationsAndOtherUseful.hideFiles();
            IdsToFile.openDir();
            //SettingsFileHandler.writeSettingsIntoFile(new Settings { seconds = 30,amount = 7,maxLevel = 10,minLevel = 4,minuets = 10,withoutFeedback=0 });
            //LogFileHandler.ClearFileContent();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var main = new PasswordPage();
            main.FormClosed += new FormClosedEventHandler(FormClosed);
            main.Show();

            Application.Run();
        }

        static void FormClosed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= FormClosed;
            if (Application.OpenForms.Count == 0) Application.ExitThread();
            else Application.OpenForms[0].FormClosed += FormClosed;
        }
    }
}
