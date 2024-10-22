package com.sigma.phsicometrylearning;

import org.json.JSONObject;

import java.io.Serializable;

public class DbQuestionParmeters implements Serializable {
    public JSONObject json_content;
    public String category;
    public int questionId;
    public int rightAnswer;

}
