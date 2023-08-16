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
    public partial class studentworking : Form
    {
        public int stdid { get; set; }
        public int cidd { get; set; }
        OracleConnection con;
        public studentworking()
        {
            InitializeComponent();
        }
        private void upgragde3()
        {
            int stdIdValue;
            int classIdValue;

            if (int.TryParse(stdid.ToString(), out stdIdValue) && int.TryParse(cidd.ToString(), out classIdValue))
            {
              
                OracleCommand getEmps = con.CreateCommand();
                getEmps.CommandText = "SELECT date_and_time, status FROM attendance WHERE s_id = :std_id AND c_id = :class_id";
                getEmps.CommandType = CommandType.Text;
                getEmps.Parameters.Add("std_id", OracleDbType.Int32).Value = stdIdValue;
                getEmps.Parameters.Add("class_id", OracleDbType.Int32).Value = classIdValue;

                OracleDataReader empDR = getEmps.ExecuteReader();
                DataTable empDT = new DataTable();
                empDT.Load(empDR);
                dataGridView3.DataSource = empDT;

                int c1 = 0;
                int c2 = 0;

                OracleCommand countCommand1 = con.CreateCommand();
                countCommand1.CommandText = "SELECT COUNT(*) FROM attendance WHERE s_id = :std_id AND c_id = :class_id";
                countCommand1.Parameters.Add("std_id", OracleDbType.Int32).Value = stdIdValue;
                countCommand1.Parameters.Add("class_id", OracleDbType.Int32).Value = classIdValue;
                object result1 = countCommand1.ExecuteScalar();
                if (result1 != null && result1 != DBNull.Value)
                {
                    c1 = Convert.ToInt32(result1);
                }

                OracleCommand countCommand2 = con.CreateCommand();
                countCommand2.CommandText = "SELECT COUNT(*) FROM attendance WHERE s_id = :std_id AND c_id = :class_id AND status = 'P'";
                countCommand2.Parameters.Add("std_id", OracleDbType.Int32).Value = stdIdValue;
                countCommand2.Parameters.Add("class_id", OracleDbType.Int32).Value = classIdValue;
                object result2 = countCommand2.ExecuteScalar();
                if (result2 != null && result2 != DBNull.Value)
                {
                    c2 = Convert.ToInt32(result2);
                }

                if (c1 >= 1)
                {
                    double percentage = ((double)c2 / c1) * 100;
                    label6.Text = percentage.ToString() + "%";
                }
                else
                {
                    label6.Text=null;
                }

                con.Close();
            }

        }
        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM student1 WHERE student_id = :sttdid";
            getEmps.CommandType = CommandType.Text;
            getEmps.Parameters.Add(":sttdid", OracleDbType.Int32).Value = stdid; // Replace 'sttdid' with the actual value

            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView2.DataSource = empDT;
            con.Close();

        }
        private void updateGrid2()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "select cd1.class_id,c1.class_name, t1.fname, cs1.starting_time, cs1.ending_time, cs1.schedule_day, cs1.class_location from teacher t1, class c1, classschedule cs1, classDetails cd1 where t1.teacherid = c1.t_id and cd1.std_id = :sttdid and cs1.class_id = c1.class_id and cd1.class_id=c1.class_id";
            getEmps.CommandType = CommandType.Text;
            getEmps.Parameters.Add(":sttdid", OracleDbType.Int32).Value = stdid; // Replace 'sttdid' with the actual value

            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView1.DataSource = empDT;
            con.Close();

        }

        private void studentworking_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
            updateGrid2();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            int stdIdValue;
            int classIdValue;
            con.Open();
            if (int.TryParse(stdid.ToString(), out stdIdValue) && int.TryParse(cid.Text, out classIdValue))
            {
                OracleCommand checkRecord = con.CreateCommand();
                checkRecord.CommandText = "SELECT class_id FROM classdetails WHERE std_id = :std_id AND class_id = :class_id";
                checkRecord.CommandType = CommandType.Text;
                checkRecord.Parameters.Add("std_id", OracleDbType.Int32).Value = stdIdValue;
                checkRecord.Parameters.Add("class_id", OracleDbType.Int32).Value = classIdValue;

                object result = checkRecord.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    cidd=classIdValue;
                    label6.Text=null;
                    upgragde3();
                    
                }
                else
                {
                    MessageBox.Show("Wrong Input");
                }
            }
            con.Close();

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            studentportal sp = new studentportal();
            sp.Show();
        }
    }
}
