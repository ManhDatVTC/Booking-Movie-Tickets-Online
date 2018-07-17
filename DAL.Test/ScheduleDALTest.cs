using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class ScheduleDALTest {
        private static ScheduleDAL sch = new ScheduleDAL ();
        private static Movies_DAL movieDAL = new Movies_DAL ();
        // private static Schedules schedule;
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
        public void TestSelectTimeByMovieIdDatetimeTrue () {
            DateTime timeStart = new DateTime (2018, 7, 20, 0, 0, 0);
            string comparedatetime1 = timeStart.ToString ($"{timeStart:yyyy-MM-dd}");
            Assert.NotNull (sch.SelectTime (1, comparedatetime1));
        }

        [Fact]
        public void TestSelectTimeByMovieIdDatetimeFail () {
            Assert.Null (sch.SelectTime (1, ""));
        }

        [Fact]
        public void TestSelectScheduleByDatimeMovieIdTrue () {
            DateTime timeStart = new DateTime (2018, 7, 20, 0, 0, 0);
            string comparedatetime1 = timeStart.ToString ($"{timeStart:yyyy-MM-dd}");
            TimeSpan timeSpan = new TimeSpan (8, 0, 0);
            string time = string.Format ("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            Assert.NotNull (sch.SelectTimeBy (1, comparedatetime1, time));
        }
        // Fail. 
        [Theory]
        [InlineData (1, "", "")]
        [InlineData (1, "2018-01-21", "01:00:00")]
        public void TestSelectScheduleByDatimeMovieIdFail (int movie_id, string datetime, string time) {
            DateTime timeStart = new DateTime (2018, 7, 20, 0, 0, 0);
            datetime = timeStart.ToString ($"{timeStart:yyyy-MM-dd}");
            TimeSpan timeSpan = new TimeSpan (1, 0, 0);
            Assert.False (sch.SelectTimeBy (movie_id, datetime, time) != null);
        }

        [Fact]
        public void TestAddMapSeatsTrue () {
            Schedules schedu = new Schedules (1, 1, 1, new DateTime (2018, 7, 20), new TimeSpan (21, 0, 0), new TimeSpan (21, 0, 0), "MapSeat", 45000);
            Assert.True (sch.AddMapSeats (schedu, "A B C D F E G H J K L M;10;"));
        }

        [Theory]
        [InlineData (null, "")]
        // [InlineData(new Schedules (1, 1, 1, new DateTime (2018, 7, 20), new TimeSpan (21, 0, 0), new TimeSpan (21, 0, 0), "MapSeat", 45000))]
        public void TestAddMapSeatsFail (Schedules schedule, string MapSeat) {
            Assert.False (sch.AddMapSeats (schedule, MapSeat));
        }

        [Fact]
        public void TestAddMapSeatsFail1 () {
            Schedules schedule = new Schedules (1, 1, 1, new DateTime (2018, 7, 20, 0, 0, 0), new TimeSpan (21, 0, 0), new TimeSpan (21, 0, 0), "", 45000);
            string MapSeat = "";
            Assert.False (sch.AddMapSeats (schedule, MapSeat));
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