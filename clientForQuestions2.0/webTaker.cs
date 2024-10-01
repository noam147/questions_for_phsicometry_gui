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
                string htmlContent = OperationsAndOtherUseful.get_string_of_question_and_option_from_json(textInJson,OperationsAndOtherUseful.DO_NOT_MARK);
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

        public static void OnCoreWebView2_colDeleteContent(WebView2 web)
        {
            if(web == null)
            {
                return;
            }
            try
            {
                //update html content in here
                if (web.CoreWebView2 != null)
                {
                    // Load the HTML content into WebView2
                    //web.NavigateToString("<br>[אזור לקטעי קריאה ותרשימים, יכול לקחת להם זמן להיטען או שלשאלה אין אותם]");
                    web.NavigateToString("");
                }
                else
                {
                    MessageBox.Show("WebView_col initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(Exception e)
            {

            }

        }
        public static async void webView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            // Cast sender to WebView2
            var webView = sender as WebView2;

            // Check if the navigation was successful
            if (e.IsSuccess)
            {
                // JavaScript to modify the content
                string script = @"
                var elements = document.querySelectorAll('mjx-mspace');
                elements.forEach(function(element) {
                    var newElement = document.createElement('p');
                    element.parentNode.insertBefore(newElement, element.nextSibling);
                });
            ";

                // Execute the JavaScript in the WebView
                await webView.CoreWebView2.ExecuteScriptAsync(script);

                string script2 = @"
    document.addEventListener('DOMContentLoaded', function () {
        const mjxElements = document.querySelectorAll('mjx-mi');
        mjxElements.forEach(function (mjxMi) {
            const letters = mjxMi.querySelectorAll('mjx-utext');
            const newSpan = document.createElement('span');
            newSpan.style.direction = 'rtl'; // Set direction to right-to-left
            newSpan.style.display = 'inline-block'; // Ensure the span respects RTL directionality

            // Convert NodeList to array and reverse the letters
            const lettersArray = Array.from(letters).reverse();

            lettersArray.forEach(function (letter) {
                newSpan.appendChild(letter.cloneNode(true)); // Clone and append each letter
            });

            mjxMi.innerHTML = ''; // Clear existing content
            mjxMi.appendChild(newSpan); // Append the new span
        });
    });
";
                await webView.CoreWebView2.ExecuteScriptAsync(script2);
            }
        }
    }
}
