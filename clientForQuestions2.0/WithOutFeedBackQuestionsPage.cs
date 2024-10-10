using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace clientForQuestions2._0
{
    public partial class WithOutFeedBackQuestionsPage : WithFeedBackQuestionsPage
    {
        private int finalTime;

        public WithOutFeedBackQuestionsPage() : base()
        {
            InitializeComponent();
        }

        public WithOutFeedBackQuestionsPage(int amount, List<string> listOfTopics, bool isQSkip, int timePerQ, questionsDifficultyLevel difficultyLevel)
            : base(amount, listOfTopics, isQSkip, timePerQ != OperationsAndOtherUseful.WITHOUT_TIMER ? timePerQ * amount : OperationsAndOtherUseful.WITHOUT_TIMER, difficultyLevel)
        {

            InitializeComponent();
            finalTime = amount * timePerQ;
        }

        protected override void actionsWithTimer()
        {
        
            //when the time ends completly
            if(this.timer.Text == "-1")
            {
                goToSummrizePage();
            }
               
            
        }
        protected void answerButton_Click(object sender, EventArgs e)
        {
            int clicked_answer = int.Parse((((Button)sender).Text.ToString()[((Button)sender).Text.ToString().Length - 1]).ToString());
            afterAnswerQuestion(clicked_answer);
        }
    }
}
