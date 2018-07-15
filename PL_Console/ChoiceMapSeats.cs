using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using BL;
using Persitence;

namespace PL_Console {
    public class ChoiceMapSeats {
        public static void MenuChoiceSeats (Schedules scheduleUp) {
            List<string> choicedSeat = new List<string> ();
            while (true) {
                // ------------------------ Map Seats in Database with Schedule.
                ScheduleBL schechuleBl = new ScheduleBL ();
                string showdate = scheduleUp.Show_date.ToString ($"{scheduleUp.Show_date:yyyy-MM-dd}");
                string starttime = string.Format ("{0:D2}:{1:D2}:{2:D2}", scheduleUp.Start_time.Hours, scheduleUp.Start_time.Minutes, scheduleUp.Start_time.Seconds);
                Schedules schedule = schechuleBl.SelectTimeBy (scheduleUp.Movie_id, showdate, starttime);
                string map = schedule.Schedule_room_seat;
                Console.Clear ();
                string[] mapArr = map.Split (";");
                string[] rowsArr = mapArr[0].Split (" ");
                int cols = Int32.Parse (mapArr[1]);
                List<string> succer = new List<string> ();
                string[] seated = mapArr[2].Split (",");
                for (int i = 0; i < seated.Length; i++) {
                    if (succer.FindIndex (x => x == seated[i]) == -1) {
                        succer.Add (seated[i]);
                    }
                }
                ShowSeatChoice (rowsArr, cols, seated, choicedSeat, schedule);
                Console.Write ("\n        •√ Bạn chọn ghế :");
                string choice = Console.ReadLine ().Trim ().ToUpper ();
                string[] choiced = choice.Split (",");
                for (int i = 0; i < choiced.Length; i++) {
                    choiced[i] = choiced[i].Trim ().ToUpper ();
                }
                string test = "";
                for (int i = 0; i < choiced.Length; i++) {
                    int dem = 0;
                    for (int k = 0; k < rowsArr.Length; k++) {
                        for (int j = 1; j <= cols; j++) {
                            string seat = rowsArr[k] + j;
                            if (seat == choiced[i]) {
                                dem++;
                            }
                        }
                    }
                    if (dem == 0) {
                        test = test + " " + choiced[i];
                    }
                }

                string checkedSeat = "";
                // List<string> choicedSeat = new List<string> ();
                for (int i = 0; i < choiced.Length; i++) {
                    if (succer.FindIndex (x => x == choiced[i]) == -1) {
                        if (choicedSeat.FindIndex (x => x == choiced[i]) == -1) {
                            succer.Add (choiced[i]);
                            choicedSeat.Add (choiced[i]);
                        } else {
                            checkedSeat = checkedSeat + " " + choiced[i];
                        }
                    } else {
                        test = test + " " + choiced[i];
                    }
                }
                if (test != "") {
                    Console.Clear ();
                    Console.WriteLine ("\n_____________________________________________________________________________");
                    Console.WriteLine ("#: Ghế " + test + " mà bạn chọn không có trong danh sách hoặc đã được đặt rồi. ");
                    Console.WriteLine ("   Vui lòng chọn lại ghế hoặc thoát                                        ");
                    Console.WriteLine ("\n_____________________________________________________________________________");
                    ComeBackMenu (scheduleUp);
                    return;
                }
                ShowSeatChoice (rowsArr, cols, seated, choicedSeat, schedule);
                if (checkedSeat != "") {
                    Console.WriteLine ("Bạn vừa chọn ghế " + checkedSeat + " này rồi ! ");
                }
                Console.WriteLine ("    |                                                                                               |");
                Console.WriteLine ("    |     1. Đặt vé các ghế đã đặt.                                                                 |");
                Console.WriteLine ("    |                                                                                               |");
                Console.WriteLine ("    |     2. Đặt thêm ghế .                                                                         |");
                Console.WriteLine ("    |                                                                                               |");
                Console.WriteLine ("    |_______________________________________________________________________________________________|");
                Console.Write ("\n             # Chọn : ");
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
                Console.WriteLine ();
                switch (number) {
                    case 1:
                        InformationChoiceBookingTicket (schedule, choicedSeat);
                        map = AddSeatForRoom (choicedSeat, seated, map);
                        YNChoice (choicedSeat, schedule, map, schechuleBl);
                        choicedSeat = null;
                        choicedSeat = new List<string> ();
                        Console.ReadLine ();
                        break;
                    case 2:
                        Console.Clear ();
                        break;
                }
            }

        }
        public static void InformationChoiceBookingTicket (Schedules schedule, List<string> choicedSeat) {
            MoviesBL movie = new MoviesBL ();
            Movies informatin = movie.getMovieById (schedule.Movie_id);
            RoomBL room = new RoomBL ();
            Rooms infor = room.GetRoomById (schedule.Room_id);
            string start1 = string.Format ("{0:D2}:{1:D2}", schedule.Start_time.Hours, schedule.Start_time.Minutes);
            string end1 = string.Format ("{0:D2}:{1:D2}", schedule.End_time.Hours, schedule.End_time.Minutes);
            string datetime = string.Format ($"{schedule.Show_date:dd/MM/yyyy}");
            Console.WriteLine ($"Phim : {informatin.Name}.     Ngày chiếu : {datetime}.     Lịch chiếu : {start1} - {end1}");
            string choiced = "";
            foreach (var item in choicedSeat) {
                choiced = choiced + " " + item;
            }
            Console.WriteLine ($"Phòng chiếu : {infor.Name}");
            Console.WriteLine ("Ghế : " + choiced);
            Console.WriteLine ("Gía tiền   : " + PaymentFare (choicedSeat));
            Console.ReadLine ();

        }

        public static double PaymentFare (List<string> choicedSeat) {
            string seat = "F3,F4,F5,F6,F7,F8,E3,E4,E5,E6,E7,E8,G3,G4,G5,G6,G7,G8,H3,H4,H5,H6,H7,H8,J3,J4,J5,J6,J7,J8,K3,K4,K5,K6,K7,K8,L3,L4,L5,L6,L7,L8";
            string[] mapArr = seat.Split (",");
            double Payment = 0;
            foreach (var item in choicedSeat) {
                int count = 0;
                for (int i = 0; i < mapArr.Length; i++) {
                    if (item == mapArr[i]) {
                        Payment = Payment + 60000;
                        count++;
                        break;
                    }
                }
                if (count == 0) {
                    Payment = Payment + 45000;
                }
            }
            return Payment;
        }

        public static void ComeBackMenu (Schedules scheduleUp) {
            while (true) {
                Console.WriteLine ("\n  1. Đặt Lại Ghế\n\n  2. Quay Lại Menu Chính");
                Console.WriteLine ("------------------------------------------------------------------------------");
                Console.Write ("\n  Chọn : ");
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
                        MenuChoiceSeats (scheduleUp);
                        return;
                    case 2:
                        CinemaInterface.Cinema ();
                        Console.Clear ();
                        return;
                }
            }
        }
        public static void YNChoice (List<string> choicedSeat, Schedules sch, string map, ScheduleBL schechule) {
            Console.Write ($"    ^: Bạn muốn vé không   (Y/N) : ");
            char tiep = ' ';
            do {
                tiep = Tieptuc (tiep);
                switch (tiep) {
                    case 'Y':
                        schechule.BuySeats (sch, map);
                        break;
                    case 'y':
                        schechule.BuySeats (sch, map);
                        break;
                    case 'N':
                        break;
                    case 'n':
                        break;
                }
            } while (tiep != 'Y' && tiep != 'N' && tiep != 'y' && tiep != 'n');
            Console.WriteLine ("================================================================================");
        }
        public static char Tieptuc (char value) {
            while (true) {
                bool ischar = char.TryParse (Console.ReadLine (), out value);
                if (!ischar) {
                    Console.Write ("Bạn đã chọn sai vui lòng chọn lại (Y/N): ");
                } else {
                    return value;
                }
            }
        }

        // ----- Show Information Seats DANG DAT VA DA DAT.
        public static void ShowSeatChoice (string[] rowsArr, int cols, string[] seated, List<string> choicedSeat, Schedules schedule) {
            Console.Clear ();
            MoviesBL movie = new MoviesBL ();
            Movies informatin = movie.getMovieById (schedule.Movie_id);
            string start1 = string.Format ("{0:D2}:{1:D2}", schedule.Start_time.Hours, schedule.Start_time.Minutes);
            string end1 = string.Format ("{0:D2}:{1:D2}", schedule.End_time.Hours, schedule.End_time.Minutes);
            string datetime = string.Format ($"{schedule.Show_date:dd/MM/yyyy}");
            Console.WriteLine ("=========================================================================================================");
            Console.WriteLine ($"Rạp chiếu phim thế giới.");
            Console.WriteLine ("\n• CHỌN GHẾ \n");
            Console.WriteLine ($"Phim : {informatin.Name}.     Ngày chiếu : {datetime}.     Lịch chiếu : {start1} - {end1}");
            Console.WriteLine ("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine ("                                              Màn Hình                                                    ");
            Console.WriteLine ("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine (" ________________________________________________________________________________________________________");
            Console.WriteLine ("|                                                                                                        |");
            // Show information Mapseat User choice
            for (int i = 0; i < rowsArr.Length; i++) {
                if (i == 0) {
                    Console.Write ("|    ");
                }
                for (int j = 1; j <= cols; j++) {
                    if (j == 3 || j == 9) {
                        Console.Write ("   .   ");
                    }
                    string seat = rowsArr[i] + j;
                    bool flag = true;
                    if (i < 4 || i > 10 || j < 3 || j > 8) {
                        for (int k = 0; k < seated.Length; k++) {
                            if (seat == seated[k]) {
                                string format = string.Format ($"[_√√_]  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        for (int k = 0; k < choicedSeat.Count; k++) {
                            if (seat == choicedSeat[k]) {
                                string format = string.Format ($"[^﹏^]  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        if (flag) {
                            Console.Write ("[_" + seat + "_]  ");
                        }
                    } else {
                        for (int k = 0; k < seated.Length; k++) {
                            if (seat == seated[k]) {
                                string format = string.Format ($"[_√√_]  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        for (int k = 0; k < choicedSeat.Count; k++) {
                            if (seat == choicedSeat[k]) {
                                string format = string.Format ($"[^﹏^]  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        if (flag) {
                            Console.Write ("[V:" + seat + "]  ");
                        }
                    }

                }
                Console.WriteLine ("     |\n|                                                                                                        |");
                Console.Write ("|    ");
            }
            Console.Write ("                                                                                                    |");
            Console.WriteLine ("\n|________________________________________________________________________________________________________|");
            Console.WriteLine ("\n");
            Console.WriteLine ("      [_X4_] : Ghế trống  [_√√_] : Ghế đã có người đặt   [^﹏^] : Ghế đang chọn  [V:__] : Ghế VIP ");
            Console.WriteLine ("\n     _______________________________________________________________________________________________");
        }

        public static string AddSeatForRoom (List<string> choicedSeat, string[] succerTest, string map) {
            foreach (var item in choicedSeat) {
                if (succerTest[0] == "") {
                    map = map + item;
                    succerTest[0] = " ";
                } else {
                    map = map + "," + item;
                }
            }
            return map;
        }
    }
}