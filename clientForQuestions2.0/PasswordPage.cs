using System;
using System.IO;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class PasswordPage : Form
    {
        private string filename = "password.pass";
        private string filePath = "";
        private string password;
        private string macAdd;
        public PasswordPage()
        {
            InitializeComponent();
            LogFileHandler.writeIntoFile("logged on");
            filePath = Environment.CurrentDirectory + "/" + filename;

            this.KeyPreview = true; // Set KeyPreview to true to capture key presses
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            macAdd = OperationsAndOtherUseful.getMacAdd();
            this.macLabel.Text = macAdd;
            password = OperationsAndOtherUseful.getEncodedMacAdd(macAdd);
            //this.macLabel.Text = password;  // to copy the passward
            macLabel.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Clipboard.SetText(password);
                    MessageBox.Show("Text copied to clipboard!");
                }
            };
            this.hintPassLabel.Text = "hint for password: (click on the text that looks like gibrish to copy it)";
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Your code here when Enter is pressed
                continueButton_Click(null, null);
                // Optionally, prevent the 'ding' sound on Enter key press
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            if (text == password)
            {
                LogFileHandler.ClearFileContent();//when this is the first time user gets into the program reset the files
                SettingsFileHandler.ClearFileContent();
                writeToFilePass();
                var menuPage = new menuPage();
                menuPage.Show();
                this.Close();
            }
            else
            {

            }
        }
        private bool checkIfPasswordAreInFile()
        {
           
            string text = "";
            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    text += line;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            if(text == password)
            {
                return true;
            }
            return false;
        }
        private void writeToFilePass()
        {

            string content = password;

            try
            {
                File.WriteAllText(filePath, content);

            }
            catch (Exception ex)
            {
               
            }
        }

        private void loggedInButton_Click(object sender, EventArgs e)
        {
            if (checkIfPasswordAreInFile())
            {
                var menuPage = new menuPage();
                menuPage.Show();
                this.Close();
            }
        }

        private void PasswordPage_Load(object sender, EventArgs e)
        {
            // check if already logged in when the page loaded
            loggedInButton_Click(null, null);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    continueButton_Click(null, null);
        }

        private void macLabel_Click(object sender, EventArgs e)
        {

        }
    }
    
}
