using System;
using System.Text.RegularExpressions;
namespace clientForQuestions2._0
{
    public class HtmlTextExtractor
    {
        public static string ExtractTextFromHtml(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
            {
                return string.Empty;
            }

            // Use Regex to remove HTML tags
            string plainText = Regex.Replace(html, "<.*?>", string.Empty);

            // Decode HTML entities (like &amp;, &lt;, etc.)
            plainText = System.Net.WebUtility.HtmlDecode(plainText);

            // Trim and return the plain text
            return plainText.Trim();
        }
    }
}