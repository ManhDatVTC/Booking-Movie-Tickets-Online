using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;
using Persitence;

namespace BL {
    public class PriceSeatsBl {
        private PriceSeats_DAL price_type = new PriceSeats_DAL ();
        public List<PriceSeats> GetPriceSeats () {
            return price_type.GetPriceSeats ();
        }
        public PriceSeats GetPriceSeatByPrice_Id (int price_id) {
            if (price_id == 0) {
                return null;
            }
            return price_type.GetPriceSeatByPrice_Id (price_id);
        }
    }
}