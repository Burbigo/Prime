using System;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace Prime
{
    public partial class Password_recovery : Form
    {
        public Password_recovery()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Password_recovery_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (var i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            var connection =
                new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
            connection.Open();
            var cmd = new SqlCommand("SELECT * FROM Users WHERE Email='" + textBox1.Text + "'", connection);
            var sdr = cmd.ExecuteReader();
            if (!sdr.Read())
            {
                MessageBox.Show(@"User with this email doesn't exist");
            }
            else
            {
                try
                {

                    var client = new SmtpClient
                    {

                        Port = 587,
                        Host = "smtp.gmail.com",
                        EnableSsl = true,
                        Timeout = 10000,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new System.Net.NetworkCredential("Vados0620@gmail.com", "VA141995XA")
                    };

                    var mm = new MailMessage("Vados0620@gmail.com", textBox1.Text, "Prime.Recovery password",
                        ("Your new password are:\n" + finalString))
                    {
                        BodyEncoding = Encoding.UTF8,
                        DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure
                    };

                    client.Send(mm);
                    connection.Close();
                    try
                    {
                        connection =
                            new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                        connection.Open();
                        cmd = new SqlCommand(
                            "UPDATE Users SET Сountersign='" + finalString + "' WHERE Email='" + textBox1.Text + "'",
                            connection);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message);
                    }
                    MessageBox.Show(@"Password changed successfully");
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