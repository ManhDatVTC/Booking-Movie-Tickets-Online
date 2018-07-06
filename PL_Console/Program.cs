using System;
using System.Collections.Generic;
using System.IO;
using BL;
using DAL;
using Persitence;

namespace PL_Console {
    class Program {
        // static string link;
        static void Main (string[] args) {

            UserInterface login = new UserInterface ();
            login.InterfaceCinema ();
            // Read ("ConnectionString.txt");
            // using (DBHelper.OpenConnection (link)) {

            // }

        }
        // static void Read (string path) {
        //     FileStream fs = new FileStream (path, FileMode.Open);
        //     using (StreamReader reader = new StreamReader (fs)) {
        //         link = reader.ReadLine ();
        //     }
        // }
    }
}