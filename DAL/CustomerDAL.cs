using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class Custome_DAL {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;
        private DBHelper DB = new DBHelper ();
        public Custome_DAL () {
            connection = DBHelper.OpenConnection ();
        }
        public static Customer GetCustomer (MySqlDataReader reader) {
            Customer customer = new Customer ();
            customer.Customer_id = reader.GetInt32 ("customer_id");
            customer.Name = reader.GetString ("name");
            customer.Email = reader.GetString ("customer_email");
            customer.Phone = reader.GetString ("customer_phone");
            customer.User_name = reader.GetString ("user_name");
            customer.Password = reader.GetString ("password");
            customer.Birthday = reader.GetDateTime ("birthday");
            customer.Address = reader.GetString ("address");
            customer.Sex = reader.GetString ("sex");
            customer.Account_bank = reader.GetString ("account_bank");
            customer.Register_date = reader.GetDateTime ("register_date");
            return customer;
        }
        public Customer Login (string email, string password) {
            string regexEmail = @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$";
            string regexPassword = @"^[-.@_a-zA-Z0-9 ]+$";
            if (Regex.IsMatch (email, regexEmail) != true || email == "" || Regex.IsMatch (password, regexPassword) != true || password == "") {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            // if (connection.State == System.Data.ConnectionState.Closed) {
            //     connection.Open ();
            // }
            query = $"Select * From Customer  where customer_email = '{email}' and password = '{password}';";
            Customer customer = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    // customer = new Customer ();
                    customer = GetCustomer (reader);
                }
            }
            // }
            connection.Close ();
            return customer;
        }
        // public List<Customer> GetCustomers (MySqlCommand command) {
        //     List<Customer> list = new List<Customer> ();
        //     using (connection = DBHelper.OpenConnection ()) {
        //         MySqlCommand cmd = new MySqlCommand (query, connection);
        //         using (reader = cmd.ExecuteReader ()) {
        //             while (reader.Read ()) {
        //                 list.Add (GetCustomer (reader));
        //             }
        //         }
        //     }
        //     return list;
        // }
    }

}