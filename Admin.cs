using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_Project
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }

        private void bunifuThinButton25_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminLogin f1 = new AdminLogin();
            f1.ShowDialog();
            
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Hide();
          
            AdminAddingStudent aas=new AdminAddingStudent();
          
            aas.ShowDialog();


        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminAddingTeacher adt = new adminAddingTeacher();
            adt.ShowDialog();

        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            this.Hide();
            adminUpdatingStudent ads = new adminUpdatingStudent();
            ads.ShowDialog();
        }

        private void bunifuThinButton24_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminUpdatingTeacher aut=new AdminUpdatingTeacher();
            aut.ShowDialog();
        }

        private void bunifuThinButton26_Click(object sender, EventArgs e)
        {
            this.Hide();
            ClassesCreation cc = new ClassesCreation();
            cc.ShowDialog();
        }

        private void bunifuThinButton27_Click(object sender, EventArgs e)
        {
            this.Hide();
            ScheduleClass sc = new ScheduleClass();
            sc.ShowDialog();
        }

        private void bunifuThinButton28_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminaddingStudentToClass aa = new AdminaddingStudentToClass();
            aa.ShowDialog();

        }
    }
}
