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
    public partial class teacherMarkingAttendance : Form
    {
        OracleConnection con;
        public int cidd1 { get; set; }

        public string datett1 { get; set; }
        public int tiddd1 { get; set; }
        public teacherMarkingAttendance()
        {
            InitializeComponent();
        }
        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "select student_id,fname from student1 ,classdetails where class_id=:cidd and std_id=student_id";
            getEmps.CommandType = CommandType.Text;
            getEmps.Parameters.Add("cidd", OracleDbType.Int32).Value = cidd1; // Replace 'cidd' with the actual value

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
            getEmps.CommandText = "SELECT s_id, status FROM attendance WHERE c_id = :cidd AND date_and_time = :date_and_time";
            getEmps.CommandType = CommandType.Text;
            getEmps.Parameters.Add("cidd", OracleDbType.Int32).Value = cidd1; // Replace 'cidd' with the actual value

            DateTime dateAndTime;
            if (DateTime.TryParse(datett1, out dateAndTime))
            {
                getEmps.Parameters.Add("date_and_time", OracleDbType.TimeStamp).Value = dateAndTime;
            }
            else
            {
                // Handle the case where datett1 is not a valid date
                MessageBox.Show("Invalid value for date_and_time.");
                con.Close();
                return; // Exit the method or perform additional error handling
            }

            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView2.DataSource = empDT;
            con.Close();

        }
        private void teacherMarkingAttendance_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
            updateGrid2();
            label4.Text=datett1;

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            con.Open();
            if (!string.IsNullOrEmpty(sid.Text) && int.TryParse(sid.Text, out int studentId) )
            {
                OracleCommand checkStudent = con.CreateCommand();
                checkStudent.CommandText = "SELECT std_id FROM classdetails WHERE std_id = :s_id";
                checkStudent.CommandType = CommandType.Text;
                checkStudent.Parameters.Add("s_id", OracleDbType.Int32).Value = studentId;

                object studentResult = checkStudent.ExecuteScalar();
                if (studentResult != null)
                {
                    OracleCommand checkAttendance = con.CreateCommand();
                    checkAttendance.CommandText = "SELECT s_id FROM attendance WHERE c_id = :cidd AND s_id = :s_id AND date_and_time = :date_and_time";
                    checkAttendance.CommandType = CommandType.Text;
                    checkAttendance.Parameters.Add("cidd", OracleDbType.Int32).Value = cidd1;
                    checkAttendance.Parameters.Add("s_id", OracleDbType.Int32).Value = studentId;

                    DateTime dateAndTime;
                    if (DateTime.TryParse(datett1, out dateAndTime))
                    {
                        checkAttendance.Parameters.Add("date_and_time", OracleDbType.TimeStamp).Value = dateAndTime;
                    }
                    else
                    {
                        // Handle the case where datett1 is not a valid date
                        MessageBox.Show("Invalid value for date_and_time.");
                        con.Close();
                        return; // Exit the method or perform additional error handling
                    }

                    object result = checkAttendance.ExecuteScalar();
                    if (result == null)
                    {
                        OracleCommand insertAttendance = con.CreateCommand();
                        insertAttendance.CommandText = "INSERT INTO attendance (status, c_id, s_id, date_and_time) VALUES (:status, :c_id, :s_id, :date_and_time)";
                        insertAttendance.CommandType = CommandType.Text;
                        insertAttendance.Parameters.Add("status", OracleDbType.Varchar2).Value = status.Text;
                        insertAttendance.Parameters.Add("c_id", OracleDbType.Int32).Value = cidd1;
                        insertAttendance.Parameters.Add("s_id", OracleDbType.Int32).Value = studentId;
                        insertAttendance.Parameters.Add("date_and_time", OracleDbType.TimeStamp).Value = dateAndTime;

                        int rowsAffected = insertAttendance.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Attendance inserted successfully.");
                        }
                        else
                        {
                            MessageBox.Show("Failed to insert attendance.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Attendance already exists.");
                    }
                }
                else
                {
                    MessageBox.Show("This student is not registered in this class.");
                }
            }
            else
            {
                MessageBox.Show("Invalid student ID.");
            }

            con.Close();
            updateGrid2();

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            teacherworking tw=new teacherworking();
            tw.tidd=tiddd1;
            tw.ShowDialog();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {


            con.Open();

            if (!string.IsNullOrEmpty(sid.Text))
            {
                OracleCommand checkRecord = con.CreateCommand();
                checkRecord.CommandText = "SELECT std_id FROM classdetails WHERE class_id = :c_id AND std_id = :s_id";
                checkRecord.CommandType = CommandType.Text;
                checkRecord.Parameters.Add("c_id", OracleDbType.Int32).Value = cidd1;
                checkRecord.Parameters.Add("s_id", OracleDbType.Varchar2).Value = sid.Text;

                object result = checkRecord.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    OracleCommand updateRecord = con.CreateCommand();
                    updateRecord.CommandText = "UPDATE attendance SET status = :status WHERE c_id = :c_id AND date_and_time = :start_time AND s_id = :s_id";
                    updateRecord.CommandType = CommandType.Text;
                    updateRecord.Parameters.Add("status", OracleDbType.Varchar2).Value = status.Text;
                    updateRecord.Parameters.Add("c_id", OracleDbType.Int32).Value = cidd1;
                    updateRecord.Parameters.Add("start_time", OracleDbType.TimeStamp).Value = DateTime.Parse(datett1);
                    updateRecord.Parameters.Add("s_id", OracleDbType.Varchar2).Value = sid.Text;

                    int rowsAffected = updateRecord.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Record updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Failed to update record.");
                    }
                }
                else
                {
                    MessageBox.Show("This student is not in this class.");
                }
            }

            con.Close();
            updateGrid2();


        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Text=datett1;
        }
    }
}
