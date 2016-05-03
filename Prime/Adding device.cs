using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Adding_device : Form
    {
        public Adding_device()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Adding_device_Load(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private static class RandomIpGenerator
        {
            private static Random _random = new Random();

            public static string GetRandomIp()
            {
                return string.Format("{0}.{1}.{2}.{3}", _random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255),
                    _random.Next(0, 255));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                MessageBox.Show("One of the fields is empty\nPlease fill in all fields");
            else
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                connection.Open();
                var cmd = new SqlCommand("SELECT * FROM Subnets WHERE Name='" + textBox4.Text + "'", connection);
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
                        cmd = new SqlCommand(
                            "INSERT INTO Devices VALUES (@name,@type,@status,@subnet,@ip,@data1,@lastchange,@data2)",
                            connection);
                        cmd.Parameters.Add("@name", textBox1.Text);
                        cmd.Parameters.Add("@type", textBox2.Text);
                        cmd.Parameters.Add("@status", textBox3.Text);
                        cmd.Parameters.Add("@subnet", textBox4.Text);
                        cmd.Parameters.Add("@ip", RandomIpGenerator.GetRandomIp());
                        cmd.Parameters.Add("@data1", dateTimePicker1.Value.Date);
                        cmd.Parameters.Add("@lastchange", @"New");
                        cmd.Parameters.Add("@data2", Program.ThisDay);
                        cmd.ExecuteNonQuery();
                        
                        int x = 0;
                        connection.Close();
                        try
                        {
                            connection.Open();
                            cmd = new SqlCommand("SELECT COUNT(*) FROM Devices WHERE Subnet_name='" + textBox4.Text + "'",
                                connection);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                                x = sdr.GetInt32(0);
                            connection.Close();
                            try
                            {
                                connection.Open();
                                cmd =
                                    new SqlCommand(
                                        "UPDATE Subnets SET Number_of_devices='" + x + "' WHERE Name='" +
                                        textBox4.Text + "'", connection);
                                cmd.ExecuteNonQuery();

                            }
                            catch (Exception exp)
                            {
                                MessageBox.Show(exp.Message);
                            }
                            
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show(exp.Message);
                        }
                        
                        MessageBox.Show(@"Device added", @"Succseseful");
                        Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message, @"Device doesn't added");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}