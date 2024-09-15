using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace clientForQuestions2._0
{

    internal class OperationsAndOtherUseful
    {
        public static int MIN_LEVEL = 0;
        public static int MAX_LEVEL = 15;
        public static int QUESTION_THAT_DID_NOT_ANSWERED = -1;
        public static int DO_NOT_MARK = -1;
        public static int SKIPPED_Q = -2; // when the user didnt answer
        public static string right2left(string s)
        {
            return "<div style=\"direction: rtl\">" + s + "</div>";
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
            string img_path = "https://lmsapi.kidum-me.com/storage/";
            string file_path = img_path + json["collections"][0]["file"]["file_path"].ToString();
            string fullImg = $"<img src=\"{file_path}\" alt=\"Question Image\" style=\"max-width:100%; height:auto;\">";
            return fullImg;
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
            string fullImg = $"<img src=\"{file_path}\" alt=\"Question Image\" style=\"max-width:100%; height:auto;\">";
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
                currIndex = q1.IndexOf("<p>");

                if (currIndex == -1)
                {
                    currIndex = list[i].IndexOf("<p ");
                    secondsIndex = list[i].IndexOf(">");//find the closing tag of p
                    list[i] = $"<p>({i + 1})\t" + list[i].Substring(secondsIndex + 1, list[i].Length - (secondsIndex + 1));
                }
                else
                {
                    list[i] = $"<p>({i + 1})\t" + list[i].Substring(currIndex + lenOfString, list[i].Length - currIndex - lenOfString);
                }
            }
            return list;

            q1 = "<p>(1)\t" + q1.Substring(currIndex, q1.Length - currIndex);
            currIndex = q2.IndexOf("<p>") + lenOfString;
            q2 = "<p>(2)\t" + q2.Substring(currIndex, q2.Length - currIndex);
            currIndex = q3.IndexOf("<p>") + lenOfString;
            q3 = "<p>(3)\t" + q3.Substring(currIndex, q3.Length - currIndex);
            currIndex = q4.IndexOf("<p>") + lenOfString;
            q4 = "<p>(4)\t" + q4.Substring(currIndex, q4.Length - currIndex);

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
                option1 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][0]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-width:100%; height:auto;\">";
                option2 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][1]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-width:100%; height:auto;\">";
                option3 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][2]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-width:100%; height:auto;\">";
                option4 = $"<img src=\"https://lmsapi.kidum-me.com/storage/{qp.json_content["option_images"][3]["file_path"].ToString()}\" alt=\"Question Image\" style=\"max-width:100%; height:auto;\">";
                listOfOptions = new List<string> { option1, option2, option3, option4 };

                if (userAnswer != DO_NOT_MARK)
                {
                    listOfOptions[optionToMarkGreen - 1] = $"<div style = \"background-color: lightgreen;display: inline-block;\"> ({optionToMarkGreen})\t</div>" + listOfOptions[optionToMarkGreen - 1];
                    //green will always be
                    if (userAnswer != optionToMarkGreen && userAnswer != SKIPPED_Q)//if the user got right - do not need to mark in red
                    {
                        listOfOptions[userAnswer - 1] = $"<div style = \"background-color: red;display: inline-block;\"> ({userAnswer})\t</div>" + listOfOptions[userAnswer - 1];
                    }
                }
                for (int i = 0; i < listOfOptions.Count; i++)
                {
                    if (listOfOptions[i].StartsWith("<img"))
                        listOfOptions[i] = $"({i + 1})\t" + listOfOptions[i];
                }
            }







            string finalOptionsString = listOfOptions[0] + listOfOptions[1] + listOfOptions[2] + listOfOptions[3];
            if (!isTextOptions)
                finalOptionsString = listOfOptions[0] + listOfOptions[1] + "<br>" + listOfOptions[2] + listOfOptions[3];
            //if question is in english
            if (!isQuestionInHebrew(qp.category))
            {
                return question + get_string_of_img_html(qp.json_content["image"]) + "<br><br>" + finalOptionsString;
            }

            return right2left(question + get_string_of_img_html(qp.json_content["image"]) + "<br><br>" + finalOptionsString);
        }

        public static string get_string_of_question_and_explanation(dbQuestionParmeters qp, int clientanswer)
        {
            string answer = qp.json_content["solving_explanation"].ToString();
            string line = "<div style=\"top: 50%; left: 0; width: 100vw; height: 1px; background-color: lightgray;\"></div>\r\n<br>הסבר:";
            var img = qp.json_content["explanation_image"];

            return get_string_of_question_and_option_from_json(qp, clientanswer) + right2left(line + answer + get_string_of_img_html(img));
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
            string hashedMac = ComputeSha256Hash(mac + "b");
            return hashedMac + "1";

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
    }
}