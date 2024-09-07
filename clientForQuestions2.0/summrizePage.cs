using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class summrizePage : Form
    {
        List<Button> m_buttonList= new List<Button>();
        public summrizePage(List<afterQuestionParametrs> questions)
        {
            InitializeComponent();
            createButtons(questions);
            displayButtons();
        }
        private void createButtons(List<afterQuestionParametrs> currQuestionRight)
        {
            for(int i = 0;i<currQuestionRight.Count;i++) 
            {

                Button btn = new Button
                {
                    Text = $"{i + 1}",
                    Width = 30,
                    Height = 30,
                    Location = new System.Drawing.Point(10+i*55, 30) // Adjust spacing
                };
                //if answer was correct
                if (currQuestionRight[i].question.rightAnswer == currQuestionRight[i].userAnswer)
                {
                    btn.BackColor = Color.Cyan;
                }
                else
                {
                    btn.BackColor = Color.Red;
                }
                m_buttonList.Add(btn);
            }
        }
        private void displayButtons()
        {
            for(int i = 0; i<m_buttonList.Count;i++) 
            {
                Button btn = m_buttonList[i];
                Controls.Add(btn);
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            int indexQuestion = (int.Parse(b.Text)-1);
            //display the question with html explanation etc
            //maybe even keep track on the user choice
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
