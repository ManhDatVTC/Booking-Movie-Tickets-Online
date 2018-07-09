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
            string regex = @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$";
            string regexPassword = @"^[-.@_a-zA-Z0-9 ]+$";
            string Email = "valentinolivgr@gmail.com";
            string pass = "123456";
            Assert.Matches (regex, Email);
            Assert.Matches (regexPassword,pass);
            Assert.NotNull (custom.Login (Email, pass));
        }

        [Theory]
        [InlineData ("valentinol''ivgrmail.com", "12345''6")]
        [InlineData ("''", "12''34s56")]
        public void TestDataLoginCinemaFail (string email, string pass) {
            string regexEmail = @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$";
            string regexPassword = @"^[-.@_a-zA-Z0-9 ]+$";
            Assert.DoesNotMatch (regexEmail, email);
            Assert.DoesNotMatch (regexPassword, pass);
            Assert.Null (custom.Login (email, pass));
        }
    }
}