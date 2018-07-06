using System;
using System.Collections.Generic;
using DAL;
using MySql.Data.MySqlClient;
using Persitence;
using Xunit;

namespace DAL.Test {
    public class MovieDALTest {
        private static Movies_DAL movieDAL = new Movies_DAL ();
        private MySqlConnection connection;
        private MySqlDataReader reader;

        [Fact]
        public void GetMoviesTest () {
            List<Movies> movies = new List<Movies> ();
            movies = movieDAL.GetMovies ();
            string query = $"select * from Movies order by rand() limit 1 ;";
            Movies movieRand = GetMovieExecQuery (query);
            string query1 = $"select * from Movies order by movie_id asc limit 1; ";
            Movies movieTop = GetMovieExecQuery (query1);
            string query2 = $"select * from Movies order by movie_id desc limit 1;";
            Movies movieBottom = GetMovieExecQuery (query2);

            Assert.NotNull (movies);
            Assert.NotNull (movieRand);
            Assert.NotNull (movieTop);
            Assert.NotNull (movieBottom);

            //  So sanh vi tri Top Bottom va Rand xem co giong trong List.
            Assert.True (movieTop.Movie_id == movies[0].Movie_id);
            Assert.True (movieBottom.Movie_id == movies[movies.Count - 1].Movie_id);
            Assert.Contains (movieRand,movies);
        }

        [Fact]
        public void GetMoviesByNameTest () {
            string name = "HARRY POTTER";
            Assert.NotNull (movieDAL.getMovieByName (name));
            Assert.Equal (name, movieDAL.getMovieByName (name).Name);
        }

        [Fact]
        public void GetMovieByIdTest () {
            Assert.NotNull (movieDAL.getMovieById (1));
            Assert.Equal (1, movieDAL.getMovieById (1).Movie_id);
        }
        private Movies GetMovieExecQuery (string query) {
            Movies movie = new Movies ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    if (reader.Read ()) {
                        movie = Movies_DAL.GetMovie (reader);
                    }
                }
            }
            return movie;
        }

    }
}