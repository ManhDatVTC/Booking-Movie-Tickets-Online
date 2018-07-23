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
                succer = AddlistByMapSeat (succer, seated);
                // -------------- Map not
                RoomBL room = new RoomBL ();
                Rooms ro = room.GetRoomById (schedule.Room_id);
                string map_ = ro.Chaire_not_placed;
                string[] map_chaire_not_placed = map_.Split (",");

                // -----------------------------------
                ShowSeatChoice (rowsArr, cols, seated, choicedSeat, schedule, map_chaire_not_placed);
                Console.WriteLine ("\n        0. Quay lại.");
                Console.WriteLine ("\n        *: Nhập số số ghế theo định dạng A1, A2, A3 để chọn ghế.");
                Console.WriteLine ("\n     _______________________________________________________________________________________________");
                string choice;
                string[] choiced;
                // Cho khánh hàng nhập các ghế mà khách hàng muốn chọn, Phải nhập đúng định dạng thì mới được break ra khỏi While(true). 
                while (true) {
                    Console.Write ("\n        •√ Bạn chọn ghế :");
                    choice = Console.ReadLine ().Trim ().ToUpper ();
                    if (choice == "0") {
                        BookingTicker tick = new BookingTicker ();
                        tick.MenuBookingTicker ();
                        return;
                    }
                    try {
                        if (choice[choice.Length - 1] == ',') {
                            choice = choice.Remove (choice.Length - 1, 1);
                        }
                    } catch (System.Exception) { }
                    choiced = choice.Split (",");
                    if (CheckInsertIntoChoice (choiced) == true) {
                        break;
                    } else {
                        Console.Clear ();
                        ShowSeatChoice (rowsArr, cols, seated, choicedSeat, schedule, map_chaire_not_placed);
                        Console.WriteLine ("\n        #: Bạn đã nhập sai định dạng ghế VD : A1, A2, A3. Vui lòng nhập lại!! Hoặc chọn 0 để thoát");
                    }
                }
                string test = "";
                test = CheckSeatInSchedule (choiced, succer, test, rowsArr, cols, map_chaire_not_placed);
                // for (int i = 0; i < map_chaire_not_placed.Length; i++) {
                //     for (int k = 0; k < choiced.Length; k++) {
                //         if (map_chaire_not_placed[i].Equals (choiced[k])) {
                //             test = test + " " + map_chaire_not_placed[i];
                //         }
                //     }
                // }
                if (test != "") {
                    Console.Clear ();
                    Console.WriteLine ("\n                    -------------------------------------------------------------------------------");
                    Console.WriteLine ("                          #: Ghế " + test + " mà bạn chọn không có trong danh sách hoặc đã được đặt rồi. ");
                    Console.WriteLine ("                             Vui lòng chọn lại ghế hoặc thoát");
                    Console.WriteLine ("\n                    -------------------------------------------------------------------------------");
                    ComeBackMenu (scheduleUp, 0);
                    return;
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
                    }
                }

                ShowSeatChoice (rowsArr, cols, seated, choicedSeat, schedule, map_chaire_not_placed);
                ChoiceBookingAndContinue ();
                if (checkedSeat != "") {
                    Console.WriteLine ("             Bạn vừa chọn ghế " + checkedSeat + " này rồi ! ");
                }
                Console.Write ("\n             #Chọn : ");
                int number;
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.Clear ();
                        ShowSeatChoice (rowsArr, cols, seated, choicedSeat, schedule, map_chaire_not_placed);
                        ChoiceBookingAndContinue ();
                        Console.WriteLine ("\n             • Giá trị sai vui lòng nhập lại.");
                        Console.Write ("\n             #Chọn : ");
                    } else if (number < 0 || number > 2) {
                        Console.Clear ();
                        ShowSeatChoice (rowsArr, cols, seated, choicedSeat, schedule, map_chaire_not_placed);
                        ChoiceBookingAndContinue ();
                        Console.WriteLine ("\n             • Giá trị sai vui lòng nhập lại 1 hoặc 2. ");
                        Console.Write ("\n             #Chọn : ");
                    } else {
                        break;
                    }
                }
                Console.WriteLine ();
                switch (number) {
                    case 1:
                        map = AddSeatsForSchedule (choicedSeat, seated, map);
                        InformationMovieBookingTickets (schedule, choicedSeat, map);
                        choicedSeat = null;
                        choicedSeat = new List<string> ();
                        break;
                    case 2:
                        Console.Clear ();
                        break;
                }
            }

        }
        public static void CheckSeats () {

        }
        // Lay du lieu tu Database rồi add vào List Succer.
        public static List<string> AddlistByMapSeat (List<string> succer, string[] seated) {
            for (int i = 0; i < seated.Length; i++) {
                if (succer.FindIndex (x => x == seated[i]) == -1) {
                    succer.Add (seated[i]);
                }
            }
            return succer;
        }
        // Check regular expression choice seats.
        public static bool CheckInsertIntoChoice (string[] choiced) {
            for (int i = 0; i < choiced.Length; i++) {
                choiced[i] = choiced[i].Trim ().ToUpper ();
                Console.WriteLine (choiced[i]);
                if (Regex.IsMatch (choiced[i].Trim (), @"^[a-zA-Z]{1}[0-9]{1,2}$") == false) {
                    return false;
                }
            }
            return true;
        }
        // Kiểm tra xem ghế mà khánh hàng chọn có trong danh sách ghế trong database hay không ?
        public static string CheckSeatInSchedule (string[] choiced, List<string> succer, string test, string[] rowsArr, int cols, string[] map_chaire_not_placed) {
            for (int i = 0; i < choiced.Length; i++) {
                if (succer.FindIndex (x => x == choiced[i]) != -1) {
                    test = test + choiced[i];
                }
                int dem = 0;
                for (int k = 0; k < rowsArr.Length; k++) {
                    for (int j = 1; j <= cols; j++) {
                        string seat = rowsArr[k] + j;
                        if (seat == choiced[i]) {
                            dem++;
                        }
                    }
                }
                for (int k = 0; k < map_chaire_not_placed.Length; k++) {
                    if (map_chaire_not_placed[k].Equals (choiced[i])) {
                        test = test + " " + map_chaire_not_placed[k];
                    }

                }
                if (dem == 0) {
                    test = test + " " + choiced[i];
                }
            }
            return test;
        }
        //  Show thông tin vé.
        public static void InformationMovieBookingTickets (Schedules schedule, List<string> choicedSeat, string map) {
            Console.Clear ();
            MoviesBL movie = new MoviesBL ();
            Movies informatin = movie.getMovieById (schedule.Movie_id);
            RoomBL room = new RoomBL ();
            Rooms infor = room.GetRoomById (schedule.Room_id);
            string start1 = string.Format ("{0:D2}:{1:D2}", schedule.Start_time.Hours, schedule.Start_time.Minutes);
            string end1 = string.Format ("{0:D2}:{1:D2}", schedule.End_time.Hours, schedule.End_time.Minutes);
            string datetime = string.Format ($"{schedule.Show_date:dd/MM/yyyy}");
            string choiced = "";
            Random random = new Random ();
            int randomNumber = random.Next (0, 100000000);
            Reservation reser = new Reservation ();
            reser.Reservation_id = 0;
            reser.Schedule_id = schedule.Schedule_id;
            reser.Customer_id = UserInterface.LoginCinema.GetCustomer ().Customer_id;
            foreach (var item in choicedSeat) {
                choiced = choiced + " " + item;
            }
            reser.Seats = choiced.Trim ();
            reser.Code_ticket = randomNumber;
            reser.Price = PaymentFareSeat (choicedSeat);
            string a = Tien (PaymentFareSeat (choicedSeat).ToString ()) + "   VND";

            string time = $"{start1} - {end1}";
            Console.Clear ();
            Console.WriteLine ($"                      √√√√√ Rạp chiếu phim thế giới.\n");
            Console.WriteLine ($"                      Thông tin vé đặt trước  !");
            Console.WriteLine ($"                     –––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––––");
            Console.WriteLine ($"                    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            Console.WriteLine ($"                    |                                                                       |");
            Console.WriteLine ($"                    |  Tên Phim          :   {string.Format ($"{informatin.Name,-47}")}|");
            Console.WriteLine ($"                    |                                                                       |");
            Console.WriteLine ($"                    |  Phòng Chiếu       :   {string.Format ($"{infor.Name,-47}")}|");
            Console.WriteLine ($"                    |                                                                       |");
            Console.WriteLine ($"                    |  Ngày Chiếu        :   {string.Format ($"{datetime,-47}")}|");
            Console.WriteLine ($"                    |                                                                       |");
            Console.WriteLine ($"                    |  Lịch Chiếu        :   {string.Format ($"{time,-47}")}|");
            Console.WriteLine ($"                    |                                                                       |");
            Console.Write ($"                    |  Ghế Ngồi          :   ");
            int count = 0;
            for (int i = 0; i < choicedSeat.Count; i++) {
                count++;
                Console.Write ($"{choicedSeat[i]} :  {Tien(PaymentFare(choicedSeat[i]).ToString())} VND    ");
                if (count == 2) count = 0;
                if ((i + 1) % 2 == 0) Console.Write ("       |\n                    |                        ");
            }
            if (count == 1) {
                Console.WriteLine ("                           |");
            } else {
                Console.WriteLine ("                                               |");
            }
            // Console.Write ("|\n");
            // Console.WriteLine ($"                    |  Ghế Ngồi          :   {string.Format ($"{choiced,-47}")}|");
            Console.WriteLine ($"                    |                                                                       |");
            Console.WriteLine ($"                    |-----------------------------------------------------------------------|");
            Console.WriteLine ($"                    |                                                                       |");
            Console.WriteLine ($"                    |  Tổng Tiền         :   {string.Format ($"{a,-47}")}|");
            Console.WriteLine ($"                    |                                                                       |");
            Console.WriteLine ($"                    +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+");
            Console.WriteLine ($"                     _______________________________________________________________________");
            YNChoice (choicedSeat, schedule, map, reser);

        }
        public static string Tien (string XXX) {
            string KetQua = "";
            int DoDai = XXX.Length;
            for (int i = DoDai - 1; i > -1; i--) {
                KetQua = XXX[i] + KetQua;
                if ((DoDai - i == 3 && DoDai > 3) || (DoDai - i == 6 && DoDai > 6))
                    KetQua = "," + KetQua;
            }
            return KetQua;
        }
        //     Phương thức PaymentFareSeat tính tiền các ghế khánh hàng lựa chọn, !!!

        public static double PaymentFareSeat (List<string> choicedSeat) {
            PriceSeatsBl price = new PriceSeatsBl ();
            string seat = "D3,D4,D5,D6,D7,D8,E3,E4,E5,E6,E7,E8,F3,F4,F5,F6,F7,F8,G3,G4,G5,G6,G7,G8,H3,H4,H5,H6,H7,H8,I3,I4,I5,I6,I7,I8,J3,J4,J5,J6,J7,J8,K3,K4,K5,K6,K7,K8";
            string[] mapArr = seat.Split (",");
            double Payment = 0;

            foreach (string item in choicedSeat) {
                int count = 0;
                for (int i = 0; i < mapArr.Length; i++) {
                    if (item == mapArr[i]) {
                        Payment = Payment + price.GetPriceSeatByPrice_Id (2).Price;
                        count++;
                        break;
                    }
                    if (count == 1) break;
                }
                string a = item;
                if (a.Trim () == "") count++;
                if (count == 0) Payment = Payment + price.GetPriceSeatByPrice_Id (1).Price;
            }
            return Payment;
        }
        public static double PaymentFare (string seats) {
            PriceSeatsBl price = new PriceSeatsBl ();
            string seat = "D3,D4,D5,D6,D7,D8,E3,E4,E5,E6,E7,E8,F3,F4,F5,F6,F7,F8,G3,G4,G5,G6,G7,G8,H3,H4,H5,H6,H7,H8,I3,I4,I5,I6,I7,I8,J3,J4,J5,J6,J7,J8,K3,K4,K5,K6,K7,K8";
            string[] mapArr = seat.Split (",");
            double Payment = 0;
            int count = 0;
            for (int i = 0; i < mapArr.Length; i++) {
                if (seats == mapArr[i]) {
                    Payment = Payment + price.GetPriceSeatByPrice_Id (2).Price;
                    count++;
                    break;
                }
            }
            if (count == 0) { Payment = Payment + price.GetPriceSeatByPrice_Id (1).Price; }
            return Payment;
        }

        public static void InformationTickets (Reservation res, int count) {
            ScheduleBL sch = new ScheduleBL ();
            Schedules schedule = sch.GetScheduleByIdSchedule (res.Schedule_id);
            MoviesBL movie = new MoviesBL ();
            Movies informatin = movie.getMovieById (schedule.Movie_id);
            RoomBL room = new RoomBL ();
            Rooms ro = room.GetRoomById (schedule.Room_id);
            string timeshow = ShowDay (schedule.Show_date, schedule.Start_time, schedule.End_time);

            Console.Clear ();
            Console.WriteLine ($"                   ++-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-++-++++");
            Console.WriteLine ($"                   |                           √√ VÉ ĐẶT TRƯỚC √√                                 |");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |   • Khánh Hàng : {String.Format(UserInterface.LoginCinema.GetCustomer ().Name),-20}  • SĐT {String.Format(UserInterface.LoginCinema.GetCustomer ().Phone),-31} |");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |   • Mẫu số : 01/VE2/003                • Ký Hiệu  : MT/17T                   |");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |   • Số vé : {res.Code_ticket,-6}                   • MST : 0303675393-001                |");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |  CÔNG TY TNHH CINEMA MẠNH ĐẠT - CHI NHÁNH HÀ NỘI                             |");
            Console.WriteLine ($"                   |  Toà nhà VTC, Số 18 phường Tam Trinh quận Hai Bà Trưng, thành phố Hà Nội     |");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |  ............................................................................|");
            Console.WriteLine ($"                   |                                                                              |");
            if (count == 0) {
                Console.WriteLine ($"                   |   CIMENA THẾ GIỚI {DateTime.Now} BOX1 ONLINE                            |");
            } else {
                Console.WriteLine ($"                   |   CIMENA THẾ GIỚI {res.Create_on} BOX1 ONLINE                            |");
            }
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |  ============================================================================|");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |   √ {string.Format ($"{informatin.Name.ToUpper (),-73}")}|");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |   √ Thời gian chiếu :   {string.Format ($"{timeshow,-53}")}|");
            Console.WriteLine ($"                   |                                                                              |");

            Console.Write ($"                   |   √ Phòng chiếu     : {string.Format ($"{ro.Name,-6}")}    Ghế   ");
            string[] arraySeat = res.Seats.Trim ().Split (" ");
            int dem = 0;
            for (int i = 0; i < arraySeat.Length; i++) {
                dem++;
                Console.Write ($"{arraySeat[i]} :  {Tien(PaymentFare(arraySeat[i]).ToString())} VND  ");
                if (dem == 2) dem = 0;
                if ((i + 1) % 2 == 0) Console.Write ("   |\n                   |                                       ");
            }
            
            if (dem == 1) {
                Console.WriteLine ("                     |");
            } else {
                Console.WriteLine ("                                       |");
            }
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |  ============================================================================|");
            Console.WriteLine ($"                   |                                                                              |");
            Console.WriteLine ($"                   |   √ Giá Tiền                                   VND   {Tien(res.Price.ToString()),-15}         | ");
            Console.WriteLine ($"                   |                                                (Đã bao gồm 5% VAT)           |");
            Console.WriteLine ($"                   ++-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-++-++++");
            Console.WriteLine ($"                     ____________________________________________________________________________");
            if (count == 0) {
                Console.WriteLine ();
                Console.WriteLine ($"                          #: Vui lòng nhớ số điện thoại và số vé để đối chiếu                    ");
                Console.WriteLine ($"                          #: Quý khánh vui lòng đến trước suất chiếu 15 phút để lấy vé           ");
                Console.WriteLine ($"                             Quá thời gian lấy vé, chúng tôi sẽ không hoàn lại tiền vé của bạn   ");
                Console.WriteLine ($"                             và vé đặt sẽ tự động bị huỷ.                                        ");
                Console.WriteLine ($"                     ____________________________________________________________________________");
                ComeBackMenu (schedule, 1);
            } else {
                return;
            }

        }
        public static string ShowDay (DateTime Date, TimeSpan spanStart, TimeSpan spanEnd) {
            string[] arr1 = new string[] { "Thứ hai", "Thứ ba", "Thứ tư", "Thứ năm", "Thứ sáu", "Thứ bảy", "Chủ nhật" };
            string[] arr2 = new string[] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            // string[] arr3 = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            string week = $"{Date.DayOfWeek}";
            string tym_Start = string.Format ("{0:D2}:{1:D2}", spanStart.Hours, spanStart.Minutes);
            string tym_End = string.Format ("{0:D2}:{1:D2}", spanEnd.Hours, spanEnd.Minutes);
            string DateShow = Date.ToString ($"{Date:dd/MM/yyyy}");
            // int month = Date.Month;
            int count = -1;
            for (int j = 0; j < arr2.Length; j++) {
                if (arr2[j] == week) {
                    count = j;
                    break;
                }
            }
            return $"{arr1[count]} . {DateShow} {tym_Start} - {tym_End}";
        }

        // ----- Show Information Seats . Seat selecting and Seat have been selected.
        public static void ShowSeatChoice (string[] rowsArr, int cols, string[] seated, List<string> choicedSeat, Schedules schedule, string[] map_chaire_not_placed) {
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
            Console.WriteLine ("\n");
            Console.WriteLine ("+++-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-++++");
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
                    if (i < 3 || i > 10 || j < 3 || j > 8) {
                        for (int k = 0; k < seated.Length; k++) {
                            if (seat == seated[k]) {
                                string format1 = "";
                                if (seated[k].Length == 3) {
                                    format1 = "[_√√_] ";
                                } else {
                                    format1 = "[_√√_]";
                                }
                                string format = string.Format ($"{format1}  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        for (int k = 0; k < choicedSeat.Count; k++) {
                            if (seat == choicedSeat[k]) {
                                string format1 = "";
                                if (choicedSeat[k].Length == 3) {
                                    format1 = "[^﹏^] ";
                                } else {
                                    format1 = "[^﹏^]";
                                }
                                string format = string.Format ($"{format1}  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        for (int k = 0; k < map_chaire_not_placed.Length; k++) {
                            if (seat == map_chaire_not_placed[k]) {
                                string format1 = "";
                                if (map_chaire_not_placed[k].Length == 3) {
                                    format1 = "       ";
                                } else {
                                    format1 = "      ";
                                }
                                string format = string.Format ($"{format1}  ");
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
                                string format1 = "";
                                if (seated[k].Length == 3) {
                                    format1 = "[_√√_] ";
                                } else {
                                    format1 = "[_√√_]";
                                }
                                string format = string.Format ($"{format1}  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        for (int k = 0; k < choicedSeat.Count; k++) {
                            if (seat == choicedSeat[k]) {
                                string format1 = "";
                                if (choicedSeat[k].Length == 3) {
                                    format1 = "[^﹏^] ";
                                } else {
                                    format1 = "[^﹏^]";
                                }
                                string format = string.Format ($"{format1}  ");
                                Console.Write (format);
                                flag = false;
                            }
                        }
                        for (int k = 0; k < map_chaire_not_placed.Length; k++) {
                            if (seat == map_chaire_not_placed[k]) {
                                string format1 = "";
                                if (map_chaire_not_placed[k].Length == 3) {
                                    format1 = "       ";
                                } else {
                                    format1 = "      ";
                                }
                                string format = string.Format ($"{format1}  ");
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
            Console.WriteLine ("\n|                                                                                                        |");
            Console.WriteLine ("+++-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-++++");
            Console.WriteLine ("\n");
            Console.WriteLine ("      [_X4_] : Ghế trống  [_√√_] : Ghế đã có người đặt   [^﹏^] : Ghế đang chọn  [V:__] : Ghế VIP ");
            Console.WriteLine ("\n     _______________________________________________________________________________________________");
            // Console.WriteLine ("\n        0. Quay lại.");
            // Console.WriteLine ("\n        *: Nhập số số ghế theo định dạng A1, A2, A3 để chọn ghế.");
            // Console.WriteLine ("\n     _______________________________________________________________________________________________");
        }

        public static string AddSeatsForSchedule (List<string> choicedSeat, string[] succerTest, string map) {
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
        public static void ChoiceBookingAndContinue () {
            Console.WriteLine ("    |                                                                                               |");
            Console.WriteLine ("    |     1. Đặt vé các ghế đã chọn.                                                                |");
            Console.WriteLine ("    |                                                                                               |");
            Console.WriteLine ("    |     2. Đặt thêm ghế .                                                                         |");
            Console.WriteLine ("    |                                                                                               |");
            Console.WriteLine ("    |_______________________________________________________________________________________________|");
        }
        public static void YNChoice (List<string> choicedSeat, Schedules sch, string map, Reservation res) {
            ScheduleBL schedule = new ScheduleBL ();
            ReservationBL reser = new ReservationBL ();
            Console.Write ($"\n                      ^: Bạn muốn đặt vé không   (Y/N) : ");
            char tiep = ' ';
            do {
                tiep = Tieptuc (tiep);
                switch (tiep) {
                    case 'Y':
                        reser.InsertIntoReservation (res);
                        schedule.BuySeats (sch, map);
                        InformationTickets (res, 0);
                        break;
                    case 'y':
                        reser.InsertIntoReservation (res);
                        schedule.BuySeats (sch, map);
                        InformationTickets (res, 0);
                        break;
                    case 'N':
                        ComeBackMenu (sch, 0);
                        break;
                    case 'n':
                        ComeBackMenu (sch, 0);
                        break;
                }
            } while (tiep != 'Y' && tiep != 'N' && tiep != 'y' && tiep != 'n');
        }
        public static char Tieptuc (char value) {
            while (true) {
                bool ischar = char.TryParse (Console.ReadLine (), out value);
                if (!ischar) {
                    Console.Write ("                         Bạn đã chọn sai vui lòng chọn lại (Y/N): ");
                } else {
                    return value;
                }
            }
        }
        public static void ComeBackMenu (Schedules scheduleUp, int count) {
            while (true) {
                if (count == 0) {
                    Console.WriteLine ("\n                      1. Đặt Lại Ghế\n\n                      2. Quay Lại Menu Chính");

                }
                if (count == 1) {
                    Console.WriteLine ("\n                          1. Đặt thêm vé cho bộ phim khác.\n\n                          2. Quay Lại Menu Chính");

                }
                Console.WriteLine ("                    ------------------------------------------------------------------------------");
                Console.Write ("\n                          Chọn : ");
                int number;
                while (true) {
                    bool isINT = Int32.TryParse (Console.ReadLine (), out number);
                    if (!isINT) {
                        Console.WriteLine ("                          Giá trị sai vui lòng nhập lại.");
                        Console.Write ("                          #Chọn : ");
                    } else if (number < 0 || number > 2) {
                        Console.WriteLine ("                          Giá trị sai vui lòng nhập lại 1 hoặc 2. ");
                        Console.Write ("                          #Chọn : ");
                    } else {
                        break;
                    }
                }
                switch (number) {
                    case 1:
                        Console.Clear ();
                        if (count == 0) {
                            MenuChoiceSeats (scheduleUp);
                        }
                        if (count == 1) {
                            BookingTicker bookig = new BookingTicker ();
                            bookig.MenuBookingTicker ();
                        }
                        return;
                    case 2:
                        CinemaInterface.Cinema ();
                        Console.Clear ();
                        return;
                }
            }
        }
    }
}