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
    public partial class AdminUpdatingTeacher : Form
    {
        OracleConnection con;
        public AdminUpdatingTeacher()
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

            con.Open();
            if (id.Text.ToString()!="")
            {
                if (int.TryParse(id.Text, out int studentId))
                {


                    int i = 0;
                    // teacherId.Text should be used as a parameter, not directly in the SQL query
                    string query = "SELECT teacherId FROM teacher WHERE teacherid = :teacherId";
                    OracleCommand command = new OracleCommand(query, con);
                    command.Parameters.Add(":teacherId", OracleDbType.Int32).Value = Convert.ToInt32(id.Text);

                    // ExecuteScalar() returns an object, so we need to cast it to the appropriate type
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        i = Convert.ToInt32(result);
                    }

                    if (i == 0)
                    {
                        MessageBox.Show("No teacher with this ID");
                    }
                    else
                    {

                        if (fname.Text.ToString()!="")
                        {
                            OracleCommand Updatename = con.CreateCommand();
                            Updatename.CommandText = "UPDATE teacher SET fname='" + fname.Text.ToString() + "' WHERE teacherid=" + id.Text.ToString();
                            Updatename.CommandType = CommandType.Text;
                            Updatename.ExecuteNonQuery();
                        }

                        if (lname.Text.ToString()!="")
                        {
                            OracleCommand Updatelname = con.CreateCommand();
                            Updatelname.CommandText = "UPDATE teacher SET lname='" + lname.Text.ToString() + "' WHERE teacherid=" + id.Text.ToString();
                            Updatelname.CommandType = CommandType.Text;
                            Updatelname.ExecuteNonQuery();
                        }
                        if (contactNumber.Text.ToString()!="")
                        {
                            OracleCommand Updatenum = con.CreateCommand();
                            Updatenum.CommandText = "UPDATE teacher SET contactNo='" + contactNumber.Text.ToString() + "' WHERE teacherid=" + id.Text.ToString();
                            Updatenum.CommandType = CommandType.Text;
                            Updatenum.ExecuteNonQuery();
                        }
                        if (userName.Text.ToString()!="")
                        {
                            OracleCommand UpdateUname = con.CreateCommand();
                            UpdateUname.CommandText = "UPDATE teacher SET user_name='" + userName.Text.ToString() + "' WHERE teacherid=" + id.Text.ToString();
                            UpdateUname.CommandType = CommandType.Text;
                            UpdateUname.ExecuteNonQuery();
                        }
                        id.Text="";
                        fname.Text="";
                        lname.Text="";
                        contactNumber.Text="";
                        userName.Text="";
                    }
                }
                else
                {
                    MessageBox.Show("Invalid format ");
                }
            }
            else
            {
                MessageBox.Show("Enter teacher id first to update ");
            }
           
            con.Close();
            updateGrid();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.ShowDialog();
        }

        private void AdminUpdatingTeacher_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);

            updateGrid();

        }
    }
}
