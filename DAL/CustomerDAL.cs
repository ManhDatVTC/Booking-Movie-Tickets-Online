using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class Custome_DAL {
        private string query;
        private MySqlConnection connection = DBHelper.OpenConnection ();
        private MySqlDataReader reader;
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
            string regexEmail = @"^[-.@_a-zA-Z0-9áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$";
            string regexPassword = @"^[-.@_a-zA-Z0-9 ]+$";
            if (Regex.IsMatch (email, regexEmail) != true || email == "" || Regex.IsMatch (password, regexPassword) != true || password == "") {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            if (Regex.IsMatch (email, @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$") != true) {
                query = $"Select * From Customer  where user_name = '{email}' and password = '{password}';";
                // Select * From Customer  where user_name = 'Đạt liv' and password = '123456';
            } else {
                query = $"Select * From Customer  where customer_email = '{email}' and password = '{password}';";
            }
            Customer customer = null;
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    customer = GetCustomer (reader);
                }
            }
            connection.Close ();
            return customer;
        }
        public bool ChangeLogin (Customer cu) {
            bool result = false;
            if (cu.User_name == null || cu.Password == null) {
                return result;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            MySqlCommand command = connection.CreateCommand ();
            MySqlTransaction transaction = connection.BeginTransaction ();
            command.Connection = connection;
            command.CommandText = "lock tables Customer write , Movies write, Rooms write ,Schedules write;";
            command.ExecuteNonQuery ();
            command.Transaction = transaction;
            try {
                // command.CommandText = $"update Schedules set schedule_room_seat = '{mapSeats}' where schedule_id = {schedule.Schedule_id};";
                // update Customer set user_name = 'Dat hihi', password = '1234',name = 'Tran Manh Dat ' , customer_email = 'valen@gmail.com',customer_phone = '0988968289',address = 'Hung Yen - VN'  where customer_id = 1;
                command.CommandText = $@"update Customer set user_name = '{cu.User_name}', password = '{cu.Password}',name = '{cu.Name}' , customer_email = '{cu.Email}',customer_phone = '{cu.Phone}',address = '{cu.Address}'  where customer_id = {cu.Customer_id};";
                command.ExecuteNonQuery ();
                transaction.Commit ();
                result = true;
            } catch (Exception ex) {
                Console.WriteLine ("Commit Exception Type: {0}", ex.GetType ());
                Console.WriteLine ("  Message: {0}", ex.Message);
                try {
                    transaction.Rollback ();
                } catch (Exception ex2) {
                    Console.WriteLine ("Rollback Exception Type: {0}", ex2.GetType ());
                    Console.WriteLine ("  Message: {0}", ex2.Message);
                }
            } finally {
                command.CommandText = "unlock tables;";
                command.ExecuteNonQuery ();

            }
            connection.Close ();
            return result;
        }
        // try {
        //     command.CommandText = $"update Schedules set schedule_room_seat = '{mapSeats}' where schedule_id = {schedule.Schedule_id};";
        //     command.ExecuteNonQuery ();
        //     transaction.Commit ();
        //     result = true;
        // } catch (Exception ex) {
        //     Console.WriteLine ("Commit Exception Type: {0}", ex.GetType ());
        //     Console.WriteLine ("  Message: {0}", ex.Message);
        //     try {
        //         transaction.Rollback ();
        //     } catch (Exception ex2) {
        //         Console.WriteLine ("Rollback Exception Type: {0}", ex2.GetType ());
        //         Console.WriteLine ("  Message: {0}", ex2.Message);
        //     }
        // } finally {
        //     command.CommandText = "unlock tables;";
        //     command.ExecuteNonQuery ();

        // }
        // // }
        // connection.Close ();
        // return result;
    }

}