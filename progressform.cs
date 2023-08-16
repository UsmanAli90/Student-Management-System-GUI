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
    public partial class progressform : Form
    {
        public progressform()
        {
            InitializeComponent();
        }

        private void progressform_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        int startpoint = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            startpoint++;
            ProgressBar.Value=startpoint;
            if (ProgressBar.Value==100)
            {
                ProgressBar.Value=0;
                timer1.Stop();
                firstpage log= new firstpage();
                this.Hide();
                log.Show();
            }
        }

        private void ProgressBar_Click(object sender, EventArgs e)
        {

        }
    }
}
