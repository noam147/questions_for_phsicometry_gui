using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

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
            filePath = Environment.CurrentDirectory + "/" + filename;
            macLabel.MouseDown += (sender, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    Clipboard.SetText(macLabel.Text);
                    MessageBox.Show("Text copied to clipboard!");
                }
            };
           

            macAdd = OperationsAndOtherUseful.getMacAdd();
            this.macLabel.Text = macAdd;
            password = OperationsAndOtherUseful.getEncodedMacAdd(macAdd);
            //this.macLabel.Text = password;  // to copy the passward

            this.hintPassLabel.Text = "hint for password: (click on the text that looks like gibrish to copy)";
  
        
        }



        private void continueButton_Click(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            if (text == password)
            {
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
    }
    
}
