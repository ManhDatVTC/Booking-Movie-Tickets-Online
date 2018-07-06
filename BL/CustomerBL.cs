using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;
using Persitence;

namespace BL {
    public class Customer_Bl {
        private Custome_DAL idal = new Custome_DAL ();
        public Customer Login (String email, String password) {
            Regex regexEmail = new Regex (@"^[^@]+@[^@.]+\.[^@]*\w\w$|^0[0-9]{9,10}$");
            Regex regexPassword = new Regex ("[a-zA-Z0-9_]");
            MatchCollection matchCollectionEmail = regexEmail.Matches (email);
            MatchCollection matchConlectionPassword = regexPassword.Matches (password);
            if (matchCollectionEmail.Count <= 0 || matchConlectionPassword.Count <= 0)
            {
                return null;
            }
            return idal.Login (email, password);
        }
    }
}