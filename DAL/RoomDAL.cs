using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class Room_DAL {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public Room_DAL () {
            connection = DBHelper.OpenConnection ();
        }
        public static Rooms GetRoom (MySqlDataReader reader) {
            Rooms room = new Rooms ();
            room.Room_id = reader.GetInt32 ("room_id");
            room.Name = reader.GetString ("name");
            room.Number_Of_seats = reader.GetInt32 ("number_of_seats");
            room.MapSeat = reader.GetString ("map_seat");
            room.Map_VIP = reader.GetString ("map_VIP");
            room.Chaire_not_placed = reader.GetString ("chaire_not_placed");
            return room;
        }
        public List<Rooms> GetRooms () {
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = "Select * from Rooms;";
            List<Rooms> list = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                list = new List<Rooms> ();
                while (reader.Read ()) {
                    list.Add (GetRoom (reader));
                }
            }
            // }
            connection.Close ();
            return list;
        }
        public Rooms GetRoomById (int room_id) {
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"Select * from Rooms where room_id = '{room_id}';";
            Rooms room = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    // room =  new Rooms ();
                    room = GetRoom (reader);
                }
            }
            // }
            connection.Close ();
            return room;
        }
    }
}