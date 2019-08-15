using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using DakarRally.Core.Services;
using Moq;
using System;
using System.Linq;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Core.UnitTests.Services
{
    public class StatsGenerator_GetRaceStatus
    {
        [Fact]
        public void NonExistingRace_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true));
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act & assert
            Assert.Throws<Exception>(() => sut.GetRaceStatus(1));
        }

        [Fact]
        public void NoVehicles_ZeroVehiclesByStatusAndType()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            RaceStatusStat stat = sut.GetRaceStatus(1);

            //assert
            Assert.Equal(0, stat.Id);
            Assert.Equal(RaceStatus.Pending, stat.Status);
            Assert.True(stat.VehiclesByStatus.All(x => x.Value == 0));
            Assert.True(stat.VehiclesByType.All(x => x.Value == 0));
        }

        [Fact]
        public void Vehicles_VehiclesByStatusAndType()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle1 = factory.CreateNew(0, "teamName", "model", new DateTime());
            Vehicle vehicle2 = factory.CreateNew(VehicleType.Truck, "teamName", "model", new DateTime());
            race.AddVehicle(vehicle1);
            race.AddVehicle(vehicle2);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            RaceStatusStat stat = sut.GetRaceStatus(1);

            //assert
            Assert.Equal(0, stat.Id);
            Assert.Equal(RaceStatus.Pending, stat.Status);
            Assert.Equal(2, stat.VehiclesByStatus[VehicleStatus.Pending.ToString()]);
            Assert.Equal(1, stat.VehiclesByType[VehicleType.SportsCar.ToString()]);
            Assert.Equal(1, stat.VehiclesByType[VehicleType.Truck.ToString()]);
        }
    }
}