using System;

namespace Persitence {
    public class Movies {
        private int movie_id;
        private String name;
        private String actor;
        private String producers;
        private String direction;
        private String genre;
        private int duration;
        private String detail_movie;
        private DateTime release_date;
        public Movies () { }

        public Movies (int movie_id, string name, string actor, string producers, string direction, string genre, int duration, string detail_movie, DateTime release_date) {
            this.movie_id = movie_id;
            this.name = name;
            this.actor = actor;
            this.producers = producers;
            this.direction = direction;
            this.genre = genre;
            this.duration = duration;
            this.detail_movie = detail_movie;
            this.release_date = release_date;
        }

        public int Movie_id { get => movie_id; set => movie_id = value; }
        public string Name { get => name; set => name = value; }
        public string Actor { get => actor; set => actor = value; }
        public string Producers { get => producers; set => producers = value; }
        public string Direction { get => direction; set => direction = value; }
        public string Genre { get => genre; set => genre = value; }
        public int Duration { get => duration; set => duration = value; }
        public string Detail_movie { get => detail_movie; set => detail_movie = value; }
        public DateTime Release_date { get => release_date; set => release_date = value; }
        public override bool Equals (object obj) {
            Movies movie = (Movies) obj;

            return Movie_id == movie.Movie_id;
        }

        public override int GetHashCode () {
            return (Movie_id + Name + Actor + Producers + Direction +
                Genre + Duration + Detail_movie + Release_date).GetHashCode ();
        }
    }
}