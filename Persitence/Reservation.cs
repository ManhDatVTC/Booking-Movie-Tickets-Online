using System;

namespace Persitence {
    public class Reservation {
        private int reservation_id;
        private Customer customer;
        private Schedules schedule;
        private String noOfseat;
        private Double price;
        private DateTime create_on;

        public Reservation () { }

        public Reservation(int reservation_id, Customer customer, Schedules schedule, string noOfseat, double price, DateTime create_on)
        {
            this.reservation_id = reservation_id;
            this.customer = customer;
            this.schedule = schedule;
            this.noOfseat = noOfseat;
            this.price = price;
            this.create_on = create_on;
        }

        public int Reservation_id { get => reservation_id; set => reservation_id = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public Schedules Schedule { get => schedule; set => schedule = value; }
        public string NoOfseat { get => noOfseat; set => noOfseat = value; }
        public double Price { get => price; set => price = value; }
        public DateTime Create_on { get => create_on; set => create_on = value; }
    }
}