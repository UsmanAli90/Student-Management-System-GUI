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
    public partial class AdminAddingStudent : Form
    {
        OracleConnection con;
        public AdminAddingStudent()
        {
            InitializeComponent();
        }

        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM student1";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            con.Close();
        }

        private void AdminAddingStudent_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);

            updateGrid();


        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void gunaTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if (!(fname.Text.ToString()=="")&&!(lname.Text.ToString()=="")&&!(gen.Text.ToString()=="")&&!(feeStatus.Text.ToString()=="")&&!(bG.Text.ToString()=="")&&!(address.Text.ToString()=="")&&!(pin.Text.ToString()=="")||!(contactNumber.Text.ToString()==""))
            {


                con.Open();
                OracleCommand countCmd = new OracleCommand("SELECT COUNT(*) FROM student1", con);
                int count = Convert.ToInt32(countCmd.ExecuteScalar());
                //yha pr mna na student ka auto genrated student id dalni th to wo usky lye direct insert ki command si ni chlli th islye assy krk krna pra
                count++;
                count=count+1000;
                // Use the count in the INSERT statement
                OracleCommand insertCmd = new OracleCommand("INSERT INTO student1 VALUES (:col1, :col2, :col3,:col4, :col5, :col6,:col7, :col8,:col9,:col10)", con);
                insertCmd.Parameters.Add("col1", OracleDbType.Int32).Value = count;
                insertCmd.Parameters.Add("col2", OracleDbType.Varchar2).Value = fname.Text.ToString();
                insertCmd.Parameters.Add("col3", OracleDbType.Varchar2).Value = lname.Text.ToString();
                insertCmd.Parameters.Add("col4", OracleDbType.Varchar2).Value = feeStatus.Text.ToString();
                insertCmd.Parameters.Add("col5", OracleDbType.Varchar2).Value = gen.Text.ToString();
                insertCmd.Parameters.Add("col6", OracleDbType.Varchar2).Value = address.Text.ToString();
                insertCmd.Parameters.Add("col7", OracleDbType.Varchar2).Value = bG.Text.ToString();
                insertCmd.Parameters.Add("col8", OracleDbType.Varchar2).Value = contactNumber.Text.ToString();
                insertCmd.Parameters.Add("col9", OracleDbType.Varchar2).Value = pin.Text.ToString(); 
                insertCmd.Parameters.Add("col10", OracleDbType.Varchar2).Value = userName.Text.ToString();

                insertCmd.ExecuteNonQuery();
                //idhr ab isk password or student id sath e student login table ma rkhny lga hu  taa kjb b badma student login kry
                OracleCommand ins = new OracleCommand("INSERT INTO studentlogin VALUES (:col1,:col2)",con);
                ins.Parameters.Add("col1", OracleDbType.Int32).Value = count;
                ins.Parameters.Add("col2", OracleDbType.Varchar2).Value = Hash.Hash_SHA1(pin.Text.ToString());

                ins.ExecuteNonQuery();

                fname.Text="";
                lname.Text="";
                contactNumber.Text="";
                userName.Text="";
                address.Text="";
                feeStatus.Text=null;
                gen.Text=null;
                pin.Text="";
                bG.Text=null;

                con.Close();
            }
            else
            {
                MessageBox.Show("Something is missing");
            }
            updateGrid();


        }

        private void bGroup_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.ShowDialog();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private int Close_Click(object sender, EventArgs e)
        {
            return 0 ;

        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}