using DakarRally.Core.Models;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Common.UnitTests.Models
{
    public class Race_AddVehicle
    {
        [Fact]
        public void Null_NullArgumentExceptionThrown()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);

            //act & assert
            Assert.Throws<ArgumentNullException>(() => sut.AddVehicle(null));
        }

        [Fact]
        public void Vehicle_VehicleAdded()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());

            //act
            sut.AddVehicle(vehicle);

            //assert
            Assert.Single(sut.Vehicles);
        }
    }
}