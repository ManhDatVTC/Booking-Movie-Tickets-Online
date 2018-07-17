using System;
using BL;
using Persitence;
using Xunit;

public class TestMovieBL {
    private MoviesBL movie = new MoviesBL ();
    [Fact]
    public void TestGetMovieBl () {
        Assert.NotNull (movie.GetMovies ());
    }

    [Fact]
    public void TestGetMovieByName () {
        Assert.NotNull (movie.getMovieByName ("HARRY POTTER"));
    }

    [Fact]
    public void TestGetMovieByName1 () {
        Assert.Null (movie.getMovieByName (null));
        // Assert.Equal (null, movie.getMovieByName (null));
    }

    [Fact]
    public void TestGetMovieByName2 () {
        Assert.Null (movie.getMovieByName (""));
        // Assert.Equal (null, movie.getMovieByName (""));

    }

    [Fact]
    public void TestgetMovieById () {
        Assert.NotNull (movie.getMovieById (1));
    }

    [Fact]
    public void TestgetMovieById1 () {
        Assert.Null (movie.getMovieById (0));
    }

}