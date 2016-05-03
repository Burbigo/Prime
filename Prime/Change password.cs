using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Change_password : Form
    {
        public Change_password()
        {
            InitializeComponent();
            CenterToScreen();
            textBox1.Text = Program.Login;
            textBox1.Enabled = false;
            textBox1.TextAlign = HorizontalAlignment.Center;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Change_password_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
                MessageBox.Show("One of the fields is empty\nPlease fill in all fields");
            else
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
                        if (textBox1.Text == "" || textBox2.Text == "")
                            throw new Exception("One of the fields is empty\nPlease fill in all fields");
                        connection.Open();
                        cmd =
                            new SqlCommand(
                                "UPDATE Users SET Сountersign='" + textBox2.Text + "' WHERE Username='" + textBox1.Text +
                                "'", connection);
                        cmd.ExecuteReader();
                        MessageBox.Show("Password changed successfully\nAnother time pls dont forget the password)");
                        Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                }
            }
        }

        
    }
}
