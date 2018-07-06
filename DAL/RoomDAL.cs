using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class Room_DAL {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public static Rooms GetRoom (MySqlDataReader reader) {
            Rooms room = new Rooms ();
            room.Room_id = reader.GetInt32 ("room_id");
            room.Name = reader.GetString ("name");
            room.Number_Of_seats = reader.GetInt32 ("number_of_seats");
            return room;
        }
        public List<Rooms> GetRooms () {
            string query = "Select * from Rooms;";
            List<Rooms> list = new List<Rooms> ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    while (reader.Read ()) {
                        list.Add (GetRoom (reader));
                    }
                }
            }
            return list;
        }
        public Rooms GetRoomById (int room_id) {
            string query = $"Select * from Rooms where room_id = '{room_id}';";
            Rooms room = new Rooms ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    if (reader.Read ()) {
                        room = GetRoom (reader);
                    }
                }
            }
            return room;
        }
    }
}