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
    public class Simulator_RemoveVehicle
    {
        [Fact]
        public void PenndingRace_VehicleRemoved()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            Race race = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateExisting(1, 0, "teamName", "model", new DateTime());
            vehicle.Race = race;
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(It.IsAny<int>(), true)).Returns(vehicle);
            mockUnitOfWork.Setup(x => x.VehicleRepository.Delete(It.IsAny<int>()));
            var rallyOptions = Options.Create(new RallyOptions());
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act
            sut.RemoveVehicle(1);

            //assert
            mockUnitOfWork.Verify(r => r.VehicleRepository.Delete(1));
        }

        [Fact]
        public void NullVehicle_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(It.IsAny<int>(), true)).Returns(() => null);
            var rallyOptions = Options.Create(new RallyOptions());
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act & assert
            Assert.Throws<Exception>(() => sut.RemoveVehicle(1));
        }

        [Fact]
        public void NonPendingRace_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            //create running race
            Race race = new Race(2019, 5, 10000);
            race.Start();

            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateExisting(1, 0, "teamName", "model", new DateTime());
            vehicle.Race = race;
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(It.IsAny<int>(), true)).Returns(vehicle);
            var rallyOptions = Options.Create(new RallyOptions());
            Simulator sut = new Simulator(mockUnitOfWork.Object, rallyOptions);

            //act & assert
            Assert.Throws<InvalidOperationException>(() => sut.RemoveVehicle(1));
        }
    }
}