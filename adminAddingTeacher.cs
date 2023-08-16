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
    public partial class adminAddingTeacher : Form
    {
        OracleConnection con;

        public adminAddingTeacher()
        {
            InitializeComponent();
        }

        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM teacher";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            con.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (!(fname.Text.ToString()=="")&&!(lname.Text.ToString()=="")&&!(gen.Text.ToString()=="")&&!(email.Text.ToString()=="")&&!(pin.Text.ToString()=="")&&!(contactNumber.Text.ToString()=="")&&!(userName.Text.ToString()==""))
            {



                con.Open();
                OracleCommand countCmd = new OracleCommand("SELECT COUNT(*) FROM teacher", con);
                int count = Convert.ToInt32(countCmd.ExecuteScalar());
                count++;
                count=count+1000;
                OracleCommand insertEmp = con.CreateCommand();
                insertEmp.CommandText = "INSERT INTO teacher (teacherid, fname, lname, email, gender, contactno, username, password) VALUES (:id, :fname, :lname, :email, :gender, :contactno, :username, :password)";
                insertEmp.Parameters.Add("id", OracleDbType.Int32).Value = count;
                insertEmp.Parameters.Add("fname", OracleDbType.Varchar2).Value = fname.Text.ToString();
                insertEmp.Parameters.Add("lname", OracleDbType.Varchar2).Value = lname.Text.ToString();
                insertEmp.Parameters.Add("email", OracleDbType.Varchar2).Value = email.Text.ToString();
                insertEmp.Parameters.Add("gender", OracleDbType.Varchar2).Value = gen.Text.ToString();
                insertEmp.Parameters.Add("contactno", OracleDbType.Varchar2).Value = contactNumber.Text.ToString();
                insertEmp.Parameters.Add("username", OracleDbType.Varchar2).Value = texttt.Text.ToString();
                insertEmp.Parameters.Add("password", OracleDbType.Varchar2).Value =pin.Text.ToString();

                insertEmp.CommandType = CommandType.Text;
                insertEmp.ExecuteNonQuery();

               
                //idhr ab isk password or student id sath e student login table ma rkhny lga hu  taa kjb b badma student login kry
                OracleCommand ins = new OracleCommand("INSERT INTO teacherlogin VALUES (:col1,:col2)", con);
                ins.Parameters.Add("col1", OracleDbType.Int32).Value = count;
                ins.Parameters.Add("col2", OracleDbType.Varchar2).Value = Hash.Hash_SHA1(pin.Text.ToString());
                ins.ExecuteNonQuery();
                con.Close();
                updateGrid();

                fname.Text="";
                lname.Text="";
                contactNumber.Text="";
                userName.Text="";
               
                gen.Text=null;
                pin.Text="";
                email.Text="";

              
            }
            else
            {
                MessageBox.Show("Something is missing");
            }
            updateGrid();

        }

        private void adminAddingTeacher_Load(object sender, EventArgs e)
        {

            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);

            updateGrid();

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.ShowDialog();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
