using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class RoomDALTest {
        private static Room_DAL roomDAL = new Room_DAL ();
        private MySqlConnection connection;
        private MySqlDataReader reader;
        // private static Rooms room = new Rooms ();

        [Fact]
        public void GetRoomById () {
            Assert.NotNull (roomDAL.GetRoomById (1));
            Assert.Equal (1, roomDAL.GetRoomById (1).Room_id);
        }

        [Fact]
        public void GetRoomTest () {
            List<Rooms> rooms = new List<Rooms> ();
            rooms = roomDAL.GetRooms ();
            string query = $"select * from Rooms order by rand() limit 1 ;";
            Rooms roomRand = GetRoomExecQuery (query);
            string query1 = $"select * from Rooms order by room_id asc limit 1; ";
            Rooms roomTop = GetRoomExecQuery (query1);
            string query2 = $"select * from Rooms order by room_id desc limit 1;";
            Rooms roomBottom = GetRoomExecQuery (query2);

            Assert.NotNull (rooms);
            Assert.NotNull (roomRand);
            Assert.NotNull (roomTop);
            Assert.NotNull (roomBottom);

            //  So sanh vi tri Top Bottom va Rand xem co giong trong List.
            Assert.True (roomTop.Room_id == rooms[0].Room_id);
            Assert.True (roomBottom.Room_id == rooms[rooms.Count - 1].Room_id);
            Assert.Contains(roomRand,rooms);
        }
        private Rooms GetRoomExecQuery (string query) {
            Rooms room = new Rooms ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    if (reader.Read ()) {
                        room = Room_DAL.GetRoom (reader);
                    }
                }
            }
            return room;
        }
    }
}