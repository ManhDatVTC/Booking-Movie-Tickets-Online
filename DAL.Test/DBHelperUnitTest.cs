using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class DBHelperUnitTest {

        [Fact]
        public void OpenConnectionTest () {
            Assert.NotNull (DBHelper.OpenConnection ());
        }
        private static string connectionString = $"server=localhost;user id=root;password=123456;port=3306;database=MovieEngagement;SslMode=none";
        [Fact]

        public void OpenConnectionWithStringTrueTest () {
            Assert.NotNull (DBHelper.OpenConnection (connectionString));
        }
        private static string connectionString1 = $"server=localhost1;user id=root;password=123456;port=3306;database=MovieEngagement1;SslMode=none";
        [Fact]
        public void OpenConnectionWithStringFailTest () {
            Assert.Null (DBHelper.OpenConnection(connectionString1));
        }

    }
}