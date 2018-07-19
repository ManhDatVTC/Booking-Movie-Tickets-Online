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
                        Console.Write ("Nhập <Enter> để trở lại.");
                        Console.ReadLine ();
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
            List<Reservation> list = reser.GetReservationByCustomerId (UserInterface.LoginCinema.GetCustomer ().Customer_id);
            if (list == null ) {
                Console.WriteLine ("Bạn chưa có giao dịch nào gần đây ! Đặt vé ngay bạn nhé.");
                Console.WriteLine ("Nhập <Enter> để trở lại.");
                return;
            } else {
                Console.Clear ();
                Console.WriteLine ("                         Vé Đã Mua         ");
                Console.WriteLine ("                  _________________________");
                Console.WriteLine ("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                Console.WriteLine ("STT   | Tên Phim                 | Ngày Chiếu               | Giá Tiền ");

                for (int i = 0; i < list.Count; i++) {
                    ScheduleBL sch = new ScheduleBL ();
                    Schedules schedule = sch.GetScheduleByIdSchedule (list[i].Schedule_id);
                    MoviesBL movie = new MoviesBL ();
                    Movies informatin = movie.getMovieById (schedule.Movie_id);
                    Random random = new Random ();
                    int randomNumber = random.Next (0, 100000000);
                    string start1 = string.Format ("{0:D2}:{1:D2}", schedule.Start_time.Hours, schedule.Start_time.Minutes);
                    string end1 = string.Format ("{0:D2}:{1:D2}", schedule.End_time.Hours, schedule.End_time.Minutes);
                    string time = schedule.Show_date.ToString ($"{schedule.Show_date:dd/MM/yyyy}");
                    string datetime = time + " " + start1 + " - " + end1;
                    string format = string.Format ($"{i+1,-6}| {informatin.Name,-25}| {datetime,-25}| {list[i].Price,-9}");
                    Console.WriteLine (format);
                }
                Console.WriteLine ("__________________________________________________________________________");

                Console.WriteLine ("\n\n\n0. Quay lại.");
                Console.WriteLine ("*: Nhập số thứ tự để xem chi tiết vé .");
                Console.WriteLine ("---------------------------------------------------------------------------");
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
        public void CustomerInformation () {
            Customer cus = UserInterface.LoginCinema.GetCustomer ();
            Console.WriteLine ("Thông tin đăng nhập.");
            Console.WriteLine ("------------------------------------------------------");
            Console.WriteLine ($"{cus.Name}");
            Console.WriteLine ("......................................................");
            Console.WriteLine ($"     Email              :   {cus.Email}");
            Console.WriteLine ($"     Số tài khoản       :   {cus.Account_bank}");
            Console.WriteLine ($"     Tài khoản          :   {cus.User_name}");
            Console.WriteLine ($"     Ngày sinh          :   {cus.Birthday.Day}/{cus.Birthday.Month}/{cus.Birthday.Year}");
            Console.WriteLine ($"     Điện thoại         :   {cus.Phone}");
            Console.WriteLine ($"     Địa chỉ            :   {cus.Address}");
            Console.WriteLine ($"     Giới tính          :   {cus.Sex}");
            Console.WriteLine ("......................................................");
            Console.WriteLine ("------------------------------------------------------");
        }
    }
}