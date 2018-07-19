using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BL;
using Persitence;

namespace PL_Console {
    class UserInterface {
        public static void InterfaceCinema () {
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
                    Console.WriteLine ("-------------------------------------------------------------");
                    Console.Write ($"{menu[i]}");
                }
            }
        }
        public class LoginCinema {
            private static string Email;
            private static string password;
            public void Login () {
                while (true) {
                    Console.WriteLine ("=============================================================");
                    Console.WriteLine ("-------------------  Đăng Nhập ");
                    Console.WriteLine ("=============================================================");
                    // string Email;
                    // string password;
                    while (true) {
                        Console.Write ("- Nhập Email          : ");
                        Email = Console.ReadLine ();
                        Console.Write ("- Nhập Mật Khẩu       : ");
                        while (true) {
                            password = "";
                            ConsoleKeyInfo keyInfo;
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
                            break;
                        }
                        if (CheckNotSpecialCharacters (Email, password) == true) {
                            break;
                        } else {
                            CheckLoginSuccessOrFailure (0, Email, password);
                        }
                    }
                    int count = 0;
                    Customer_Bl ad = new Customer_Bl ();
                    if (ad.Login (Email, password) != null) {
                        count++;
                    }
                    CheckLoginSuccessOrFailure (count, Email, password);
                }

            }
            private static bool CheckNotSpecialCharacters (string Email, string password) {
                if (Regex.IsMatch (Email, @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$") != true ||
                    Email == "" || Regex.IsMatch (password, @"^[-.@_a-zA-Z0-9 ]+$") != true || password == "") {
                    return false;
                }
                return true;
            }

            private static void CheckLoginSuccessOrFailure (int count, string Email, string password) {
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
                                InterfaceCinema ();
                                break;
                        }
                        if (number == 1) {
                            break;
                        }
                    }
                } else {
                    // Customer_Bl cus = new Customer_Bl ();
                    // GetCustomerID (cus.Login (Email, password).Customer_id);
                    CinemaInterface.Cinema ();
                    Console.WriteLine ("-------------------------------------------------------------");
                    return;
                }
            }
            public static Customer GetCustomer () {
                Customer_Bl cus = new Customer_Bl ();
                Customer customer = cus.Login (Email, password);
                return customer;
            }
        }
    }
}