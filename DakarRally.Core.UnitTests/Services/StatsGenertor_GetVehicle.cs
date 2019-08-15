using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using DakarRally.Core.Services;
using Moq;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Core.UnitTests.Services
{
    public class StatsGenertor_GetVehicle
    {
        [Fact]
        public void NonExistingVehicle_ExceptionThrown()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(It.IsAny<int>(), true));
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act & assert
            Assert.Throws<Exception>(() => sut.GetVehicle(1));
        }

        [Fact]
        public void Vehicle_VehicleReturned()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(It.IsAny<int>(), true)).Returns(vehicle);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            Vehicle returnedVehicle = sut.GetVehicle(1);

            //assert
            Assert.Equal(vehicle, returnedVehicle);
        }
    }
}
