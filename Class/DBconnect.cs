using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ATM.Class
{
    class DBconnect
    {
        static string connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";" ;
        static MySqlConnection connection = new MySqlConnection(connectionString);
        static  SqlConnection conns;
       static string server = "127.0.0.1";
        static string database = "atm_query";
        static string uid = "root";
        static string password = "admin";
        String accounttnum = "";

        public DBconnect()
        {
            Initialize();
        }

        public void Initialize()
        {
            server = "127.0.0.1";
            database = "atm_query";
            uid = "root";
            password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
            conns = new SqlConnection(connectionString);  
        }

        private bool OpenConnection()
        {
            /*  server = "127.0.0.1";
              database = "atm_query";
              uid = "root";
              password = "admin";
              string connectionString;
              connectionString = "SERVER=" + server + ";" + "DATABASE=" +
              database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

              connection = new MySqlConnection(connectionString);
              conns = new SqlConnection(connectionString); */
        //    MultipleActiveResultSets = true;
            try
            {

                connection.Open();


                Console.WriteLine("connection establish");
                return true;

            }
            catch (MySqlException ex)
            {
                
                return false;
            }
        }




        private bool OpenConnections()
        {

            try
            {

                connection.Open();


                Console.WriteLine("connection establish");
                return true;

            }
            catch (MySqlException ex)
            {
                try
                {
                    string connString = "server=localhost;userid=root;password=root";
                    MySqlConnection conn = new MySqlConnection(connString);

                    conn.Open();

                    MySqlCommand cmdsss = new MySqlCommand("CREATE DATABASE IF NOT EXISTS atm_query;", conn);
                    cmdsss.ExecuteNonQuery();

                    MySqlCommand cmd = new MySqlCommand("CREATE TABLE IF NOT EXISTS atm_query.user_accounts(\r\nactnum VARCHAR(10) NOT NULL, \r\nPIN VARCHAR(6) NOT NULL,\r\nfirst_name VARCHAR(30) NOT NULL,\r\nmiddle_initial VARCHAR(3) NOT NULL,\r\nlast_name VARCHAR(30) NOT NULL,\r\ncurrent_balance VARCHAR(8) NOT NULL,\r\n\r\nPRIMARY KEY (actnum));", conn);
                    cmd.ExecuteNonQuery();

                    MySqlCommand cmds = new MySqlCommand("INSERT INTO atm_query.user_accounts VALUES ('1234567890','123456', 'Rory', 'C', 'Gilmore', '123456');", conn);
                    cmds.ExecuteNonQuery();

                    Console.WriteLine("Database Sample_DB created successfully");
                    conn.Close();


                }

                catch
                {

                }




                switch (ex.Number)
                {
                    case 0:

                        MessageBox.Show("Cannot connect to server.  Contact administrator, hello");
                        string connString = "server=localhost;userid=root;password=admin";
                        MySqlConnection conn = new MySqlConnection(connString);

                        conn.Open();

                        // MySqlCommand cmd = new MySqlCommand("CREATE DATABASE atm_query;", conn);
                        // cmd.ExecuteNonQuery();


                        MySqlCommand cmd = new MySqlCommand("CREATE DATABASE atm_query; CREATE TABLE IF NOT EXISTS user_accounts(\r\nactnum VARCHAR(10) NOT NULL, \r\nPIN VARCHAR(6) NOT NULL,\r\nfirst_name VARCHAR(30) NOT NULL,\r\nmiddle_initial VARCHAR(3) NOT NULL,\r\nlast_name VARCHAR(30) NOT NULL,\r\ncurrent_balance VARCHAR(8) NOT NULL,\r\n\r\nPRIMARY KEY (actnum));\r\n", conn);
                        cmd.ExecuteNonQuery();

                        MySqlCommand cmds = new MySqlCommand("INSERT INTO user_accounts VALUES ('1234567890','123456', 'Rory', 'C', 'Gilmore', '123456')", conn);
                        cmds.ExecuteNonQuery();

                        Console.WriteLine("Database Sample_DB created successfully");
                        conn.Close();
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        /* public bool DBExists(string conn, string dbName)
        {
            bool functionReturnValue = false;

            using (MySqlConnection dbconn = new MySqlConnection(conn))
            {
                using (MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM information_schema.schemata WHERE SCHEMA_NAME=@dbName", conn))
                {
                    functionReturnValue = false;
                    cmd.Parameters.AddWithValue("@dbName", database);
                    dbconn.Open();
                    if (cmd.ExecuteNonQuery() != 0)
                    {
                        functionReturnValue = true;
                    }
                    dbconn.Close();
                }
            }
            return functionReturnValue;
        } */

        public void Insert(string query)
        {
            if (this.OpenConnection() == true)
            {
             
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string query)
        {
            // string query = "UPDATE tableinfo SET name='Joe', age='22' WHERE name='John Smith'";

            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Delete statement
        public void Delete(string query)
        {
            // string query = "DELETE FROM tableinfo WHERE name='John Smith'";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        //   public void getNum();
        // {
        //    String actnums;

        // return actnums;
        // }


        public bool validateloguser(String un, String pass)
        {
            Dictionary<string, string> userDetails = new Dictionary<string, string>();
            try
            {
                // DBconnect con = new DBconnect();

                OpenConnection();
                string query = "SELECT * FROM atm_query.user_accounts";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    String checku = dataReader.GetString("actnum");
                    Console.WriteLine(checku);
                    String checkp = dataReader.GetString("PIN");
                    Console.WriteLine(checku);

                    if (checku == un && checkp == pass)
                    {
                        String accounttnum = dataReader.GetString("actnum");

                        accounttnum = actnums();

                        userDetails["act_num"] = dataReader.GetString("actnum"); ;
                        userDetails["pin"] = dataReader.GetString("PIN"); ;
                        userDetails["firstName"] = dataReader.GetString("first_name"); ;
                        userDetails["middleInitial"] = dataReader.GetString("middle_initial"); ;
                        userDetails["lastName"] = dataReader.GetString("last_name"); ;
                        userDetails["currentBalance"] = dataReader.GetString("current_balance"); ;
                        dataReader.Close();
                        CloseConnection();
                        return true;
                    }
                }

            }
            catch
            {

            }
            
            CloseConnection();
            return false;
        }

        public string actnums() {
            //   string actnu = num;

            return accounttnum;
        }

     //   public Dictionary<string, string>(){ 
            
            
           // userDetails = new Dictionary<string, string>();
       //     }
        public String getFirstName(String actnum)
        {
            try
            {
                // DBconnect con = new DBconnect();
                OpenConnection();
                string query = "SELECT first_name FROM atm_query.user_accounts WHERE actnum =" + actnum + ";";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    String fname = dataReader.GetString("first_name");


                    return fname;
                       
                      
                }
                dataReader.Close();
            }
            catch
            {

            }
            CloseConnection();
            return "";
        }
        public void UpdateBalance(String amount, String account_number){

             string query = "UPDATE atm_query.user_accounts SET current_balance = '" + amount + "' WHERE actnum= '" + account_number + "';";
        
             try
              {
            OpenConnection();
                
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
               CloseConnection();
            }
            catch
            {
                   
            }
        }

        public String getBalance(String actnum)
        {
        //    String balance = "";
            try
            {
                // DBconnect con = new DBconnect();
                OpenConnection();
                string query = "SELECT current_balance FROM atm_query.user_accounts WHERE actnum =" + actnum + ";";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    //       balance = dataReader.GetInt32("current_balance");
                    // balance = int.Parse(dataReader["current_balance"].ToString());
                    String balance = dataReader.GetString("current_balance");

               //     Console.WriteLine(balance);

                    return balance;


                }
                dataReader.Close();
            }
            catch
            {

            }
            CloseConnection();
            return "";
        }


        public Dictionary<string, string> getUserDetails(String actnum)
        {
         
            Dictionary<string, string> userDetails = new Dictionary<string, string>();
            try
            {
               
                OpenConnection();
                string query = "SELECT * FROM atm_query.user_accounts WHERE actnum =" + actnum + ";";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                   
                  //  accounttnum = actnums();

                    userDetails["act_num"] = dataReader.GetString("actnum"); 
                    userDetails["pin"] = dataReader.GetString("PIN"); 
                    userDetails["firstName"] = dataReader.GetString("first_name"); 
                    userDetails["middleInitial"] = dataReader.GetString("middle_initial"); 
                    userDetails["lastName"] = dataReader.GetString("last_name"); 
                    userDetails["currentBalance"] = dataReader.GetString("current_balance"); 

                   // return userDetails;


                }
                dataReader.Close();
            }
            catch
            {

            }

            CloseConnection();
            return userDetails;
        }






        public MySqlDataReader getLIstDonations()
        {

            string query = "SELECT name FROM donation_accounts";

            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader listdons = cmd.ExecuteReader();

            CloseConnection();
            return listdons;
        }


        public List<String> getDonationList()
        {
            List<String> DonationList = new List<String>();

            OpenConnection();
            string query = "SELECT donate_name FROM donation_account;";

            MySqlCommand cmd = new MySqlCommand(query, connection);

            MySqlDataReader dataReader = cmd.ExecuteReader();

            while (dataReader.Read())
            {
                DonationList.Add(dataReader.GetString("donate_name"));
            }
            dataReader.Close();
            CloseConnection();
            return DonationList;
            
        }

        public void updateDonateAccounts(String amount, String donate_num)
        {
            OpenConnection();
            int balance = 0;
            string donationNum = " ";
            string querys = "SELECT donate_amount, donate_actnum FROM atm_query.donation_account WHERE donate_name = '" + donate_num + "';";
            MySqlCommand cmds = new MySqlCommand(querys, connection);
            MySqlDataReader dataReader = cmds.ExecuteReader();


            int amounts;
            Int32.TryParse(amount, out amounts);

            int totalBalance = balance + amounts;
            amount = totalBalance.ToString();
            Console.WriteLine(amount);

            dataReader.Close();

            string query = "UPDATE atm_query.donation_account SET donate_amount = '" + amount + "' WHERE donate_actnum= '" + "126" + "';";

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = query;
            cmd.Connection = connection;
            cmd.ExecuteNonQuery();

            CloseConnection();
        }

        public void updateDonateAccount(String amount, String donate_num)
        {
            //string query = "UPDATE atm_query.donation_account SET donate_amount = '" + amount  + "' WHERE donate_name= '" + donate_num + "';";

            // try
            // {

            server = "127.0.0.1";
            database = "atm_query";
            uid = "root";
            password = "admin";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

           // MySqlConnection connections = new MySqlConnection(connectionString);

           // connections.Open();
            OpenConnection();
            int balance = 0;
            string donationNum = " ";

                string querys = "SELECT donate_amount, donate_actnum FROM atm_query.donation_account WHERE donate_name = '" + donate_num+ "';";
                MySqlCommand cmds = new MySqlCommand(querys, connection);
                MySqlDataReader dataReader = cmds.ExecuteReader();

               while (dataReader.Read())
              {
            //if (dataReaders.HasRows) { 
                    Int32.TryParse(dataReader.GetString("donate_amount"), out balance);
                   donationNum = (dataReader.GetString("donate_actnum"));
           /// }
               }

            int amounts;
                Int32.TryParse(amount, out amounts);

                int totalBalance = balance + amounts;
                amount = totalBalance.ToString();
                Console.WriteLine(amount);

            dataReader.Close();
            string query = "UPDATE atm_query.donation_account SET donate_amount = '" + amount + "' WHERE donate_actnum= '" + donationNum + "';";

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();


          //  connections.Close();
          CloseConnection();    
          //  }
         //   catch
          //  {
           //     Console.WriteLine("error");
           // }
        }


        public void InsertDonationAccount(String name, String address, String amount)
        {
            OpenConnection();
            String query = "INSERT INTO atm_query.donation_account (donate_name, donate_address, donate_amount) VALUES ('" + name + "','" + address
                            + "','" + amount +"');";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            CloseConnection();
               
        }

        public void InsertUserAccounts()
        {
            OpenConnection();
            string query = "";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            CloseConnection();

        }


        public void getAccounts(string accountNumber)
        {
            OpenConnection();

            string querys = "SELECT * FROM atm_query.user_accounts WHERE actnum = '" + accountNumber + "';";
            MySqlCommand cmds = new MySqlCommand(querys, connection);
            MySqlDataReader dataReader = cmds.ExecuteReader();

            while (dataReader.Read())
            {

            }


            dataReader.Close(); 
            CloseConnection();
        }


        public void UpdateAccountDetails(string account_number, string fname, string mi, string lname, string cbalance)
        {

            string query = "UPDATE atm_query.user_accounts SET first_name = '" + fname+ "'," + "middle_initial = '" + mi + "'," + "last_name ='" + lname +"'," +  "current_balance = '" + cbalance + "' WHERE actnum= '" + account_number + "';";

        //    try
          //  {
                OpenConnection();

                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                CloseConnection();
          //  }
         //   catch
          //  {
                
          //  }
        }




        public void InsertUsersAccount(string fname, string mi, string lname, string cbalance)
        {
            OpenConnection();
            string account_number;
            Random r = new Random();
            int actNum = r.Next(10000000, 999999999);

           // Random r = new Random();
            int pin = r.Next(99999, 999999);


            String query = "INSERT INTO atm_query.user_accounts VALUES ('" + actNum.ToString() + "','" + pin.ToString() + "','" + fname + "','" + mi
                            + "','" + lname + "','" + cbalance + "');";

            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            string message = "Account Created:" + 
                                "\n Account Number: " + actNum.ToString()
                               + "\n Pin:" + pin.ToString()
                               + "\n Account Name: " + fname + " " + mi + " " + lname
                               + "\n Balance: " + cbalance;
            MessageBox.Show(message);
            CloseConnection();

        }

        public AutoCompleteStringCollection getDonationNamesList()
        {
            OpenConnection();

            String query = "SELECT donate_name from atm_query.donation_account";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            MySqlDataAdapter datadapt = new MySqlDataAdapter(cmd);
            DataSet donateList = new DataSet();

            datadapt.Fill(donateList);
            AutoCompleteStringCollection column1 = new AutoCompleteStringCollection();

            for (int i = 0; i < donateList.Tables[0].Rows.Count - 1; i++)
            {
                column1.Add(donateList.Tables[0].Rows[i]["donate_name"].ToString());
            }


            CloseConnection();
            return column1;
        }





        public Dictionary<string, string> getDonationDetails(String donateName)
        {

            Dictionary<string, string> userDetails = new Dictionary<string, string>();
            try
            {

                OpenConnection();

            /*
                string querys = "SELECT donate_actnum FROM atm_query.donation_account WHERE donate_name = '" + donateName + "';";
                MySqlCommand cmds = new MySqlCommand(querys, connection);
                MySqlDataReader dataReader = cmds.ExecuteReader();

                while (dataReader.Read())
                {
                    //if (dataReaders.HasRows) { 
                   
                  
                    /// }
                }   */










                string query = "SELECT * FROM atm_query.donation_account WHERE donate_name ='" + donateName + "';";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {

                    //  accounttnum = actnums(); 

                    userDetails["donate_name"] = dataReader.GetString("donate_name");
                    userDetails["donate_address"] = dataReader.GetString("donate_address");
                   
                    // return userDetails;


                }
                dataReader.Close();
            }
            catch
            {

            }

            CloseConnection();
            return userDetails;
        }

        public void deleteUserAccount(string actnum)
        {
            OpenConnection();
            string query = "DELETE FROM atm_query.user_accounts WHERE actnum='" + actnum + "'";
            


                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();

            CloseConnection();
            
        }


        public bool validatelogAdmin(String un, String pass)
        {
            Dictionary<string, string> userDetails = new Dictionary<string, string>();
            try
            {
                // DBconnect con = new DBconnect();

                OpenConnection();
                string query = "SELECT * FROM atm_query.admin_table";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    String checku = dataReader.GetString("admin_username");
                  //  Console.WriteLine(checku);
                    String checkp = dataReader.GetString("admin_password");
                   // Console.WriteLine(checku);

                    if (checku == un && checkp == pass)
                    {
                       
                        CloseConnection();
                        return true;
                    }
                }

            }
            catch
            {

            }

            CloseConnection();
            return false;
        }


        public String getAdminName(String username)
        {
            try
            {
                // DBconnect con = new DBconnect();
                OpenConnection();
                string query = "SELECT admin_name FROM atm_query.admin_table WHERE admin_username ='" + username + "';";

                MySqlCommand cmd = new MySqlCommand(query, connection);

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while (dataReader.Read())
                {
                    String fname = dataReader.GetString("admin_name");

                    CloseConnection();
                    return fname;


                }
                dataReader.Close();
            }
            catch
            {

            }
            CloseConnection();
            return "";
        }


    }
}