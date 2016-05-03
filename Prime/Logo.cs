using System;
using System.Windows.Forms;

namespace Prime
{
    public partial class Logo : Form
    {
        public Logo()
        {
            InitializeComponent();
        }

        private void clear_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Close();
            var fm1 = new LogIn();
            if (fm1.ShowDialog() == DialogResult.Cancel && Program.Check == false) 
                Application.Exit();
        }
    }
}