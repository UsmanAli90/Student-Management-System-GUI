using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DB_Project
{
    public partial class AdminLogin : Form
    {

        OracleConnection con;

        public AdminLogin()
        {
            InitializeComponent();
        }
     

        private void Form1_Load(object sender, EventArgs e)
        {

            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);


        }



        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            con.Open();
            //string mypass = Hash.Hash_SHA1(password.Text.ToString());

            //string query = "INSERT INTO adminlogin VALUES ('admin', :mypass)";
            //OracleCommand command = new OracleCommand(query, con);
            //command.Parameters.Add(":mypass", OracleDbType.Varchar2).Value = mypass;
            //command.ExecuteNonQuery();

            //con.Close();

            OracleCommand command = new OracleCommand("SELECT password FROM adminlogin where name ='admin'", con);
            string pass = command.ExecuteScalar().ToString();

            if (pass== Hash.Hash_SHA1(password.Text) && userName.Text.ToString()=="admin")
            {
                this.Hide();
                Admin ad = new Admin();
                ad.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("incorrect passsword or username");
            }

            con.Close();



        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            firstpage fp = new firstpage();
            fp.ShowDialog();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
