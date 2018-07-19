using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DAL;
using Persitence;

namespace BL {
    public class Customer_Bl {
        // private Custome_DAL idal = new Custome_DAL ();
        public Customer Login (String email, String password) {
            Custome_DAL idal = new Custome_DAL ();
            string regexEmail = @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$";
            string regexPassword = @"^[-.@_a-zA-Z0-9 ]+$";
            if (Regex.IsMatch (email, regexEmail) != true || email == "" || Regex.IsMatch (password, regexPassword) != true || password == "") {
                return null;
            }
            return idal.Login (email, password);
        }
    }
}