using Async_Inn.Models.DTOs;
using Async_Inn.Models.Interfaces;
using Async_Inn.Models.Services;
using Microsoft.Extensions.Primitives;
using System;
using Xunit;

namespace AsyncInnTesting
{
    public class AmenitiesServiceTests : DatabaseTestBase
    {
        private IAmenity BuildAmenityRepo()
        {
            return new AmenityRepository(_db);
        }

        [Fact]
        public async void CanGetAnAmenity()
        {
            //Arrange
            string testAmenity = "Test Amenity";
            AmenityDTO testAmenityDTO = new AmenityDTO
            {
                Name = testAmenity
            };

            var testAmenityRepo = BuildAmenityRepo();

            //Act
            var savedAmenity = await testAmenityRepo.Create(testAmenityDTO);
            var gottenAmenity = await testAmenityRepo.GetAmenity(savedAmenity.Id);

            //Assert
            Assert.NotNull(gottenAmenity);
            Assert.NotEqual(0, gottenAmenity.Id);
            Assert.Equal(testAmenityDTO.Name, gottenAmenity.Name);

            Dispose();
        }

        [Fact]
        public async void CanGetListOfAmenities()
        {
            //Arrange
            string testAmenity1 = "Test Amenity1";
            AmenityDTO testAmenityDTO1 = new AmenityDTO
            {
                Name = testAmenity1
            };
            string testAmenity2 = "Test Amenity2";
            AmenityDTO testAmenityDTO2 = new AmenityDTO
            {
                Name = testAmenity2
            };

            var testAmenityRepo = BuildAmenityRepo();

            //Act
            await testAmenityRepo.Create(testAmenityDTO1);
            await testAmenityRepo.Create(testAmenityDTO2);

            var gottenAmenities = await testAmenityRepo.GetAmenities();

            //Assert
            Assert.NotNull(gottenAmenities);
            Assert.True(gottenAmenities.Count >= 2);

            Dispose();
        }

        [Fact]
        public async void CanCreateAnAmenity()
        {
            //Arrange
            string testAmenity = "Test Amenity";
            AmenityDTO testAmenityDTO = new AmenityDTO
            {
                Name = testAmenity
            };

            var testAmenityRepo = BuildAmenityRepo();

            //Act
            var savedAmenity = await testAmenityRepo.Create(testAmenityDTO);

            //Assert
            Assert.NotNull(savedAmenity);
            Assert.NotEqual(0, savedAmenity.Id);
            Assert.Equal(testAmenityDTO.Name, savedAmenity.Name);

            Dispose();
        }

        [Fact]
        public async void CanUpdateAmenity()
        {
            //Arrange
            string testAmenity = "Test Amenity";
            AmenityDTO testAmenityDTO = new AmenityDTO
            {
                Name = testAmenity
            };

            var testAmenityRepo = BuildAmenityRepo();

            //Act
            var savedAmenity = testAmenityRepo.Create(testAmenityDTO);
            string updatedTestAmenityName = "Updated Name";
            AmenityDTO updatedTestAmenityDTO = new AmenityDTO
            {
                Id = savedAmenity.Id,
                Name = updatedTestAmenityName
            };
            var updatedAmenity = await testAmenityRepo.Update(updatedTestAmenityDTO);

            //Assert
            Assert.NotNull(updatedAmenity);
            Assert.NotEqual(0, updatedAmenity.Id);
            Assert.Equal(updatedTestAmenityName, updatedAmenity.Name);
            Assert.Equal(savedAmenity.Id, updatedAmenity.Id);

            Dispose();
        }

        [Fact]
        public async void CanDeleteAmenity()
        {
            //Arrange
            string testAmenity = "Test Amenity";
            AmenityDTO testAmenityDTO = new AmenityDTO
            {
                Name = testAmenity
            };

            var testAmenityRepo = BuildAmenityRepo();

            //Act
            var savedAmenity = testAmenityRepo.Create(testAmenityDTO);
            await testAmenityRepo.Delete(savedAmenity.Id);
            var nullAmenity = await testAmenityRepo.GetAmenity(savedAmenity.Id);

            //Assert
            Assert.Null(nullAmenity);

            Dispose();
        }
    }
}
