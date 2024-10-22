package com.sigma.phsicometrylearning;

import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.inputmethod.EditorInfo;
import android.widget.EditText;
import android.widget.TextView;

import androidx.activity.EdgeToEdge;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.graphics.Insets;
import androidx.core.view.ViewCompat;
import androidx.core.view.WindowInsetsCompat;

import com.google.android.material.textfield.TextInputEditText;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        EdgeToEdge.enable(this);
        setContentView(R.layout.activity_main);
        ViewCompat.setOnApplyWindowInsetsListener(findViewById(R.id.main), (v, insets) -> {
            Insets systemBars = insets.getInsets(WindowInsetsCompat.Type.systemBars());
            v.setPadding(systemBars.left, systemBars.top, systemBars.right, systemBars.bottom);
            return insets;
        });
        setEnterListenr();
    }
    private void setEnterListenr()
    {
        EditText editText = findViewById(R.id.editTextNumberDecimal);

        editText.setOnEditorActionListener(new TextView.OnEditorActionListener() {

            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == EditorInfo.IME_ACTION_DONE ||
                        (event != null && event.getKeyCode() == KeyEvent.KEYCODE_ENTER && event.getAction() == KeyEvent.ACTION_DOWN)) {
                    // "Enter" key was pressed or IME_ACTION_DONE was triggered

                    // Handle the event here, e.g., get the input value
                    String input = editText.getText().toString();
                    // Return true to consume the event
                    sendButtonClicked(null);
                    return true;
                }
                return false;
            }
        });

    }
    public void sendButtonClicked(View view)
    {
        EditText questionId = findViewById(R.id.editTextNumberDecimal);
        String idString = questionId.getText().toString(); // Get the text as a string
        int id;

        try {
            id = Integer.parseInt(idString); // Parse the string to an integer
        } catch (NumberFormatException e) {
            // Handle the case where the input is not a valid integer
            id = 0; // Or set to a default value or show an error message
            e.printStackTrace();
        }
        Intent intent = new Intent(MainActivity.this, ShowQuestionByIdActivity.class);
        intent.putExtra("id",id);
        startActivity(intent);

    }
}