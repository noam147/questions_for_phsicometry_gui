package com.sigma.phsicometrylearning;

import android.os.Bundle;
import android.text.Html;
import android.webkit.WebView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import org.json.JSONException;

public class ShowQuestionByIdActivity extends AppCompatActivity {

    private int questionId;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_show_question_by_id);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
        questionId = getIntent().getIntExtra("id",-1);
        DbManager dbManager = new DbManager(this);
        DbQuestionParmeters currQuestion = dbManager.getQuestionBasedOnId(questionId);


        WebView questionsWebView = findViewById(R.id.QuestionsWebView);

// HTML content to load into the WebView
        String htmlToDisplay = "<html><body><h1>This is the question</h1><p>Question details go here...</p></body></html>";
        try {
            htmlToDisplay = OperationAndOtherUseful.get_string_of_question_and_explanation(currQuestion,currQuestion.rightAnswer);
        } catch (JSONException e) {
            throw new RuntimeException(e);
        }
// Pass the HTML content to the WebView
        htmlToDisplay = Html.escapeHtml(htmlToDisplay);
        questionsWebView.getSettings().setJavaScriptEnabled(true); // Enable JavaScript if needed
        questionsWebView.loadData(htmlToDisplay, "text/html", "UTF-8");
    }
}