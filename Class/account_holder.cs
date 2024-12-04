using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM.Class
{
    



    class account_holder
    {
        private String accountnum;
        private String pin;
        private String first_name;
        private String middle_initial;
        private String last_name;
        private String current_balance;
        private float num_balance;

        public string Accountnum { get => accountnum; set => accountnum = value; }
        public string Pin { get => pin; set => pin = value; }
        public string First_name { get => first_name; set => first_name = value; }
        public string Middle_initial { get => middle_initial; set => middle_initial = value; }
        public string Last_name { get => last_name; set => last_name = value; }
        public string Current_balance { get => current_balance; set => current_balance = value; }
        public float Num_balance { get => num_balance; set => num_balance = value; }
    }
}
