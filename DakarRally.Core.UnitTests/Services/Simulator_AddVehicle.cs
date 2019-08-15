using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using DakarRally.Core.Options;
using DakarRally.Core.Services;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.RaceSimulator.UnitTests.Models
{
    public class Simulator_AddVehicle
    {
        [Fact]
        public void ValidRace_VehicleAdded()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 3, 10000);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            var rallyOptions = Options.Create(new RallyOptions());
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());

            //act
            sut.AddVehicle(1, vehicle);

            //assert
            Assert.Single(race.Vehicles);
        }

        [Fact]
        public void NoRace_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(() => null);
            var rallyOptions = Options.Create(new RallyOptions());
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);
            int raceId = 2;

            //act & assert
            Assert.Throws<Exception>(() => sut.AddVehicle(raceId, null));
        }

        [Fact]
        public void NoPendingRace_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            var rallyOptions = Options.Create(new RallyOptions());
            //set running race
            Race race = new Race(2019, 5, 10000);
            race.Start();

            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());

            //act & assert
            Assert.Throws<Exception>(() => sut.AddVehicle(1, vehicle));
        }
    }
}