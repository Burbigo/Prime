using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Change_operation : Form
    {
        public Change_operation()
        {
            InitializeComponent();
            CenterToScreen();
            switch (Program.Status)
            {
                case "Engineer":
                    comboBox1.Items.Add("Completed");
                    comboBox1.Items.Add("In progress");
                    comboBox1.Items.Add("Rejected");
                    textBox2.Text = Program.Login;
                    textBox2.Enabled = false;
                    break;
                case "Admin":
                    comboBox1.Items.Add("Closed");
                    comboBox1.Items.Add("Canceled");
                    break;
            }
        }

        private void Change_operation_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""  || comboBox1.Text == "")
                MessageBox.Show("One of the fields is empty\nPlease fill in all fields");
            else
            {
                try
                {
                    var connection =
                        new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                    connection.Open();
                    var cmd = new SqlCommand(
                        "SELECT MAX(ID) FROM Operations", connection);
                    var sdr = cmd.ExecuteReader();
                    if (!sdr.Read() || (sdr.GetInt32(0) < Int32.Parse(textBox1.Text)))
                    {
                        MessageBox.Show(@"Operation with this ID doesn't exist");
                    }
                    else
                    {
                        string s = null;
                        string device = null;
                        string status = null;
                        bool check = true;

                        connection.Close();
                        connection.Open();
                        cmd = new SqlCommand("SELECT Subnet_name FROM Operations WHERE ID='" + textBox1.Text + "'",
                            connection);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                            s = sdr.GetString(0);
                        connection.Close();

                        connection.Open();
                        cmd = new SqlCommand("SELECT Device_name FROM Operations WHERE ID='" + textBox1.Text + "'",
                            connection);
                        sdr = cmd.ExecuteReader();
                        if (sdr.Read())
                            device = sdr.GetString(0);
                        connection.Close();

                        connection.Open();
                        cmd =
                            new SqlCommand(
                                "SELECT Engineer FROM Subnets WHERE Name='" + s + "'", connection);
                        sdr = cmd.ExecuteReader();
                        if (!sdr.Read() || !sdr.GetString(0).Contains(textBox2.Text))
                        {
                            check = false;
                            MessageBox.Show(
                                @"This engineer isn't responsble for this subnet or doesn't exist");
                        }
                        else
                        {
                            if (textBox2.Text != "")
                            {
                                connection.Close();
                                connection.Open();
                                cmd = new SqlCommand(
                                    "UPDATE Operations SET Engineer='" + textBox2.Text + "' WHERE ID='" + textBox1.Text +
                                    "'", connection);
                                cmd.ExecuteNonQuery();
                            }

                            connection.Close();
                            connection.Open();
                            cmd = new SqlCommand(
                                "SELECT Name FROM Operations WHERE ID='" + textBox1.Text + "'", connection);
                            sdr = cmd.ExecuteReader();
                            if (sdr.Read())
                            {
                                if (sdr.GetString(0) == "Connecting")
                                {
                                    connection.Close();
                                    connection.Open();
                                    cmd = new SqlCommand(
                                        "SELECT Device_status FROM Devices WHERE Name='" + device + "'", connection);
                                    sdr = cmd.ExecuteReader();
                                    if (sdr.Read())
                                        status = sdr.GetString(0);
                                    if (status == "New" && comboBox1.Text == "Completed")
                                    {
                                        connection.Close();
                                        connection.Open();
                                        cmd = new SqlCommand(
                                            "UPDATE Devices SET Device_status='Operating' WHERE Name='" + device + "'",
                                            connection);
                                        cmd.ExecuteNonQuery();

                                        connection.Close();
                                        connection.Open();
                                        cmd = new SqlCommand(
                                            "UPDATE Devices SET Last_change='New-->Operating' WHERE Name='" + device +
                                            "'", connection);
                                        cmd.ExecuteNonQuery();
                                    }
                                    else if (status == "Repairing" && comboBox1.Text == "Completed")
                                    {
                                        connection.Close();
                                        connection.Open();
                                        cmd = new SqlCommand(
                                            "UPDATE Devices SET Device_status='Operating' WHERE Name='" + device + "'",
                                            connection);
                                        cmd.ExecuteNonQuery();

                                        connection.Close();
                                        connection.Open();
                                        cmd = new SqlCommand(
                                            "UPDATE Devices SET Last_change='Repairing-->Operating' WHERE Name='" +
                                            device +
                                            "'", connection);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                                if (sdr.GetString(0) == "Repairing" && comboBox1.Text == "Completed")
                                {

                                    connection.Close();
                                    connection.Open();
                                    cmd = new SqlCommand(
                                        "UPDATE Devices SET Device_status='Repairing' WHERE Name='" + device + "'",
                                        connection);
                                    cmd.ExecuteNonQuery();

                                    connection.Close();
                                    connection.Open();
                                    cmd = new SqlCommand(
                                        "UPDATE Devices SET Last_change='Operating-->Repairing' WHERE Name='" + device +
                                        "'", connection);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        connection.Close();
                        connection.Open();
                        if (comboBox1.Text == "Rejected")
                        {
                            var rejected = new Rejected(textBox1.Text);
                            rejected.ShowDialog();
                        }
                        cmd =
                            new SqlCommand(
                                "UPDATE Operations SET Status_of_operation='" + comboBox1.Text + "' WHERE ID='" +
                                textBox1.Text + "'", connection);
                        cmd.ExecuteNonQuery();
                        if (check)
                        {
                            MessageBox.Show(@"Operation changed successfully");
                            Close();
                        }
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Operation doesn't changed\n"+exp.Message);
                }
            }
        }
    }
}
