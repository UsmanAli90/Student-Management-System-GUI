using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;
namespace DB_Project
{
    public partial class teacherworking : Form
    {
        public int tidd { get; set; }
        public int cidd { get; set; }
       
        public string datett { get; set; }
        OracleConnection con;
        public teacherworking()
        {
            InitializeComponent();
        }
        private void updateGrid()
        {
            con.Open();

            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT DISTINCT c.class_id, c.class_name FROM teacher t, class c WHERE c.t_id = t.teacherid AND t.teacherid = :tddid";
            getEmps.CommandType = CommandType.Text;
            getEmps.Parameters.Add("tddid", OracleDbType.Int32).Value = tidd; // Replace 'tidd' with the actual value

            OracleDataReader empDR = getEmps.ExecuteReader();
            if (empDR.HasRows)
            {
                DataTable empDT = new DataTable();
                empDT.Load(empDR);
                dataGridView1.DataSource = empDT;
            }
            else
            {
                MessageBox.Show("This teacher is not teaching any class.");
                this.Hide();
                teacherportal tp = new teacherportal();
                tp.ShowDialog();
            }

            con.Close();


        }
        private void teacherworking_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            con.Open();
            if (classId.Text.ToString()!="")
            {
                OracleCommand getClassId = con.CreateCommand();
                getClassId.CommandText = "SELECT class_id FROM class WHERE t_id = :tidd and class_id=:cid";
                getClassId.CommandType = CommandType.Text;
                getClassId.Parameters.Add("tidd", OracleDbType.Int32).Value = tidd; // Replace 'tidd' with the actual value
                getClassId.Parameters.Add("cid", OracleDbType.Int32).Value = classId.Text;

                string classIdResult = getClassId.ExecuteScalar()?.ToString();

                if ((classIdResult==classId.Text.ToString()))
                {
                    cidd = int.Parse(classId.Text);

                    OracleCommand getStartingTime = con.CreateCommand();
                    getStartingTime.CommandText = "SELECT starting_time FROM classschedule WHERE class_id = :classId";
                    getStartingTime.CommandType = CommandType.Text;
                    getStartingTime.Parameters.Add("classId", OracleDbType.Varchar2).Value = classId.Text;
                    OracleDataReader startTimeDR = getStartingTime.ExecuteReader();
                    DataTable startTimeDT = new DataTable();
                    startTimeDT.Load(startTimeDR);
                    dataGridView2.DataSource = startTimeDT;



                }
                else
                {
                    MessageBox.Show("Invalid class entered.");
                }
            }
            else
            {
                MessageBox.Show("Enter class Id first");
            }
            con.Close();


        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            con.Open();
            string input =timeS.Text;
            string format = "M/d/yyyy h:mm:ss tt";

            DateTime parsedDateTime;
            bool isValidFormat = DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime);

            if (isValidFormat)
            {
                if (!(timeS.Text.ToString()==""))
                {
                    int temp = cidd;
                    if (temp == Convert.ToInt32(classId.Text))
                    {

                        OracleCommand getStartingTime = con.CreateCommand();
                        getStartingTime.CommandText = "SELECT starting_time FROM classschedule WHERE class_id = :classId AND starting_time = :startTime";
                        getStartingTime.CommandType = CommandType.Text;
                        getStartingTime.Parameters.Add("classId", OracleDbType.Varchar2).Value = cidd;
                        getStartingTime.Parameters.Add("startTime", OracleDbType.TimeStamp).Value = Convert.ToDateTime(timeS.Text);
                        string datett = getStartingTime.ExecuteScalar()?.ToString();

                        if (datett==timeS.Text)
                        {
                            this.Hide();
                            teacherMarkingAttendance tma = new teacherMarkingAttendance();
                            tma.datett1=datett;
                            tma.cidd1=cidd;
                            tma.tiddd1=tidd;
                            
                            tma.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("invalid date");
                        }
                    }
                    else
                    {
                        MessageBox.Show("First enter correct class Id");
                    }

                    con.Close();
                }

                else
                {
                    MessageBox.Show("first enter the time stamp");
                }
            }
            else
            {
                MessageBox.Show("invaid format");
            }
            con.Close();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            this.Hide();
            teacherportal tp = new teacherportal();
            tp.ShowDialog();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
