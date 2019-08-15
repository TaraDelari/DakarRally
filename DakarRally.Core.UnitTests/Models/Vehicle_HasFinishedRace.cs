using DakarRally.Core.Models;
using System;
using Xunit;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Common.UnitTests.Models
{
    public class Vehicle_HasFinishedRace
    {
        [Fact]
        public void Pending_False()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());
            sut.AddVehicle(vehicle);
            vehicle.Race = sut;

            //act
            bool finishedRace = vehicle.HasFinishedRace(); 

            //assert
            Assert.False(finishedRace);
        }

        [Fact]
        public void Running_False()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());
            sut.AddVehicle(vehicle);
            vehicle.Race = sut;
            sut.Start();

            //act
            bool finishedRace = vehicle.HasFinishedRace();

            //assert
            Assert.False(finishedRace);
        }

        [Fact]
        public void FinishedOrHeavyMalfunction_True()
        {
            //arrange
            Race sut = new Race(2019, 5, 10000);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew(0, "teamName", "model", new DateTime());
            sut.AddVehicle(vehicle);
            vehicle.Race = sut;
            sut.Start();
            //setting race status to finished
            while (sut.RaceStatus != RaceStatus.Finished)
                sut.Update();

            //act
            bool finishedRace = vehicle.HasFinishedRace();

            //assert
            Assert.True(finishedRace);
        }
    }
}