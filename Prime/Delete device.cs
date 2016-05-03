using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Delete_device : Form
    {
        public Delete_device()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void Delete_device_Load(object sender, EventArgs e)
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
            connection.Open();
            var cmd = new SqlCommand("SELECT * FROM Devices WHERE Name='" + textBox4.Text + "'", connection);
            var sdr = cmd.ExecuteReader();
            if (!sdr.Read())
            {
                MessageBox.Show(@"Device with this name doesn't exist");
            }
            else
            {
                var s = sdr.GetString(4);
                int x = 0;
                connection.Close();
                try
                {
                    connection.Open();
                    cmd = new SqlCommand("SELECT Number_of_devices FROM Subnets WHERE Name='" + s + "'", connection);
                    sdr = cmd.ExecuteReader();
                    if (sdr.Read())
                    {
                        x = sdr.GetInt32(0);
                    }

                    connection.Close();
                    x--;
                    connection.Open();
                    cmd = new SqlCommand("UPDATE Subnets SET Number_of_devices='"+x+"' WHERE Name='" + s + "'", connection);
                    cmd.ExecuteNonQuery();

                    connection.Close();
                    connection.Open();
                    cmd = new SqlCommand("DELETE FROM Devices WHERE Name='" + textBox4.Text + "'", connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(@"Device deleted", @"Succseseful");
                    Close();
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message, @"Device doesn't deleted");
                }
            }
        }
    }
}
