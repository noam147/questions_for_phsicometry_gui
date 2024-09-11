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
        public static int DO_NOT_MARK = -1;
        public static string right2left(string s)
        {
            return "<div style=" + '"' + "direction: rtl" + '"' + ">" + s + "</div>";
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

        public static List<string> addNumberToQuestions(string q1, string q2, string q3, string q4)
        {
            //this should be in a separate file
            q1 = "<p>(1)\t" + q1.Substring(3, q1.Length - 3);
            q2 = "<p>(2)\t" + q2.Substring(3, q2.Length - 3);
            q3 = "<p>(3)\t" + q3.Substring(3, q3.Length - 3);
            q4 = "<p>(4)\t" + q4.Substring(3, q4.Length - 3);
            List<string> list = new List<string> { q1, q2, q3, q4 };
            return list;
        }


        public static string get_string_of_question_and_option_from_json(dbQuestionParmeters qp,int userAnswer)
        {
            //optionToMarkRed = user answer
            int optionToMarkCyan = qp.rightAnswer;
            string question = qp.json_content["question"].ToString();
            string option1;
            string option2;
            string option3;
            string option4;
            try
            {
                 option1 = qp.json_content["options"][0]["text"].ToString();
                 option2 = qp.json_content["options"][1]["text"].ToString();
                 option3 = qp.json_content["options"][2]["text"].ToString();
                 option4 = qp.json_content["options"][3]["text"].ToString();
            }
            catch
            {
                //FIX_THIS
                //this is a question with images not with text options
                return "";
            }
            List<string> listOfOptions = addNumberToQuestions(option1, option2, option3, option4);
            
            
            //if the user answsered go to this - mark an answer
            if(userAnswer != DO_NOT_MARK)
            {
                listOfOptions[optionToMarkCyan - 1] = $"<div style = \"background-color: cyan;display: inline-block;\">{listOfOptions[optionToMarkCyan - 1]}</div><br>";
                //cyan will always be
                if (userAnswer != optionToMarkCyan)//if the user got right - do not need to mark in red
                {
                    listOfOptions[userAnswer - 1] = $"<div style = \"background-color: red;display: inline-block;\">{listOfOptions[userAnswer - 1]}</div><br>";
                }
            }



            string finalOptionsString = listOfOptions[0] + listOfOptions[1] + listOfOptions[2] + listOfOptions[3];


                if (qp.category == "Restatements" || qp.category == "Reading Comprehension" || qp.category == "אוצר-מילים" || qp.category == "Sentence Completions")
                {
                    return question + get_string_of_img_html(qp.json_content["image"]) + "<br><br>" + finalOptionsString;
                }

                return right2left(question + get_string_of_img_html(qp.json_content["image"]) + "<br><br>" + finalOptionsString);



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

        public static string get_string_of_question_and_explanation(dbQuestionParmeters qp, int clientanswer)
        {
            string answer = qp.json_content["solving_explanation"].ToString();
            string line = "<div style=\"top: 50%; left: 0; width: 100vw; height: 1px; background-color: lightgray;\"></div>\r\n<br>explanation:";
            var img = qp.json_content["explanation_image"];
            if(qp.category == "Restatements" || qp.category == "Reading Comprehension" || qp.category == "אוצר-מילים" || qp.category == "Sentence Completions")
            {
                return get_string_of_question_and_option_from_json(qp, clientanswer) + right2left(line + answer + get_string_of_img_html(img));
            }
            return right2left(get_string_of_question_and_option_from_json(qp, clientanswer) + line + answer + get_string_of_img_html(img));

        }
       
    }
}
