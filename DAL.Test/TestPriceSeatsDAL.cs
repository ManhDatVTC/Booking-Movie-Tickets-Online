using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class PriceSeatsTest {
        private static PriceSeats_DAL pri = new PriceSeats_DAL ();
        [Fact]
        public void TestGetPriceSeats () {
            Assert.NotNull (pri.GetPriceSeats ());
        }

        [Fact]
        public void TestGetPriceSeatByPrice_Id () {
            Assert.NotNull (pri.GetPriceSeatByPrice_Id (1));
        }
        [Fact]
        public void TestGetPriceSeatByPrice_IdFail()
        {
            Assert.Null (pri.GetPriceSeatByPrice_Id (0));
        }
    }
}