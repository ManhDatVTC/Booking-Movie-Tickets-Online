using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class Movies_DAL {
        private MySqlConnection connection;
        private MySqlDataReader reader;
        public Movies_DAL () {
            connection = DBHelper.OpenConnection ();
        }
        public static Movies GetMovie (MySqlDataReader reader) {
            Movies movie = new Movies ();
            movie.Movie_id = reader.GetInt32 ("movie_id");
            movie.Name = reader.GetString ("movie_name");
            movie.Actor = reader.GetString ("actor");
            movie.Producers = reader.GetString ("producers");
            movie.Direction = reader.GetString ("director");
            movie.Genre = reader.GetString ("genre");
            movie.Duration = reader.GetInt32 ("duration");
            movie.Detail_movie = reader.GetString ("detail_movie");
            movie.Release_date = reader.GetDateTime ("release_date");
            movie.End_date = reader.GetDateTime ("end_date");
            return movie;
        }
        public List<Movies> GetMovies () {
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = "Select * from Movies;";
            List<Movies> list = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                list = new List<Movies> ();
                while (reader.Read ()) {
                    list.Add (GetMovie (reader));
                }
            }
            // }
            connection.Close ();
            return list;
        }

        public Movies getMovieByName (string Name) {
            if (Name == "") {
                return null;
            }
            string query = $"SELECT * FROM Movies WHERE movie_name= '{Name}';";
            Movies movie = null;
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    // movie = new Movies ();
                    movie = GetMovie (reader);
                }
            }
            connection.Close ();
            // }
            return movie;
        }
        public Movies getMovieById (int id) {
            if (id == 0) {
                return null;
            }
            if (connection.State == System.Data.ConnectionState.Closed) {
                connection.Open ();
            }
            string query = $"SELECT * FROM Movies WHERE movie_id= '{id}';";
            Movies movie = null;
            // using (connection = DBHelper.OpenConnection ()) {
            MySqlCommand cmd = new MySqlCommand (query, connection);
            using (reader = cmd.ExecuteReader ()) {
                if (reader.Read ()) {
                    movie = new Movies ();
                    movie = GetMovie (reader);
                }
            }
            // }
            connection.Close ();
            return movie;
        }
    }
}