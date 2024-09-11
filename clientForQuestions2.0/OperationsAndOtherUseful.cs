using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientForQuestions2._0
{
    
    internal class OperationsAndOtherUseful
    {
        public static int QUESTION_THAT_DID_NOT_ANSWERED = -1;
        private static string get_string_of_img_html(JToken json)
        {
            //this should be in a separate file
            if (json == null )
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

        public static string addNumberToQuestions(string q1, string q2, string q3, string q4)
        {
            //this should be in a separate file
            q1 = "<p>(1)\t" + q1.Substring(3, q1.Length - 3);
            q2 = "<p>(2)\t" + q2.Substring(3, q2.Length - 3);
            q3 = "<p>(3)\t" + q3.Substring(3, q3.Length - 3);
            q4 = "<p>(4)\t" + q4.Substring(3, q4.Length - 3);
            return q1 + q2 + q3 + q4;
        }
        public static string get_string_of_question_and_option_from_json(JToken json)
        {
            string question = json["question"].ToString();
            string option1 = json["options"][0]["text"].ToString();
            string option2 = json["options"][1]["text"].ToString();
            string option3 = json["options"][2]["text"].ToString();
            string option4 = json["options"][3]["text"].ToString();

            string allOptions = addNumberToQuestions(option1, option2, option3, option4);
            return question + get_string_of_img_html(json["image"]) + "<br><br>" + allOptions;
        }


        private static string getFinalStringOfOptions(string option1, string option2, string option3, string option4, int wrongUserChoice)
        {
            if (wrongUserChoice == 1)
            {
                option1 = $"<div style = \"background-color: red;display: inline-block;\">{option1}</div><br>";
            }
            if (wrongUserChoice == 2)
            {
                option2 = $"<div style = \"background-color: red;display: inline-block;\">{option2}</div><br>";
            }
            if (wrongUserChoice == 3)
            {
                option3 = $"<div style = \"background-color: red;display: inline-block;\">{option3}</div><br>";
            }
            if (wrongUserChoice == 4)
            {
                option4 = $"<div style = \"background-color: red;display: inline-block;\">{option4}</div><br>";
            }
            return option1 + option2 + option3 + option4;
        }
        public static string get_string_of_question_and_option_from_json(JToken json, int answer)
        {
            string libToAdd = "<head><script type=\"text/javascript\" async src=\"https://cdnjs.cloudflare.com/ajax/libs/mathjax/3.2.0/es5/tex-mml-chtml.js\"></script></head>\r\n";
            string question = json["question"].ToString();
            string option1 = json["options"][0]["text"].ToString();
            string option2 = json["options"][1]["text"].ToString();
            string option3 = json["options"][2]["text"].ToString();
            string option4 = json["options"][3]["text"].ToString();
            string final = addNumberToQuestions(option1, option2, option3, option4);
            option1 = final.Substring(0, final.IndexOf("<p>(2)"));
            final = final.Substring(final.IndexOf("<p>(2)"), final.Length - option1.Length);
            option2 = final.Substring(0, final.IndexOf("<p>(3)"));
            final = final.Substring(final.IndexOf("<p>(3)"), final.Length - option2.Length);
            option3 = final.Substring(0, final.IndexOf("<p>(4)"));
            final = final.Substring(final.IndexOf("<p>(4)"), final.Length - option3.Length);
            option4 = final;
            int option1tf = (int)json["options"][0]["is_correct"];
            int option2tf = (int)json["options"][1]["is_correct"];
            int option3tf = (int)json["options"][2]["is_correct"];
            int option4tf = (int)json["options"][3]["is_correct"];
            string startOfMsg = libToAdd+question + get_string_of_img_html(json["image"]) + "<br><br>";
            string[] options = { option1, option2, option3, option4 }; // Assuming you have these defined
            int[] optionFlags = { option1tf, option2tf, option3tf, option4tf };
            //option[n]tf = 1 - this is the real true answer
            for (int i = 1; i < 5; i++)
            {
                if (optionFlags[i - 1] == 1)
                {
                    options[i - 1] = $"<div style = \"background-color: cyan;display: inline-block;\">{options[i - 1]}</div><br>";
                    if (answer != i)
                    {
                        //if the user were wrong
                        return startOfMsg + getFinalStringOfOptions(options[0], options[1], options[2], options[3], answer);
                    }
                    //if the user got right
                    return startOfMsg + options[0] + options[1] + options[2] + options[3];
                }
            }
            return "";
        }

        public static string get_string_of_question_and_explanation(JToken json, int clientanswer)
        {
            string answer = json["solving_explanation"].ToString();
            string line = "<div style=\"top: 50%; left: 0; width: 100vw; height: 1px; background-color: lightgray;\"></div>\r\n<br>explanation:";
            var img = json["explanation_image"];
            return get_string_of_question_and_option_from_json(json, clientanswer) + line + answer + get_string_of_img_html(img);

        }
       
    }
}
