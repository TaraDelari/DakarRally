using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using DakarRally.Core.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Core.UnitTests.Services
{
    public class StatsGenerator_GetVehicles
    {
        [Fact]
        public void EmptyParams_EmptyList()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            List<Vehicle> vehicles = new List<Vehicle>();
            VehicleFactory factory = GetFactory();
            Vehicle vehicle1 = factory.CreateExisting(1, 0, "teamName", "model", new DateTime(2010, 5, 5));
            Vehicle vehicle2 = factory.CreateExisting(2, 0, "teamName", "model", new DateTime(2010, 5, 5));
            Vehicle vehicle3 = factory.CreateExisting(3, 0, "teamName", "model", new DateTime(2010, 5, 5));
            Vehicle vehicle4 = factory.CreateExisting(4, 0, "teamName", "model", new DateTime(2010, 5, 5));
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            vehicles.Add(vehicle3);
            vehicles.Add(vehicle4);
            IQueryable<Vehicle> queryableVehicles = vehicles.AsQueryable();
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(true)).Returns(queryableVehicles);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            List<Vehicle> returnedVehicles = sut.GetVehicles("", "", "", "" , "");

            //assert
            Assert.Empty(returnedVehicles);
        }

        [Fact]
        public void AllParams_AscList()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            List<Vehicle> vehicles = new List<Vehicle>();
            VehicleFactory factory = GetFactory();
            Vehicle vehicle1 = factory.CreateExisting(1, 0, "teamName", "model", new DateTime(2010, 5, 5));
            Vehicle vehicle2 = factory.CreateExisting(2, 0, "teamName", "model", new DateTime(2010, 5, 4));
            Vehicle vehicle3 = factory.CreateExisting(3, 0, "teamName1", "model", new DateTime(2010, 5, 5));
            Vehicle vehicle4 = factory.CreateExisting(4, 0, "teamName", "model", new DateTime(2010, 5, 5));
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            vehicles.Add(vehicle3);
            vehicles.Add(vehicle4);
            IQueryable<Vehicle> queryableVehicles = vehicles.AsQueryable();
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(true)).Returns(queryableVehicles);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            List<Vehicle> returnedVehicles = sut.GetVehicles("teamName", "model", "5/5/10", "Pending", "0");

            //assert
            Assert.Equal(2, returnedVehicles.Count);
            Assert.Equal(1, returnedVehicles.First().Id);
            Assert.Equal(4, returnedVehicles.Last().Id);
            Assert.True(returnedVehicles.All(x => x.TeamName == "teamName"));
            Assert.True(returnedVehicles.All(x => x.Model == "model"));
            Assert.True(returnedVehicles.All(x => x.ManufacturingDate == new DateTime(2010, 5, 5)));
            Assert.True(returnedVehicles.All(x => x.VehicleStatus == VehicleStatus.Pending));
            Assert.True(returnedVehicles.All(x => x.DistanceTraveled == 0));
        }

        [Fact]
        public void AllParamsDesc_DescList()
        {
            //arrange
            Mock<IUnitOfWork> mockUnitOfWork = new Mock<IUnitOfWork>();
            List<Vehicle> vehicles = new List<Vehicle>();
            VehicleFactory factory = GetFactory();
            Vehicle vehicle1 = factory.CreateExisting(1, 0, "teamName", "model", new DateTime(2010, 5, 5));
            Vehicle vehicle2 = factory.CreateExisting(2, 0, "teamName", "model", new DateTime(2010, 5, 4));
            Vehicle vehicle3 = factory.CreateExisting(3, 0, "teamName1", "model", new DateTime(2010, 5, 5));
            Vehicle vehicle4 = factory.CreateExisting(4, 0, "teamName", "model", new DateTime(2010, 5, 5));
            vehicles.Add(vehicle1);
            vehicles.Add(vehicle2);
            vehicles.Add(vehicle3);
            vehicles.Add(vehicle4);
            IQueryable<Vehicle> queryableVehicles = vehicles.AsQueryable();
            mockUnitOfWork.Setup(x => x.VehicleRepository.Get(true)).Returns(queryableVehicles);
            StatsGenerator sut = new StatsGenerator(mockUnitOfWork.Object);

            //act
            List<Vehicle> returnedVehicles = sut.GetVehicles("teamName", "model", "5/5/10", "Pending", "0", "desc");

            //assert
            Assert.Equal(2, returnedVehicles.Count);
            Assert.Equal(4, returnedVehicles.First().Id);
            Assert.Equal(1, returnedVehicles.Last().Id);
            Assert.True(returnedVehicles.All(x => x.TeamName == "teamName"));
            Assert.True(returnedVehicles.All(x => x.Model == "model"));
            Assert.True(returnedVehicles.All(x => x.ManufacturingDate == new DateTime(2010, 5, 5)));
            Assert.True(returnedVehicles.All(x => x.VehicleStatus == VehicleStatus.Pending));
            Assert.True(returnedVehicles.All(x => x.DistanceTraveled == 0));
        }
    }
}