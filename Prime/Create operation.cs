using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Create_operation : Form
    {
        public Create_operation()
        {
            InitializeComponent();
            CenterToScreen();
            comboBox1.Items.Add("Connecting");
            comboBox1.Items.Add("Repairing");
            textBox5.Text = Program.Login;
            textBox5.Enabled = false;
            textBox5.TextAlign = HorizontalAlignment.Center;
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void Create_operation_Load(object sender, System.EventArgs e)
        {
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == "" ||
                comboBox1.Text == "")
                MessageBox.Show("One of the fields is empty\nPlease fill in all fields");
            else
            {


                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                SqlCommand cmd;
                SqlDataReader sdr;
                string s;
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("SELECT Name FROM Subnets WHERE Name='" + textBox2.Text + "'", connection);
                    sdr = cmd.ExecuteReader();
                    if (!sdr.Read())
                    {
                        MessageBox.Show(@"Subnet with this name doesn't exist");
                    }
                    else
                    {
                        s = sdr.GetString(0);
                        connection.Close();
                        try
                        {
                            connection.Open();
                            cmd = new SqlCommand("SELECT * FROM Devices WHERE Name='" + textBox1.Text + "'", connection);
                            sdr = cmd.ExecuteReader();
                            if (!sdr.Read())
                            {
                                MessageBox.Show(@"Device with this name doesn't exist");
                            }
                            else
                            {
                                connection.Close();
                                try
                                {
                                    connection.Open();
                                    cmd =
                                        new SqlCommand(
                                            "SELECT Engineer FROM Subnets WHERE Name='" + textBox2.Text + "'",
                                            connection);
                                    sdr = cmd.ExecuteReader();
                                    if (!sdr.Read() || !sdr.GetString(0).Contains(textBox3.Text))
                                    {
                                        MessageBox.Show(
                                            @"This engineer isn't responsble for this subnet or doesn't exist");
                                    }
                                    else
                                    {
                                        connection.Close();
                                        try
                                        {
                                            connection.Open();
                                            cmd =
                                                new SqlCommand(
                                                    "INSERT INTO Operations VALUES(@name,@subnet,@device,@engineer,@autor,@status,@reject)",
                                                    connection);
                                            cmd.Parameters.Add("@name", comboBox1.Text);
                                            cmd.Parameters.Add("@subnet", textBox2.Text);
                                            cmd.Parameters.Add("@device", textBox1.Text);
                                            cmd.Parameters.Add("@engineer", textBox3.Text);
                                            cmd.Parameters.Add("@autor", textBox5.Text);
                                            cmd.Parameters.Add("@status", textBox4.Text);
                                            cmd.Parameters.Add("@reject", "");
                                            cmd.ExecuteNonQuery();
                                            MessageBox.Show("Operation created successfully");
                                            Close();
                                        }
                                        catch (Exception exp)
                                        {
                                            MessageBox.Show(exp.Message);
                                        }
                                    }
                                }
                                catch
                                    (Exception exp)
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
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }
            }
        }
    }
}