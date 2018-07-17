using System;

namespace Persitence {
    public class PriceSeats {
        private int price_Id;
        private string ticketType;
        private double price;
        public PriceSeats(){}

        public PriceSeats(int price_Id, string ticketType, double price)
        {
            this.price_Id = price_Id;
            this.ticketType = ticketType;
            this.price = price;
        }

        public int Price_Id { get => price_Id; set => price_Id = value; }
        public string TicketType { get => ticketType; set => ticketType = value; }
        public double Price { get => price; set => price = value; }
    }
}