using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Class
{

    class old_account
    {
        string account_number = " ";
        string pin = " ";
        string first_name = " ";
        string middle_i = " ";
        string last_name = " ";
        string current_balance = " ";
        DBconnect data;
        Dictionary<string, string> userDetails = new Dictionary<string, string>();

        public old_account()
        {
            data = new DBconnect();
        }
        public old_account(string accountNo)
        {
            data = new DBconnect();
           // userDetails = data.getUserDetails(accountNo);

            foreach (KeyValuePair<string, string> us in data.getUserDetails(accountNo))
            {
                userDetails.Add(us.Key.ToString(), us.Value.ToString());

            }
            //Console.WriteLine(userDetails["act_num"]);
            this.account_number = userDetails["act_num"];
          //  Console.WriteLine(account_number);
            this.pin = userDetails["pin"];
            this.first_name = userDetails["firstName"];
            this.middle_i = userDetails["middleInitial"];
            this.last_name = userDetails["lastName"];
            this.current_balance = userDetails["currentBalance"];


           
            



        }

        public string Account_number { get => account_number; set => account_number = value; }
        public string Pin { get => pin; set => pin = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Middle_i { get => middle_i; set => middle_i = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Current_balance { get => current_balance; set => current_balance = value; }

        

        public void updateDonationBalance(string amount, string donate_name)
        {
         // DBconnect datas = new DBconnect();
            data.updateDonateAccount(amount, donate_name);
        }

        public Dictionary<string, string> searchAccounts(string actNum)
        {
         // DBconnect datas = new DBconnect();
            data.getUserDetails(actNum);
            Console.WriteLine(account_number);
            return userDetails;
        }

        public void updateAccountDetails(string account_number, string fname, string mi, string lname, string cbalance)
        {
            data.UpdateAccountDetails(account_number,fname,mi,lname,cbalance);
        }

        public void InsertUserAccounts(string fname, string mi, string lname, string cbalance)
        {
            data.InsertUsersAccount(fname,mi,lname,cbalance);   
        }

        public void deleteAccount(string actnum)
        {
            data.deleteUserAccount(actnum);
        }
    }
}
