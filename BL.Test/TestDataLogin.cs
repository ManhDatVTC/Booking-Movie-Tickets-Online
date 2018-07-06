using System;
using System.Text.RegularExpressions;
using BL;
using Xunit;

namespace BL.Test {
    public class TestDataLogin {
        private static Customer_Bl custom = new Customer_Bl ();
        // User Regex format Email 
        [Fact]
        public void TestDataLoginCinemaTrue () {
            string regex = @"^[^@]+@[^@.]+\.[^@]*\w\w$|^0[0-9]{9,10}$";
            string Email = "valentinolivgr@gmail.com";
            Assert.Matches (regex, Email);
            Assert.NotNull (custom.Login (Email, "123456"));
        }

        [Theory]
        [InlineData ("valentinolivgrmail.com", "123456")]
        [InlineData ("valentinolivgassrmail.com", "1234s56")]
        public void TestDataLoginCinemaFail (string email,string pass) {
            string regex = @"^[^@]+@[^@.]+\.[^@]*\w\w$|^0[0-9]{9,10}$";
            Assert.DoesNotMatch (regex, email);
            Assert.Null (custom.Login (email, pass));
        }
    }
}