using System;
using System.Text.RegularExpressions;
using BL;
using Persitence;
using Xunit;

namespace BL.Test {
    public class ReservationBLTest {

        // [Fact]
        // public void TestReservationByCustomerId () {
        //     ReservationBL re = new ReservationBL ();
        //     Assert.Null (re.GetReservationByCustomerId (null));
        // }

        [Fact]
        public void TestReservationByCustomerId1 () {
            ReservationBL re = new ReservationBL ();
            Assert.Null (re.GetReservationByCustomerId (0));
        }

        [Fact]
        public void TestInsertIntoReservation () {
            ReservationBL re = new ReservationBL ();
            Reservation reser = new Reservation (2, 1, 2, "A5 A6 A7 A8",123456, 10000.00, DateTime.Now);
            Assert.True (re.InsertIntoReservation (reser));
        }

        [Fact]
        public void TestInsertIntoReservationFail () {
            ReservationBL re = new ReservationBL ();
            Reservation reser = new Reservation (0, 0, 0, "A5 A6 A7 A8",123456, 10000.00, DateTime.Now);
            Assert.False (re.InsertIntoReservation (reser));
        }
        [Fact]
        public void TestInsertIntoReservationFail1 () {
            ReservationBL re = new ReservationBL ();
            Reservation reser = new Reservation (2, 1, 2, null,123456, 10000.00, DateTime.Now);
            Assert.False (re.InsertIntoReservation (reser));
        }

    }
}