using System;

namespace Persitence {
    public class Rooms {
        private int room_id;
        private String name;
        private int number_Of_seats;
        public Rooms () { }
        public Rooms (int room_id, string name, int number_Of_seats) {
            this.room_id = room_id;
            this.name = name;
            this.number_Of_seats = number_Of_seats;
        }

        public int Room_id { get => room_id; set => room_id = value; }
        public string Name { get => name; set => name = value; }
        public int Number_Of_seats { get => number_Of_seats; set => number_Of_seats = value; }
        public override bool Equals (object obj) {
            Rooms room = (Rooms) obj;

            return Room_id == room.Room_id;
        }

        public override int GetHashCode () {
            return (Room_id + Name + Number_Of_seats).GetHashCode ();
        }
    }
}