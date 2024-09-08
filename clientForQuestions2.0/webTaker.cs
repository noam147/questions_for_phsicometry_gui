using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace clientForQuestions2._0
{
    internal class webTaker
    {
        public static void OnCoreWebView2InitializationCompleted(WebView2 web, JToken textInJson)
        {
            //update html content in here
            if (web.CoreWebView2 != null)
            {
                if(textInJson == null)
                {
                    return;
                }
                //this.m_currAnswer = this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer;
                string htmlContent = sqlDb.get_string_of_question_and_option_from_json(textInJson);
                // Load the HTML content into WebView2
                web.NavigateToString(htmlContent);
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
