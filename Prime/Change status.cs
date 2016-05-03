using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Change_status : Form
    {
        public Change_status()
        {

            InitializeComponent();
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Engineer");
            comboBox1.Items.Add("Client");
            CenterToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var connection =
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
            connection.Open();
            var cmd = new SqlCommand("SELECT * FROM Users WHERE Username='" + textBox1.Text + "'", connection);
            var sdr = cmd.ExecuteReader();
            if (!sdr.Read())
            {
                MessageBox.Show(@"User with this login doesn't exist");
            }
            else
            {
                connection.Close();
                try
                {
                    connection.Open();
                    cmd =
                        new SqlCommand(
                            "UPDATE Users SET User_status='" + comboBox1.Text + "' WHERE Username='" + textBox1.Text +
                            "'", connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(@"User status changed successfully");
                    Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, @"Status doesn't changed");
                }
            }
        }

        private void Change_status_Load(object sender, EventArgs e)
        {

        }
    }
}

