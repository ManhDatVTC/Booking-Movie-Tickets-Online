using System;

namespace Persitence {
    public class Schedules {
        private int schedule_id;
        private int movie_id;
        private int room_id;
        private DateTime show_date;
        private TimeSpan start_time;
        private TimeSpan end_time;
        private string schedule_room_seat;

        private double price;
        public Schedules () { }

        public Schedules(int schedule_id, int movie_id, int room_id, DateTime show_date, TimeSpan start_time, TimeSpan end_time, string schedule_room_seat, double price)
        {
            this.schedule_id = schedule_id;
            this.movie_id = movie_id;
            this.room_id = room_id;
            this.show_date = show_date;
            this.start_time = start_time;
            this.end_time = end_time;
            this.schedule_room_seat = schedule_room_seat;
            this.price = price;
        }

        public int Schedule_id { get => schedule_id; set => schedule_id = value; }
        public int Movie_id { get => movie_id; set => movie_id = value; }
        public int Room_id { get => room_id; set => room_id = value; }
        public DateTime Show_date { get => show_date; set => show_date = value; }
        public TimeSpan Start_time { get => start_time; set => start_time = value; }
        public TimeSpan End_time { get => end_time; set => end_time = value; }
        public string Schedule_room_seat { get => schedule_room_seat; set => schedule_room_seat = value; }
        public double Price { get => price; set => price = value; }

        public override bool Equals (object obj) {
            Schedules schedDetail = (Schedules) obj;

            return this.GetHashCode() == schedDetail.GetHashCode();
        }

        public override int GetHashCode () {
            return ("" + Schedule_id + Movie_id + Room_id + Show_date + Start_time + End_time + Price + Schedule_room_seat).GetHashCode ();
        }
    }
}