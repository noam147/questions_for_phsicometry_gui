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
        public static void OnCoreWebView21InitializationCompleted(WebView2 web, dbQuestionParmeters textInJson)
        {
            //update html content in here
            if (web.CoreWebView2 != null)
            {
                if(textInJson.json_content == null)
                {
                    return;
                }
                //this.m_currAnswer = this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer;
                string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_option_from_json(textInJson);
                // Load the HTML content into WebView2
                web.NavigateToString(htmlContent);
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static void OnCoreWebView2_colInitializationCompleted(WebView2 web, dbQuestionParmeters textInJson)
        {
            //update html content in here
            if (web.CoreWebView2 != null)
            {
                if (textInJson.json_content == null)
                {
                    return;
                }

                //this.m_currAnswer = this.questionDetails[this.m_indexOfCurrQuestion].rightAnswer;
                string htmlContent = OperationsAndOtherUseful.get_string_of_img_col_html(textInJson.json_content);
                // Load the HTML content into WebView2
                web.NavigateToString(htmlContent);
            }
            else
            {
                MessageBox.Show("WebView_col initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
