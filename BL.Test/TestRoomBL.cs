using System;
using BL;
using Persitence;
using Xunit;

public class TestRoomBL {
    private RoomBL room = new RoomBL ();
    [Fact]
    public void TestGetRoomTrue () {
        Assert.NotNull (room.GetRooms ());
    }

    [Fact]
    public void TestGetRoomByIdtTrue () {
        Assert.NotNull (room.GetRoomById (1));
    }

    [Fact]
    public void TestGetRoomByIdFail () {
        Assert.Null (room.GetRoomById(100));
    }
}