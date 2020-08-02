using Async_Inn.Models;
using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.Extensions.Primitives;
using System;
using Xunit;

namespace AsyncInnTesting
{
    public class HotelRoomServiceTests : DatabaseTestBase
    {
        private IHotelRoom BuildHotelRoomRepo()
        {
            return new HotelRoomRepository(_db);
        }

        private IRoom BuildRoomRepo()
        {
            return new RoomRepository(_db);
        }

        private IAmenity BuildAmenityRepo()
        {
            return new AmenityRepository(_db);
        }

        [Fact]
        public async void CanGetAHotelRoom()
        {
            //Arrange
            //string testRoomName = "Test Room";
            //int testRoomLayout = 8;

            //HotelRoomDTO testHotelRoomDTO = new HotelRoomDTO()
            //{
            //    HotelId = x.HotelId,
            //    RoomNumber = x.RoomNumber,
            //    RoomId = x.RoomId,
            //    Rate = x.Rate,
            //    PetFriendly = x.PetFriendly
            //})

            //var testRoomRepo = BuildRoomRepo();

            ////Act
            //var savedRoom = await testRoomRepo.Create(testRoomDTO);
            //var gottenRoom = await testRoomRepo.GetRoom(savedRoom.Id, BuildAmenityRepo());

            ////Assert
            //Assert.NotNull(gottenRoom);
            //Assert.NotEqual(0, gottenRoom.Id);
            //Assert.Equal(testRoomName, gottenRoom.Name);
            //Assert.Equal(testRoomLayout, gottenRoom.Layout);

            //Dispose();
        }

        //[Fact]
        //public async void CanGetRooms()
        //{
        //    //Arrange
        //    string testRoomName1 = "Test Room1";
        //    int testRoomLayout1 = 8;
        //    RoomDTO testRoomDTO1 = new RoomDTO
        //    {
        //        Name = testRoomName1,
        //        Layout = testRoomLayout1
        //    };

        //    string testRoomName2 = "Test Room2";
        //    int testRoomLayout2 = 11;
        //    RoomDTO testRoomDTO2 = new RoomDTO
        //    {
        //        Name = testRoomName2,
        //        Layout = testRoomLayout2
        //    };

        //    var testRoomRepo = BuildRoomRepo();

        //    //Act
        //    await testRoomRepo.Create(testRoomDTO1);
        //    await testRoomRepo.Create(testRoomDTO2);
        //    var gottenRooms = await testRoomRepo.GetRooms(BuildAmenityRepo());

        //    //Assert
        //    Assert.NotNull(gottenRooms);
        //    Assert.True(gottenRooms.Count >= 2);

        //    Dispose();
        //}

        //[Fact]
        //public async void CanCreateARoom()
        //{
        //    //Arrange
        //    string testRoomName = "Test Room";
        //    int testRoomLayout = 8;
        //    RoomDTO testRoomDTO = new RoomDTO
        //    {
        //        Name = testRoomName,
        //        Layout = testRoomLayout
        //    };

        //    var testRoomRepo = BuildRoomRepo();

        //    //Act
        //    var savedRoom = await testRoomRepo.Create(testRoomDTO);

        //    //Assert
        //    Assert.NotNull(savedRoom);
        //    Assert.NotEqual(0, savedRoom.Id);
        //    Assert.Equal(testRoomName, savedRoom.Name);
        //    Assert.Equal(testRoomLayout, savedRoom.Layout);

        //    Dispose();
        //}

        //[Fact]
        //public async void CanUpdateARoom()
        //{
        //    //Arrange
        //    string testRoomName = "Test Room";
        //    int testRoomLayout = 8;
        //    RoomDTO testRoomDTO = new RoomDTO
        //    {
        //        Name = testRoomName,
        //        Layout = testRoomLayout
        //    };

        //    var testRoomRepo = BuildRoomRepo();

        //    //Act
        //    var savedRoom = testRoomRepo.Create(testRoomDTO);

        //    string updatedRoomName = "Test Room";
        //    int updatedRoomLayout = 8;
        //    RoomDTO updatedRoomDTO = new RoomDTO
        //    {
        //        Id = savedRoom.Id,
        //        Name = updatedRoomName,
        //        Layout = updatedRoomLayout
        //    };

        //    var updatedRoom = await testRoomRepo.Update(updatedRoomDTO);

        //    //Assert
        //    Assert.NotNull(updatedRoom);
        //    Assert.NotEqual(0, updatedRoom.Id);
        //    Assert.Equal(updatedRoomDTO.Name, updatedRoom.Name);
        //    Assert.Equal(updatedRoomDTO.Layout, updatedRoom.Layout);

        //    Dispose();
        //}


        //[Fact]
        //public async void CanDeleteARoom()
        //{
        //    //Arrange
        //    string testRoomName = "Test Room";
        //    int testRoomLayout = 8;
        //    RoomDTO testRoomDTO = new RoomDTO
        //    {
        //        Name = testRoomName,
        //        Layout = testRoomLayout
        //    };

        //    var testRoomRepo = BuildRoomRepo();

        //    //Act
        //    var savedRoom = await testRoomRepo.Create(testRoomDTO);
        //    await testRoomRepo.Delete(savedRoom.Id);
        //    var nullRoom = await testRoomRepo.GetRoom(savedRoom.Id, BuildAmenityRepo());

        //    //Assert
        //    Assert.Null(nullRoom);

        //    Dispose();
        //}
    }
}
