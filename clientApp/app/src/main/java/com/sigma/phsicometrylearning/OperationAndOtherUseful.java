package com.sigma.phsicometrylearning;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class OperationAndOtherUseful {
    public static String img_max_height = "200px";
    public static int DO_NOT_MARK = -1;

    public static int SKIPPED_Q = -2; // when the user didnt answer

    private static Boolean isQuestionInHebrew(String category)
    {
        if (category.equals("Restatements") || category.equals("Reading Comprehension") || category.equals("אוצר-מילים") || category.equals("Sentence Completions"))
        {
            return false;
        }
        return true;
    }
    public static List<String> addNumberToQuestions(String q1, String q2, String q3, String q4) {
        final int lenOfString = 3;
        List<String> list = new ArrayList<>(Arrays.asList(q1, q2, q3, q4));
        int currIndex;
        int secondsIndex;

        for (int i = 0; i < list.size(); i++) {
            currIndex = list.get(i).indexOf("<p>");

            if (currIndex == -1) {
                currIndex = list.get(i).indexOf("<p ");
                secondsIndex = list.get(i).indexOf(">"); // Find the closing tag of <p>
                list.set(i, "<p>(" + (i + 1) + ")\t" + list.get(i).substring(secondsIndex + 1));
            } else {
                String startOfQuestion = list.get(i).substring(0, currIndex);
                list.set(i, startOfQuestion + "<p>(" + (i + 1) + ")\t" + list.get(i).substring(currIndex + lenOfString));
            }
        }
        return list;
    }
    public static String get_string_of_question_and_option_from_json(DbQuestionParmeters qp, int userAnswer){
        // optionToMarkGreen = correct answer index
        boolean isTextOptions = false;
        try {
            isTextOptions = ((JSONArray) qp.json_content.get("options")).length() != 0;
        } catch (JSONException e) {
            throw new RuntimeException(e);
        }
        int optionToMarkGreen = qp.rightAnswer;
        String question = null;
        try {
            question = qp.json_content.get("question").toString();
        } catch (JSONException e) {
            throw new RuntimeException(e);
        }
        String option1="", option2="", option3="", option4 ="";
        List<String> listOfOptions = new ArrayList<>();

        if (isTextOptions) {
            try {
                option1 = qp.json_content.optJSONArray("options").optJSONObject(0).getString("text");
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }
            try {
                option2 = qp.json_content.optJSONArray("options").optJSONObject(1).getString("text");
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }
            try {
                option3 = qp.json_content.optJSONArray("options").optJSONObject(2).getString("text");
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }
            try {
                option4 = qp.json_content.optJSONArray("options").optJSONObject(3).getString("text");
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }
            listOfOptions = addNumberToQuestions(option1, option2, option3, option4);

            // If the user answered, mark the answer
            if (userAnswer != DO_NOT_MARK) {
                listOfOptions.set(optionToMarkGreen - 1, "<div style=\"background-color: lightgreen;display: inline-block;\">" + listOfOptions.get(optionToMarkGreen - 1) + "</div><br>");
                // Green will always be
                if (userAnswer != optionToMarkGreen && userAnswer != SKIPPED_Q) { // If the user got right - do not need to mark in red
                    listOfOptions.set(userAnswer - 1, "<div style=\"background-color: red;display: inline-block;\">" + listOfOptions.get(userAnswer - 1) + "</div><br>");
                }
            }
        } else {
            try {
                option1 = "<img src=\"https://lmsapi.kidum-me.com/storage/" + qp.json_content.optJSONArray("option_images").optJSONObject(0).getString("file_path") + "\" alt=\"Question Image\" style=\"max-height:" + img_max_height + "; width:auto;\">";
            } catch (JSONException e) {
                //throw new RuntimeException(e);
            }
            try {
                option2 = "<img src=\"https://lmsapi.kidum-me.com/storage/" + qp.json_content.optJSONArray("option_images").optJSONObject(1).getString("file_path") + "\" alt=\"Question Image\" style=\"max-height:" + img_max_height + "; width:auto;\">";
            } catch (JSONException e) {
                //throw new RuntimeException(e);
            }
            try {
                option3 = "<img src=\"https://lmsapi.kidum-me.com/storage/" + qp.json_content.optJSONArray("option_images").optJSONObject(2).getString("file_path") + "\" alt=\"Question Image\" style=\"max-height:" + img_max_height + "; width:auto;\">";
            } catch (JSONException e) {
                //throw new RuntimeException(e);
            }
            try {
                option4 = "<img src=\"https://lmsapi.kidum-me.com/storage/" + qp.json_content.optJSONArray("option_images").optJSONObject(3).getString("file_path") + "\" alt=\"Question Image\" style=\"max-height:" + img_max_height + "; width:auto;\">";
            } catch (JSONException e) {
                //throw new RuntimeException(e);
            }
            listOfOptions = new ArrayList<>(Arrays.asList(option1, option2, option3, option4));

            if (userAnswer != DO_NOT_MARK) {
                listOfOptions.set(optionToMarkGreen - 1, "<div style=\"background-color: lightgreen;display: inline-block;\"> (" + optionToMarkGreen + ")\t</div><br>" + listOfOptions.get(optionToMarkGreen - 1));
                // Green will always be
                if (userAnswer != optionToMarkGreen && userAnswer != SKIPPED_Q) { // If the user got right - do not need to mark in red
                    listOfOptions.set(userAnswer - 1, "<div style=\"background-color: red;display: inline-block;\"> (" + userAnswer + ")\t</div><br>" + listOfOptions.get(userAnswer - 1));
                }
            }

            for (int i = 0; i < listOfOptions.size(); i++) {
                if (listOfOptions.get(i).startsWith("<img")) {
                    listOfOptions.set(i, "<div><p>(" + (i + 1) + ")\t</p></div><br>" + listOfOptions.get(i));
                }
            }
        }

        String finalOptionsString = String.join("", listOfOptions);

        if (!isTextOptions) {
            finalOptionsString = String.join("<br>", listOfOptions);
        }
        JSONObject image_JSONO = null;
        image_JSONO = qp.json_content.optJSONObject("image");
        String image_str = "";
        if (image_JSONO != null) {
            try {
                image_str = getStringOfImgHtml(image_JSONO);
            } catch (JSONException e) {
                //throw new RuntimeException(e);
            }
        }

        // If the question is in English
        if (!isQuestionInHebrew(qp.category)) {
            return left2right(get_string_of_img_col_html(qp.json_content) + "<br>" + question + image_str + "<br><br>" + finalOptionsString);
        }
        String strForEight2Left = question;
        strForEight2Left+= image_str;
        strForEight2Left += "<br><br>"+ finalOptionsString;
        return right2left(get_string_of_img_col_html(qp.json_content) + "<br>"  + strForEight2Left);
    }
    private static String getStringOfImgHtml(JSONObject json) throws JSONException {
        // This should be in a separate file
        if (json == null) {
            return "";
        }

        try {
            if (!json.has("file_path")) {
                return "";
            }
            String imgPath = "https://lmsapi.kidum-me.com/storage/";
            String filePath = imgPath + json.getString("file_path");
            String fullImg = String.format("<img src=\"%s\" alt=\"Question Image\" style=\"max-height:%s; width:auto;\">", filePath, img_max_height);
            return fullImg;

        } catch (JSONException e) {
            return "";
        }

    }
    public static String right2left(String s) {
        // Aligned to the right
        String htmlContent = "<head> <style> body { text-align: right; /* Right-align all text */ } </style> </head> " +
                "<div style=\"direction: rtl; text-align: right;\">" + s + "</div>";

        // Regular expression to match Hebrew characters (Unicode)
        String hebrewUnicodePattern = "[\\u0590-\\u05FF]"; // Unicode range for Hebrew characters

        // Regular expression to match HTML entities for Hebrew letters
        String hebrewHtmlEntitiesPattern = "&#(1488|1489|1490|1491|1492|1493|1494|1495|1496|1497|1498|1499|1500|1501|1502|1503|1504|1505|1506|1507|1508|1509|1510|1511|1512|1513|1514);"; // Matches HTML entities for Hebrew letters

        // Match <mi> tags that may contain Hebrew characters (Unicode or HTML entities)
        String pattern = "<mi>(.*?)</mi>";
        Pattern regex = Pattern.compile(pattern);
        Matcher matcher = regex.matcher(htmlContent);

        StringBuffer result = new StringBuffer();

        while (matcher.find()) {
            String content = matcher.group(1);

            // Check if the content contains Hebrew characters (Unicode or HTML entities)
            if (content.matches(hebrewUnicodePattern) || content.matches(hebrewHtmlEntitiesPattern)) {
                matcher.appendReplacement(result, "<mi style=\"direction: rtl; text-align: right;\">" + content + "</mi>");
            } else {
                matcher.appendReplacement(result, matcher.group(0)); // Return original match if no Hebrew characters or entities are found
            }
        }
        matcher.appendTail(result);

        return result.toString();
    }
    public static String left2right(String s)
    {
        // aligned to the left
        String htmlContent = "<head> <style> body { text-align: left; /* Left-align all text */ } </style> </head> " + "<div style=\"direction: ltr; text-align: left;\">" + s + "</div>";
        return htmlContent;
    }

    public static String get_string_of_img_col_html(JSONObject json)
    {
        //this should be in a separate file
        if (json == null)
        {
            return "";
        }
        try {

            JSONArray collections = json.optJSONArray("collections");
            if (collections.length() == 0)
                return "";
            String cover = collections.optJSONObject(0).getString("cover");
            String img_path = "https://lmsapi.kidum-me.com/storage/";
            String file_path = img_path + collections.optJSONObject(0).optJSONObject("file").getString("file_path");
            String fullImg = "<img src=\"" + file_path + "\" alt=\"Question Image\" style=\" max-width:100%; height:auto;\">";
            return cover + fullImg;

        }
        catch (org.json.JSONException e)
        {
            return "";
        }
    }

    public static String get_string_of_question_and_explanation(DbQuestionParmeters qp, int clientanswer) throws JSONException {
        String line = "<div style=\"top: 50%; left: 0; width: 100vw; height: 1px; background-color: lightgray;\"></div>\r\n<br>הסבר:";

        return get_string_of_question_and_option_from_json(qp, clientanswer) + right2left(line) + get_explanation(qp);
    }
    public static String get_explanation(DbQuestionParmeters qp) throws JSONException {
        String answer = qp.json_content.getString("solving_explanation");
        JSONObject img = qp.json_content.optJSONObject("explanation_image"); // Use optJSONObject to avoid NullPointerException
        return right2left(answer + getStringOfImgHtml(img));
    }

    public static String fixNewlineMathml(String html) {
        // the MathML newline
        String newLineMathml = "linebreak=\"newline\"";
        String newHtml = html;

        if (newHtml.contains(newLineMathml)) {
            // get the whole MathML newline element (e.g.: '<mspace linebreak="newline"></mspace>')
            int i = newHtml.indexOf(newLineMathml) - 1;
            String startElement = "";
            while (i >= 0 && newHtml.charAt(i + 1) != '<') {
                startElement = newHtml.charAt(i) + startElement;
                i--;
            }

            int count = 0;
            i = newHtml.indexOf(newLineMathml) + newLineMathml.length();
            String closeElement = "";
            while (i >= 0 && count != 2) {
                closeElement = closeElement + newHtml.charAt(i);
                if (newHtml.charAt(i) == '>') {
                    count++;
                }
                i++;
            }

            if (closeElement.contains(startElement.replace("<", "").replace(">", "").replace(" ", "").replace("\n", ""))) {
                newLineMathml = startElement + newLineMathml + closeElement;
            } else {
                // if the element is <mspace linebreak="newline"> and not <mspace linebreak="newline"></mspace>
                newLineMathml = startElement + newLineMathml + closeElement.substring(0, closeElement.indexOf(">") + 1);
            }

            List<String> openTags = new ArrayList<>();
            List<String> closeTags = new ArrayList<>();
            i = newHtml.indexOf(newLineMathml) - 1;

            List<String> alreadyClosedTags = new ArrayList<>();

            // Loop to find all opening tags up to the <mjx-container> or the <math>
            while (true) {
                if (newHtml.charAt(i) == '>') {
                    // Extract the opening tag
                    String openString = "";

                    // Read backwards to get the entire opening tag
                    while (i >= 0 && newHtml.charAt(i) != '<') {
                        openString = newHtml.charAt(i) + openString;
                        i--;
                    }
                    openString = "<" + openString;

                    // if it is another element in the tree that is already closed, ignore it
                    if (openString.startsWith("</")) {
                        alreadyClosedTags.add(openString);
                        continue;
                    }

                    // if there are attributes (e.g., src="...", style="..."), ignore them for the closing tag
                    String withoutOptions = openString.contains(" ") ? openString.substring(0, openString.indexOf(" ") + 1) + ">" : openString;

                    // Generate the corresponding closing tag
                    String closeString = withoutOptions.replace("<", "</");

                    // if the tag is already closed, ignore it
                    if (alreadyClosedTags.contains(closeString)) {
                        alreadyClosedTags.remove(closeString);
                        continue;
                    }

                    // add to the lists
                    openTags.add(openString);
                    closeTags.add(closeString);

                    // Break if we find the closing tag for <mjx-container> or <math>
                    if (openString.contains("mjx-container") || openString.contains("math")) {
                        break;
                    }
                }
                i--;
                // Check to prevent index out of bounds
                if (i < 0) break;
            }

            Collections.reverse(closeTags);
            Collections.reverse(openTags);

            // Replace the <mspace> with the closing tags, <br>, and reopening tags
            newHtml = newHtml.substring(0, newHtml.indexOf(newLineMathml))
                    + String.join("", closeTags) // Close tags in reverse order
                    + "<br><br>&nbsp;"
                    + String.join("", openTags)  // Open tags in normal order
                    + newHtml.substring(newHtml.indexOf(newLineMathml) + newLineMathml.length());
        }

        // to edit all new_lines
        if (newHtml.equals(html))
            return newHtml;
        return fixNewlineMathml(newHtml); // recursive call
    }

}
