using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            //var c = sqlDb.get_all_data();
            //var d = sqlDb.get_n_questions_from_specofic_category(3, "הסתברות");
            //List<string> list = new List<string>();
            //list.Add("הסתברות");
            //list.Add("אנלוגיות");
            //var e = sqlDb.get_n_questions_from_arr_of_categorys(4,list);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var main = new menuPage();
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
