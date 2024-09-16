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
    public partial class collectionsQuestionsMenu : Form
    {
        Random random = new Random();
        String chosen_text = "";

        Dictionary<string, List<int>> title2colIds = new Dictionary<string, List<int>>
        {
            { "הסקה מתרשים", new List<int> { 45, 47, 48, 49, 50, 55, 63, 64, 65, 70, 71, 72, 73, 74, 78, 79, 80, 81, 82, 87, 88, 89, 90, 91, 92, 93, 94, 95, 96, 97, 127, 128, 129, 130, 132, 133, 134, 135 } },
            { "קטע קריאה", new List<int> { 37, 38, 39, 40, 56, 57, 58, 60, 61, 62, 75, 76, 77, 98, 119, 120, 121, 122, 123, 124, 125, 126 } },
            { "Reading Comprehension", new List<int> { 41, 42, 43, 44, 46, 51, 52, 53, 54, 59, 66, 67, 68, 69, 83, 84, 85, 86, 99, 100, 101, 102, 103, 104, 105, 106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 136 } }
        };
        Dictionary<int, List<int>> colId2qIds = new Dictionary<int, List<int>>()
{
    { 37, new List<int> { 7136, 7137, 7138 } },
    { 38, new List<int> { 7140, 7143, 7145 } },
    { 39, new List<int> { 7149, 7152, 7153, 7154, 7155, 7159 } },
    { 40, new List<int> { 7173, 7174, 7175, 7176, 7177, 7178 } },
    { 41, new List<int> { 7179, 7180, 7181, 7182, 7183 } },
    { 42, new List<int> { 7184, 7185, 7186, 7187, 7188 } },
    { 43, new List<int> { 7189, 7190, 7195, 7200, 7205 } },
    { 44, new List<int> { 7232, 7233, 7258, 7259, 7260 } },
    { 45, new List<int> { 7261, 7262, 7263, 10315 } },
    { 46, new List<int> { 7269, 7270, 7271, 7284, 7285 } },
    { 47, new List<int> { 7265, 7266, 7267, 7268 } },
    { 48, new List<int> { 7272, 7273, 7274, 7275 } },
    { 49, new List<int> { 7276, 7277, 7279, 10864 } },
    { 50, new List<int> { 7280, 7281, 7282, 7283 } },
    { 51, new List<int> { 7313, 7314, 7315, 7316, 7317 } },
    { 52, new List<int> { 7323, 7327, 7333, 7337, 7341 } },
    { 53, new List<int> { 7345, 7359, 7360, 7361, 7362 } },
    { 54, new List<int> { 7398, 7401, 7403, 7404, 7405 } },
    { 55, new List<int> { 7397, 7399, 7400, 7402 } },
    { 56, new List<int> { 7406, 7407, 7408, 7409, 7410, 7411 } },
    { 57, new List<int> { 7412, 7413, 7414, 7415, 7416, 7417 } },
    { 58, new List<int> { 7418, 7419, 7420, 7421, 7422, 7423 } },
    { 59, new List<int> { 7424, 7425, 7460, 7461, 7462 } },
    { 60, new List<int> { 7430, 7431, 7432, 7433, 7434, 7435 } },
    { 61, new List<int> { 7436, 7437, 7438, 7439, 7440, 7441 } },
    { 62, new List<int> { 7442, 7443, 7456, 7457, 7458, 7459 } },
    { 63, new List<int> { 7444, 7445, 7446, 7447 } },
    { 64, new List<int> { 7448, 7449, 7450, 7451 } },
    { 65, new List<int> { 7452, 7453, 7454, 7455 } },
    { 66, new List<int> { 7463, 7464, 7465, 7466, 7467 } },
    { 67, new List<int> { 7468, 7469, 7470, 7471, 7472 } },
    { 68, new List<int> { 7473, 7474, 7475, 7476, 7477 } },
    { 69, new List<int> { 7478, 7479, 7480, 7481, 7482 } },
    { 70, new List<int> { 7483, 7484, 7485, 7486 } },
    { 71, new List<int> { 7487, 7488, 7489, 7490 } },
    { 72, new List<int> { 7491, 7492, 7493, 7494 } },
    { 73, new List<int> { 7495, 7496, 7497, 7498 } },
    { 74, new List<int> { 7499, 7500, 7501, 7502 } },
    { 75, new List<int> { 7516, 7517, 7518, 7519, 7520, 7521 } },
    { 76, new List<int> { 7522, 7523, 7524, 7525, 7526 } },
    { 77, new List<int> { 7528, 7529, 7532, 7534, 7536, 7537 } },
    { 78, new List<int> { 7530, 7531, 7533, 7535 } },
    { 79, new List<int> { 7538, 7539, 7540, 7541 } },
    { 80, new List<int> { 7542, 7543, 7544, 7545 } },
    { 81, new List<int> { 7546, 7547, 7548, 7549 } },
    { 82, new List<int> { 7550, 7551, 7552, 7553 } },
    { 83, new List<int> { 7554, 7555, 7556, 7557, 7558 } },
    { 84, new List<int> { 7559, 7560, 7561, 7562, 7563 } },
    { 85, new List<int> { 7564, 7565, 7566, 7567, 7568 } },
    { 86, new List<int> { 7569, 7570, 7571, 7572, 7573 } },
    { 87, new List<int> { 7582, 7583, 7584, 7585 } },
    { 88, new List<int> { 7586, 7587, 7588, 7589 } },
    { 89, new List<int> { 7595, 7596, 7598, 7599, 10934, 10935 } },
    { 90, new List<int> { 7601, 7603, 7604, 7605 } },
    { 91, new List<int> { 7606, 7607, 7608, 7609 } },
    { 92, new List<int> { 7610, 7611, 7612, 7613 } },
    { 93, new List<int> { 7614, 7615, 7616, 7617 } },
    { 94, new List<int> { 7618, 7619, 7620, 7621, 7622 } },
    { 95, new List<int> { 7637, 7638, 7641, 7642, 7643 } },
    { 96, new List<int> { 7644, 7645, 7646, 7647, 7648 } },
    { 97, new List<int> { 7649, 7650, 7651, 7652, 7653 } },
    { 98, new List<int> { 7690, 7692, 7693, 7694, 7695, 7696 } },
    { 99, new List<int> { 8740, 8741, 8742, 8743, 8744 } },
    { 100, new List<int> { 8746, 8747, 8748, 8749, 8750 } },
    { 101, new List<int> { 8753, 8754, 8755, 8756, 8757 } },
    { 102, new List<int> { 8758, 8759, 8760, 8761, 8762 } },
    { 103, new List<int> { 8763, 8764, 8765, 8766, 8767 } },
    { 104, new List<int> { 8768, 8769, 8770, 8771, 8772 } },
    { 105, new List<int> { 8773, 8774, 8775, 8776, 8777 } },
    { 106, new List<int> { 8778, 8779, 8780, 8781, 8782 } },
    { 107, new List<int> { 8783, 8784, 8785, 8786, 8788 } },
    { 108, new List<int> { 8789, 8790, 8791, 8792, 8793 } },
    { 109, new List<int> { 8849, 8850, 8851, 8852, 8853 } },
    { 110, new List<int> { 8854, 8855, 8856, 8857, 8858 } },
    { 111, new List<int> { 8859, 8860, 8861, 8862, 8863 } },
    { 112, new List<int> { 8864, 8865, 8866, 8867, 8868 } },
    { 113, new List<int> { 8869, 8870, 8871, 8872, 8873 } },
    { 114, new List<int> { 8874, 8875, 8876, 8877, 8878 } },
    { 115, new List<int> { 8879, 8880, 8881, 8882, 8883 } },
    { 116, new List<int> { 8884, 8885, 8886, 8887, 8888 } },
    { 117, new List<int> { 8889, 8890, 8891, 8892, 8893 } },
    { 119, new List<int> { 8918, 8919, 8920, 8921, 8922, 8923 } },
    { 120, new List<int> { 8924, 8925, 8926, 8927, 8928, 8929 } },
    { 121, new List<int> { 8931, 8932, 8933, 8935, 8936 } },
    { 122, new List<int> { 8934 } },
    { 123, new List<int> { 8938, 8939, 8940, 8941, 8942, 8945 } },
    { 124, new List<int> { 8966, 8967, 8969, 8970, 8972, 8973 } },
    { 125, new List<int> { 8975, 8976, 8977, 8978, 8979, 8980 } },
    { 126, new List<int> { 8981, 8982, 8983, 8984, 8985, 8986 } },
    { 127, new List<int> { 10280, 10281, 10282, 10283, 10336 } },
    { 128, new List<int> { 10284, 10285, 10286, 10287 } },
    { 129, new List<int> { 10288, 10289, 10290, 10291 } },
    { 130, new List<int> { 10292, 10293, 10294, 10295 } },
    { 132, new List<int> { 10302, 10303, 10304, 10316 } },
    { 133, new List<int> { 10305, 10306, 10307, 10308 } },
    { 134, new List<int> { 10391, 10392, 10393, 10394, 10395 } },
    { 135, new List<int> { 10401, 10402, 10403, 10404 } },
    { 136, new List<int> { 11157, 11158, 11159, 11160, 11161 } }
};

        public collectionsQuestionsMenu()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void colButton_Click(object sender, EventArgs e)
        {
            this.continueButton.Enabled = true;
            this.chosen_text = ((Button)sender).Text.ToString();

            this.colButton1.BackColor = Color.White;
            this.colButton2.BackColor = Color.White;
            this.colButton3.BackColor = Color.White;

            ((Button)sender).BackColor = Color.LightBlue; // show which category is chosen
        }

        private void continueButton_Click(object sender, EventArgs e)
        {
            List<int> colIds = title2colIds[this.chosen_text];

            int col_id = colIds[random.Next(colIds.Count)]; // choose rand collection of the category
            List<int> questions = colId2qIds[col_id]; // get the questions of the collection

            questionsPage c;
            //get the questions here

            if (this.timePerQPicker.Enabled)
                c = new questionsPage(col_id, questions, timePerQPicker.Value.Minute * 60 + timePerQPicker.Value.Second); // CHANGE!!!!!42
            else
                c = new questionsPage(col_id, questions, 0); // CHANGE!!!!!42

            c.Show();
            this.Close();
        }

        private void backToMainMenu_Click(object sender, EventArgs e)
        {
            menuPage c = new menuPage();

            c.Show();
            this.Close();
        }
        private void timePerQCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.timePerQPicker.Enabled = ((CheckBox)sender).Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
