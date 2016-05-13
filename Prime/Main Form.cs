using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var logo = new Logo();
            logo.ShowDialog();
            ShowDevices();
            ButtonVisible();
            groupBox1.Text = Program.Login+"("+Program.Status+")";
        }

        private void ButtonVisible()
        {
            switch (Program.Status)
            {
                case "Client":
                    button3.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = false;
                    button8.Enabled = false;
                    button9.Enabled = false;
                    button11.Enabled = false;
                    button12.Enabled = false;
                    button13.Enabled = false;
                    button14.Enabled = false;
                    button15.Enabled = false;
                    button17.Enabled = false;
                    button18.Enabled = false;
                    break;
                case "Engineer":
                    button3.Enabled = false;
                    button5.Enabled = false;
                    button6.Enabled = false;
                    button7.Enabled = false;
                    button8.Enabled = false;
                    button11.Enabled = false;
                    button13.Enabled = false;
                    button14.Enabled = false;
                    button15.Enabled = false;
                    button18.Enabled = false;
                    break;
            }
        }

        private void ShowDevices()
        {
            try
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                var cmd = new SqlCommand("SELECT * FROM Devices", connection);
                connection.Open();
                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void ShowOperations() 
        {
            try
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                var cmd = new SqlCommand("SELECT * FROM Operations", connection);
                connection.Open();
                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void ShowHistory()
        {
            try
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                var cmd = new SqlCommand("SELECT * FROM History", connection);
                connection.Open();
                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ShowUsers()
        {
            try
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                var cmd = new SqlCommand("SELECT * FROM Users", connection);
                connection.Open();
                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ShowSubnets() 
        {
            try
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                var cmd = new SqlCommand("SELECT * FROM Subnets", connection);
                connection.Open();
                var sda = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                sda.Fill(dt);
                dataGridView.DataSource = dt;
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
        private void Prime_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowDevices();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowUsers();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowHistory();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var exit = new Exit();
            exit.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            ShowDevices();
            var deldev = new Delete_device();
            deldev.ShowDialog();
            ShowDevices();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            ShowDevices();
            var addev = new Adding_device();
            addev.ShowDialog();
            ShowDevices();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            var about = new About();
            about.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            ShowOperations();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ShowUsers();
            var chstatus = new Change_status();
            chstatus.ShowDialog();
            ShowUsers();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowSubnets();
            var adsub = new Adding_subnet();
            adsub.ShowDialog();
            ShowSubnets();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ShowOperations();
            var chop = new Change_operation();
            chop.ShowDialog();
            ShowOperations();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowOperations();
            var newop = new Create_operation();
            newop.ShowDialog();
            ShowOperations();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ShowSubnets();
            var delsub = new Delete_subnet();
            delsub.ShowDialog();
            ShowSubnets();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                connection.Open();
                var cmd = new SqlCommand("DELETE FROM History", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show(@"History is cleaned");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            ShowHistory();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var chpass = new Change_password();
            chpass.ShowDialog();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ShowSubnets();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ShowSubnets();
            var chsub = new Change_subnet();
            chsub.ShowDialog();
            ShowSubnets();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {
                var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                connection.Open();
                var cmd = new SqlCommand("DELETE FROM Operations", connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show(@"Operations is cleaned");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            ShowOperations();
        }
    }
}