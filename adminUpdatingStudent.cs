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
    public partial class adminUpdatingStudent : Form
    {
        OracleConnection con;
        public adminUpdatingStudent()
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
        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            con.Open();

            if (id.Text.ToString()!="")
            {
                if (int.TryParse(id.Text, out int studentId))
                {
                    int i = 0;

                    string query = "SELECT student_id FROM student1 WHERE student_id = :studentId";
                    OracleCommand command = new OracleCommand(query, con);
                    command.Parameters.Add(":studentId", OracleDbType.Int32).Value = Convert.ToInt32(id.Text);


                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        i = Convert.ToInt32(result);
                    }

                    if (i == 0)
                    {
                        MessageBox.Show("No student with this ID");
                    }
                    else
                    {
                        if (fname.Text.ToString()!="")
                        {
                            OracleCommand Updatename = con.CreateCommand();
                            Updatename.CommandText = "UPDATE student1 SET fname='" + fname.Text.ToString() + "' WHERE student_id=" + id.Text.ToString();
                            Updatename.CommandType = CommandType.Text;
                            Updatename.ExecuteNonQuery();
                        }

                        if (lname.Text.ToString()!="")
                        {
                            OracleCommand Updatelname = con.CreateCommand();
                            Updatelname.CommandText = "UPDATE student1 SET lname='" + lname.Text.ToString() + "' WHERE student_id=" + id.Text.ToString();
                            Updatelname.CommandType = CommandType.Text;
                            Updatelname.ExecuteNonQuery();
                        }
                        if (address.Text.ToString()!="")
                        {
                            OracleCommand Updateaddress = con.CreateCommand();
                            Updateaddress.CommandText = "UPDATE student1 SET address='" + address.Text.ToString() + "' WHERE student_id=" + id.Text.ToString();
                            Updateaddress.CommandType = CommandType.Text;
                            Updateaddress.ExecuteNonQuery();
                        }
                        if (contactNumber.Text.ToString()!="")
                        {
                            OracleCommand Updatenum = con.CreateCommand();
                            Updatenum.CommandText = "UPDATE student1 SET contactnumber='" + contactNumber.Text.ToString() + "' WHERE student_id=" + id.Text.ToString();
                            Updatenum.CommandType = CommandType.Text;
                            Updatenum.ExecuteNonQuery();
                        }
                        if (userName.Text.ToString()!="")
                        {
                            OracleCommand UpdateUname = con.CreateCommand();
                            UpdateUname.CommandText = "UPDATE student1 SET user_name='" + userName.Text.ToString() + "' WHERE student_id=" + id.Text.ToString();
                            UpdateUname.CommandType = CommandType.Text;
                            UpdateUname.ExecuteNonQuery();
                        }
                        id.Text="";
                        fname.Text="";
                        lname.Text="";
                        contactNumber.Text="";
                        userName.Text="";
                        address.Text="";

                    }
                }
                else
                {
                    MessageBox.Show("invalid format");
                }
            }
            else
            {
                MessageBox.Show("Enter student id first to update ");
            }
           
            con.Close();
            updateGrid();

        }

        private void adminUpdatingStudent_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);

            updateGrid();

        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin ad = new Admin();
            ad.ShowDialog();
            
        }
    }
}
