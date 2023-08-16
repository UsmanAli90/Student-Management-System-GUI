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
    public partial class AdminaddingStudentToClass : Form
    {
        OracleConnection con;
     
        public AdminaddingStudentToClass()
        {
            InitializeComponent();
        }

       
        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM classDetails";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            con.Close();
        }


        private void updateGrid2()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT class_id,class_name FROM class";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView2.DataSource = empDT;
            con.Close();
        }
        private void updateGrid3()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT student_id,fname,lname FROM student1";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView3.DataSource = empDT;
            con.Close();
        }
        private void AdminaddingStudentToClass_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
            updateGrid2();
            updateGrid3();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            con.Open();
            int i = 0;
            int j = 0;
            if (!(cid.Text.ToString()=="")  &&!(sid.Text.ToString()==""))
            {
                if (int.TryParse(cid.Text, out int cid1) && int.TryParse(sid.Text, out int sid1))

                {
                    // teacherId.Text should be used as a parameter, not directly in the SQL query
                    string query = "SELECT student_id FROM student1 WHERE student_id = :studentId";
                    OracleCommand command = new OracleCommand(query, con);
                    command.Parameters.Add(":studentId", OracleDbType.Int32).Value = Convert.ToInt32(sid.Text);
                    object result = command.ExecuteScalar();


                    string query1 = "SELECT class_id FROM class WHERE class_id = :classId";
                    OracleCommand command1 = new OracleCommand(query1, con);
                    command1.Parameters.Add(":classId", OracleDbType.Int32).Value = Convert.ToInt32(cid.Text);


                    // ExecuteScalar() returns an object, so we need to cast it to the appropriate type
                    object result1 = command1.ExecuteScalar();

                    if (result != null && result != DBNull.Value && result1 != null && result1 != DBNull.Value)
                    {
                        i = Convert.ToInt32(result);
                        j = Convert.ToInt32(result1);
                    }

                    if (i == 0 && j == 0)
                    {
                        MessageBox.Show("Make sure that the class Id and Student Id is correct");
                    }
                    else
                    {
                        OracleCommand command3 = con.CreateCommand();
                        command3.CommandText = "SELECT std_id, class_id FROM classDetails  WHERE std_id = :sid AND class_id = :cid";
                        command3.Parameters.Add(":sid", OracleDbType.Int32).Value = int.Parse(sid.Text);
                        command3.Parameters.Add(":cid", OracleDbType.Int32).Value = int.Parse(cid.Text);

                        object result3 = command3.ExecuteScalar();

                        if (result3 == null)
                        {
                            OracleCommand insertEmp = con.CreateCommand();
                            insertEmp.CommandText = "INSERT INTO classDetails (class_id, std_id) VALUES (:cid, :sid)";
                            insertEmp.Parameters.Add(":cid", OracleDbType.Int32).Value = int.Parse(cid.Text);
                            insertEmp.Parameters.Add(":sid", OracleDbType.Int32).Value = int.Parse(sid.Text);
                            insertEmp.CommandType = CommandType.Text;
                            insertEmp.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("This Student is already in this class");
                        }

                    }
                }
                else
                {
                    
                    MessageBox.Show("Invalid format");
                }
            }
            else
            {
                MessageBox.Show("something is missing");
            }
            con.Close();
            updateGrid();
            updateGrid2();
            updateGrid3();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin admin = new Admin();
            admin.ShowDialog();
        }
    }
}
