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
    public partial class firstpage : Form
    {
        OracleConnection con;
        public firstpage()
        {
            InitializeComponent();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminLogin adm=new AdminLogin();
            adm.ShowDialog();

        }

        private void firstpage_Load(object sender, EventArgs e)
        {
            string conStr = @"DATA SOURCE = localhost:1521/xe; USER ID=hasans;PASSWORD=12345";
            con = new OracleConnection(conStr);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            teacherportal tp=new teacherportal();
            tp.ShowDialog();
          
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

            this.Hide();
            studentportal tp = new studentportal();
            tp.ShowDialog();

        }
    }
}
