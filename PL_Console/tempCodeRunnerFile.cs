  // public static void ShowSeat (string[] rowsArr, int cols, string[] seated, Schedules schedule) {
        //     Console.Clear ();
        //     MoviesBL movie = new MoviesBL ();
        //     Movies informatin = movie.getMovieById (schedule.Movie_id);
        //     string start1 = string.Format ("{0:D2}:{1:D2}", schedule.Start_time.Hours, schedule.Start_time.Minutes);
        //     string end1 = string.Format ("{0:D2}:{1:D2}", schedule.End_time.Hours, schedule.End_time.Minutes);
        //     string datetime = string.Format ($"{schedule.Show_date:dd/MM/yyyy}");
        //     Console.WriteLine ("=========================================================================================================");
        //     Console.WriteLine ($"Rạp chiếu phim thế giới.");
        //     Console.WriteLine ("\n• CHỌN GHẾ ");
        //     Console.WriteLine ($"Phim : {informatin.Name}.     Ngày chiếu : {datetime}.     Lịch chiếu : {start1} - {end1}");
        //     Console.WriteLine ("---------------------------------------------------------------------------------------------------------");
        //     Console.WriteLine ("                                              Màn Hình                                                    ");
        //     Console.WriteLine ("---------------------------------------------------------------------------------------------------------");
        //     Console.WriteLine (" ________________________________________________________________________________________________________");
        //     Console.WriteLine ("|                                                                                                        |");
        //     // Show information Mapseat User choice
        //     for (int i = 0; i < rowsArr.Length; i++) {
        //         if (i == 0) {
        //             Console.Write ("|    ");
        //         }
        //         for (int j = 1; j <= cols; j++) {
        //             if (j == 3 || j == 9) {
        //                 Console.Write ("   .   ");
        //             }
        //             string seat = rowsArr[i] + j;
        //             bool flag = true;
        //             if (i < 4 || i > 10 || j < 3 || j > 8) {
        //                 for (int k = 0; k < seated.Length; k++) {
        //                     if (seat == seated[k]) {
        //                         string format = string.Format ($"[_√√_]  ");
        //                         Console.Write (format);
        //                         flag = false;
        //                     }
        //                 }
        //                 if (flag) {
        //                     Console.Write ("[_" + seat + "_]  ");
        //                 }
        //             } else {
        //                 for (int k = 0; k < seated.Length; k++) {
        //                     if (seat == seated[k]) {
        //                         string format = string.Format ($"[_√√_]  ");
        //                         Console.Write (format);
        //                         flag = false;
        //                     }
        //                 }
        //                 if (flag) {
        //                     Console.Write ("[V:" + seat + "]  ");
        //                 }
        //             }

        //         }
        //         Console.WriteLine ("     |\n|                                                                                                        |");
        //         Console.Write ("|    ");
        //     }
        //     Console.Write ("                                                                                                    |");
        //     Console.WriteLine ("\n|________________________________________________________________________________________________________|");
        //     Console.WriteLine ("\n");
        //     Console.WriteLine ("      [_X4_] : Ghế trống  [_√√_] : Ghế đã có người đặt   [^﹏^] : Ghế đang chọn  [V:__] : Ghế VIP ");
        //     Console.WriteLine ("\n    -----------------------------------------------------------------------------------------------");
        // }