using ATM.Class;
using ATM.forms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATM
{
    public partial class MainMenu : Form
    {
        Boolean depositpan = false;
        Boolean withdrawpan = false;
        Boolean donatepan = false;
        DBconnect datas = new DBconnect();
        Dictionary<string, string> userDetails = new Dictionary<string, string>();
        String actnum;
        old_account user;



        string balance;
      
        public MainMenu(String accountnumber)
        {
            InitializeComponent();
            DBconnect datassss = new DBconnect();
            List<string> donatelist = datassss.getDonationList();
            user = new old_account(accountnumber);

            foreach(var d in donatelist)
            {
                cmbdonationlist.Items.Add(d);
            }

            actnum = accountnumber;
       
            this.actnum = accountnumber;

            DBconnect datas = new DBconnect();
            foreach (KeyValuePair<string, string> us in datas.getUserDetails(accountnumber))
                {
                userDetails.Add(us.Key.ToString(), us.Value.ToString());
                }

            foreach (KeyValuePair<string, string> us in userDetails)
            {
                lblactbalance.Text = userDetails["currentBalance"];
                label3.Text = userDetails["firstName"];
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

     

        private void checkbalancebtn_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click(object sender, EventArgs e)
        {
            LogIn log = new LogIn();
           

            this.Visible = false;
            log.ShowDialog();

            this.Close();
        }

    

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            LogIn log = new LogIn();
            log.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LogIn log = new LogIn();
            log.Visible = true;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void depositbtn_Click(object sender, EventArgs e)
        {
         
            String amount =  depositamounttxtfield.Text;
       //     database.UpdateBalance(am,);

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            changepanels(false, false,true);
        }

        public void changepanels(Boolean deposit, Boolean withdraw, Boolean donate)
        {
            if(deposit == true)
            {
                    withdrawpanel.Visible = false;
                    withdrawpan = false;
         
                    donationpanel.Visible = false;
                    donatepan = false;
           
                depositpanel.Location = new Point(withdrawpanel.Location.X, withdrawpanel.Location.Y + withdrawpanel.Height - 220);
                depositpan = true;
                depositpanel.Visible = true;
            }

            if (withdraw == true)
            {
            //    if (deposit == true)
           //     {
                    depositpanel.Visible = false;
                    depositpan = false;
           //     }
           //     else if (donate == true)
             //   {
                    donationpanel.Visible = false;
                    donatepan = false;
         //       }
                withdrawpanel.Visible = true;
                withdrawpan = true;
            }

            if (donate == true)
            {
              //  if (withdraw == true)
            //    {
                    withdrawpanel.Visible = false;
                    withdrawpan = false;
           //     }
          //      else if (deposit == true)
          //      {
                    depositpanel.Visible = false;
                    depositpan = false;
                //    }
                donationpanel.Location = new Point(withdrawpanel.Location.X, withdrawpanel.Location.Y + withdrawpanel.Height - 220);
              //  donationpanel.Top = withdrawpanel.Top;
             //   donationpanel.Height = withdrawpanel.Height;
                donationpanel.Visible = true;
                donatepan = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            changepanels(true, false, false);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            changepanels(false, true, false);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogIn logs = new LogIn();
            //  mains.Visible = true;
            this.Visible = false;
            logs.ShowDialog();

            this.Close();

        }

        private void withdrawbtn_Click(object sender, EventArgs e)
        {
            DBconnect datass = new DBconnect();
            string withdrawamounts = "";
            withdrawamounts = withdrawamount.Text;
            string balances = userDetails["currentBalance"];
            int currentamount;
            int withamount;
            int newamount;

            Int32.TryParse(balances, out currentamount);
            Int32.TryParse(withdrawamounts, out withamount);
            newamount = currentamount - withamount;
            balances = newamount.ToString();
            lblactbalance.Text = balances;
            datass.UpdateBalance(balances, userDetails["act_num"]); 
            userDetails["currentBalance"] = balances;         
        }

        private void button5_Click(object sender, EventArgs e)  //deposit button
        {
            DBconnect datass = new DBconnect();
            string depositamounts = depositamounttxtfield.Text;
            //   string balances = datas.getBalance(actnum);
            string balances = userDetails["currentBalance"];
            int currentamount;
            int depositamount;
            int newamount;

            Int32.TryParse(balances, out currentamount);
            Int32.TryParse(depositamounts, out depositamount);

            newamount = currentamount + depositamount;

            balances = newamount.ToString();

            lblactbalance.Text = balances;
            //  Console.WriteLine(balance);

            datass.UpdateBalance(balances, userDetails["act_num"]);
            userDetails["currentBalance"] = balances;
        }

        private void button6_Click(object sender, EventArgs e)  //donate button
        {
            

            DBconnect datass = new DBconnect();
            string withdrawamounts = "";
            withdrawamounts = donateAmountxtField.Text;
            string balances = userDetails["currentBalance"];
            int currentamount;
            int withamount;
            int newamount;

            Int32.TryParse(balances, out currentamount);
            Int32.TryParse(withdrawamounts, out withamount);
            newamount = currentamount - withamount;
            balances = newamount.ToString();
            lblactbalance.Text = balances;
            datass.UpdateBalance(balances, userDetails["act_num"]);
            userDetails["currentBalance"] = balances;

            

            DBconnect datasss = new DBconnect();

            //updateDonateAccount
            string donatewithdrawamounts = "";
            donatewithdrawamounts = donateAmountxtField.Text;
            string donatebalances = userDetails["currentBalance"];
            string chosenDonation = cmbdonationlist.GetItemText(cmbdonationlist.SelectedItem);

            //Console.WriteLine(chosenDonation);
            int donatecurrentamount;
            int donatewithamount;
            int donatenewamount;

            Int32.TryParse(donatebalances, out donatecurrentamount);
            Int32.TryParse(donatewithdrawamounts, out donatewithamount);
            donatenewamount = donatecurrentamount + donatewithamount;
            donatebalances = donatenewamount.ToString();

            // datasss.updateDonateAccount(withdrawamounts, chosenDonation);
            user.updateDonationBalance(withdrawamounts, chosenDonation);




        }
    }
}
