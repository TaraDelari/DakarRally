using DakarRally.Core.Models;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Common.UnitTests.Models
{
    public class Vehicle_VehicleFactory_CreateExisting
    {
        [Fact]
        public void SportsCar_SportsCarCharacteristics()
        {
            //arrange
            int vehicleId = 9;
            VehicleType type = VehicleType.SportsCar;
            string teamName = "FIC206";
            string model = "ZZ";
            DateTime manufacturingDate = new DateTime(2010, 5, 4);
            VehicleFactory factory = GetFactory();

            //act
            Vehicle vehicle = factory.CreateExisting(vehicleId, type, teamName, model, manufacturingDate);

            //assert
            Assert.Equal(9, vehicle.Id);
            Assert.Equal(140, vehicle.MaxSpeed);
            Assert.Equal(5, vehicle.RepairTime);
            Assert.Equal(12, vehicle.LightMalfunctionProbability);
            Assert.Equal(2, vehicle.HeavyMalfunctionProbability);
        }

        [Fact]
        public void TerrainCar_TerrainCarCharacteristics()
        {
            //arrange
            int vehicleId = 9;
            VehicleType type = VehicleType.TerrainCar;
            string teamName = "TT02";
            string model = "Top";
            DateTime manufacturingDate = new DateTime(2005, 1, 1);
            VehicleFactory factory = GetFactory();

            //act
            Vehicle vehicle = factory.CreateExisting(vehicleId, type, teamName, model, manufacturingDate);

            //assert
            Assert.Equal(9, vehicle.Id);
            Assert.Equal(100, vehicle.MaxSpeed);
            Assert.Equal(5, vehicle.RepairTime);
            Assert.Equal(3, vehicle.LightMalfunctionProbability);
            Assert.Equal(1, vehicle.HeavyMalfunctionProbability);
        }

        [Fact]
        public void Truck_TruckCharacteristics()
        {
            //arrange
            int vehicleId = 9;
            VehicleType type = VehicleType.Truck;
            string teamName = "MaxPower";
            string model = "Truckers";
            DateTime manufacturingDate = new DateTime(2000, 10, 1);
            VehicleFactory factory = GetFactory();

            //act
            Vehicle vehicle = factory.CreateExisting(vehicleId, type, teamName, model, manufacturingDate);

            //assert
            Assert.Equal(9, vehicle.Id);
            Assert.Equal(80, vehicle.MaxSpeed);
            Assert.Equal(7, vehicle.RepairTime);
            Assert.Equal(6, vehicle.LightMalfunctionProbability);
            Assert.Equal(4, vehicle.HeavyMalfunctionProbability);
        }

        [Fact]
        public void SportMotorcycle_SportMotorcycleCharacteristics()
        {
            //arrange
            int vehicleId = 9;
            VehicleType type = VehicleType.SportMotorcycle;
            string teamName = "VVTop";
            string model = "Xunit";
            DateTime manufacturingDate = new DateTime(2015, 9, 18);
            VehicleFactory factory = GetFactory();

            //act
            Vehicle vehicle = factory.CreateExisting(vehicleId, type, teamName, model, manufacturingDate);

            //assert
            Assert.Equal(9, vehicle.Id);
            Assert.Equal(130, vehicle.MaxSpeed);
            Assert.Equal(3, vehicle.RepairTime);
            Assert.Equal(18, vehicle.LightMalfunctionProbability);
            Assert.Equal(10, vehicle.HeavyMalfunctionProbability);
        }

        [Fact]
        public void CrossMotorcycle_CrossMotorcycleCharacteristics()
        {
            //arrange
            int vehicleId = 9;
            VehicleType type = VehicleType.CrossMotorcycle;
            string teamName = "Terminator";
            string model = "Beat3000";
            DateTime manufacturingDate = new DateTime(2018, 10, 16);
            VehicleFactory factory = GetFactory();

            //act
            Vehicle vehicle = factory.CreateExisting(vehicleId, type, teamName, model, manufacturingDate);

            //assert
            Assert.Equal(9, vehicle.Id);
            Assert.Equal(85, vehicle.MaxSpeed);
            Assert.Equal(3, vehicle.RepairTime);
            Assert.Equal(3, vehicle.LightMalfunctionProbability);
            Assert.Equal(2, vehicle.HeavyMalfunctionProbability);
        }

        [Fact]
        public void InvalidType_InvalidOperationThrow()
        {
            //arrange
            int vehicleId = 9;
            VehicleType type = (VehicleType)9;
            string teamName = "Terminator";
            string model = "Beat3000";
            DateTime manufacturingDate = new DateTime(2018, 10, 16);
            VehicleFactory factory = GetFactory();

            //act & assert
            Assert.Throws<InvalidOperationException>(() => factory.CreateExisting(vehicleId, type, teamName, model, manufacturingDate));
        }
    }
}