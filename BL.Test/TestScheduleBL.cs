using BL;
using Xunit;

public class TestScheduleBl {

    [Fact]
    public void CheckDateTimeTrue () {
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
        ScheduleBL sch = new ScheduleBL ();
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

        ScheduleBL sch = new ScheduleBL ();
        Assert.NotNull (sch.SelectTimeBy(movie_id,datetime,time));
    }

    [Theory]
    [InlineData(1,"2018-007-26", "08:000:00")]
    [InlineData(1,"201s8-007-26", "0008:000:00")]
    public void SelectScheduleTimeByTestFail(int movie_id, string datetime, string time)
    {
        string regexDate = @"(?<year>\d{2,4})-(?<month>\d{1,2})-(?<day>\d{1,2})";
        string regexTime = @"^(\d{1,2}|\d\.\d{2}):([0-5]\d):([0-5]\d)(\.\d+)?$";
        Assert.DoesNotMatch (regexDate, datetime);
        Assert.DoesNotMatch (regexTime, time);
        ScheduleBL sch = new ScheduleBL ();
        Assert.Null (sch.SelectTimeBy(movie_id,datetime,time));
    }
}