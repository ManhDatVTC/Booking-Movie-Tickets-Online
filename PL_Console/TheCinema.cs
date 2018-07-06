using System;
using System.Collections.Generic;
using BL;
using Persitence;

namespace PL_Console {
    class CinemaInterface {
        public void Cinema () {
            while (true) {

                Console.Clear ();
                MenuCinema ();
                int number;
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại. ");
                        Console.Write ("#Chọn: ");
                    } else if (number < 0 || number > 4) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại từ 1 - 4.");
                        Console.Write ("#Chọn : ");
                    } else {
                        break;
                    }
                }
                /*
                 TryParse phương pháp không ném một ngoại lệ nếu chuyển đổi thất bại. Nó loại bỏ sự cần thiết phải sử dụng xử lý ngoại lệ
                 để kiểm tra cho một Format Exception trong trường hợp không hợp lệ và không thể được thành công phân tích cú pháp.
                */
                Console.WriteLine ("=============================================================");
                switch (number) {
                    case 1:
                        Console.Clear ();
                        MoviesInformation infomovie = new MoviesInformation ();
                        infomovie.ShowInformationMovie ();
                        break;
                    case 2:
                        BookingTicker ticker = new BookingTicker ();
                        ticker.MenuBookingTicker ();
                        Console.Clear ();
                        break;
                    case 3:
                        Information info = new Information ();
                        // info.Information_acc ();
                        Console.ReadLine ();
                        break;
                    case 4:
                        UserInterface user = new UserInterface ();
                        user.InterfaceCinema ();
                        return;
                }
            }
        }
        public static void MenuCinema () {
            string[] menu = { "Thông tin phim.", "Đặt vé.", "Thông tin cá nhân.", "Đăng Xuất.", "#Chọn: " };
            Console.WriteLine ("=============================================================");
            Console.WriteLine ("------------- Chào Mừng Bạn Đến Với Thế Giới Phim -----------");
            Console.WriteLine ("=============================================================");
            for (int i = 0; i < 5; i++) {
                if (i != 4) {
                    Console.WriteLine ($"{i+1}. {menu[i]}");
                } else {
                    Console.Write ($"{menu[i]}");
                }
            }
        }
    }
}