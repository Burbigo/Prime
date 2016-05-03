using System;
using System.Windows.Forms;

namespace Prime
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void About_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
