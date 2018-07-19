using System;

namespace Persitence {
    public class Reservation {
        private int reservation_id;
        private int customer_id;
        private int schedule_id;
        private string seats;
        private int code_ticket;
        private double price;
        private DateTime create_on;

        public Reservation () { }

        public Reservation(int reservation_id, int customer_id, int schedule_id, string seats, int code_ticket, double price, DateTime create_on)
        {
            this.reservation_id = reservation_id;
            this.customer_id = customer_id;
            this.schedule_id = schedule_id;
            this.seats = seats;
            this.code_ticket = code_ticket;
            this.price = price;
            this.create_on = create_on;
        }

        public int Reservation_id { get => reservation_id; set => reservation_id = value; }
        public int Customer_id { get => customer_id; set => customer_id = value; }
        public int Schedule_id { get => schedule_id; set => schedule_id = value; }
        public string Seats { get => seats; set => seats = value; }
        public int Code_ticket { get => code_ticket; set => code_ticket = value; }
        public double Price { get => price; set => price = value; }
        public DateTime Create_on { get => create_on; set => create_on = value; }

        // public Reservation(int reservation_id, int customer_id, int schedule_id, string seats, double price, DateTime create_on)
        // {
        //     this.reservation_id = reservation_id;
        //     this.customer_id = customer_id;
        //     this.schedule_id = schedule_id;
        //     this.seats = seats;
        //     this.price = price;
        //     this.create_on = create_on;
        // }

        // public int Reservation_id { get => reservation_id; set => reservation_id = value; }
        // public int Customer_id { get => customer_id; set => customer_id = value; }
        // public int Schedule_id { get => schedule_id; set => schedule_id = value; }
        // public string Seats { get => seats; set => seats = value; }
        // public double Price { get => price; set => price = value; }
        // public DateTime Create_on { get => create_on; set => create_on = value; }

        public override bool Equals (object obj) {
            Reservation reser = (Reservation) obj;

            return this.GetHashCode() == reser.GetHashCode();
        }
        public override int GetHashCode () {
            return ("" + Reservation_id + Customer_id + Schedule_id + Seats + Price + Create_on + Code_ticket).GetHashCode ();
        }
    }
}