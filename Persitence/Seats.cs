using System;

namespace Persitence {
    public class Seats {
        private string row;
        private int col;
        private Rooms room;
        private String status = "X";

        public string Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
        public Rooms Room { get => room; set => room = value; }
        public string Status { get => status;}
    }
}