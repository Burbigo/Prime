﻿using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Prime
{
    public partial class Rejected : Form
    {
        private static string ID;
        public Rejected(string id)
        {
            CenterToScreen();
            InitializeComponent();
            ID = id;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var connection =
                    new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Prime;Integrated Security=True");
                SqlCommand cmd;
                string s;
            connection.Close();
            try
            {
                connection.Open();
                cmd =
                    new SqlCommand(
                        "UPDATE Operations SET Cause_of_rejection='" + @textBox1.Text + "' WHERE ID='" + ID + "'",
                        connection);
                cmd.ExecuteNonQuery();
                MessageBox.Show(@"Cause added successfuly");
                Close();
            }
            catch 
            {
                MessageBox.Show("Cause doesn't added\nCheck input text");
            } 
        }
    }
}
