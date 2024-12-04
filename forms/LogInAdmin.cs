using ATM.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM.forms
{
    public partial class LogInAdmin : Form
    {
        DBconnect data = new DBconnect();
        public LogInAdmin()
        {
            InitializeComponent();
          //  Form.ShowInTaskbar = false;
        }

        private void button1_Click(object sender, EventArgs e)   //log in button
        {

            if (data.validatelogAdmin(name.Text, password.Text))
            {
                AdminPage mains = new AdminPage(data.getAdminName(name.Text));

                this.Visible = false;
                mains.ShowDialog();

                this.Close();
            }
            else
            {

            }
        }

        private void LogInAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
