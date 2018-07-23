using System;

namespace Persitence {
    public class Rooms {
        private int room_id;
        private String name;
        private int number_Of_seats;

        private string mapSeat;
        private string map_VIP;
        private string chaire_not_placed;
        public Rooms () { }

        public Rooms(int room_id, string name, int number_Of_seats, string mapSeat, string map_VIP, string chaire_not_placed)
        {
            this.room_id = room_id;
            this.name = name;
            this.number_Of_seats = number_Of_seats;
            this.mapSeat = mapSeat;
            this.map_VIP = map_VIP;
            this.chaire_not_placed = chaire_not_placed;
        }

        public int Room_id { get => room_id; set => room_id = value; }
        public string Name { get => name; set => name = value; }
        public int Number_Of_seats { get => number_Of_seats; set => number_Of_seats = value; }
        public string MapSeat { get => mapSeat; set => mapSeat = value; }
        public string Map_VIP { get => map_VIP; set => map_VIP = value; }
        public string Chaire_not_placed { get => chaire_not_placed; set => chaire_not_placed = value; }

        public override bool Equals (object obj) {
            Rooms room = (Rooms) obj;
            return this.GetHashCode () == room.GetHashCode ();
        }

        public override int GetHashCode () {
            return (Room_id + Name + Number_Of_seats + MapSeat).GetHashCode ();
        }
    }
}