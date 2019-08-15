using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using DakarRally.Core.Services;
using Moq;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Stats.UnitTests.Models
{
    public class StatsGenerator_GetRaceLeaderboard
    {
        [Fact]
        public void NonExistingRace_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true));
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act & assert
            Assert.Throws<Exception>(() => sut.GetRaceLeaderboard(1));
        }

        [Fact]
        public void NoVehicles_EmptyLeaderboard()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            Leaderboard leaderboard = sut.GetRaceLeaderboard(1);

            //assert
            Assert.Empty(leaderboard.Vehicles);
        }

        [Fact]
        public void HappyPath_Leaderboard()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle1 = factory.CreateNew(0, "teamName", "model", new DateTime());
            Vehicle vehicle2 = factory.CreateNew(0, "teamName", "model", new DateTime());
            race.AddVehicle(vehicle1);
            race.AddVehicle(vehicle2);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            Leaderboard leaderboard = sut.GetRaceLeaderboard(1);

            //assert
            Assert.Equal(2, leaderboard.Vehicles[1].Count);
        }

        [Fact]
        public void NonExistingRace_ExceptionThrown_ByType()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true));
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act & assert
            Assert.Throws<Exception>(() => sut.GetRaceLeaderboard(1, 0));
        }

        [Fact]
        public void NonExistingType_ExceptionThrown_ByType()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act & assert
            Assert.Throws<Exception>(() => sut.GetRaceLeaderboard(1, 6));
        }

        [Fact]
        public void NoVehicles_EmptyLeaderboard_ByType()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            Leaderboard leaderboard = sut.GetRaceLeaderboard(1, 0);

            //assert
            Assert.Empty(leaderboard.Vehicles);
        }

        [Fact]
        public void NoVehicleType_EmptyLeaderboard_ByType()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle1 = factory.CreateNew(0, "teamName", "model", new DateTime());
            Vehicle vehicle2 = factory.CreateNew(VehicleType.TerrainCar, "teamName", "model", new DateTime());
            race.AddVehicle(vehicle1);
            race.AddVehicle(vehicle2);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            Leaderboard leaderboard = sut.GetRaceLeaderboard(1, 2);

            //assert
            Assert.Empty(leaderboard.Vehicles);
        }

        [Fact]
        public void HappyPath_Leaderboard_ByType()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle1 = factory.CreateNew(0, "teamName", "model", new DateTime());
            Vehicle vehicle2 = factory.CreateNew(VehicleType.TerrainCar, "teamName", "model", new DateTime());
            race.AddVehicle(vehicle1);
            race.AddVehicle(vehicle2);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            Leaderboard leaderboard = sut.GetRaceLeaderboard(1, 0);

            //assert
            Assert.Single(leaderboard.Vehicles[1]);
        }
    }
}