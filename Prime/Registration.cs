using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Registration_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataReader sdr;
            SqlCommand cmd;
            var connection =
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
            if (textBox6.Text.Length == 0 || textBox5.Text.Length == 0 || textBox1.Text.Length == 0 ||
                textBox2.Text.Length == 0 || textBox3.Text.Length == 0)
                MessageBox.Show("One of the fields is empty\nPlease fill in all fields");
            else
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("SELECT * FROM Users WHERE Username='" + textBox1.Text + "'", connection);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        MessageBox.Show(@"User with this login is already exist");
                    }
                    else
                    {
                        connection.Close();
                        connection.Open();
                        cmd = new SqlCommand("SELECT * FROM Users WHERE Email='" + textBox2.Text + "'", connection);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                        {
                            MessageBox.Show(@"User with this email is alreade exist");
                        }
                        else
                        {
                            connection.Close();
                            try
                            {
                                connection.Open();
                                cmd =
                                    new SqlCommand(
                                        "INSERT INTO Users VALUES(@name,@surname,@username,@email,@password,@status)",
                                        connection);

                                cmd.Parameters.Add("@name", textBox6.Text);
                                cmd.Parameters.Add("@surname", textBox5.Text);
                                cmd.Parameters.Add("@username", textBox1.Text);
                                cmd.Parameters.Add("@email", textBox2.Text);
                                cmd.Parameters.Add("@password", textBox3.Text);
                                cmd.Parameters.Add("@status", textBox4.Text);
                                cmd.ExecuteNonQuery();
                                MessageBox.Show(@"You successfully registered");
                                Close();

                            }
                            catch (Exception exp)
                            {

                                MessageBox.Show(exp.Message);
                            }
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
    }
}