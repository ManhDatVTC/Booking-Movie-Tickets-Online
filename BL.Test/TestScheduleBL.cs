using System;
using BL;
using Persitence;
using Xunit;

public class TestScheduleBl {
    private ScheduleBL sch = new ScheduleBL ();
    [Fact]
    public void GetSchduleTest () {
        Assert.NotNull (sch.GetSchedules ());
    }

    [Fact]
    public void GetScheduleByIdTest () {
        Assert.NotNull (sch.GetScheduleByIdSchedule (1));
    }

    [Fact]
    public void GetScheduleByIdMovieTest () {
        Assert.NotNull (sch.GetScheduleByIdMovie (1));
    }

    [Fact]
    public void GetScheduleByIdRoomTest () {
        Assert.NotNull (sch.GetScheduleByIdRooms (1));
    }

    [Fact]
    public void SelectDateTimeByMovieIdTest () {
        Assert.NotNull (sch.SelectDatetime (1));
    }

    [Fact]
    public void SelectTimeByDateAndIdMovieTrueTest () {
        string regex = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
        string datetime = "2018-07-26";
        Assert.Matches (regex, datetime);
        ScheduleBL sch = new ScheduleBL ();
        Assert.NotNull (sch.SelectTime (1, datetime));
    }

    [Fact]
    public void SelecDateTimeFail () {
        string regex = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
        string datetime = "2018-077-26";
        Assert.DoesNotMatch (regex, datetime);
        // ScheduleBL sch = new ScheduleBL ();
        Assert.Null (sch.SelectTime (1, datetime));
    }

    [Theory]
    [InlineData (1, "2018-07-26", "08:00:00")]
    [InlineData (1, "2018-07-25", "10:00:00")]
    public void SelectScheduleTimeByTestTrue (int movie_id, string datetime, string time) {
        string regexDate = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
        string regexTime = @"^(\d{1,2}|\d\.\d{2}):([0-5]\d):([0-5]\d)(\.\d+)?$";

        Assert.Matches (regexDate, datetime);
        Assert.Matches (regexTime, time);
        // DateTime datetime = DateTime.Now;
        // ScheduleBL sch = new ScheduleBL ();
        Assert.NotNull (sch.SelectTimeBy (movie_id, datetime, time));
    }

    [Theory]
    [InlineData (1, "2018-007-26", "08:000:00")]
    [InlineData (1, "201s8-007-26", "0008:000:00")]
    public void SelectScheduleTimeByTestFail (int movie_id, string datetime, string time) {
        string regexDate = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
        string regexTime = @"^(\d{1,2}|\d\.\d{2}):([0-5]\d):([0-5]\d)(\.\d+)?$";
        Assert.DoesNotMatch (regexDate, datetime);
        Assert.DoesNotMatch (regexTime, time);
        ScheduleBL sch = new ScheduleBL ();
        Assert.Null (sch.SelectTimeBy (movie_id, datetime, time));
    }

    [Fact]
    public void BuySeatsTestTrue () {
        DateTime date = new DateTime (2018, 7, 20);
        TimeSpan time = new TimeSpan (8, 0, 0);

        Schedules schedu = new Schedules (1, 1, 1, date, time, time, "MapSeat", 45000);
        Assert.True (sch.BuySeats(schedu,"A B C D F E G H J K L M;10;"));

    }

    [Fact]
    public void BuySeatsTestFail()
    {
        DateTime date = new DateTime (2018, 7, 20);
        TimeSpan time = new TimeSpan (8, 0, 0);

        Schedules schedu = new Schedules (1, 1, 1, date, time, time, "MapSeat", 45000);
        Assert.False (sch.BuySeats(schedu,""));
    }
}