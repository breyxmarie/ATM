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
    public partial class AdminPage : Form
    {
        Boolean addAccount = false;
        Boolean addDonation = false;
        Boolean editAccount = false;
        old_account users;
        Dictionary<string, string> userDetails = new Dictionary<string, string>();
        AutoCompleteStringCollection listsDon = new AutoCompleteStringCollection();
        public AdminPage(string name)
        {
            InitializeComponent();

            adminName.Text = name;  

            DBconnect db = new DBconnect();
            listsDon = db.getDonationNamesList();

            findDonname.AutoCompleteSource = AutoCompleteSource.CustomSource;
            findDonname.AutoCompleteCustomSource = listsDon;
            findDonname.AutoCompleteMode = AutoCompleteMode.Suggest;    

        }

        public void changePanels(Boolean addDonations, Boolean addAccounts, Boolean editAccounts)
        {
            if(addDonations == true)
            {
                
                addDonationPanel.Location = new Point(EditAccountPanel.Location.X, EditAccountPanel.Location.Y + EditAccountPanel.Height - 220);
                addDonationPanel.Visible = true;
                addDonation = true;
                addAccount = false;
                editAccount = false;

                addAccountpanel.Visible = false;
                EditAccountPanel.Visible = false;
            }

            // addAccountpanel

            else if (addAccounts == true)
            {
                addAccountpanel.Location = new Point(EditAccountPanel.Location.X, EditAccountPanel.Location.Y + EditAccountPanel.Height - 220);
                addAccountpanel.Visible = true;
                addAccount = true;
                addDonation = false;
                editAccount = false;

                addDonationPanel.Visible = false;
                EditAccountPanel.Visible = false;
            }

            // edit Account panel

            else if (editAccounts == true)
            {
                EditAccountPanel.Visible = true;
                addAccount = false;
                addDonation = false;
                editAccount = true;


                addDonationPanel.Visible = false;
                addAccountpanel.Visible = false;
            }

        }




        private void AdminPage_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void putDonIn_Click(object sender, EventArgs e)
        {
            DBconnect datas = new DBconnect();
            string name = putDonName.Text;
            string location = putDonLoc.Text;
            string amount = amountTxt.Text;

            datas.InsertDonationAccount(name, location, amount);

            MessageBox.Show("Donation Added Succesfully");


        }

        private void addAcountbtn_Click(object sender, EventArgs e)
        {
            changePanels(false,true,false);
        }

        private void addDonationbtn_Click(object sender, EventArgs e)
        {
            changePanels(true, false,false);
        }

        private void AddAccountBtn_Click(object sender, EventArgs e)
        {
            //addActFName
            // changePanels(false, false,true);

            old_account addUser = new old_account();
            addUser.InsertUserAccounts(addActFName.Text.ToString(), addActMid.Text.ToString(), addActLName.Text.ToString(), addActamount.Text.ToString());
            MessageBox.Show("Account Added Successfully");

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void searchAccountbtn_Click(object sender, EventArgs e)
        {
            // editAccountNumber.Text
            //
            try
            {
                users = new old_account(searchItemtxt.Text);
                // userDetails = users.searchAccounts(searchItemtxt.Text);
                editAccountNumber.Text = users.Account_number;
                editAccountFname.Text = users.First_name;
                editAccountMiddleI.Text = users.Middle_i;
                editAccountLname.Text = users.Last_name;
                editAccountBalance.Text = users.Current_balance;
            }
            catch
            {
                MessageBox.Show("Account Not Avilable");
            }
          //  users.
          //     Console.WriteLine(editAccountNumber.Text);

        }

        private void editAccountbtn_Click(object sender, EventArgs e)
        {
            changePanels(false, false, true);
        }

        private void saveAccountbtn_Click(object sender, EventArgs e)
        {

            users = new old_account(editAccountNumber.Text);
            users.updateAccountDetails(editAccountNumber.Text, editAccountFname.Text, editAccountMiddleI.Text, editAccountLname.Text, editAccountBalance.Text);

            


        }

        private void editAccountNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void putDonLoc_TextChanged(object sender, EventArgs e)
        {

        }

        private void addActFName_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void searchfindDonation_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> donateDetails = new Dictionary<string, string>();

            DBconnect cons = new DBconnect();

           // donateDetails = con.getDonationDetails(findDonname.Text.ToString());



            foreach (KeyValuePair<string, string> us in cons.getDonationDetails(findDonname.Text))
            {
                donateDetails.Add(us.Key.ToString(), us.Value.ToString());

            }

            findDonname.Text = donateDetails["donate_name"];
            donationAmounttxt.Text = donateDetails["donate_address"];

        }

        private void deleteAccountbtn_Click(object sender, EventArgs e)
        {
            users = new old_account();
            users.deleteAccount(editAccountNumber.Text);

        }

        private void logoutbtn_Click(object sender, EventArgs e)
        {
            LogInAdmin mains = new LogInAdmin();

            this.Visible = false;
            mains.ShowDialog();

            this.Close();
        }
    }
}
