using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using BL;
using DAL;
using Persitence;

namespace PL_Console {
    class Program {
        static void Main (string[] args) {
            string[] a = {
                @"                                                                 
                THẾ GIỚI PHIM TRÊN ĐẦU NGÓN TAY ",
                @"
                Chào mừng bạn !",
            };
            for (int i = 0; i < a.Length; i++) {
                Console.Write (a[i]);
                Thread.Sleep (1000);
            }
            string[] ar = { "THẾ", " GIỚI", " PHIM", " TRÊN", " ĐẦU", " NGÓN", " TAY", "\nChào mừng bạn !" };
            for (int i = 0; i < ar.Length; i++) {
                Console.Write (ar[i]);
                Thread.Sleep (200);
            }
            Thread.Sleep (1000);
            UserInterface.InterfaceCinema ();
        }
    }
}