using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Delete_subnet : Form
    {
        public Delete_subnet()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var connection =
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
            connection.Open();
            var cmd = new SqlCommand("SELECT * FROM Subnets WHERE Name='" + textBox1.Text + "'", connection);
            var sdr = cmd.ExecuteReader();
            if (!sdr.Read())
            {
                MessageBox.Show(@"Subnet with this name doesn't exist");
            }
            else
            {
                connection.Close();
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("DELETE FROM Subnets WHERE Name='" + textBox1.Text + "'", connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(@"Subnet deleted", @"Succseseful");
                    Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, @"Subnet doesn't deleted");
                }
            }
        }

        private void Delete_subnet_Load(object sender, EventArgs e)
        {

        }
    }
}
        