using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BL;
using Persitence;

namespace PL_Console {
    class BookingTicker {
        public void MenuBookingTicker () {
            while (true) {
                Console.Clear ();
                // MenuBookingTicker menu cho đặt vé.
                MoviesBL movie = new MoviesBL ();
                Console.WriteLine ("============================================================================================");
                Console.WriteLine ("----------------------------  Chọn Phim Bạn Muốn Xem  --------------------------------------");
                Console.WriteLine ("--------------------------------------------------------------------------------------------");
                Console.WriteLine ();
                // Show ra tất cả các phim đang có trong danh sách.
                foreach (var item in movie.GetMovies ()) {
                    string format = string.Format ($"{item.Movie_id,1}.     {item.Name,-40}   {item.Duration,37} Phút\n       {item.Genre,-20} \n");
                    Console.WriteLine (format);
                }
                Console.WriteLine ("--------------------------------------------------------------------------------------------");
                int number;
                Console.WriteLine ("0. Quay lại menu chính.");
                Console.WriteLine ("*: Nhập số thứ tự để chọn phim.");
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
                ChooseMovieScheduleForYou (number);
                Console.ReadLine ();
            }
        }
        public void ChooseMovieScheduleForYou (int movie_id) {
            Console.Clear ();
            MoviesBL movie = new MoviesBL ();
            ScheduleBL schechule = new ScheduleBL ();
            RoomBL room = new RoomBL ();
            // information . Lấy ra phim nhờ id movie
            Movies informatin = movie.getMovieById (movie_id);
            // list lấy ra tất cả lịch chiếu của phim.
            List<DateTime> list = schechule.SelectDatetime (movie_id);
            TimeSpan datefortimespan = DateTime.Now.TimeOfDay;
            DateTime comparedatetime = DateTime.Now;
            string comparedatetime1 = comparedatetime.ToString ($"{comparedatetime:dd/MM/yyyy}");
            // Array này lấy ra ngày để connect tới database format by yyyy-MM-dd.
            List<string> array = new List<string> ();
            // Array1 này. Lưu lại thời gian để so sách với thời gian hiện tại.
            List<string> array1 = new List<string> ();

            int dem = 0;
            string[] arr1 = new string[] { "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy", "Chủ nhật" };
            string[] arr2 = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            Console.WriteLine ("============================================================================================");
            Console.WriteLine ($"----------------- Chọn Ngày Chiếu Của Phim  :  {informatin.Name} ");
            Console.WriteLine ("============================================================================================");
            for (int i = 0; i < list.Count; i++) {
                string date = list[i].Date.ToString ($"{list[i].Date:dd/MM/yyyy}");
                if (list[i].Date > DateTime.Now && comparedatetime1.CompareTo (date) < 0 || comparedatetime1.CompareTo (date) == 0) {
                    string date1 = list[i].Date.ToString ($"{list[i].Date:yyyy-MM-dd}");
                    array.Add (date1);
                    array1.Add (date);
                    int count = -1;
                    string week = $"{list[i].Date.DayOfWeek}";
                    for (int j = 0; j < arr2.Length; j++) {
                        if (arr2[j] == week) {
                            count = j;
                            break;
                        }
                    }
                    string format = string.Format ($"{dem+1,2}.  {date}   {arr1[count],-8}  ");
                    dem++;
                    Console.WriteLine ($"{format}");
                    Console.WriteLine ();
                }
            }
            int number;
            Console.Write (" * ^: Nhập số STT để chọn ngày chiếu : ");
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                if (!isINT) {
                    Console.WriteLine ("Bạn vừa nhập sai giá trị, vui lòng nhập lại.");
                    Console.Write ("#Chọn: ");
                } else if (number <= 0 || number > array.Count) {
                    Console.WriteLine ($"Giá trị trong khoảng từ 1 - > { array.Count }");
                    Console.Write ("#Chọn: ");
                } else {
                    break;
                }
            }
            Console.Clear ();
            // Lấy ra lịch chiếu nhờ STT của Ngày chiếu. 
            Console.WriteLine ("=====================================================================");
            Console.WriteLine ($"-Chọn Lịch Chiếu Của Phim  :  {informatin.Name} Ngày {array1[number - 1]} -");
            Console.WriteLine ("---------------------------------------------------------------------");
            Console.WriteLine ("STT  |  Lịch Chiếu          |  Phòng          |   Số Ghế    ");
            Console.WriteLine ("---------------------------------------------------------------------");
            List<Schedules> demons = schechule.SelectTime (movie_id, array[number - 1]);
            // Lưu tất cả các TimeSpan để so sánh với DateTime.Now.TimeOfDay
            List<string> list1 = new List<string> ();
            // List2 để lấy ra các thời gian show ra màn hình.
            List<string> list2 = new List<string> ();

            string tym_one = string.Format ("{0:D2}:{1:D2}:{2:D2}", datefortimespan.Hours, datefortimespan.Minutes, datefortimespan.Seconds);
            int count1 = 0;
            foreach (var item in demons) {
                string timeText = string.Format ("{0:D2}:{1:D2}:{2:D2}", item.Start_time.Hours, item.Start_time.Minutes, item.Start_time.Seconds);
                list1.Add (timeText);
            }
            for (int j = 0; j < demons.Count; j++) {
                Rooms ro = room.GetRoomById (demons[j].Room_id);
                if (comparedatetime1.CompareTo (array1[number - 1]) == 0) {
                    if (tym_one.CompareTo (list1[j]) < 0) {
                        string start = string.Format ("{0:D2}:{1:D2}", demons[j].Start_time.Hours, demons[j].Start_time.Minutes);
                        string end = string.Format ("{0:D2}:{1:D2}", demons[j].End_time.Hours, demons[j].End_time.Minutes);
                        count1++;
                        string addtimetolist2 = string.Format ("{0:D2}:{1:D2}:{2:D2}", demons[j].Start_time.Hours, demons[j].Start_time.Minutes, demons[j].Start_time.Seconds);
                        list2.Add (addtimetolist2);
                        string format = string.Format ($"{count1}.   | {start,-5} -  {end,-5}       |  {ro.Name,-10}     | {ro.Number_Of_seats}");
                        Console.WriteLine (format);
                    }
                } else {
                    string start = string.Format ("{0:D2}:{1:D2}", demons[j].Start_time.Hours, demons[j].Start_time.Minutes);
                    string end = string.Format ("{0:D2}:{1:D2}", demons[j].End_time.Hours, demons[j].End_time.Minutes);
                    count1++;
                    string addtimetolist2 = string.Format ("{0:D2}:{1:D2}:{2:D2}", demons[j].Start_time.Hours, demons[j].Start_time.Minutes, demons[j].Start_time.Seconds);
                    list2.Add (addtimetolist2);
                    string format = string.Format ($"{count1}.   | {start,-5} -  {end,-5}       |  {ro.Name,-10}     |  {ro.Number_Of_seats}");
                    Console.WriteLine (format);
                }
            }
            Console.WriteLine ("======================================================================");
            int number1;
            Console.Write (" * ^: Nhập số STT để chọn ngày chiếu : ");
            while (true) {
                bool isINT = Int32.TryParse (Console.ReadLine (), out number1);
                if (!isINT) {
                    Console.WriteLine ("Bạn vừa nhập sai giá trị, vui lòng nhập lại.");
                    Console.Write ("#Chọn: ");
                } else if (number1 <= 0 || number1 > list2.Count) {
                    Console.WriteLine ($"Giá trị trong khoảng từ 1 - > { list2.Count }");
                    Console.Write ("#Chọn: ");
                } else {
                    break;
                }
            }
            Console.Clear ();
            Schedules sch = schechule.SelectTimeBy (movie_id, array[number - 1], list2[number1 - 1]);
            string start1 = string.Format ("{0:D2}:{1:D2}", sch.Start_time.Hours, sch.Start_time.Minutes);
            string end1 = string.Format ("{0:D2}:{1:D2}", sch.End_time.Hours, sch.End_time.Minutes);
            Console.WriteLine ("=====================================================================");
            Console.WriteLine ($"Rạp chiếu phim thế giới.");
            Console.WriteLine ($"Phim : {informatin.Name}.  Ngày chiếu : {array1[number - 1]}. Lịch chiếu : {start1} - {end1}");
            Console.WriteLine ("---------------------------------------------------------------------");
            Console.WriteLine ("---------------------------------------------------------------------");
            Console.WriteLine ("CHỌN GHẾ ");
            Console.WriteLine ("---------------------------------------------------------------------");

        }
    }
}