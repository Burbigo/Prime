using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Change_subnet : Form
    {
        public Change_subnet()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Change_subnet_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var connection =
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
            SqlCommand cmd;
            SqlDataReader sdr;
            string s;

            bool check = true;
            if (textBox3.Text == "")
            {
                MessageBox.Show("\"Old name\" is empty.\nPlease fill in \"Old name\"");
                check = false;
            }
            else
            {
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("SELECT * FROM Subnets WHERE Name='" + textBox3.Text + "'", connection);
                    sdr = cmd.ExecuteReader();
                    if (!sdr.Read())
                    {
                        MessageBox.Show(@"Subnet with this name doesn't exist");
                        check = false;
                    }
                    else
                    {
                        if (textBox2.Text != "")
                        {
                            connection.Close();
                            try
                            {
                                connection.Open();
                                cmd =
                                    new SqlCommand(
                                        "SELECT User_status FROM Users WHERE Username='" + textBox2.Text + "'",
                                        connection);
                                sdr = cmd.ExecuteReader();
                                if (!sdr.Read() || sdr.GetString(0) != "Engineer")
                                {
                                    MessageBox.Show(@"Engineer with this login doesn't exist");
                                    check = false;
                                }
                                else
                                {
                                    connection.Close();
                                    try
                                    {
                                        connection.Open();
                                        cmd =
                                            new SqlCommand(
                                                "SELECT Engineer FROM Subnets WHERE Name='" + textBox3.Text + "'",
                                                connection);
                                        sdr = cmd.ExecuteReader();
                                        if (sdr.Read() && sdr.GetString(0).Contains(textBox2.Text))
                                        {
                                            MessageBox.Show(@"This enginner is already attached to this subnet");
                                            check = false;
                                        }
                                        else
                                        {
                                            if (sdr.GetString(0) == "")
                                                s = textBox2.Text;
                                            else
                                                s = sdr.GetString(0) + ", " + textBox2.Text;
                                            connection.Close();
                                            try
                                            {
                                                connection.Open();
                                                cmd =
                                                    new SqlCommand(
                                                        "UPDATE Subnets SET Engineer='" + s + "' WHERE Name='" +
                                                        textBox3.Text +
                                                        "'",
                                                        connection);
                                                cmd.ExecuteNonQuery();
                                            }
                                            catch (Exception exp1)
                                            {
                                                MessageBox.Show(exp1.Message);
                                                check = false;
                                            }
                                        }
                                    }
                                    catch (Exception exp2)
                                    {
                                        MessageBox.Show(exp2.Message);
                                        check = false;
                                    }
                                }
                            }
                            catch (Exception exp3)
                            {
                                MessageBox.Show(exp3.Message);
                                check = false;
                            }
                        }

                        if (textBox4.Text != "")
                        {
                            connection.Close();
                            try
                            {
                                connection.Open();
                                cmd =
                                    new SqlCommand(
                                        "SELECT Engineer FROM Subnets WHERE Name='" + textBox3.Text + "'",
                                        connection);
                                sdr = cmd.ExecuteReader();
                                if (!sdr.Read() || !sdr.GetString(0).Contains(textBox4.Text))
                                {
                                    MessageBox.Show(@"This enginner doesn't attached to this subnet");
                                    check = false;
                                }
                                else
                                {
                                    s = sdr.GetString(0);
                                    if (s.IndexOf(textBox4.Text) - 2 < 0)
                                        s = s.Remove(s.IndexOf(textBox4.Text), textBox4.TextLength + 2);
                                    else
                                        s = s.Remove(s.IndexOf(textBox4.Text) - 2, textBox4.TextLength + 2);
                                   

                                    connection.Close();
                                    try
                                    {
                                        connection.Open();
                                        cmd =
                                            new SqlCommand(
                                                "UPDATE Subnets SET Engineer='" + s + "' WHERE Name='" +
                                                textBox3.Text +
                                                "'",
                                                connection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception exp1)
                                    {
                                        MessageBox.Show(exp1.Message);
                                        check = false;
                                    }
                                }
                            }
                            catch (Exception exp2)
                            {
                                MessageBox.Show(exp2.Message);
                                check = false;
                            }
                        }

                        if (textBox1.Text != "")
                        {
                            try
                            {
                                connection.Close();
                                connection =
                                    new SqlConnection(
                                        @"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                                connection.Open();
                                cmd = new SqlCommand("SELECT * FROM Subnets WHERE Name='" + textBox1.Text + "'",
                                    connection);
                                sdr = cmd.ExecuteReader();
                                if (sdr.Read())
                                {
                                    MessageBox.Show("Subnet with \"New name\" is already exist");
                                    check = false;
                                }
                                else
                                {
                                    connection.Close();
                                    try
                                    {
                                        connection.Open();
                                        cmd =
                                            new SqlCommand(
                                                "UPDATE Subnets SET Name='" + textBox1.Text + "' WHERE Name='" +
                                                textBox3.Text +
                                                "'",
                                                connection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    catch (Exception exp1)
                                    {
                                        MessageBox.Show(exp1.Message);
                                        check = false;
                                    }
                                }
                            }
                            catch (Exception exp2)
                            {
                                MessageBox.Show(exp2.Message);
                                check = false;
                            }
                        }
                    }
                }
                catch (Exception exp1)
                {
                    MessageBox.Show(exp1.Message);
                    check = false;
                }
            }
            if (check)
            {
                MessageBox.Show("Subnet changed successfully");
                Close();
            }
        }
    }
}