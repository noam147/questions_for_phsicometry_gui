package com.sigma.phsicometrylearning;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

public class DbManager extends SQLiteOpenHelper {
    private static final String DATABASE_NAME = "kidum_jsons.db";
    private static final int DATABASE_VERSION = 1;
    private static final String DATABASE_PATH = "/data/data/com.sigma.phsicometrylearning/databases/";
    private final Context context;

    public DbManager(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
        this.context = context;
        // Copy the database if it doesn't exist
        try {
            copyDatabaseIfNeeded(false);
        } catch (IOException e) {
            Log.e("DBManager", "Error copying database: " + e.getMessage());
        }
    }
    private void copyDatabaseIfNeeded(boolean overrideCuurentFile) throws IOException
    {
        File dbFile = new File(DATABASE_PATH + DATABASE_NAME);
        if(overrideCuurentFile)
        {
            dbFile.delete();
        }

        // Check if the database already exists
        if (!dbFile.exists()) {
            // Create the database folder if it doesn't exist
            File dbFolder = new File(DATABASE_PATH);
            if (!dbFolder.exists()) {
                dbFolder.mkdirs();
            }

            // Copy the database from assets
            try (InputStream input = context.getAssets().open(DATABASE_NAME);
                 OutputStream output = new FileOutputStream(dbFile)) {

                byte[] buffer = new byte[1024];
                int length;
                while ((length = input.read(buffer)) > 0) {
                    output.write(buffer, 0, length);
                }
                output.flush();
                Log.d("DBManager", "Database copied successfully.");
            } catch (IOException e) {
                throw new IOException("Error copying database: " + e.getMessage());
            }
            catch (Exception e)
            {
                int a =5;
            }
        }
    }
    @Override
    public void onCreate(SQLiteDatabase db) {
        // This method is called when the database is created for the first time.
        // You can define your table creation SQL statements here if needed.
        //Log.d("DBManager", "Database created.");
        // db.execSQL("CREATE TABLE IF NOT EXISTS words (word_id INTEGER PRIMARY KEY, word TEXT, meaning TEXT, word_type TEXT, origin_place TEXT, amountOfStars INTEGER, knowledge_level TEXT, isWordMark INTEGER)");
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        // This method is called when the database needs to be upgraded.
        //Log.d("DBManager", "Database upgraded.");
    }

    public SQLiteDatabase openDb() {
        SQLiteDatabase db = null;  // Initialize db to null
        try {
            db = this.getWritableDatabase(); // Attempt to open the database
            Log.d("DBManager", "Database opened successfully.");
        } catch (Exception e) {
            Log.e("DBManager", "Cannot open database: " + e.getMessage());
        }
        return db; // Return the database (may be null if an error occurred)
    }

    public void closeDb(SQLiteDatabase db) {
        this.close();
        Log.d("DBManager", "Database closed.");
    }

    public List<DbQuestionParmeters> doQuery(String query) throws JSONException {
        List<DbQuestionParmeters> dbQuestions = new ArrayList<>();
        JSONArray jsonArray = new JSONArray();
        List<String> categories = new ArrayList<>();
        List<Integer> ids = new ArrayList<>();

        String firstLib = "<script src=\"https://polyfill.io/v3/polyfill.min.js?features=es6\"></script>\n";
        String secondLib = "<script id=\"MathJax-script\" async src=\"https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js\"></script>";
        secondLib= "<script src=\"https://cdnjs.cloudflare.com/ajax/libs/mathjax/2.7.7/MathJax.js?config=TeX-AMS_HTML\"></script>\n";
        String includeJsLibs = "<head>"+firstLib+secondLib+"</head>";


        SQLiteDatabase db = this.getReadableDatabase(); // Use the SQLiteOpenHelper method to get the database instance

        try (Cursor cursor = db.rawQuery(query, null)) {
            if (cursor.moveToFirst()) {
                do {
                    JSONObject jsonObject = new JSONObject(cursor.getString(cursor.getColumnIndexOrThrow("json_question")));
                    JSONObject check = jsonObject.getJSONArray("data").getJSONObject(0);
                    JSONObject realJsonObject = new JSONObject(check.toString());
                    jsonArray.put(realJsonObject);
                    ids.add(cursor.getInt(cursor.getColumnIndexOrThrow("question_id")));
                    categories.add(cursor.getString(cursor.getColumnIndexOrThrow("question_type")));
                } while (cursor.moveToNext());
            }
        } catch (Exception e) {
            Log.e("DBManager", "Error executing query: " + e.getMessage());
            return null;
        }

        for (int i = 0; i < jsonArray.length(); i++) {
            DbQuestionParmeters dbParmeters = new DbQuestionParmeters();
            try {
                dbParmeters.json_content = jsonArray.getJSONObject(i);
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }
            try {
                dbParmeters.json_content.put("question", includeJsLibs + dbParmeters.json_content.getString("question"));
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }
            try {
                dbParmeters.json_content.put("question", dbParmeters.json_content.getString("question").replace("<mspace linebreak=\"newline\"/>", "<mspace linebreak=\"newline\"/><p></p>"));
            } catch (JSONException e) {
                throw new RuntimeException(e);
            }

            dbParmeters.category = categories.get(i);
            dbParmeters.questionId = ids.get(i);
            dbParmeters.rightAnswer = getNumOfCorrectAnswer(jsonArray.getJSONObject(i));
            dbQuestions.add(dbParmeters);
        }

        return dbQuestions;
    }
    private static int getNumOfCorrectAnswer(JSONObject json) {
        int option1, option2, option3, option4;

        try {
            option1 = json.getJSONArray("options").getJSONObject(0).getInt("is_correct");
            option2 = json.getJSONArray("options").getJSONObject(1).getInt("is_correct");
            option3 = json.getJSONArray("options").getJSONObject(2).getInt("is_correct");
            option4 = json.getJSONArray("options").getJSONObject(3).getInt("is_correct");
        } catch (JSONException e) {
            return 1;  // Default value in case of error
        }

        if (option1 == 1) {
            return 1;
        }
        if (option2 == 1) {
            return 2;
        }
        if (option3 == 1) {
            return 3;
        }
        return 4;
    }


    public DbQuestionParmeters getQuestionBasedOnId(int questionId) {
        String query = "SELECT * FROM questions WHERE question_id = '" + questionId + "'";
        List<DbQuestionParmeters> questionParameters = null;
        try {
            questionParameters = doQuery(query);
        } catch (JSONException e) {
            throw new RuntimeException(e);
        }

        if (questionParameters != null && !questionParameters.isEmpty()) {
            return questionParameters.get(0);
        }

        // This will return something with id = 0 - that can't happen in real question
        return new DbQuestionParmeters();
    }

}
