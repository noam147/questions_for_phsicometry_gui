using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System;

namespace clientForQuestions2._0
{

    internal class OperationsAndOtherUseful
    {
        public static decimal MIN_LEVEL = 0;
        public static decimal MAX_LEVEL = 10;
        public static int QUESTION_THAT_DID_NOT_ANSWERED = -1;
        public static int MARGIN_OF_HEIGHT = 20;//for text to not be cut down
        public static int DO_NOT_MARK = -1;
        public static int NOT_A_REAL_TEST_ID = -1;
        public static int WITHOUT_TIMER = -3;
        public static int SKIPPED_Q = -2; // when the user didnt answer
        public static string img_max_hight = "200px";
        public static Dictionary<string, List<string>> topicsdict = new Dictionary<string, List<string>>
        {
            {

                "בחירת כל הנושאים", new List<string>
                {
                    "אותיות וספרות",
                    "פעולות מומצאות",
                    "הצבת תשובות",
                    "הצבת מספרים",
                    "דמיון צורות",
                    "אנליטית",
                    "תלת-ממד",
                    "פיתגורס",
                    "שטחים",
                    "קווים וזוויות",
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים",
                    "צירופים",
                    "הסתברות",
                    "חלוקה",
                    "תנועה",
                    "הספק",
                    "ממוצע",
                    "כלליות 1",
                    "כלליות 2",
                    "אחוזים",
                    "יחס",
                    "טווחים",
                    "התנהגות אלגברית",
                    "חיובי שלילי וערך מוחלט",
                    "שלמים",
                    "משוואות",
                    "ביטויים",
                    "אי-שוויון",
                    "עצרת",
                    "אנלוגיות",
                    "שאלות פסקה",
                    "השלמת משפטים מילולי",
                    "עריכת ניסוי",
                    "משמעות מילולית",
                    "כללים וסידורים",
                    "Restatements",
                    "אוצר-מילים",
                    "Sentence Completions"
                }
            },
            {
                "חשיבה כמותית", new List<string>
                {
                    "אותיות וספרות",
                    "פעולות מומצאות",
                    "הצבת תשובות",
                    "הצבת מספרים",
                    "דמיון צורות",
                    "אנליטית",
                    "תלת-ממד",
                    "פיתגורס",
                    "שטחים",
                    "קווים וזוויות",
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים",
                    "צירופים",
                    "הסתברות",
                    "חלוקה",
                    "תנועה",
                    "הספק",
                    "ממוצע",
                    "כלליות 1",
                    "כלליות 2",
                    "אחוזים",
                    "יחס",
                    "טווחים",
                    "התנהגות אלגברית",
                    "חיובי שלילי וערך מוחלט",
                    "שלמים",
                    "משוואות",
                    "ביטויים",
                    "אי-שוויון",
                    "עצרת"
                }
            },
            {
                "חשיבה מילולית", new List<string>
                {
                    "אנלוגיות",
                    "שאלות פסקה",
                    "השלמת משפטים מילולי",
                    "עריכת ניסוי",
                    "משמעות מילולית",
                    "כללים וסידורים"
                }
            },
            {
                "אנגלית", new List<string>
                {
                    "Restatements",
                    "אוצר-מילים",
                    "Sentence Completions"
                }
            },
            {
                "הצבה", new List<string>
                {
                    "הצבת תשובות",
                    "הצבת מספרים"
                }
            },
            {
                "גיאומטריה", new List<string>
                {
                    "דמיון צורות",
                    "אנליטית",
                    "תלת-ממד",
                    "פיתגורס",
                    "שטחים",
                    "קווים וזוויות",
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים"
                }
            },
            {
                "מצולעים", new List<string>
                {
                    "מצולעים",
                    "מרובעים",
                    "מעגלים",
                    "משולשים"
                }
            },
            {
                "בעיות מילוליות", new List<string>
                {
                    "צירופים",
                    "הסתברות",
                    "חלוקה",
                    "תנועה",
                    "הספק",
                    "ממוצע",
                    "כלליות 1",
                    "כלליות 2",
                    "אחוזים",
                    "יחס",
                    "טווחים"
                }
            },
            {
                "התנהגות אלגברית", new List<string>
                {
                    "התנהגות אלגברית",
                    "חיובי שלילי וערך מוחלט",
                    "שלמים"
                }
            },
            {
                "משוואות", new List<string>
                {
                    "משוואות",
                    "ביטויים",
                    "אי-שוויון",
                    "עצרת"
                }
            },
            {
                "הבנה והסקה", new List<string>
                {
                    "שאלות פסקה",
                    "השלמת משפטים מילולי",
                    "עריכת ניסוי",
                    "משמעות מילולית",
                    "כללים וסידורים"
                }
            },
            {
                "אוצר מילים", new List<string>
                {
                    "אוצר-מילים",
                    "Sentence Completions"
                }
            }
        };
        public static Dictionary<string, List<int>> title2colIds = new Dictionary<string, List<int>>
        {
            { "הסקה מתרשים", new List<int> { 45, 47, 48, 49, 50, 55, 63, 64, 65, 70, 71, 72, 73, 74, 78, 79, 80, 81, 82, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 127, 128, 129, 130, 132, 133, 134, 135 } },
            { "קטע קריאה", new List<int> { 37, 38, 39, 40, 56, 57, 58, 60, 61, 62, 75, 76, 77, 98, 119, 120, 121, 122, 123, 124, 125, 126 } },
            // only collections who have 6 questions
            { "קטע קריאה פרקים", new List<int> { 39, 40, 56, 57, 58, 60, 61, 62, 75, 77, 98, 119, 120, 123, 124, 125, 126 } },
            { "Reading Comprehension", new List<int> { 41, 42, 43, 44, 46, 51, 52, 53, 54, 59, 66, 67, 68, 69, 83, 84, 85, 86, 107, 108 } } //  109, 110, 111, 112, 113, 114, 115, 116, 136, 117
        };
        public static Dictionary<int, List<int>> colId2qIds = new Dictionary<int, List<int>>()
{
    { 37, new List<int> { 7136, 7137, 7138 } },
    { 38, new List<int> { 7140, 7143, 7145 } },
    { 39, new List<int> { 7149, 7152, 7153, 7154, 7155, 7159 } },
    { 40, new List<int> { 7173, 7174, 7175, 7176, 7177, 7178 } },
    { 41, new List<int> { 7179, 7180, 7181, 7182, 7183 } },
    { 42, new List<int> { 7184, 7185, 7186, 7187, 7188 } },
    { 43, new List<int> { 7189, 7190, 7195, 7200, 7205 } },
    { 44, new List<int> { 7232, 7233, 7258, 7259, 7260 } },
    { 45, new List<int> { 7261, 7262, 7263, 10315 } },
    { 46, new List<int> { 7269, 7270, 7271, 7284, 7285 } },
    { 47, new List<int> { 7265, 7266, 7267, 7268 } },
    { 48, new List<int> { 7272, 7273, 7274, 7275 } },
    { 49, new List<int> { 7276, 7277, 7279, 10864 } },
    { 50, new List<int> { 7280, 7281, 7282, 7283 } },
    { 51, new List<int> { 7313, 7314, 7315, 7316, 7317 } },
    { 52, new List<int> { 7323, 7327, 7333, 7337, 7341 } },
    { 53, new List<int> { 7345, 7359, 7360, 7361, 7362 } },
    { 54, new List<int> { 7398, 7401, 7403, 7404, 7405 } },
    { 55, new List<int> { 7397, 7399, 7400, 7402 } },
    { 56, new List<int> { 7406, 7407, 7408, 7409, 7410, 7411 } },
    { 57, new List<int> { 7412, 7413, 7414, 7415, 7416, 7417 } },
    { 58, new List<int> { 7418, 7419, 7420, 7421, 7422, 7423 } },
    { 59, new List<int> { 7424, 7425, 7460, 7461, 7462 } },
    { 60, new List<int> { 7430, 7431, 7432, 7433, 7434, 7435 } },
    { 61, new List<int> { 7436, 7437, 7438, 7439, 7440, 7441 } },
    { 62, new List<int> { 7442, 7443, 7456, 7457, 7458, 7459 } },
    { 63, new List<int> { 7444, 7445, 7446, 7447 } },
    { 64, new List<int> { 7448, 7449, 7450, 7451 } },
    { 65, new List<int> { 7452, 7453, 7454, 7455 } },
    { 66, new List<int> { 7463, 7464, 7465, 7466, 7467 } },
    { 67, new List<int> { 7468, 7469, 7470, 7471, 7472 } },
    { 68, new List<int> { 7473, 7474, 7475, 7476, 7477 } },
    { 69, new List<int> { 7478, 7479, 7480, 7481, 7482 } },
    { 70, new List<int> { 7483, 7484, 7485, 7486 } },
    { 71, new List<int> { 7487, 7488, 7489, 7490 } },
    { 72, new List<int> { 7491, 7492, 7493, 7494 } },
    { 73, new List<int> { 7495, 7496, 7497, 7498 } },
    { 74, new List<int> { 7499, 7500, 7501, 7502 } },
    { 75, new List<int> { 7516, 7517, 7518, 7519, 7520, 7521 } },
    { 76, new List<int> { 7522, 7523, 7524, 7525, 7526 } },
    { 77, new List<int> { 7528, 7529, 7532, 7534, 7536, 7537 } },
    { 78, new List<int> { 7530, 7531, 7533, 7535 } },
    { 79, new List<int> { 7538, 7539, 7540, 7541 } },
    { 80, new List<int> { 7542, 7543, 7544, 7545 } },
    { 81, new List<int> { 7546, 7547, 7548, 7549 } },
    { 82, new List<int> { 7550, 7551, 7552, 7553 } },
    { 83, new List<int> { 7554, 7555, 7556, 7557, 7558 } },
    { 84, new List<int> { 7559, 7560, 7561, 7562, 7563 } },
    { 85, new List<int> { 7564, 7565, 7566, 7567, 7568 } },
    { 86, new List<int> { 7569, 7570, 7571, 7572, 7573 } },
    { 87, new List<int> { 7582, 7583, 7584, 7585 } },
    { 88, new List<int> { 7586, 7587, 7588, 7589 } },
    { 89, new List<int> { 7595, 7596, 7598, 7599, 10934, 10935 } },
    { 90, new List<int> { 7601, 7603, 7604, 7605 } },
    { 91, new List<int> { 7606, 7607, 7608, 7609 } },
    { 92, new List<int> { 7610, 7611, 7612, 7613 } },
    { 93, new List<int> { 7614, 7615, 7616, 7617 } },
    { 94, new List<int> { 7618, 7619, 7620, 7621, 7622 } },
    { 95, new List<int> { 7637, 7638, 7641, 7642, 7643 } },
    { 96, new List<int> { 7644, 7645, 7646, 7647, 7648 } },
    { 97, new List<int> { 7649, 7650, 7651, 7652, 7653 } },
    { 98, new List<int> { 7690, 7692, 7693, 7694, 7695, 7696 } },
    { 99, new List<int> { 8740, 8741, 8742, 8743, 8744 } },
    { 100, new List<int> { 8746, 8747, 8748, 8749, 8750 } },
    { 101, new List<int> { 8753, 8754, 8755, 8756, 8757 } },
    { 102, new List<int> { 8758, 8759, 8760, 8761, 8762 } },
    { 103, new List<int> { 8763, 8764, 8765, 8766, 8767 } },
    { 104, new List<int> { 8768, 8769, 8770, 8771, 8772 } },
    { 105, new List<int> { 8773, 8774, 8775, 8776, 8777 } },
    { 106, new List<int> { 8778, 8779, 8780, 8781, 8782 } },
    { 107, new List<int> { 8783, 8784, 8785, 8786, 8788 } },
    { 108, new List<int> { 8789, 8790, 8791, 8792, 8793 } },
    //{ 109, new List<int> { 8849, 8850, 8851, 8852, 8853 } },
    //{ 110, new List<int> { 8854, 8855, 8856, 8857, 8858 } },
    //{ 111, new List<int> { 8859, 8860, 8861, 8862, 8863 } },
    //{ 112, new List<int> { 8864, 8865, 8866, 8867, 8868 } },
    //{ 113, new List<int> { 8869, 8870, 8871, 8872, 8873 } },
    //{ 114, new List<int> { 8874, 8875, 8876, 8877, 8878 } },
    //{ 115, new List<int> { 8879, 8880, 8881, 8882, 8883 } },
    //{ 116, new List<int> { 8884, 8885, 8886, 8887, 8888 } },
    //{ 117, new List<int> { 8889, 8890, 8891, 8892, 8893 } },
    { 119, new List<int> { 8918, 8919, 8920, 8921, 8922, 8923 } },
    { 120, new List<int> { 8924, 8925, 8926, 8927, 8928, 8929 } },
    { 121, new List<int> { 8931, 8932, 8933, 8935, 8936 } },
    { 122, new List<int> { 8934 } },
    { 123, new List<int> { 8938, 8939, 8940, 8941, 8942, 8945 } },
    { 124, new List<int> { 8966, 8967, 8969, 8970, 8972, 8973 } },
    { 125, new List<int> { 8975, 8976, 8977, 8978, 8979, 8980 } },
    { 126, new List<int> { 8981, 8982, 8983, 8984, 8985, 8986 } },
    { 127, new List<int> { 10280, 10281, 10282, 10283, 10336 } },
    { 128, new List<int> { 10284, 10285, 10286, 10287 } },
    { 129, new List<int> { 10288, 10289, 10290, 10291 } },
    { 130, new List<int> { 10292, 10293, 10294, 10295 } },
    { 132, new List<int> { 10302, 10303, 10304, 10316 } },
    { 133, new List<int> { 10305, 10306, 10307, 10308 } },
    { 134, new List<int> { 10391, 10392, 10393, 10394, 10395 } },
    { 135, new List<int> { 10401, 10402, 10403, 10404 } },
    //{ 136, new List<int> { 11157, 11158, 11159, 11160, 11161 } }
};


        public static string right2left(string s)
        {
            // aligned to the right
            string htmlContent = "<head> <style> body { text-align: right; /* Right-align all text */ } </style> </head> " + "<div style=\"direction: rtl; text-align: right;\">" + s + "</div>";
            
            // for hebrew words in MathML (inside <mi> </mi>)
            //
            // Regular expression to match Hebrew characters (Unicode)
            string hebrewUnicodePattern = @"[\u0590-\u05FF]"; // Unicode range for Hebrew characters

            // Regular expression to match HTML entities for Hebrew letters
            string hebrewHtmlEntitiesPattern = @"&#(1488|1489|1490|1491|1492|1493|1494|1495|1496|1497|1498|1499|1500|1501|1502|1503|1504|1505|1506|1507|1508|1509|1510|1511|1512|1513|1514);"; // Matches HTML entities for Hebrew letters

            // Match <mi> tags that may contain Hebrew characters (Unicode or HTML entities)
            string pattern = @"<mi>(.*?)<\/mi>";
            Regex regex = new Regex(pattern);

            htmlContent = regex.Replace(htmlContent, match =>
            {
                string content = match.Groups[1].Value;

                // Check if the content contains Hebrew characters (Unicode or HTML entities)
                if (Regex.IsMatch(content, hebrewUnicodePattern) || Regex.IsMatch(content, hebrewHtmlEntitiesPattern))
                {
                    return $"<mi style=\"direction: rtl; text-align: right;\">{content}</mi>";
                }
                return match.Value; // Return original match if no Hebrew characters or entities are found
            });

            return htmlContent;
        }

        public static string left2right(string s)
        {
            // aligned to the left
            string htmlContent = "<head> <style> body { text-align: left; /* Left-align all text */ } </style> </head> " + "<div style=\"direction: ltr; text-align: left;\">" + s + "</div>";
            return htmlContent;
        }

        public static string get_time_mmss_fromseconds(int sec)
        {
            if (sec == -1)
                return "-1";

            if (sec % 60 < 10)
                return $"{sec / 60}:0{sec % 60}";
            else
                return $"{sec / 60}:{sec % 60}";
        }

        public static int get_col_id_of_question(JToken json)
        {
            if ( ((JArray)json["collections"]).Count == 0)
                return 0;
            return (int) json["collections"][0]["id"];
        }

        public static string get_string_of_img_col_html(JToken json)
        {
            //this should be in a separate file
            if (json == null)
            {
                return "";
            }
            if (!json.HasValues)
            {
                return "";
            }
            string cover = json["collections"][0]["cover"].ToString();
            string img_path = "https://lmsapi.kidum-me.com/storage/";
            string file_path = img_path + json["collections"][0]["file"]["file_path"].ToString();
            string fullImg = $"<img src=\"{file_path}\" alt=\"Question Image\" style=\" max-width:100%; height:auto;\">";

            if (OperationsAndOtherUseful.title2colIds["Reading Comprehension"].Contains((int)json["collections"][0]["id"]))
                return left2right(cover + fullImg);
            return right2left(cover + fullImg);
        }

        private static string get_string_of_img_html(JToken json)
        {
            //this should be in a separate file
            if (json == null)
            {
                return "";
            }
            if (!json.HasValues)
            {
                return "";
            }
            string img_path = "https://lmsapi.kidum-me.com/storage/";
            string file_path = img_path + json["file_path"].ToString();
            string fullImg = $"<img src=\"{file_path}\" alt=\"Question Image\" style=\"max-height:{img_max_hight}; width:auto;\">";
            return fullImg;
        }

        public static List<string> addNumberToQuestions(string q1, string q2, string q3, string q4)
        {
            const int lenOfString = 3;
            List<string> list = new List<string> { q1, q2, q3, q4 };
            int currIndex = -1;
            int secondsIndex = -1;
            //id = 3426
            //for gpt:
            //this is problomatic:
            //<p style="text-align: justify;">28</p>
            //this is not prblomatic:
            //'<p><span style="font-weight: 400;">זוגי</span></p>'


            //this method sucseed
            for (int i = 0; i < list.Count; i++)
            {
                currIndex = list[i].IndexOf("<p>");

                if (currIndex == -1)
                {
                    currIndex = list[i].IndexOf("<p ");
                    secondsIndex = list[i].IndexOf(">");//find the closing tag of p
                    list[i] = $"<p>({i + 1})\t" + list[i].Substring(secondsIndex + 1, list[i].Length - (secondsIndex + 1));
                }
                else
                {
                    string startOfQuestion = list[i].Substring(0, currIndex);
                    list[i] = startOfQuestion+$"<p>({i + 1})\t" + list[i].Substring(currIndex + lenOfString, list[i].Length - currIndex - lenOfString);
                }
            }
            return list;
        }


        public static string get_string_of_question_and_option_from_json(dbQuestionParmeters qp, int userAnswer)
        {
            //optionToMarkRed = user answer
            bool isTextOptions = ((JArray)qp.json_content["options"]).Count != 0;
            int optionToMarkGreen = qp.rightAnswer;
            string question = qp.json_content["question"].ToString();
            string option1;
            string option2;
            string option3;
            string option4;
            List<string> listOfOptions = new List<string>();
            if (isTextOptions)
            {
                option1 = qp.json_content["options"][0]["text"].ToString();
                option2 = qp.json_content["options"][1]["text"].ToString();
                option3 = qp.json_content["options"][2]["text"].ToString();
                option4 = qp.json_content["options"][3]["text"].ToString();
                listOfOptions = addNumberToQuestions(option1, option2, option3, option4);
                //if the user answsered go to this - mark an answer
                if (userAnswer != DO_NOT_MARK)
                {
                    listOfOptions[optionToMarkGreen - 1] = $"<div style = \"background-color: lightgreen;display: inline-block;\">{listOfOptions[optionToMarkGreen - 1]}</div><br>";
                    //green will always be
                    if (userAnswer != optionToMarkGreen && userAnswer != SKIPPED_Q)//if the user got right - do not need to mark in red
                    {
                        listOfOptions[userAnswer - 1] = $"<div style = \"background-color: red;display: inline-block;\">{listOfOptions[userAnswer - 1]}</div><br>";
                    }
                }
            }
            else
            {
                option1 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][0]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-height:{img_max_hight}; width:auto;\">";
                option2 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][1]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-height:{img_max_hight}; width:auto;\">";
                option3 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][2]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-height:{img_max_hight}; width:auto;\">";
                option4 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][3]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-height:{img_max_hight}; width:auto;\">";
                listOfOptions = new List<string> { option1, option2, option3, option4 };

                if (userAnswer != DO_NOT_MARK)
                {
                    listOfOptions[optionToMarkGreen - 1] = $"<div style = \"background-color: lightgreen;display: inline-block;\"> ({optionToMarkGreen})\t</div> <br>" + listOfOptions[optionToMarkGreen - 1];
                    //green will always be
                    if (userAnswer != optionToMarkGreen && userAnswer != SKIPPED_Q)//if the user got right - do not need to mark in red
                    {
                        listOfOptions[userAnswer - 1] = $"<div style = \"background-color: red;display: inline-block;\"> ({userAnswer})\t</div> <br>" + listOfOptions[userAnswer - 1];
                    }
                }
                for (int i = 0; i < listOfOptions.Count; i++)
                {
                    if (listOfOptions[i].StartsWith("<img"))
                        listOfOptions[i] = $"<div <p>({i + 1})\t</p> </div> <br>" + listOfOptions[i];
                }
            }







            string finalOptionsString = listOfOptions[0] + listOfOptions[1] + listOfOptions[2] + listOfOptions[3];
            if (!isTextOptions)
                finalOptionsString = listOfOptions[0] + "<br>" + listOfOptions[1] + "<br>" + listOfOptions[2] + "<br>" + listOfOptions[3];
            //if question is in english
            if (!isQuestionInHebrew(qp.category))
                return left2right(question + get_string_of_img_html(qp.json_content["image"]) + "<br><br>" + finalOptionsString);

            return right2left(question + get_string_of_img_html(qp.json_content["image"]) + "<br><br>" + finalOptionsString);
        }

        public static string get_explanation(dbQuestionParmeters qp)
        {
            string answer = qp.json_content["solving_explanation"].ToString();
            var img = qp.json_content["explanation_image"];
            return right2left(answer + get_string_of_img_html(img));
        }
        public static string get_string_of_question_and_explanation(dbQuestionParmeters qp, int clientanswer)
        {
            string line = "<div style=\"top: 50%; left: 0; width: 100vw; height: 1px; background-color: lightgray;\"></div>\r\n<br><p style=\"font-size: 18px; font-weight: bold; direction: rtl; text-decoration: underline;\">הסבר:<p>";

            return get_string_of_question_and_option_from_json(qp, clientanswer) + right2left(line) + get_explanation(qp);
        }
        private static bool isQuestionInHebrew(string category)
        {
            if (category == "Restatements" || category == "Reading Comprehension" || category == "אוצר-מילים" || category == "Sentence Completions")
            {
                return false;
            }
            return true;
        }



        public static string getMacAdd()
        {
            var macAddress = NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault();
            return macAddress;
        }
        public static string getEncodedMacAdd(string mac)
        {
            string hashedMac = ComputeSha256Hash(mac + "42chochai");
            string secondHashed = ComputeSha256Hash(hashedMac + "sig69ma");
            return "42"+hashedMac+ secondHashed + "1a";
        }

        //func to encode from gpt
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256 instance
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convert the input string to a byte array and compute the hash
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert the byte array to a hexadecimal string
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // חשיבה מילולית
        public static List<dbQuestionParmeters> sendChapter_hebrew_Questions()
        {
            Random random = new Random();

            //get אנלוגיות
            List<dbQuestionParmeters> anlog_qs = sqlDb.get_n_questions_from_specofic_category(6, "אנלוגיות"); // qs of normal qs
            anlog_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get הבנה והסקה
            List<dbQuestionParmeters> havana_qs = sqlDb.get_n_questions_from_arr_of_categorys(11, OperationsAndOtherUseful.topicsdict["הבנה והסקה"]); // qs of normal qs
            havana_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get קטע קריאה
            List<int> colIds = OperationsAndOtherUseful.title2colIds["קטע קריאה פרקים"]; // get all ids of קטע קריאה
            int col_id = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
                                                            // qs of קטע קריאה
            List<dbQuestionParmeters> col_qs = new List<dbQuestionParmeters>();
            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id])
            {
                col_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }
            anlog_qs.AddRange(havana_qs);
            anlog_qs.AddRange(col_qs);
            return anlog_qs;

        }

        /// <summary>
        /// This function gets multiple chapters of math without graph.
        /// </summary>
        /// <param name="amount">The amount of chapters (each chapter 20 questions).</param>
        /// <returns>list of -> lists of questions details - all the sub lists are chapters.</returns>
        public static List<List<dbQuestionParmeters>> getMultipleExrecisesOfMath_without_graph(int amount,List<int> idsOfPreviousQuestions)
        {
            return null;
            List<List<dbQuestionParmeters >> all_questions = new List<List<dbQuestionParmeters>> ();
            for (int i = 0; i < amount; i++)
            {
                all_questions.Add(sendChapter_math_Questions_without_graph(idsOfPreviousQuestions));
            }
            return all_questions;
        }
        /// <summary>
        /// This function gets one chapter of math without graph.
        /// </summary>
        /// <returns>list of questions details</returns>
        public static List<dbQuestionParmeters> sendChapter_math_Questions_without_graph(List<int> idsOfPreviousQuestions)
        {

            List<String> topics = OperationsAndOtherUseful.topicsdict["חשיבה כמותית"];
                questionsDifficultyLevel currentDifficultyLevel = new questionsDifficultyLevel();
                currentDifficultyLevel.minlevel = 0m;
                currentDifficultyLevel.maxLevel = 4m;
            //first phaze of question 1- 6
                List<dbQuestionParmeters> firstPhase = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel_Without_arr_of_q_ids(6, topics, currentDifficultyLevel,idsOfPreviousQuestions);
                firstPhase.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));

                currentDifficultyLevel.minlevel = 4.5m;
                currentDifficultyLevel.maxLevel = 6.5m;
            //second phaze of question 7 - 13
            List<dbQuestionParmeters> secondPhaze = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel_Without_arr_of_q_ids(7, topics, currentDifficultyLevel, idsOfPreviousQuestions);
                secondPhaze.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));

                currentDifficultyLevel.minlevel = 7;
                currentDifficultyLevel.maxLevel = 10;
            //third phaze of question 13 - 20
            List<dbQuestionParmeters> thirdPhaze = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel_Without_arr_of_q_ids(7, topics, currentDifficultyLevel, idsOfPreviousQuestions);
                thirdPhaze.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));

                List<dbQuestionParmeters> allQuestions = new List<dbQuestionParmeters>();
            //get the values in lists to the final list of all questions together
                foreach (var item in firstPhase)
                {
                    allQuestions.Add(item);
                }
                foreach (var item in secondPhaze)
                {
                    allQuestions.Add(item);
                }
                foreach (var item in thirdPhaze)
                {
                    allQuestions.Add(item);
                }
            
            return allQuestions;
        }

        // חשיבה כמותית
        public static List<dbQuestionParmeters> sendChapter_math_Questions()
        {
            Random random = new Random();

            List<String> topics = OperationsAndOtherUseful.topicsdict["חשיבה כמותית"];

            List<int> colIds = OperationsAndOtherUseful.title2colIds["הסקה מתרשים"]; // get all ids of tarshim
            int col_id = colIds[random.Next(colIds.Count)]; // choose rand collection of the category

            // qs of tarshim
            List<dbQuestionParmeters> col_qs = new List<dbQuestionParmeters>();
            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id])
            {
                col_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }

            //List<dbQuestionParmeters> normal_qs = sqlDb.get_n_questions_from_arr_of_categorys(20 - col_qs.Count, topics); // qs of normal qs

            // sort by "difficulty_level", from easiest to hardest
            //normal_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // difficulty_level of the tarshim to determine if it would be at the start, middle or end. 
            float col_diffLvl = (float)col_qs[0].json_content["collections"][0]["difficulty_level"];

            List<dbQuestionParmeters> questions = new List<dbQuestionParmeters>();

            // add to start
            if (col_diffLvl >= 0 && col_diffLvl <= 4.5)
            {
                questionsDifficultyLevel difflvl = new questionsDifficultyLevel();
                difflvl.minlevel = 4.5m;
                difflvl.maxLevel = 10.0m;

                List<dbQuestionParmeters> normal_qs = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(20 - col_qs.Count, topics, difflvl);
                normal_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));

                col_qs.AddRange(normal_qs);
                questions = col_qs;
            }
            // add to middle
            if (col_diffLvl > 4.5 && col_diffLvl <= 6.5)
            {
                questionsDifficultyLevel difflvl = new questionsDifficultyLevel();
                difflvl.minlevel = 0.0m;
                difflvl.maxLevel = 4.5m;

                List<dbQuestionParmeters> normal_qs = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(8, topics, difflvl);
                normal_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));

                questions.AddRange(normal_qs);
                questions.AddRange(col_qs); //after the 8th element

                difflvl.minlevel = 6.5m;
                difflvl.maxLevel = 10.0m;

                normal_qs = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(12 - col_qs.Count, topics, difflvl);
                normal_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));

                questions.AddRange(normal_qs);
            }
            // add to end
            if (col_diffLvl > 6.5 && col_diffLvl <= 10)
            {
                questionsDifficultyLevel difflvl = new questionsDifficultyLevel();
                difflvl.minlevel = 0.0m;
                difflvl.maxLevel = 7.0m;

                List<dbQuestionParmeters> normal_qs = sqlDb.get_n_questions_from_arr_of_categorysWithDiffcultyLevel(20 - col_qs.Count, topics, difflvl);
                normal_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));

                normal_qs.AddRange(col_qs);
                questions = normal_qs;
            }

            return questions;
        }

        //אנגלית
        public static List<dbQuestionParmeters> sendChapter_english_Questions()
        {
            Random random = new Random();

            //get Sentence Completions
            List<dbQuestionParmeters> Sentence_Completions_qs = sqlDb.get_n_questions_from_specofic_category(8, "Sentence Completions"); // qs of normal qs
            Sentence_Completions_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get Restatements
            List<dbQuestionParmeters> Restatements_qs = sqlDb.get_n_questions_from_specofic_category(4, "Restatements"); // qs of normal qs
            Restatements_qs.Sort((x, y) => ((float)x.json_content["difficulty_level"]).CompareTo((float)y.json_content["difficulty_level"]));
            // get קטע קריאה
            List<int> colIds = OperationsAndOtherUseful.title2colIds["Reading Comprehension"]; // get all ids of קטע קריאה
            int col_id1 = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
            int col_id2 = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
            while (col_id1 == col_id2) // so there will be different texts
                col_id2 = colIds[random.Next(colIds.Count)]; // choose rand collection of the category

            // qs of קטע קריאה
            List<dbQuestionParmeters> col1_qs = new List<dbQuestionParmeters>();
            List<dbQuestionParmeters> col2_qs = new List<dbQuestionParmeters>();

            LogFileHandler.writeIntoFile($"try to accsess col ids, id1: {col_id1}; id2: {col_id2}");

            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id1])
            {
                col1_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }
            foreach (int col_qID in OperationsAndOtherUseful.colId2qIds[col_id2]) // TODO the easier text first
            {
                col2_qs.Add(sqlDb.get_question_based_on_id(col_qID));
            }


            Sentence_Completions_qs.AddRange(Restatements_qs);

            // if col2 is harder than col1
            if ((float)col2_qs[0].json_content["collections"][0]["difficulty_level"] > (float)col1_qs[0].json_content["collections"][0]["difficulty_level"])
            {
                for (int i = 0; i < col1_qs.Count; i++)
                    col1_qs[i].json_content["collections"][0]["cover"] += "<p style=\"font-size: 18px; font-style: italic;\">Text I</p>";

                for (int i = 0; i < col2_qs.Count; i++)
                    col2_qs[i].json_content["collections"][0]["cover"] += "<p style=\"font-size: 18px; font-style: italic;\">Text II</p>";

                Sentence_Completions_qs.AddRange(col1_qs);
                Sentence_Completions_qs.AddRange(col2_qs);
            }
            // if col1 is harder than col2
            else
            {
                for (int i = 0; i < col2_qs.Count; i++)
                    col2_qs[i].json_content["collections"][0]["cover"] += left2right("<p style=\"font-size: 18px; font-style: italic;\">Text I</p>");

                for (int i = 0; i < col1_qs.Count; i++)
                    col1_qs[i].json_content["collections"][0]["cover"] += left2right("<p style=\"font-size: 18px; font-style: italic;\">Text II</p>");

                Sentence_Completions_qs.AddRange(col2_qs);
                Sentence_Completions_qs.AddRange(col1_qs);
            }

            return Sentence_Completions_qs;
        }
        public List<int> getListsOfIdBasedOnQuestions(List<dbQuestionParmeters> questions)
        {
            List<int> idOfQuestions = new List<int>();
            foreach(var question in questions) 
            { idOfQuestions.Add(question.questionId); }
            return idOfQuestions;
        }
    }
}