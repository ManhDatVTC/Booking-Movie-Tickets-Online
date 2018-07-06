using System;

namespace Persitence {
    public class Customer {
        private int customer_id;
        private string name;
        private string email;
        private string phone;
        private string user_name;
        private string password;
        private DateTime birthday;
        private String address;
        private String sex;
        private string account_bank;
        private DateTime register_date;
        public Customer () { }

        public Customer(int customer_id, string name, string email, string phone, string user_name, string password, DateTime birthday, string address, string sex, string account_bank, DateTime register_date)
        {
            this.customer_id = customer_id;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.user_name = user_name;
            this.password = password;
            this.birthday = birthday;
            this.address = address;
            this.sex = sex;
            this.account_bank = account_bank;
            this.register_date = register_date;
        }

        public int Customer_id { get => customer_id; set => customer_id = value; }
        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Phone { get => phone; set => phone = value; }
        public string User_name { get => user_name; set => user_name = value; }
        public string Password { get => password; set => password = value; }
        public string Account_bank { get => account_bank; set => account_bank = value; }
        public DateTime Register_date { get => register_date; set => register_date = value; }
        public DateTime Birthday { get => birthday; set => birthday = value; }
        public string Address { get => address; set => address = value; }
        public string Sex { get => sex; set => sex = value; }
    }
}