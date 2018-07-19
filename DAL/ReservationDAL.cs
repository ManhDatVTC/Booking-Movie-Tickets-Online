using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class ReservationDAL {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public ReservationDAL () {
            connection = DBHelper.OpenConnection ();
        }
        public static Reservation GetReservation (MySqlDataReader reader) {
            Reservation reservation = new Reservation ();
            reservation.Reservation_id = reader.GetInt32 ("reservation_id");
            reservation.Customer_id = reader.GetInt32 ("customer_Id");
            reservation.Schedule_id = reader.GetInt32 ("schedule_id");
            reservation.Seats = reader.GetString ("no_of_seat");
            reservation.Code_ticket = reader.GetInt32 ("code_ticket");
            reservation.Price = reader.GetDouble ("price");
            reservation.Create_on = reader.GetDateTime ("create_on");
            return reservation;
        }

        public List<Reservation> GetReservationByCustomerId (int Customer_id) {
            if ( Customer_id == 0) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"Select * from Reservation where customer_Id = {Customer_id};";
            List<Reservation> reservation = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                reservation = new List<Reservation> ();
                while (reader.Read ()) {
                    reservation.Add (GetReservation (reader));
                }
            }
            connection.Close ();
            // }
            return reservation;
        }

        public bool InsertIntoReservation (Reservation reservation) {
            bool result = false;
            if (reservation.Seats == null || reservation.Seats == "" || reservation.Customer_id == 0 || reservation.Schedule_id == 0 || reservation.Seats == " ") {
                return result;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand command = connection.CreateCommand ();
            MySqlTransaction transaction = connection.BeginTransaction ();
            command.Connection = connection;
            command.CommandText = "lock tables Customer write , Movies write, Rooms write ,Schedules write, Reservation write ;";
            command.ExecuteNonQuery ();
            command.Transaction = transaction;
            try {
                command.CommandText = $@"Insert Into Reservation (customer_Id,schedule_id,no_of_seat,code_ticket,price) 
                                            value ({reservation.Customer_id},{reservation.Schedule_id},'{reservation.Seats}',{reservation.Code_ticket},{reservation.Price});";
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
                // command.CommandText = "unlock tables;";
                command.CommandText = "unlock tables;";
                command.ExecuteNonQuery ();
            }
            // }
            connection.Close ();
            return result;
        }

    }
}