using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace DAL {
    public class DBHelper {
        // private static MySqlConnection connection;
        // public static MySqlConnection GetConnection () {
        //     MySqlConnection connection = new MySqlConnection {
        //         ConnectionString = "server=localhost;user id=root;password=123456;port=3306;database=MovieEngagement;SslMode=none"
        //     };
        //     return connection;
        // }
        // public static MySqlConnection OpenConnection () {
        //     MySqlConnection connection = GetConnection ();
        //     connection.Open ();
        //     return connection;
        // }
        public static MySqlConnection OpenConnection () {
            try {
                using (FileStream fileStream = new FileStream ("ConnectionString.txt", FileMode.Open)) {
                    string connectionString;
                    using (StreamReader reader = new StreamReader (fileStream)) {
                        connectionString = reader.ReadLine ();
                    }
                    return OpenConnection (connectionString);
                }
                // FileStream fileStream = new FileStream("ConnectionString.txt", FileMode.Open);
                // using (StreamReader reader = new StreamReader(fileStream))
                // {
                //     connectionString = reader.ReadLine();
                // }
                // return OpenConnection(connectionString);
            } catch {
                return null;
            }
        }

        public static MySqlConnection OpenConnection (string connectionString) {
            try {
                MySqlConnection connection = new MySqlConnection {
                    ConnectionString = connectionString
                };
                connection.Open ();
                return connection;
            } catch {
                return null;
            }
        }
    }
}