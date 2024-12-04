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
using System.Xml.Linq;

namespace ATM.forms
{
    public partial class LogIn : Form
    {
        private String actnum;

        public String GetActnum() { 
            return actnum;  }

        public LogIn()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void loginbtn_Click(object sender, EventArgs e)
        {
           String un =  actnumtxt.Text;
           String pw = passtxt.Text;

            
            DBconnect datas = new DBconnect();
           
            bool trys = datas.validateloguser(un, pw);
           
            if(trys == true)
            {
                Dictionary<string, string> userDetails = new Dictionary<string, string>();

           //     userDetails = datas.
                Console.WriteLine("hello");
                string actnumsa = datas.actnums();
                Console.WriteLine("Hello");
                Console.WriteLine(actnumsa);
                MainMenu mains = new MainMenu(un);
              
                this.Visible = false;
                mains.ShowDialog();
                
                this.Close();

            }

            else
            {
                MessageBox.Show("Invalid pin, please try again");
            }
        }

        private void adminloginclick_Click(object sender, EventArgs e)
        {
            LogInAdmin mains = new LogInAdmin();

            this.Visible = false;
            mains.ShowDialog();

            this.Close();
        }
    }
}
