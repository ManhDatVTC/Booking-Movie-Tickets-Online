using System;
using System.Collections.Generic;
using DAL;
using Persitence;

namespace BL {
    public class RoomBL {
        private Room_DAL dal = new Room_DAL ();
        public List<Rooms> GetRooms () {
            return dal.GetRooms ();
        }
        public Rooms GetRoomById (int id) {
            return dal.GetRoomById (id);
        }
    }
}