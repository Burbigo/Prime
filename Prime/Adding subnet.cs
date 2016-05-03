using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Adding_subnet : Form
    {
        public Adding_subnet()
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
            if (textBox1.Text == "" || textBox3.Text == "")
                MessageBox.Show("One of the fields is empty\nPlease fill in all fields");
            else
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                connection.Open();
                var cmd = new SqlCommand("SELECT User_status FROM Users WHERE Username='" + textBox3.Text + "'",
                    connection);
                var sdr = cmd.ExecuteReader();
                if (!sdr.Read() || sdr[0].ToString() != "Engineer")
                {
                    MessageBox.Show(@"Engineer with this name doesn't exist");
                }
                else
                {
                    connection.Close();
                    try
                    {
                        int x = 0;
                        connection.Open();
                        cmd = new SqlCommand(
                            "INSERT INTO Subnets VALUES (@name,@number,@engineer)",
                            connection);
                        cmd.Parameters.Add("@name", textBox1.Text);
                        cmd.Parameters.Add("@number", x);
                        cmd.Parameters.Add("@engineer", textBox3.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show(@"Subnet added", @"Succseseful");
                        Close();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message, @"Subnet doesn't added");
                    }
                }
            }
        }

        private void Adding_subnet_Load(object sender, EventArgs e)
        {

        }
    }
}

