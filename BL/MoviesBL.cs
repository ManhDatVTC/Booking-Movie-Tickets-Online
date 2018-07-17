using System;
using System.Collections.Generic;
using DAL;
using Persitence;

namespace BL {
    public class MoviesBL {
        private Movies_DAL dal = new Movies_DAL ();
        public List<Movies> GetMovies () {
            return dal.GetMovies ();
        }
        public Movies getMovieByName (String name) {
            if (name == null || name == "") {
                return null;
            }
            return dal.getMovieByName (name);
        }
        public Movies getMovieById (int id) {
            if (id == 0) {
                return null;
            }
            return dal.getMovieById (id);
        }
    }
}