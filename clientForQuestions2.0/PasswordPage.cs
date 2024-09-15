using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class PasswordPage : Form
    {
        private string password;
        private string macAdd;
        public PasswordPage()
        {
            InitializeComponent();

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
                var menuPage = new menuPage();
                menuPage.Show();
                this.Close();
            }
            else
            {

            }
        }
    }
}
