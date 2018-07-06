using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BL;
using Persitence;

namespace PL_Console {
    public class MoviesInformation {
        public void ShowInformationMovie () {
            while (true) {
                Console.WriteLine ("============================================================================================");
                Console.WriteLine ("---------------------------- 	Danh sách các phim đang chiếu ------------------------------");
                Console.WriteLine ("--------------------------------------------------------------------------------------------");
                ListMovie ();
                int number;
                MoviesBL movie = new MoviesBL ();
                Console.WriteLine ("0. Quay lại menu chính.");
                Console.WriteLine ("*: Nhập số thứ tự của phim để xem thông tin chi tiết phim.");
                Console.WriteLine ("--------------------------------------------------------------------------------------------");
                Console.Write ("#Chọn :  ");
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.WriteLine ("Giá trị sai vui lòng nhập lại. ");
                        Console.Write ("#Chọn: ");
                    } else if (number < 0 || number > movie.GetMovies ().Count) {
                        Console.WriteLine ($"Giá trị sai vui lòng nhập lại 0 -> {movie.GetMovies ().Count}.");
                        Console.Write ("#Chọn : ");
                    } else {
                        break;
                    }
                }
                if (number == 0) {
                    return;
                }
                Console.Clear ();
                InformationMovieById (number);
            }
        }
        // Lấy ra và hiển thị toàn bộ các phim đang chiếu. 
        public void ListMovie () {
            MoviesBL movie = new MoviesBL ();
            Console.WriteLine ();
            foreach (var item in movie.GetMovies ()) {
                string format = string.Format ($"{item.Movie_id,1}.     {item.Name,-40}   {item.Duration,37} Phút\n       {item.Genre,-20} \n");
                Console.WriteLine (format);
            }
            Console.WriteLine ("--------------------------------------------------------------------------------------------");
        }
        // Lấy ra movie detail  nhờ id.
        public void InformationMovieById (int movie_id) {
            while (true) {
                Console.Clear ();
                MoviesBL movie = new MoviesBL ();
                Movies informatin = movie.getMovieById (movie_id);
                Console.WriteLine ("--------------------------------------------------------------------------------------------");
                Console.WriteLine ($"-----------------  {informatin.Name}");
                Console.WriteLine ("--------------------------------------------------------------------------------------------");
                Console.WriteLine ($" Ngày phát hành          :  {informatin.Release_date.Day}/{informatin.Release_date.Month}/{informatin.Release_date.Year}");
                Console.WriteLine ($" Thể loại                :  {informatin.Genre}");
                Console.WriteLine ($" Diễn viên               :  {informatin.Actor}");
                Console.WriteLine ($" Đạo diễn                :  {informatin.Direction}");
                Console.WriteLine ($" Thời lượng              :  {informatin.Duration} Phút");
                Console.WriteLine ($" Hãng sản xuất           :  {informatin.Producers}");
                Console.Write ($" Nội dung                :  ");
                for (int i = 0; i < informatin.Detail_movie.ToCharArray ().Length; i++) {
                    Console.Write ($"{informatin.Detail_movie[i]}");
                    if (i % 61 == 0 && i != 0) {
                        Console.WriteLine ();
                        Console.Write ($"                           ");
                    }
                }
                Console.WriteLine ($"");
                Console.WriteLine ("--------------------------------------------------------------------------------------------");
                Console.WriteLine ("1. Xem lịch chiếu phim.");
                Console.WriteLine ("\n2. Xem thông tin phim khác.");
                Console.WriteLine ("--------------------------------------------------------------------------------------------");

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
                        Schedule_Infor_By_IDMOVIE_DATE (movie_id);
                        break;
                    case 2:
                        Console.Clear ();
                        return;
                }
            }
        }

        // Show toàn bộ các lịch chiếu của phim, so sách với mốc thời gian hiện tại nếu thời gian đã qua thì k in nữa.
        public static void Schedule_Infor_By_IDMOVIE_DATE (int movie_id) {

            Console.Clear ();
            MoviesBL movie = new MoviesBL ();
            Movies informatin = movie.getMovieById (movie_id);
            ScheduleBL sch = new ScheduleBL ();
            List<DateTime> list = sch.SelectDatetime (movie_id);
            List<string> array = new List<string> ();
            TimeSpan datefortimespan = DateTime.Now.TimeOfDay;
            DateTime comparedatetime = DateTime.Now;
            string comparedatetime1 = comparedatetime.ToString ($"{comparedatetime:dd/MM/yyyy}");
            int dem = 0;
            // Khai báo 2 mảng để in ra thứ nhờ Date.DayOfWeek return về 1 ngày trong tuần.
            string[] arr1 = new string[] { "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy", "Chủ nhật" };
            string[] arr2 = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            Console.WriteLine ("============================================================================================");
            Console.WriteLine ($"----------------- Lịch chiếu phim :   {informatin.Name} ");
            Console.WriteLine ("============================================================================================");
            Console.WriteLine ("Ngày chiếu                |   Thời gian chiếu ( Thời gian bắt đầu - Thời gian kết thúc )");
            Console.WriteLine ("============================================================================================");
            for (int i = 0; i < list.Count; i++) {
                string date = list[i].Date.ToString ($"{list[i].Date:dd/MM/yyyy}");
                // So sánh các mốc thời gian chiếu của phim nếu lớn hơn  hoặc bằng thời gian hiện tại thì show ra GUI. Nếu < thì k show.
                if (list[i].Date > DateTime.Now && comparedatetime1.CompareTo (date) < 0 || comparedatetime1.CompareTo (date) == 0) {
                    string date1 = list[i].Date.ToString ($"{list[i].Date:yyyy-MM-dd}");
                    array.Add (date1);
                    int count = -1;
                    // Lấy ra ngày trong tuần và so sánh để lấy ra index. Chuyền vào arr1 để dịch ngày đó sang tiếng việt.
                    string week = $"{list[i].Date.DayOfWeek}";
                    for (int j = 0; j < arr2.Length; j++) {
                        if (arr2[j] == week) {
                            count = j;
                            break;
                        }
                    }
                    string format = string.Format ($"{dem+1,2}. {list[i].Date:dd/MM/yyyy} {arr1[count],-10} |  ");
                    Console.Write ($"{format}");
                    // Format time HH:MM:SS 
                    List<Schedules> demons = sch.SelectTime (movie_id, array[dem]);
                    string tym_one = string.Format ("{0:D2}:{1:D2}:{2:D2}", datefortimespan.Hours, datefortimespan.Minutes, datefortimespan.Seconds);
                    // Add tất cả thời gian chiếu trong ngày vào.
                    List<string> list1 = new List<string> ();
                    foreach (var item in demons) {
                        string timeText = string.Format ("{0:D2}:{1:D2}:{2:D2}", item.Start_time.Hours, item.Start_time.Minutes, item.Start_time.Seconds);
                        list1.Add (timeText);
                    }
                    for (int j = 0; j < demons.Count; j++) {
                        // Nếu ngày hiện tại bằng ngày chiếu của phim. Thì so sánh các khoảng thời gian chiếu trong ngày với thời gian hiện tại.
                        if (comparedatetime1.CompareTo (date) == 0) {
                            if (tym_one.CompareTo (list1[j]) < 0) {
                                string start = string.Format ("{0:D2}:{1:D2}", demons[j].Start_time.Hours, demons[j].Start_time.Minutes);
                                string end = string.Format ("{0:D2}:{1:D2}", demons[j].End_time.Hours, demons[j].End_time.Minutes);
                                Console.Write ($" {start} - {end} ");
                                if (j == 3) {
                                    Console.WriteLine ();
                                    Console.Write ("                          |                  ");
                                }
                            }
                        } else {
                            string start = string.Format ("{0:D2}:{1:D2}", demons[j].Start_time.Hours, demons[j].Start_time.Minutes);
                            string end = string.Format ("{0:D2}:{1:D2}", demons[j].End_time.Hours, demons[j].End_time.Minutes);
                            Console.Write ($" {start} - {end} ");
                            if (j == 3) {
                                Console.WriteLine ();
                                Console.Write ("                          |                 ");
                            }
                        }
                    }
                    dem++;
                    Console.WriteLine ("");
                    Console.WriteLine ("__________________________|_________________________________________________________________");

                }

            }

            Console.WriteLine ($"");
            Console.WriteLine ("--------------------------------------------------------------------------------------------");
            Console.WriteLine ("1. Đặt vé.");
            Console.WriteLine ("\n2. Trở lại.");
            Console.WriteLine ("\n3. Trở về menu chính.");
            Console.WriteLine ("--------------------------------------------------------------------------------------------");

            Console.Write ("#Chọn : ");
            int number;
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                if (!isINT) {
                    Console.WriteLine ("Giá trị sai vui lòng nhập lại");
                    Console.Write ("#Chọn : ");
                } else if (number < 0 || number > 3) {
                    Console.WriteLine ("Giá trị sai vui lòng nhập lại 1 - 3. ");
                    Console.Write ("#Chọn : ");
                } else {
                    break;
                }
            }
            switch (number) {
                case 1:
                    BookingTicker ticket = new BookingTicker ();
                    ticket.ChooseMovieScheduleForYou (movie_id);
                    break;
                case 2:
                    Console.Clear ();
                    return;
                case 3:
                    CinemaInterface cinema = new CinemaInterface ();
                    cinema.Cinema ();
                    break;

            }
            Console.WriteLine ();

        }
    }

}