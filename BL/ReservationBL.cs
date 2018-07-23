using System;
using System.Collections.Generic;
using DAL;
using Persitence;

namespace BL {
    public class ReservationBL {
        private ReservationDAL reser = new ReservationDAL ();
        public List<Reservation> GetReservationByCustomerId (int Customer_id) {
            if (Customer_id == 0) {
                return null;
            }
            if (reser.GetReservationByCustomerId (Customer_id) == null) {
                return null;
            }

            return reser.GetReservationByCustomerId (Customer_id);
        }

        public bool InsertIntoReservation (Reservation reservation) {
            if (reservation == null || reservation.Schedule_id == 0 || reservation.Customer_id == 0 || reservation.Seats == null || reservation.Seats == "") {
                return false;
            }
            return reser.InsertIntoReservation (reservation);
        }
    }
}