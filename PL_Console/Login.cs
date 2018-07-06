using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BL;
using Persitence;

namespace PL_Console {
    class UserInterface {
        public void InterfaceCinema () {
            while (true) {
                Console.Clear ();
                Menu_Interface ();
                LoginCinema login = new LoginCinema ();
                int number;
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại.");
                        Console.Write ("# Chon : ");
                    } else if (number < 0 || number > 2) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại 1 hoặc 2. ");
                        Console.Write ("#Chọn : ");
                    } else {
                        break;
                    }
                }
                switch (number) {
                    case 1:
                        Console.Clear ();
                        login.Login ();
                        Console.Clear ();
                        break;
                    case 2:
                        Environment.Exit (0);
                        Console.Clear ();
                        return;
                }
            }
        }
        public static void Menu_Interface () {
            string[] menu = { "Đăng Nhập.", "Thoát", "#Chọn: " };
            Console.WriteLine ("=============================================================");
            Console.WriteLine ("------------ Thế Giới Phim Trên Đầu Ngón Tay ----------------");
            Console.WriteLine ("=============================================================");
            for (int i = 0; i < 3; i++) {
                if (i != 2) {
                    Console.WriteLine ($"{i+1}. {menu[i]}");
                } else {
                    Console.Write ($"{menu[i]}");
                }
            }
        }
        class LoginCinema {
            public void Login () {
                while (true) {
                    Console.WriteLine ("=============================================================");
                    Console.WriteLine ("-------------------  Đăng Nhập ");
                    Console.WriteLine ("=============================================================");
                    Customer_Bl ad = new Customer_Bl ();
                    string Email;
                    Console.Write ("- Nhập Email          : ");
                    while (true) {
                        Email = Console.ReadLine ();
                        // - Regex Email validation 
                        if (Regex.IsMatch (Email, @"^[^@]+@[^@.]+\.[^@]*\w\w$|^0[0-9]{9,10}$") != true) {
                            Console.WriteLine ("-----\n*^: Bạn đã nhập sai định dạng email. VD: valentinoliv@gmail.com\n------");
                            Console.Write ("- Nhập lại email: ");
                        } else {
                            break;
                        }
                    }
                    string password = "";
                    Console.Write ("- Nhập Mật Khẩu       : ");
                    ConsoleKeyInfo keyInfo;
                    while (true) {
                        do {
                            keyInfo = Console.ReadKey (true);
                            // Skip if Backspace or Enter is Pressed
                            if (keyInfo.Key != ConsoleKey.Backspace && keyInfo.Key != ConsoleKey.Enter) {
                                password += keyInfo.KeyChar;
                                Console.Write ("*");
                            } else {
                                // Remove last charcter if Backspace is Pressed
                                if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0) {
                                    password = password.Substring (0, (password.Length - 1));
                                    Console.Write ("\b \b");
                                }
                            }
                        } while (keyInfo.Key != ConsoleKey.Enter);
                        if (password != "") {
                            break;
                        } else {
                            Console.Clear ();
                            Console.WriteLine ("Bạn chưa nhập mật khẩu, vui lòng nhập lại.");
                            Console.Write ("- Nhập mật khẩu       : ");
                        }
                    }

                    int count = 0;
                    Customer customer = ad.Login (Email, password);
                    if (ad.Login(Email,password).Email == Email && ad.Login(Email,password).Password == password) {
                        count ++;
                    }
                    if (count != 1) {
                        Console.Clear ();
                        Console.WriteLine ("-------------------------------------------------------------");
                        Console.WriteLine ("  *^:   Email hoặc mật khẩu của bạn chưa chính xác.");
                        Console.WriteLine ("-------------------------------------------------------------");
                        while (true) {
                            Console.WriteLine ("1. Thử lại.");
                            Console.WriteLine ("2. Thoát");
                            Console.Write ("#Chọn : ");
                            int number;
                            while (true) {
                                bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                                if (!isINT) {
                                    Console.WriteLine ("Giá trị sai vui lòng nhập lại");
                                    Console.Write ("#Chọn : ");
                                } else if (number < 0 || number > 2) {
                                    Console.WriteLine ("Giá trị sai vui lòng nhập lại 1 - 2. ");
                                    Console.Write ("#Chọn : ");
                                } else {
                                    break;
                                }
                            }
                            switch (number) {
                                case 1:
                                    Console.Clear ();
                                    break;
                                case 2:
                                    Console.Clear ();
                                    return;
                            }
                            if (number == 1) {
                                break;
                            }
                        }
                    } else {
                        CinemaInterface cinema = new CinemaInterface ();
                        cinema.Cinema ();
                        Console.WriteLine ("-------------------------------------------------------------");
                        return;
                    }
                }

            }
        }
    }
}