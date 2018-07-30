using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class ScheduleDAL {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public ScheduleDAL () {
            connection = DBHelper.OpenConnection ();
        }
        public static Schedules GetSchedule (MySqlDataReader reader) {
            Schedules schedule = new Schedules ();
            schedule.Schedule_id = reader.GetInt32 ("schedule_id");
            schedule.Movie_id = reader.GetInt32 ("movie_id");
            schedule.Room_id = reader.GetInt32 ("room_id");
            schedule.Show_date = reader.GetDateTime ("show_date");
            schedule.Start_time = reader.GetTimeSpan ("start_time");
            schedule.End_time = reader.GetTimeSpan ("end_time");
            schedule.Price = reader.GetDouble ("price");
            schedule.Schedule_room_seat = reader.GetString ("schedule_room_seat");
            return schedule;
        }
        // Lấy ra tất cả các schedule movie trong DB.
        public List<Schedules> GetSchedules () {
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = "Select * from Schedules;";
            List<Schedules> list = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                list = new List<Schedules> ();
                while (reader.Read ()) {
                    list.Add (GetSchedule (reader));
                }
            }
            // }
            connection.Close ();
            return list;
        }
        // Lấy ra lịch chiếu khi chuyền id của lịch.
        public Schedules GetScheduleByIdSchedule (int schedule_id) {
            if (schedule_id == 0) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"Select * from Schedules where schedule_id = '{schedule_id}';";
            Schedules schedule = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    // schedule = new Schedules ();
                    schedule = GetSchedule (reader);
                }
            }
            // }
            connection.Close ();
            return schedule;
        }
        // Lấy ra lịch chiếu khi chuyền id của phim.
        public List<Schedules> GetScheduleByIdMovie (int movie_id) {
            if (movie_id == 0) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"Select * from Schedules where movie_id = '{movie_id}';";
            List<Schedules> schedule = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                schedule = new List<Schedules> ();
                while (reader.Read ()) {
                    schedule.Add (GetSchedule (reader));
                }
            }
            // }
            connection.Close ();
            return schedule;
        }
        // Lấy ra lịch chiếu khi chuyền ID CỦA ROOM.
        public Schedules GetScheduleByIdRooms (int room_id) {
            if (room_id == 0) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"Select * from Schedules where room_id = '{room_id}';";
            Schedules schedule = new Schedules ();
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    schedule = GetSchedule (reader);
                }
            }
            // }
            connection.Close ();
            return schedule;
        }
        // Methor phụ, để trả về 1 datatime - > by Show date.
        private DateTime GetDateTimeForSchedule (MySqlDataReader reader) {

            DateTime Show_date = reader.GetDateTime ("show_date");
            return Show_date;
        }
        // Lấy ra một danh sách các datetime Schedule nhờ movie_id
        public List<DateTime> SelectDatetime (int movie_id) {
            if (movie_id == 0) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"SELECT DISTINCT show_date FROM Schedules where movie_id = '{movie_id}';";
            List<DateTime> date = new List<DateTime> ();
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                while (reader.Read ()) {
                    date.Add (GetDateTimeForSchedule (reader));
                }
            }
            // }
            connection.Close ();
            return date;
        }
        // Lấy ra các lịch chiếu nhờ movie id và date
        public List<Schedules> SelectTime (int movie_id, string date) {
            string regexDate = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
            if (Regex.IsMatch (date, regexDate) != true) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"Select * from Schedules where movie_id = '{movie_id}' and show_date = '{date}' ;";
            List<Schedules> schedule = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                schedule = new List<Schedules> ();
                while (reader.Read ()) {
                    schedule.Add (GetSchedule (reader));
                }
            }
            // }
            connection.Close ();

            return schedule;
        }
        // Lay ra lich chieu nho movie id date start time. 
        public Schedules SelectTimeBy (int movie_id, string date, string start_time) {
            string regexDate = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
            string regexTime = @"^(\d{1,2}|\d\.\d{2}):([0-5]\d):([0-5]\d)(\.\d+)?$";
            if (Regex.IsMatch (date, regexDate) != true || Regex.IsMatch (start_time, regexTime) != true) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"Select * from Schedules where movie_id = '{movie_id}' and show_date = '{date}' and start_time = '{start_time}' ;";
            Schedules schedule = null;
            // using (connection = DBHelper.OpenConnection ()) {
            // schedule = new Schedules ();
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    schedule = new Schedules ();
                    schedule = GetSchedule (reader);
                }
            }
            // }
            connection.Close ();
            return schedule;
        }
        public bool AddMapSeats (Schedules schedule, string mapSeats) {
            bool result = false;

            if (schedule == null || schedule.Schedule_id == 0 || schedule.Schedule_room_seat == null || schedule.Schedule_room_seat.Equals ("") || mapSeats == "") {
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
                command.CommandText = $"update Schedules set schedule_room_seat = '{mapSeats}' where schedule_id = {schedule.Schedule_id};";
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
            // }
            connection.Close ();
            return result;

        }
    }
}