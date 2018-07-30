using System;
using System.Collections.Generic;
using BL;
using Persitence;
namespace PL_Console {
    class Information {
        public void Information_acc () {
            while (true) {
                Console.Clear ();
                Menu_Interface ();
                int number;
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại.");
                        Console.Write ("# Chon : ");
                    } else if (number < 0 || number > 3) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại 1 hoặc 3. ");
                        Console.Write ("#Chọn : ");
                    } else {
                        break;
                    }
                }
                switch (number) {
                    case 1:
                        Console.Clear ();
                        HistoryBookingTicket ();
                        break;
                    case 2:
                        Console.Clear ();
                        CustomerInformation ();
                        break;
                    case 3:
                        return;
                }
            }

        }
        public static void Menu_Interface () {
            string[] menu = { "Lịch sử đặt vé.", "Quản lý thông tin cá nhân.", "Trở lại menu chính", "#Chọn: " };
            Console.WriteLine ("=============================================================");
            Console.WriteLine ("--------------------- Rạp Chiếu Của Tôi ---------------------");
            Console.WriteLine ("=============================================================");
            for (int i = 0; i < 4; i++) {
                if (i != 3) {
                    Console.WriteLine ($"{i+1}. {menu[i]}");
                } else {
                    Console.Write ($"{menu[i]}");
                }
            }
        }
        public void HistoryBookingTicket () {
            ReservationBL reser = new ReservationBL ();
            // List<Reservation> list = null;
            try {
                List<Reservation> list = reser.GetReservationByCustomerId (UserInterface.LoginCinema.GetCustomer ().Customer_id);
                if (list.Count == 0 || list == null) {
                    Console.WriteLine ("Bạn chưa có giao dịch nào với Rạp Thế Giới ! Đặt vé ngay bạn yêu nhé.");
                    Console.WriteLine ("Nhập <Enter> để trở lại.");
                    Console.ReadLine ();
                    return;
                } else {
                    Console.Clear ();
                    Console.WriteLine ("                              Thông tin vé đặt trước  ");
                    Console.WriteLine ("                             _________________________");
                    Console.WriteLine ("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine ("STT   | Tên Phim                 | Ngày Chiếu               | Số Lượng (V-T)    |Giá Tiền ");
                    Console.WriteLine ("_____________________________________________________________________________________________");

                    for (int i = 0; i < list.Count; i++) {
                        ScheduleBL sch = new ScheduleBL ();
                        Schedules schedule = sch.GetScheduleByIdSchedule (list[i].Schedule_id);
                        MoviesBL movie = new MoviesBL ();
                        Movies informatin = movie.getMovieById (schedule.Movie_id);
                        string start1 = string.Format ("{0:D2}:{1:D2}", schedule.Start_time.Hours, schedule.Start_time.Minutes);
                        string end1 = string.Format ("{0:D2}:{1:D2}", schedule.End_time.Hours, schedule.End_time.Minutes);
                        string time = schedule.Show_date.ToString ($"{schedule.Show_date:dd/MM/yyyy}");
                        string datetime = time + " " + start1 + " - " + end1;
                        string[] a = list[i].Seats.Trim ().Split (" ");

                        string format = string.Format ($"{i+1,-6}| {informatin.Name,-25}| {datetime,-25}| {CheckCount(a),-18}|{ChoiceMapSeats.Tien (list[i].Price.ToString ()),-9} VND");
                        Console.WriteLine (format);
                        Console.WriteLine ("");
                    }
                    Console.WriteLine ("_____________________________________________________________________________________________");
                    Console.WriteLine ("\n\n\n0. Quay lại.");
                    Console.WriteLine ("*: Nhập số thứ tự để xem chi tiết vé .");
                    Console.WriteLine ("---------------------------------------------------------------------------------------------");
                    Console.Write ("#Chọn :  ");
                    int number;
                    while (true) {
                        bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                        if (!isINT) {
                            Console.WriteLine ("Giá trị sai vui lòng nhập lại. ");
                            Console.Write ("#Chọn: ");
                        } else if (number < 0 || number > list.Count) {
                            Console.WriteLine ($"Giá trị sai vui lòng nhập lại 0 -> {list.Count}.");
                            Console.Write ("#Chọn : ");
                        } else {
                            break;
                        }
                    }
                    if (number == 0) {
                        return;
                    }
                    ChoiceMapSeats.InformationTickets (list[number - 1], 1);
                    Console.Write ("                   Nhập <Enter> để trở lại.");
                    Console.ReadLine ();
                    return;
                }
            } catch (System.Exception) {
                List<Reservation> list = reser.GetReservationByCustomerId (Login.GetCustomer1 ().Customer_id);
                if (list.Count == 0 || list == null) {
                    Console.WriteLine ("Bạn chưa có giao dịch nào với Rạp Thế Giới ! Đặt vé ngay bạn yêu nhé.");
                    Console.WriteLine ("Nhập <Enter> để trở lại.");
                    Console.ReadLine ();
                    return;
                } else {
                    Console.Clear ();
                    Console.WriteLine ("                              Thông tin vé đặt trước  ");
                    Console.WriteLine ("                             _________________________");
                    Console.WriteLine ("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                    Console.WriteLine ("STT   | Tên Phim                 | Ngày Chiếu               | Số Lượng (V-T)    |Giá Tiền ");
                    Console.WriteLine ("_____________________________________________________________________________________________");

                    for (int i = 0; i < list.Count; i++) {
                        ScheduleBL sch = new ScheduleBL ();
                        Schedules schedule = sch.GetScheduleByIdSchedule (list[i].Schedule_id);
                        MoviesBL movie = new MoviesBL ();
                        Movies informatin = movie.getMovieById (schedule.Movie_id);
                        string start1 = string.Format ("{0:D2}:{1:D2}", schedule.Start_time.Hours, schedule.Start_time.Minutes);
                        string end1 = string.Format ("{0:D2}:{1:D2}", schedule.End_time.Hours, schedule.End_time.Minutes);
                        string time = schedule.Show_date.ToString ($"{schedule.Show_date:dd/MM/yyyy}");
                        string datetime = time + " " + start1 + " - " + end1;
                        string[] a = list[i].Seats.Trim ().Split (" ");

                        string format = string.Format ($"{i+1,-6}| {informatin.Name,-25}| {datetime,-25}| {CheckCount(a),-18}|{ChoiceMapSeats.Tien (list[i].Price.ToString ()),-9} VND");
                        Console.WriteLine (format);
                        Console.WriteLine ("");
                    }
                    Console.WriteLine ("_____________________________________________________________________________________________");
                    Console.WriteLine ("\n\n\n0. Quay lại.");
                    Console.WriteLine ("*: Nhập số thứ tự để xem chi tiết vé .");
                    Console.WriteLine ("---------------------------------------------------------------------------------------------");
                    Console.Write ("#Chọn :  ");
                    int number;
                    while (true) {
                        bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                        if (!isINT) {
                            Console.WriteLine ("Giá trị sai vui lòng nhập lại. ");
                            Console.Write ("#Chọn: ");
                        } else if (number < 0 || number > list.Count) {
                            Console.WriteLine ($"Giá trị sai vui lòng nhập lại 0 -> {list.Count}.");
                            Console.Write ("#Chọn : ");
                        } else {
                            break;
                        }
                    }
                    if (number == 0) {
                        return;
                    }
                    ChoiceMapSeats.InformationTickets (list[number - 1], 1);
                    Console.Write ("                   Nhập <Enter> để trở lại.");
                    Console.ReadLine ();
                    return;
                }
            }
        }
        public static string CheckCount (string[] array) {
            PriceSeatsBl price = new PriceSeatsBl ();
            string seat = "D3,D4,D5,D6,D7,D8,E3,E4,E5,E6,E7,E8,F3,F4,F5,F6,F7,F8,G3,G4,G5,G6,G7,G8,H3,H4,H5,H6,H7,H8,I3,I4,I5,I6,I7,I8,J3,J4,J5,J6,J7,J8,K3,K4,K5,K6,K7,K8";
            string[] mapArr = seat.Split (",");
            int V = 0;
            int T = 0;
            for (int i = 0; i < array.Length; i++) {
                int Count = 0;
                for (int k = 0; k < mapArr.Length; k++) {
                    if (mapArr[k] == array[i]) {
                        V++;
                        Count++;
                        break;
                    }
                }
                if (Count == 0) T++;
            }
            return $"{V}V - {T}T    ";

        }
        public void CustomerInformation () {
            // Customer cus = UserInterface.LoginCinema.GetCustomer ();
            try {
                Customer cus = UserInterface.LoginCinema.GetCustomer ();
                Console.WriteLine ("Thông tin đăng nhập.");
                Console.WriteLine ("------------------------------------------------------");
                Console.WriteLine ($"{cus.Name}");
                Console.WriteLine ("......................................................");
                Console.WriteLine ($"     Email              :   {cus.Email}");
                Console.WriteLine ($"     Tài khoản          :   {cus.User_name}");
                Console.WriteLine ($"     Ngày sinh          :   {cus.Birthday.Day}/{cus.Birthday.Month}/{cus.Birthday.Year}");
                Console.WriteLine ($"     Điện thoại         :   {cus.Phone}");
                Console.WriteLine ($"     Địa chỉ            :   {cus.Address}");
                Console.WriteLine ($"     Giới tính          :   {cus.Sex}");
                Console.WriteLine ("......................................................");
                Console.WriteLine ("------------------------------------------------------");
                Console.WriteLine ("1. Thay đổi thông tin");
                Console.WriteLine ("2. Trở lại");
                Console.WriteLine ("------------------------------------------------------");
                Console.Write ("#Chọn :  ");
                int number;
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại.");
                        Console.Write ("# Chon : ");
                    } else if (number < 0 || number > 3) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại 1 hoặc 3. ");
                        Console.Write ("#Chọn : ");
                    } else {
                        break;
                    }
                }
                switch (number) {
                    case 1:
                        Console.Clear ();
                        Login.Show ();
                        break;
                    case 2:
                        return;
                }

            } catch (System.Exception) {
                Console.Clear ();
                Customer cus = Login.GetCustomer1 ();
                Console.WriteLine ("Thông tin đăng nhập.");
                Console.WriteLine ("------------------------------------------------------");
                Console.WriteLine ($"{cus.Name}");
                Console.WriteLine ("......................................................");
                Console.WriteLine ($"     Email              :   {cus.Email}");
                Console.WriteLine ($"     Tài khoản          :   {cus.User_name}");
                Console.WriteLine ($"     Ngày sinh          :   {cus.Birthday.Day}/{cus.Birthday.Month}/{cus.Birthday.Year}");
                Console.WriteLine ($"     Điện thoại         :   {cus.Phone}");
                Console.WriteLine ($"     Địa chỉ            :   {cus.Address}");
                Console.WriteLine ($"     Giới tính          :   {cus.Sex}");
                Console.WriteLine ("......................................................");
                Console.WriteLine ("------------------------------------------------------");
                Console.WriteLine ("1. Thay đổi thông tin");
                Console.WriteLine ("2. Trở lại");
                Console.WriteLine ("------------------------------------------------------");
                Console.Write ("#Chọn :  ");
                int number;
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại.");
                        Console.Write ("# Chon : ");
                    } else if (number < 0 || number > 3) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại 1 hoặc 3. ");
                        Console.Write ("#Chọn : ");
                    } else {
                        break;
                    }
                }
                switch (number) {
                    case 1:
                        Console.Clear ();
                        Login.Show ();
                        break;
                    case 2:
                        return;
                }
            }

        }

    }
}