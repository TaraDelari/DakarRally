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
    public class Simulator_Start
    {
        [Fact]
        public void NonExistingRace_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            var rallyOptions = Options.Create(new RallyOptions());
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true));
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act & assert
            Assert.Throws<Exception>(() => sut.Start(1));
        }

        [Fact]
        public void NonPenndingRace_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            var rallyOptions = Options.Create(new RallyOptions());
            //create running race
            Race race = new Race(2019, 5, 10000);
            race.Start();

            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act & assert
            Assert.Throws<Exception>(() => sut.Start(1));
        }

        [Fact]
        public void NoVehicles_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            var rallyOptions = Options.Create(new RallyOptions());
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act & assert
            Assert.Throws<Exception>(() => sut.Start(1));
        }

        [Fact]
        public void HappyPath_RaceStarted()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            var rallyOptions = Options.Create(new RallyOptions());

            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateExisting(1, 0, "teamName", "model", new DateTime());
            race.AddVehicle(vehicle);
            mockUnitOfWork.Setup(x => x.RaceRepository.Get(It.IsAny<int>(), true)).Returns(race);
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act
            sut.Start(1);

            //assert
            Assert.Equal(RaceStatus.Running, race.RaceStatus);
        }
    }
}