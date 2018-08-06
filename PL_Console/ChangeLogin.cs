using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using BL;
using Persitence;

namespace PL_Console {
    public class Login {
        // static public string _linkDB { get; set; }
        static Login instance = new Login ();
        private Login () {

            _valueUser = "";
            _valuePass = "";
            _name = "";
            _email = "";
            _phone = "";
            _address = "";
            _count = 0;
        }
        public static string _valueUser { get; set; }
        public static string _valuePass { get; set; }
        public static string _name { get; set; }
        public static string _email { get; set; }
        public static string _phone { get; set; }
        public static string _address { get; set; }
        public static int _count { get; set; }

        public static void Show () {

            ConsoleKeyInfo key;
            const string flag = "Để thay đổi thông tin nhấn <Enter> hoặc nhấn <ESC> để huỷ và thoát.";
            int spaceInput = 0;

            int choice = 2;
            string customer = "";
            string pass = "";
            string name = "";
            string email = "";
            string phone = "";
            string address = "";
            bool checs1 = true;
            bool checs2 = true;
            bool checs3 = true;
            bool checs4 = true;
            bool checs5 = true;
            bool checs6 = true;
            while (true) {
                if (choice == 2 || choice == 3 || choice == 4 || choice == 5 || choice == 6 || choice == 7)
                    Console.CursorVisible = true;
                else
                    Console.CursorVisible = false;

                if (choice == 2) spaceInput = _valueUser.Length;
                if (choice == 3) spaceInput = _valuePass.Length;
                if (choice == 4) spaceInput = _name.Length;
                if (choice == 5) spaceInput = _email.Length;
                if (choice == 6) spaceInput = _phone.Length;
                if (choice == 7) spaceInput = _address.Length;

                Console.Clear ();
                Console.WriteLine ("--------------------------------- Thay đổi thông tin cá nhân ---------------------------------------\n");
                Console.Write ("Thay đổi tên đăng nhập    : ");
                string.Format ($"{_valueUser,-25}");
                Console.Write (string.Format ($"{_valueUser,-25}"));
                check (customer);
                Console.WriteLine ();
                Console.Write ("Thay đổi mật khẩu         : ");
                Console.Write (string.Format ($"{new string ('*', _valuePass.Length),-25}"));
                check (pass);
                Console.WriteLine ();
                Console.Write ("Thay đổi họ tên           : ");
                Console.Write (string.Format ($"{_name,-25}"));
                check (name);
                Console.WriteLine ();
                Console.Write ("Thay đổi Email            : ");
                Console.Write (string.Format ($"{_email,-25}"));
                check (email);
                Console.WriteLine ();
                Console.Write ("Thay đổi số điện thoại    : ");
                Console.Write (string.Format ($"{_phone,-25}"));
                check (phone);
                Console.WriteLine ();
                Console.Write ("Thay đổi địa chỉ          : ");
                Console.Write (string.Format ($"{_address,-25}"));
                check (address);
                Console.WriteLine ();
                Console.WriteLine ();
                Console.Write ("Thay đổi thông tin       ");
                if (choice == 9) Console.Write ("" + flag);
                Console.WriteLine ("\n \n \n \n---------------------------------------------------------------------------------------------------");
                Console.WriteLine ("---------------------------------------------------------------------------------------------------");
                Console.SetCursorPosition (28 + spaceInput, choice);

                //up down
                key = Console.ReadKey ();
                if (key.Key == ConsoleKey.UpArrow)
                    choice--;
                else if (key.Key == ConsoleKey.DownArrow) {
                    if (choice == 2) {
                        if (Regex.IsMatch (_valueUser, @"^[-.@_a-zA-Z0-9áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$") != true) {
                            customer = "Tên đang nhập không được chức ký tự đặc biệt.";
                            checs1 = false;
                        } else { customer = "√"; checs1 = true; }
                    }
                    if (choice == 3) {
                        if (Regex.IsMatch (_valuePass, @"^[-.@_a-zA-Z0-9 ]+$") != true || _valuePass.Trim () == "") {
                            pass = "Mật khẩu không được chứa ký tự đặc biệt.";
                            checs2 = false;
                        } else { pass = "√"; checs2 = true; }
                    }
                    if (choice == 4) {
                        if (Regex.IsMatch (_name, @"^[-.@_a-zA-ZáàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$") != true || _name.Trim () == "") {
                            name = "Họ tên không được chứa ký tự đắc biệt và không chứa số.";
                            checs3 = false;
                        } else { name = "√"; checs3 = true; }
                    }
                    if (choice == 5) {
                        if (Regex.IsMatch (_email, @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$") != true) {
                            email = "Bạn nhập sai định dạng Email. Vui lòng nhập lại. VD valen@gmail.com";
                            checs4 = false;
                        } else { email = "√"; checs4 = true; }
                    }
                    if (choice == 6) {
                        if (Regex.IsMatch (_phone, @"^(01[2689]|09)[0-9]{8}$") != true) {
                            phone = "Bạn đã nhập sai số điện thoại. VD 0988968289";
                            checs5 = false;
                        } else { phone = "√"; checs5 = true; }
                    }
                    if (choice == 7) {
                        if (Regex.IsMatch (_address, @"^[-.@_a-zA-Z0-9áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$") != true) {
                            address = "Địa chỉ không được chứa ký tự đặc biệt và không được trống.";
                            checs6 = false;
                        } else { address = "√"; checs6 = true; }
                    }
                    choice++;

                } else if (key.Key == ConsoleKey.Enter) {
                    if (choice == 2) {
                        if (Regex.IsMatch (_valueUser, @"^[-.@_a-zA-Z0-9áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$") != true) {
                            customer = "Tên đang nhập không được chức ký tự đặc biệt.";
                            checs1 = false;
                        } else { customer = "√"; checs1 = true; }
                    }
                    if (choice == 3) {
                        if (Regex.IsMatch (_valuePass, @"^[-.@_a-zA-Z0-9 ]+$") != true || _valuePass.Trim () == "") {
                            pass = "Mật khẩu không được chứa ký tự đặc biệt.";
                            checs2 = false;
                        } else { pass = "√"; checs2 = true; }
                    }
                    if (choice == 4) {
                        if (Regex.IsMatch (_name, @"^[-.@_a-zA-Z0-9áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$") != true || _name.Trim () == "") {
                            name = "Tên không được chứa ký tự đắc biệt và không chứa số.";
                            checs3 = false;
                        } else { name = "√"; checs3 = true; }
                    }
                    if (choice == 5) {
                        if (Regex.IsMatch (_email, @"^[^<>()[\]\\,;:'\%#^\s@\$&!@]+@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z0-9]+\.)+[a-zA-Z]{2,}))$") != true) {
                            email = "Nhập đúng định dạng Email. VD valen@gmail.com";
                            checs4 = false;
                        } else { email = "√"; checs4 = true; }
                    }
                    if (choice == 6) {
                        if (Regex.IsMatch (_phone, @"^(01[2689]|09)[0-9]{8}$") != true) {
                            phone = "Bạn đã nhập sai số điện thoại. VD 0988968289";
                            checs5 = false;
                        } else { phone = "√"; checs5 = true; }
                    }
                    if (choice == 7) {
                        if (Regex.IsMatch (_address, @"^[-.@_a-zA-Z0-9áàảãạâấầẩẫậăắằẳẵặđéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵÁÀẢÃẠÂẤẦẨẪẬĂẮẰẲẴẶĐÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴ ]+$") != true) {
                            address = "Địa chỉ không được chứa ký tự đặc biệt và không được trống.";
                            checs6 = false;
                        } else { address = "√"; checs6 = true; }
                    }
                    if (choice == 9) {
                        if (checs1 == true && checs2 == true && checs3 == true && checs4 == true && checs5 == true && checs6 == true) {
                            if (_count == 0) {
                                if (MessageLogin (_valueUser, _valuePass, _name, _email, _phone, _address, _count) == true) {
                                    _count++;
                                    Console.WriteLine ("Bạn đã thay đổi thông tin tài khoản thành công !");
                                    Console.WriteLine ("Nhấn Enter để trở lại.");
                                    Console.ReadLine ();
                                    return;
                                } else {
                                    Console.WriteLine ("Thay đổi thông tin không thành công do lỗi hệ thống ! ");
                                    Console.ReadLine ();
                                }
                            } else {
                                Console.WriteLine ("Bạn không thể thay đổi tài khoản liện tục, vui lòng thoát ra vào lại !");
                                Console.WriteLine ("Enter để trở lại !");
                                Console.ReadLine ();
                                return;
                            }

                        } else {
                            Console.WriteLine ("Điền đúng thông tin thì mới thay đổi được, vui lòng nhập lại hoặc thoát.");
                            key = Console.ReadKey ();
                            if (key.Key == ConsoleKey.Escape) { Console.Clear (); return; }
                        }

                        // break;
                    }
                    choice++;
                } else if (key.Key == ConsoleKey.Escape) { Console.Clear (); return; } else if (key.Key == ConsoleKey.Backspace) {
                    if (choice == 2 && _valueUser.Length > 0)
                        _valueUser = _valueUser.Remove (_valueUser.Length - 1);
                    if (choice == 3 && _valuePass.Length > 0)
                        _valuePass = _valuePass.Remove (_valuePass.Length - 1);
                    if (choice == 4 && _name.Length > 0)
                        _name = _name.Remove (_name.Length - 1);
                    if (choice == 5 && _email.Length > 0)
                        _email = _email.Remove (_email.Length - 1);
                    if (choice == 6 && _phone.Length > 0)
                        _phone = _phone.Remove (_phone.Length - 1);
                    if (choice == 7 && _address.Length > 0)
                        _address = _address.Remove (_address.Length - 1);
                } else {
                    if (choice == 2)
                        _valueUser += key.KeyChar.ToString () [0];
                    if (choice == 3)
                        _valuePass += key.KeyChar.ToString () [0];
                    if (choice == 4)
                        _name += key.KeyChar.ToString () [0];
                    if (choice == 5)
                        _email += key.KeyChar.ToString () [0];
                    if (choice == 6)
                        _phone += key.KeyChar.ToString () [0];
                    if (choice == 7)
                        _address += key.KeyChar.ToString () [0];
                }
                if (choice < 2) choice = 2;
                else if (choice > 9) choice = 9;
            }
        }
        public static void check (string arr) {
            if (arr == "√") {
                Console.ForegroundColor = System.ConsoleColor.Green;
                Console.Write ("√");
                Console.ResetColor ();
            } else if (arr == "") {
                Console.Write ("");
            } else {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = System.ConsoleColor.Black;
                Console.Write ("X");
                Console.ResetColor ();
                Console.Write ($": {arr}");
            }
        }

        static bool MessageLogin (string _valueUser, string _valuePass, string _name, string _email, string _phone, string _address, int _count) {
            Customer cus = UserInterface.LoginCinema.GetCustomer ();
            Customer customer = new Customer ();
            customer.User_name = _valueUser;
            customer.Password = _valuePass;
            customer.Name = _name;
            customer.Email = _email;
            customer.Phone = _phone;
            customer.Address = _address;
            customer.Customer_id = cus.Customer_id;
            Customer_Bl cuBL = new Customer_Bl ();
            return cuBL.ChangLogin (customer);;
        }
        public static Customer GetCustomer1 () {
            Customer_Bl cus = new Customer_Bl ();
            Customer customer = cus.Login (_email, _valuePass);
            return customer;
        }

    }
}