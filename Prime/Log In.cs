using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class LogIn : Form
    {
        public LogIn()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var rg = new Registration();
            rg.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var connection =
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
            if (textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("One of the fields is empty\nPlease fill in all fields");
            }
            else
            {
                try
                {
                    connection.Open();
                    var cmd = new SqlCommand("SELECT Сountersign FROM Users WHERE Username='" + textBox3.Text + "'", connection);
                    var sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        if (sdr.GetString(0) == textBox1.Text)
                        {
                            Program.Check = true;
                            Close();
                            connection.Close();
                            connection.Open();
                            cmd = new SqlCommand("INSERT INTO History VALUES(@login,@data)", connection);
                            cmd.Parameters.Add("@login", textBox3.Text);
                            cmd.Parameters.Add("@data", Program.ThisDay);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show(@"Wrong Password!");
                        }
                        connection.Close();
                        try
                        {
                            connection.Open();
                            cmd = new SqlCommand("SELECT TOP 1 * FROM History ORDER BY ID DESC", connection);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                var s = sdr.GetString(1);
                                Program.Login = s;
                                connection.Close();
                                try
                                {
                                    connection.Open();
                                    cmd = new SqlCommand("SELECT User_status FROM Users WHERE Username='" + s + "'",
                                        connection);
                                    sdr = cmd.ExecuteReader();
                                    if (sdr.Read())
                                    {
                                        Program.Status = sdr.GetString(0);
                                    }
                                }
                                catch (Exception exp)
                                {
                                    MessageBox.Show(exp.Message);
                                }
                            }
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Users with this login doesn't exist.\nPlease Sign Up before entering in the system");
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var pr = new Password_recovery();
            pr.ShowDialog();
        }
       
    }
}