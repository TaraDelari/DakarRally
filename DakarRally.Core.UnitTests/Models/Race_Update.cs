using DakarRally.Core.Models;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Common.UnitTests.Models
{
    public class Race_Update
    {
        [Fact]
        public void HappyPath_UpdateTimeChanged()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());
            sut.AddVehicle(vehicle);
            vehicle.Race = sut;
            sut.Start();

            //act
            sut.Update();

            //assert
            Assert.True(sut.UpdateTime.AddSeconds(-5) == sut.StartTime);
        }

        [Fact]
        public void HappyPath_RaceFinished()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());
            sut.AddVehicle(vehicle);
            vehicle.Race = sut;
            sut.Start();

            //act
            //setting race status to finished
            while (sut.RaceStatus != RaceStatus.Finished)
                sut.Update();

            //assert
            Assert.Equal(RaceStatus.Finished, sut.RaceStatus);
        }
    }
}