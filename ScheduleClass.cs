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
using System.Globalization;
namespace DB_Project
{
    public partial class ScheduleClass : Form
    {

        OracleConnection con;

        public ScheduleClass()
        {
            InitializeComponent();
        }
        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM classschedule";
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
        private void ScheduleClass_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);

            updateGrid();
            updateGrid2();

        }


        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            string st = stime.Text;
            string et = etime.Text;
            string format = "yyyy-MM-dd HH:mm:ss";
            DateTime startTime;
            DateTime endTime;

            if (!string.IsNullOrEmpty(st) && !string.IsNullOrEmpty(et) && !string.IsNullOrEmpty(sday.Text) && !string.IsNullOrEmpty(cid.Text))
            {
                con.Open();
                int i = 0;

                // Check if the class with the specified ID exists
                string query = "SELECT class_id FROM class WHERE class_id = :classId";
                OracleCommand command = new OracleCommand(query, con);
                command.Parameters.Add(":classId", OracleDbType.Int32).Value = Convert.ToInt32(cid.Text);

                object result = command.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    i = Convert.ToInt32(result);
                }

                if (DateTime.TryParseExact(st, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out startTime) &&
                    DateTime.TryParseExact(et, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                {
                    if (sday.Text.ToLower() == "monday" || sday.Text.ToLower() == "tuesday" || sday.Text.ToLower() == "wednesday" || sday.Text.ToLower() == "thursday" || sday.Text.ToLower() == "friday")
                    {
                        if (i == 0)
                        {
                            MessageBox.Show("No class with this ID");
                        }
                        else
                        {
                            // Check if the class schedule already exists for the specified time
                            string checkQuery = "SELECT class_id FROM classschedule WHERE class_id = :classId AND starting_time = :startTime AND ending_time = :endTime";
                            OracleCommand checkCommand = new OracleCommand(checkQuery, con);
                            checkCommand.Parameters.Add(":classId", OracleDbType.Int32).Value = Convert.ToInt32(cid.Text);
                            checkCommand.Parameters.Add(":startTime", OracleDbType.TimeStamp).Value = startTime;
                            checkCommand.Parameters.Add(":endTime", OracleDbType.TimeStamp).Value = endTime;
                          
                            object checkResult = checkCommand.ExecuteScalar();
                            if (checkResult != null && checkResult != DBNull.Value)
                            {
                                MessageBox.Show("Class is already scheduled at this time");
                            }
                            else
                            {
                                OracleCommand insertEmp = con.CreateCommand();
                                insertEmp.CommandText = "INSERT INTO classschedule (starting_time, ending_time, schedule_day, class_id,class_location) VALUES (:s_t, :e_t, :s_d, :cid,:cloc)";

                                insertEmp.Parameters.Add(":s_t", OracleDbType.TimeStamp).Value = startTime;
                                insertEmp.Parameters.Add(":e_t", OracleDbType.TimeStamp).Value = endTime;
                                insertEmp.Parameters.Add(":s_d", OracleDbType.Varchar2).Value = sday.Text.ToLower();
                                insertEmp.Parameters.Add(":cid", OracleDbType.Int32).Value = Convert.ToInt32(cid.Text);
                                insertEmp.Parameters.Add(":cloc", OracleDbType.Varchar2).Value = classLoc.Text.ToString();
                                insertEmp.CommandType = CommandType.Text;
                                insertEmp.ExecuteNonQuery();

                                MessageBox.Show("Class schedule inserted successfully.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Day");
                    }
                }
                else
                {
                    MessageBox.Show("Date format is not correct");
                }

                con.Close();
                updateGrid();
            }
            else
            {
                MessageBox.Show("Something is missing");
            }

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin a = new Admin();
            a.ShowDialog();

        }
    }
}
