using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace clientForQuestions2._0
{
    public partial class check : menuPage
    {
        private List<CustomData> customDataList;
        private ContextMenuStrip sortContextMenu; // Context menu for sorting
        private bool isSortAscending = true;

        public class CustomData
        {
            public int Numerator { get; set; }
            public int Denominator { get; set; }

            public int Percentage => (Denominator == 0) ? 0 : (Numerator * 100) / Denominator;

            public string DisplayValue => $"{Percentage}% {Numerator}/{Denominator}";
        }

        WebView2 webView21;
        

        public check()
        {
            InitializeComponent();
            LoadData();
            SetupDataGridView();
            SetupContextMenu();
        }

        private void LoadData()
        {
            customDataList = new List<CustomData>
        {
            new CustomData { Numerator = 20, Denominator = 100 },
            new CustomData { Numerator = 15, Denominator = 50 },
            new CustomData { Numerator = 5, Denominator = 0 },
            new CustomData { Numerator = 30, Denominator = 75 }
        };
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AutoSize = true;
            dataGridView1.Location = new Point(0, 0);

            // Create a single column for displaying the custom value
            var displayColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "Value",
                DataPropertyName = "DisplayValue", // Bind to the DisplayValue property
                Width = 150
            };
            dataGridView1.Columns.Add(displayColumn);

            // Bind data
            dataGridView1.DataSource = customDataList;

            // Handle column header click for sorting
            dataGridView1.ColumnHeaderMouseClick += dataGridView1_ColumnHeaderMouseClick;
        }

        private void SetupContextMenu()
        {
            sortContextMenu = new ContextMenuStrip();

            // Add sorting options to the context menu
            sortContextMenu.Items.Add("Sort by Numerator", null, SortByNumerator);
            sortContextMenu.Items.Add("Sort by Denominator", null, SortByDenominator);
            sortContextMenu.Items.Add("Sort by Percentage", null, SortByPercentage);
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0) // Assuming the custom display column is at index 0
            {
                // Show context menu at the cursor's position
                sortContextMenu.Show(Cursor.Position);
            }
        }

        private void SortByNumerator(object sender, EventArgs e)
        {
            customDataList = customDataList.OrderBy(data => data.Numerator).ToList();
            RefreshDataGridView();
        }

        private void SortByDenominator(object sender, EventArgs e)
        {
            customDataList = customDataList.OrderBy(data => data.Denominator).ToList();
            RefreshDataGridView();
        }

        private void SortByPercentage(object sender, EventArgs e)
        {
            customDataList = customDataList.OrderBy(data => data.Percentage).ToList();
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            // Refresh the DataGridView to reflect the sorted list
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = customDataList;
        }


        private async void InitializeWebView21()
        {
            // Ensure that UI updates are made on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView21));
                return;
            }
            // Dispose of any existing instance before creating a new one
            if (webView21 != null)
            {
                webView21.Dispose();
                webView21 = null; // Clear reference
            }


            // Initialize a new instance of WebView2
            webView21 = new WebView2
            {
                Location = new Point(200, 0),
                Size = new Size(200, 200)
            };


            webView21.CoreWebView2InitializationCompleted += OnCoreWebView21InitializationCompleted;

            int maxRetries = 10;
            int retryCount = 0;
            bool initialized = false;

            while (!initialized && retryCount < maxRetries)
            {
                try
                {
                    retryCount++;

                    // Attempt to initialize WebView2 runtime
                    var task = webView21.EnsureCoreWebView2Async(null);
                    await task; // Await the task to ensure it runs on the UI thread

                    // Check if the task has completed successfully
                    if (task.IsCompleted && webView21.CoreWebView2 != null)
                    {
                        initialized = true; // Exit loop if initialization is successful
                    }
                    else if (task.IsFaulted)
                    {
                        // Extract exception details for better debugging
                        var exceptionMessage = task.Exception != null
                            ? string.Join("\n", task.Exception.InnerExceptions.Select(ex => ex.Message))
                            : "Unknown error initializing WebView2.";

                        MessageBox.Show($"Attempt {retryCount} failed: {exceptionMessage}",
                            "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    // Add delay between retries
                    await Task.Delay(500); // Wait before retrying
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during WebView2 initialization attempt {retryCount}: {ex.Message}",
                        "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    // Wait before next retry
                    await Task.Delay(500); // Wait for 2 seconds
                }
            }

            if (initialized)
            {
                // Safely add the control to the form on the main thread
                try
                {
                    whenFinishInitWebView();
                    webView21.NavigationCompleted += webTaker.webView_NavigationCompleted;
                    Controls.Add(webView21);
                    webView21.SendToBack(); // Send WebView2 to the back
                }
                catch (Exception addEx)
                {
                    MessageBox.Show($"Error adding WebView2 to the form: {addEx.Message}",
                        "Add Control Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Failed to initialize WebView2 after multiple attempts.",
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void whenFinishInitWebView()
        {
            OnCoreWebView21InitializationCompleted(null, null);
        }

        //sender and e are not in use
        private void OnCoreWebView21InitializationCompleted(object sender, EventArgs e)
        {
            string includeJsLibs = "<head> <script src=\"https://polyfill.io/v3/polyfill.min.js?features=es6\"></script>\r\n  <script id=\"MathJax-script\" async src=\"https://cdn.jsdelivr.net/npm/mathjax@3/es5/tex-mml-chtml.js\"></script> </head>";

            string htmlContent = includeJsLibs + "<p>\u05e9\u05d0\u05dc\u05d4 \u05d6\u05d5 \u05de\u05e9\u05dc\u05d1\u05ea \u05d0\u05ea \u05e0\u05d5\u05e1\u05d7\u05ea \u05d4\u05de\u05de\u05d5\u05e6\u05e2 \u05d1\u05d0\u05dc\u05d2\u05d1\u05e8\u05d4. \u05db\u05d9\u05d5\u05d5\u05df \u05e9\u05e0\u05e9\u05d0\u05dc\u05d9\u05dd \u05e2\u05dc \u05de\u05de\u05d5\u05e6\u05e2 a \u05d5\u001bb, \u05d9\u05e9 \u05dc\u05e4\u05e9\u05d8 \u05d0\u05ea \u05e0\u05d5\u05e1\u05d7\u05ea \u05d4\u05db\u05e4\u05dc \u05d4\u05de\u05e7\u05d5\u05e6\u05e8:&nbsp;<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><semantics><mrow><mi>a</mi><mo>+</mo><mi>b</mi><mo>=</mo><mn>6</mn><mo>&#8592;</mo><msup><mfenced><mrow><mi>a</mi><mo>+</mo><mi>b</mi></mrow></mfenced><mn>2</mn></msup><mo>=</mo><mn>36</mn><mo>&#8592;</mo><mfrac><msup><mfenced><mrow><mi>a</mi><mo>+</mo><mi>b</mi></mrow></mfenced><mn>2</mn></msup><mn>4</mn></mfrac><mo>=</mo><mn>9</mn><mo>&#8592;</mo><mfrac><mrow><msup><mi>a</mi><mn>2</mn></msup><mo>+</mo><mn>2</mn><mi>ab</mi><mo>+</mo><msup><mi>b</mi><mn>2</mn></msup></mrow><mn>4</mn></mfrac><mo>=</mo><mn>9</mn></mrow><annotation encoding=\"application/vnd.wiris.mtweb-params+json\">{\"language\":\"he\",\"toolbar\":\"<toolbar ref='general' removeLinks='true'>\\n                    <tab ref='general'>\\n                    <removeItem ref='autoDisplayBevelledFraction' />\\n                    <removeItem ref='subscript' />\\n                    <removeItem ref='squareBracket' />\\n                    <removeItem ref='curlyBracket' />\\n                    <removeItem ref='/' />\\n                    <removeItem ref='&#177;' />\\n                    <removeItem ref='&#247;' />\\n                    <removeItem ref='&#8712;' />\\n                    <removeItem ref='&#8746;' />\\n                    <removeItem ref='&#8834;' />\\n                    <removeItem ref='&#8745;' />\\n                    <removeItem ref='&#8709;' />\\n                    <removeItem ref='&#8734;' />\\n                    <removeItem ref='autoItalic' />\\n                    <removeItem ref='&#183;' /><item ref='&#183;' before='squareRoot' />\\n                    <removeItem ref='&#176;' /><item ref='&#176;' before='superscript' />\\n                    <removeItem ref='&#8738;' /><item ref='&#8738;' before='&#8805;' />\\n                    <removeItem ref='&#8594;' /><item ref='&#8594;' before='&#8804;' />\\n                    <removeItem ref='&#8592;' /><item ref='&#8592;' before='&#8594;' />\\n                    <removeItem ref='&#946;' /><item ref='&#946;' before='numberPi' />\\n                    <removeItem ref='&#945;' /><item ref='&#945;' before='&#946;' />\\n                    </tab>\\n                    </toolbar>\\n                  \",\"fontFamily\":\"'Noto Sans Hebrew',Calibri, sans-serif\",\"fontStyle\":\"normal\",\"fontSize\":\"16px\",\"fonts\":[{\"id\":\"Noto Sans Hebrew\",\"label\":\"Noto Sans Hebrew\",\"fontFamily\":\"'Noto Sans Hebrew', sans-serif\"},{\"id\":\"Times New Roman\",\"label\":\"Times New Roman\"},{\"id\":\"David\",\"label\":\"David font\",\"fontFamily\":\"'David Libre', serif\"}]}</annotation></semantics></math>.</p>\r\n<p>\u05de\u05de\u05d5\u05e6\u05e2 \u05d4\u05de\u05e1\u05e4\u05e8\u05d9\u05dd a \u05d5\u001bb \u05d4\u05d5\u05d0:<math xmlns=\"http://www.w3.org/1998/Math/MathML\"><semantics><mrow><mo>.</mo><mi>ממוצע</mi><mo>=</mo><mfrac><mrow><mi style=\"direction: rtl; text-align: right;\">&#1492;&#1488;&#1497;&#1489;&#1512;&#1497;&#1501;</mi><mo>&#160;</mo><mo>&#160;</mo><mi>&#1505;&#1499;&#1493;&#1501;</mi></mrow><mrow><mi>&#1492;&#1488;&#1497;&#1489;&#1512;&#1497;&#1501;</mi><mo>&#160;</mo><mo>&#160;</mo><mi>&#1502;&#1505;&#1508;&#1512;</mi></mrow></mfrac><mo>=</mo><mfrac><mrow><mi>a</mi><mo>+</mo><mi>b</mi></mrow><mn>2</mn></mfrac><mo>=</mo><mfrac><mn>6</mn><mn>2</mn></mfrac><mo>=</mo><mn>3</mn></mrow><annotation encoding=\"application/vnd.wiris.mtweb-params+json\">{\"language\":\"he\",\"toolbar\":\"<toolbar ref='general' removeLinks='true'>\\n                    <tab ref='general'>\\n                    <removeItem ref='autoDisplayBevelledFraction' />\\n                    <removeItem ref='subscript' />\\n                    <removeItem ref='squareBracket' />\\n                    <removeItem ref='curlyBracket' />\\n                    <removeItem ref='/' />\\n                    <removeItem ref='&#177;' />\\n                    <removeItem ref='&#247;' />\\n                    <removeItem ref='&#8712;' />\\n                    <removeItem ref='&#8746;' />\\n                    <removeItem ref='&#8834;' />\\n                    <removeItem ref='&#8745;' />\\n                    <removeItem ref='&#8709;' />\\n                    <removeItem ref='&#8734;' />\\n                    <removeItem ref='autoItalic' />\\n                    <removeItem ref='&#183;' /><item ref='&#183;' before='squareRoot' />\\n                    <removeItem ref='&#176;' /><item ref='&#176;' before='superscript' />\\n                    <removeItem ref='&#8738;' /><item ref='&#8738;' before='&#8805;' />\\n                    <removeItem ref='&#8594;' /><item ref='&#8594;' before='&#8804;' />\\n                    <removeItem ref='&#8592;' /><item ref='&#8592;' before='&#8594;' />\\n                    <removeItem ref='&#946;' /><item ref='&#946;' before='numberPi' />\\n                    <removeItem ref='&#945;' /><item ref='&#945;' before='&#946;' />\\n                    </tab>\\n                    </toolbar>\\n                  \",\"fontFamily\":\"'Noto Sans Hebrew',Calibri, sans-serif\",\"fontStyle\":\"normal\",\"fontSize\":\"16px\",\"fonts\":[{\"id\":\"Noto Sans Hebrew\",\"label\":\"Noto Sans Hebrew\",\"fontFamily\":\"'Noto Sans Hebrew', sans-serif\"},{\"id\":\"Times New Roman\",\"label\":\"Times New Roman\"},{\"id\":\"David\",\"label\":\"David font\",\"fontFamily\":\"'David Libre', serif\"}]}</annotation></semantics></math></p>\r\n<p>&nbsp;</p>\r\n<p id=\"ans\">\u05d4\u05ea\u05e9\u05d5\u05d1\u05d4 \u05d4\u05e0\u05db\u05d5\u05e0\u05d4 \u05d4\u05d9\u05d0 (3)</p>";
            htmlContent = htmlContent.Replace("<mi>", "<mi style=\"direction: rtl; text-align: right;\">");
            if (webView21.CoreWebView2 != null)
            {
                
                webView21.BringToFront();
                webView21.NavigateToString(htmlContent);
            }
            else
                MessageBox.Show("error in OnCoreWebView21InitializationCompleted");
            return;
        }

    }
}
