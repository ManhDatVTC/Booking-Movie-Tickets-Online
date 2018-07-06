using System;
using System.Collections.Generic;
using DAL;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class CustomerDALTest {
        private Custome_DAL cus = new Custome_DAL ();
        [Fact]
        public void LoginTest () {
            Assert.NotNull (cus.Login ("valentinolivgr@gmail.com", "123456"));
        }

        [Fact]
        public void LoginTest1 () {
            Customer customer = cus.Login ("", "");
            // Assert.NotEqual ("", customer.Email);
            // Assert.NotEqual ("", customer.Password);
            Assert.Null (customer);
        }

        [Fact]
        public void TestName () {
            Customer customer = cus.Login (",,1`12", "asdfg");
            Assert.Null (customer);
        }

    }
}