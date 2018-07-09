using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;
using Persitence;

namespace BL {
    public class ScheduleBL {
        private ScheduleDAL sche = new ScheduleDAL ();
        public List<Schedules> GetSchedules () {
            return sche.GetSchedules ();
        }
        public Schedules GetScheduleByIdSchedule (int schedule_id) {
            return sche.GetScheduleByIdSchedule (schedule_id);
        }
        public List<Schedules> GetScheduleByIdMovie (int movie_id) {
            return sche.GetScheduleByIdMovie (movie_id);
        }
        public Schedules GetScheduleByIdRooms (int room_id) {
            return sche.GetScheduleByIdRooms (room_id);
        }
        public List<DateTime> SelectDatetime (int movie_id) {
            return sche.SelectDatetime (movie_id);
        }
        public List<Schedules> SelectTime (int movie_id, string date) {
            string regexDate = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
            if (Regex.IsMatch(date,regexDate) != true) {
                return null;
            }
            return sche.SelectTime (movie_id, date);
        }
        public Schedules SelectTimeBy (int movie_id, string date, string start_time) {
            string regexDate = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
            string regexTime = @"^(\d{1,2}|\d\.\d{2}):([0-5]\d):([0-5]\d)(\.\d+)?$";
            if (Regex.IsMatch(date,regexDate) != true || Regex.IsMatch(start_time,regexTime) != true) {
                return null;
            }
            return sche.SelectTimeBy (movie_id, date, start_time);
        }
    }
}