using System;
using System.Collections.Generic;
using System.IO;
using MySql.Data.MySqlClient;

namespace DAL {
    public class DBHelper {
        public static MySqlConnection OpenConnection () {
            try {
                using (FileStream fileStream = new FileStream ("ConnectionString.txt", FileMode.Open)) {
                    string connectionString;
                    using (StreamReader reader = new StreamReader (fileStream)) {
                        connectionString = reader.ReadLine ();
                    }
                    return OpenConnection (connectionString);
                }
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