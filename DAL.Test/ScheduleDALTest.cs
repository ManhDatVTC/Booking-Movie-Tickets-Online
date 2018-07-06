using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class ScheduleDALTest {
        private static ScheduleDAL sch = new ScheduleDAL ();
        private static Movies_DAL movieDAL = new Movies_DAL ();
        private MySqlConnection connection;
        private MySqlDataReader reader;

        [Fact]
        public void GetSchedulesTest () {
            List<Schedules> schedule = sch.GetSchedules ();
            string query = $"select * from Schedules order by rand() limit 1 ;";
            Schedules scheduleRand = GetScheduleExecQuery (query);
            string query1 = $"select * from Schedules order by schedule_id asc limit 1;";
            Schedules scheduleTop = GetScheduleExecQuery (query1);
            string query2 = $"select * from Schedules order by schedule_id desc limit 1;";
            Schedules scheduleBottom = GetScheduleExecQuery (query2);

            Assert.NotNull (schedule);
            Assert.NotNull (scheduleRand);
            Assert.NotNull (scheduleTop);
            Assert.NotNull (scheduleBottom);

            // Assert.True (scheduleTop.Schedule_id == schedule[0].Schedule_id);
            // Assert.True (scheduleBottom.Schedule_id == schedule[schedule.Count - 1].Schedule_id);
            Assert.True (schedule.IndexOf (scheduleBottom) == schedule.Count - 1);
            Assert.True (schedule.IndexOf (scheduleTop) == 0);
            Assert.Contains (scheduleRand, schedule);

        }

        [Fact]
        public void GetScheduleByIdScheduleTest () {
            Assert.NotNull (sch.GetScheduleByIdSchedule (1));
            Assert.Equal (1, sch.GetScheduleByIdSchedule (1).Schedule_id);
        }

        [Fact]
        public void GetScheduleByIdMovieTest () {
            Assert.NotNull (sch.GetScheduleByIdMovie (1));
        }

        [Fact]
        public void GetScheduleByIdRoomsTest () {
            Assert.NotNull (sch.GetScheduleByIdRooms (1));
        }

        [Fact]
        public void SelectDatetimeByMovieidTest () {
            Assert.NotNull (sch.SelectDatetime (1));
        }

        [Fact]
        public void SelectTimeByMovieIDDateTimeTest () {
            DateTime timeStart = new DateTime (2018, 7, 20, 0, 0, 0);
            string comparedatetime1 = timeStart.ToString ($"{timeStart:yyyy-MM-dd}");
            Assert.NotNull (sch.SelectTime (1, comparedatetime1));
        }

        [Fact]
        public void SelectTimeByMovieIDDatetimeandTimeTest () {
            DateTime timeStart = new DateTime (2018, 7, 20, 0, 0, 0);
            string comparedatetime1 = timeStart.ToString ($"{timeStart:yyyy-MM-dd}");
            TimeSpan timeSpan = new TimeSpan (8, 0, 0);
            string time = string.Format ("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            // Movie id , DateTime , Time =>>>   
            Assert.NotNull (sch.SelectTimeBy (1, comparedatetime1, time));
        }
        // Fail. 
        [Fact]
        public void SelectTimeByMovieIDDatetimeandTimeTestFail () {
            DateTime timeStart = new DateTime (2018, 7, 20, 0, 0, 0);
            string comparedatetime1 = timeStart.ToString ($"{timeStart:yyyy-MM-dd}");
            TimeSpan timeSpan = new TimeSpan (1, 0, 0);
            string time = string.Format ("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            // Movie id , DateTime , Time =>>>   
            Assert.True (sch.SelectTimeBy (1, comparedatetime1, time) == null);
        }
        private Schedules GetScheduleExecQuery (string query) {
            Schedules sche = new Schedules ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    if (reader.Read ()) {
                        sche = ScheduleDAL.GetSchedule (reader);
                    }
                }
            }
            return sche;
        }

    }
}