using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Persitence;

namespace DAL {
    public class Movies_DAL {
        private MySqlConnection connection;
        private MySqlDataReader reader;
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
            return movie;
        }
        public List<Movies> GetMovies () {
            string query = "Select * from Movies;";
            List<Movies> list = new List<Movies> ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    while (reader.Read ()) {
                        list.Add (GetMovie (reader));
                    }
                }
            }
            return list;
        }

        public Movies getMovieByName (string Name) {
            string query = $"SELECT * FROM Movies WHERE movie_name= '{Name}';";
            Movies movie = new Movies ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    if (reader.Read ()) {
                        movie = GetMovie (reader);
                    }
                }
            }
            return movie;
        }
        public Movies getMovieById (int id) {
            string query = $"SELECT * FROM Movies WHERE movie_id= '{id}';";
            Movies movie = new Movies ();
            using (connection = DBHelper.OpenConnection ()) {
                MySqlCommand cmd = new MySqlCommand (query, connection);
                using (reader = cmd.ExecuteReader ()) {
                    if (reader.Read ()) {
                        movie = GetMovie (reader);
                    }
                }
            }
            return movie;
        }
    }
}