using DakarRally.Core.Models;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Common.UnitTests.Models
{
    public class Vehicle_Update
    {
        [Fact]
        public void NullArgument_NullArgumentExceptionThrown()
        {
            //arrange
            int vehicleId = 9;
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateExisting(vehicleId, VehicleType.SportsCar, "FIC206", "ZZ", new DateTime(2010, 5, 4));

            //act & assert
            Assert.Throws<ArgumentNullException>(() => vehicle.Update(null));
        }

        [Fact]
        public void HappyPath_CharacteristicsChanged()
        {
            //arrange
            int vehicleId = 9;
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateExisting(vehicleId, VehicleType.SportsCar, "FIC206", "ZZ", new DateTime(2010, 5, 4));
            Vehicle newVehicle = factory.CreateExisting(5, VehicleType.TerrainCar, "FIC", "ZY", new DateTime(2016, 8, 3));

            //act
            vehicle.Update(newVehicle);

            //assert
            Assert.Equal(vehicleId, vehicle.Id);
            Assert.Equal(100, vehicle.MaxSpeed);
            Assert.Equal(5, vehicle.RepairTime);
            Assert.Equal(3, vehicle.LightMalfunctionProbability);
            Assert.Equal(1, vehicle.HeavyMalfunctionProbability);
            Assert.Equal(VehicleType.TerrainCar, vehicle.VehicleType);
            Assert.Equal("FIC", vehicle.TeamName);
            Assert.Equal("ZY", vehicle.Model);
            Assert.Equal(new DateTime(2016, 8, 3), vehicle.ManufacturingDate);
        }
    }
}