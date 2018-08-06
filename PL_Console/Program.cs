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
            Console.Clear ();
            List<string> chars = new List<string> () {
                ".",
                ".",
                ".",
                "T",
                "H",
                "Ế",
                " ",
                "G",
                "I",
                "Ớ",
                "I",
                " ",
                "P",
                "H",
                "I",
                "M",
                " ",
                "T",
                "R",
                "Ê",
                "N",
                " ",
                "Đ",
                "Ầ",
                "U",
                " ",
                "N",
                "G",
                "Ó",
                "N",
                " ",
                "T",
                "A",
                "Y"
            };
            string[] a = {
                "...",
                "Xin chào bạn!",
            };
            for (int i = 0; i < chars.Count; i++) {
                Console.Write (chars[i]);
                if (i == 2) {
                    Console.WriteLine ();

                }
                Console.ResetColor ();
                Thread.Sleep (50);
            }
            Console.WriteLine ();
            for (int i = 0; i < a.Length; i++) {
                Console.WriteLine (a[i]);
                Thread.Sleep (100);
            }
            Thread.Sleep (1000);
            UserInterface.InterfaceCinema ();
        }
    }
}