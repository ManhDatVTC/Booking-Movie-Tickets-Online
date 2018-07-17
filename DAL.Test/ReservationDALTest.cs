using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class TestReservation {
        private ReservationDAL reservation = new ReservationDAL ();

        [Fact]
        public void TestGetReservationByCustomerIdFail () {
            // Có thể Null vì khánh hàng chưa đặt gì.
            // Vậy thì test cái gì nhỉ.
            Assert.Null (reservation.GetReservationByCustomerId (null));
        }

        [Fact]
        public void TestInsertIntoReservationTrue () {
            Reservation reser = new Reservation (1, 1, 1, "A1 A2 A3 A4 A5", 225000.0, DateTime.Now);
            Assert.True (reservation.InsertIntoReservation (reser));
        }

        [Fact]
        public void TestInsertIntoReservationFail () {
            Reservation reser = new Reservation (1, 1, 1, null, 225000.0, DateTime.Now);
            Assert.False (reservation.InsertIntoReservation (reser));
        }

        [Fact]
        public void TestInsertIntoReservationFail1 () {
            Reservation reser = new Reservation (1, 0, 1, null, 225000.0, DateTime.Now);
            Assert.False (reservation.InsertIntoReservation (reser));
        }

        [Fact]
        public void TestInsertIntoReservationFail2 () {
            Reservation reser = new Reservation (1, 0, 0, null, 225000.0, DateTime.Now);
            Assert.False (reservation.InsertIntoReservation (reser));
        }
        [Fact]
        public void TestInsertIntoReservationFail3()
        {
            Reservation reser = new Reservation (1, 1, 1, "", 225000.0, DateTime.Now);
            Assert.False (reservation.InsertIntoReservation (reser));
        }

    }
}