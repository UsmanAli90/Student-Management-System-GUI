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
    public partial class ClassesCreation : Form
    {
        OracleConnection con;
        public ClassesCreation()
        {
            InitializeComponent();
        }
        private void updateGrid()
        {
            con.Open();
            OracleCommand getEmps = con.CreateCommand();
            getEmps.CommandText = "SELECT * FROM class";
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
            getEmps.CommandText = "SELECT fname,teacherid FROM teacher";
            getEmps.CommandType = CommandType.Text;
            OracleDataReader empDR = getEmps.ExecuteReader();
            DataTable empDT = new DataTable();
            empDT.Load(empDR);
            dataGridView2.DataSource = empDT;
            con.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
         
            con.Open();
            int i = 0;
            if (!(className.Text.ToString()=="")  &&!(teacherId.Text.ToString()==""))
            {

                if (int.TryParse(teacherId.Text, out int teach))
                  {

                    // teacherId.Text should be used as a parameter, not directly in the SQL query
                    string query = "SELECT teacherId FROM teacher WHERE teacherid = :teacherId";
                    OracleCommand command = new OracleCommand(query, con);
                    command.Parameters.Add(":teacherId", OracleDbType.Int32).Value = Convert.ToInt32(teacherId.Text);

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
                        OracleCommand countCmd = new OracleCommand("SELECT COUNT(*) FROM class", con);
                        int count = Convert.ToInt32(countCmd.ExecuteScalar());
                        
                        count++;
                        count=count+1000;
                        OracleCommand insertEmp = con.CreateCommand();
                        insertEmp.CommandText = "INSERT INTO class (class_id, class_name,t_id) VALUES (:cid, :cname, :tid)";
                        insertEmp.Parameters.Add(":cid", OracleDbType.Int32).Value = count;
                        insertEmp.Parameters.Add(":cname", OracleDbType.Varchar2).Value = className.Text.ToString();
                        insertEmp.Parameters.Add(":tid", OracleDbType.Int32).Value = teacherId.Text;
                        insertEmp.CommandType = CommandType.Text;
                        insertEmp.ExecuteNonQuery();


                    }
                }
            }
            else
            {
                MessageBox.Show("something is missing");
            }
            con.Close();
            updateGrid();
        }

        private void ClassesCreation_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);
            updateGrid();
            updateGrid2();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            Admin a = new Admin();
            a.ShowDialog();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void teacherId_TextChanged(object sender, EventArgs e)
        {

        }

        private void classLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void className_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
