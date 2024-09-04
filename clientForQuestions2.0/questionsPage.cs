using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using System.IO;
namespace clientForQuestions2._0
{
    public partial class questionsPage : Form
    {
        private WebView2 webView21;

        public questionsPage()
        {
            InitializeComponent();
            Task.Run(() => InitializeWebView2());

            //webView21.CoreWebView2.Navigate("https://www.example.com");
        }
        private void InitializeWebView2()
        {
            // Ensure that UI updates are made on the main thread
            if (InvokeRequired)
            {
                Invoke(new Action(InitializeWebView2));
                return;
            }

            webView21 = new WebView2
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(webView21);

            // Event handler for when CoreWebView2 initialization is complete
            webView21.CoreWebView2InitializationCompleted += OnCoreWebView2InitializationCompleted;

            try
            {
                // Initialize WebView2 runtime
                webView21.EnsureCoreWebView2Async(null).ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        MessageBox.Show($"Error initializing WebView2: {task.Exception.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebView2: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnCoreWebView2InitializationCompleted(object sender, EventArgs e)
        {
            if (webView21.CoreWebView2 != null)
            {
                // Now you can navigate
                //webView21.CoreWebView2.Navigate("https://www.example.com");
                string htmlContent = File.ReadAllText("D:/htmlF.html");

                // Load the HTML content into WebView2
                webView21.NavigateToString(htmlContent);
            }
            else
            {
                MessageBox.Show("WebView2 initialization failed.", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
