package com.sigma.phsicometrylearning;

import android.os.Bundle;
import android.text.Html;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.util.Log;
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
        Log.i("html", htmlToDisplay);
// Pass the HTML content to the WebView
        //htmlToDisplay = Html.escapeHtml(htmlToDisplay);
        questionsWebView.getSettings().setJavaScriptEnabled(true); // Enable JavaScript if needed
        WebSettings webSettings = questionsWebView.getSettings();
        webSettings.setSupportMultipleWindows(true);
        webSettings.setDomStorageEnabled(true);
        webSettings.setDatabaseEnabled(true);
        webSettings.setAllowFileAccess(true);
        webSettings.setAllowContentAccess(true);
        webSettings.setAllowFileAccess(true);
        webSettings.setUseWideViewPort(true);
        webSettings.setSupportZoom(true);
        webSettings.setLoadWithOverviewMode(true);
        webSettings.setJavaScriptEnabled(true);
        webSettings.setJavaScriptCanOpenWindowsAutomatically(false);
        webSettings.setUseWideViewPort(true);
        webSettings.setJavaScriptEnabled(true); // Enable JavaScript if needed
        webSettings.setBuiltInZoomControls(true); // Enable zoom controls
        webSettings.setDisplayZoomControls(false); // Hide the zoom buttons if you only want pinch-to-zoom
        webSettings.setSupportZoom(true);
        questionsWebView.loadDataWithBaseURL(null, htmlToDisplay, "text/html", "UTF-8", null);

        //questionsWebView.loadData(htmlToDisplay, "text/html", "UTF-8");
    }
}