using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class PriceSeats_DAL {
        private string query;
        private MySqlConnection connection;
        private MySqlDataReader reader;

        public PriceSeats_DAL () {
            connection = DBHelper.OpenConnection ();
        }

        public static PriceSeats GetPriceSeat (MySqlDataReader reader) {
            PriceSeats pri = new PriceSeats ();
            pri.Price_Id = reader.GetInt32 ("price_id");
            pri.TicketType = reader.GetString ("ticketType");
            pri.Price = reader.GetDouble ("price");
            return pri;
        }
        public List<PriceSeats> GetPriceSeats () {
            query = "Select * from PriceSeat;";
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            List<PriceSeats> list = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                list = new List<PriceSeats> ();
                while (reader.Read ()) {
                    list.Add (GetPriceSeat (reader));
                }
            }
            connection.Close ();
            // }
            return list;
        }
        public PriceSeats GetPriceSeatByPrice_Id (int price_id) {
            if (price_id == 0) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            query = $"Select * from PriceSeat where price_id = '{price_id}';";
            PriceSeats pri = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    // pri = new PriceSeats ();
                    pri = GetPriceSeat (reader);
                }
            }
            // }
            connection.Close ();
            return pri;
        }
    }
}