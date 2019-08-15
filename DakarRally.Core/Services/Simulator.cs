using DakarRally.Core.Constants;
using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using DakarRally.Core.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Core.Services
{
    public class Simulator
    {
        readonly IUnitOfWork uow;
        readonly RallyOptions rallyOptions;

        public Simulator(IUnitOfWork uow, IOptions<RallyOptions> rallyOptionsAccessor)
        {
            this.uow = uow;
            rallyOptions = rallyOptionsAccessor.Value;
        }

        public int Create(int year)
        {
            Race race = new Race(year, rallyOptions.UpdateRate, rallyOptions.Distance);
            uow.RaceRepository.Insert(race);
            uow.SaveChanges();
            return race.Id;
        }

        public int AddVehicle(int raceId, Vehicle vehicle)
        {
            Race race = uow.RaceRepository.Get(raceId, true);
            if (race == null || race.RaceStatus != RaceStatus.Pending)
                throw new Exception(ErrorMessages.PENDING_RACE_NOT_FOUND);
            race.AddVehicle(vehicle);
            uow.SaveChanges();
            return vehicle.Id;
        }

        public void RemoveVehicle(int vehicleId)
        {
            Vehicle vehicle = uow.VehicleRepository.Get(vehicleId, true);
            if (vehicle == null)
            {
                throw new Exception(ErrorMessages.VEHICLE_NOT_FOUND);
            }
            if (vehicle.Race.RaceStatus != RaceStatus.Pending)
            {
                throw new InvalidOperationException(ErrorMessages.VEHICLE_REMOVAL_FORBBIDEN);
            }
            uow.VehicleRepository.Delete(vehicleId);
            uow.SaveChanges();
        }

        public void UpdateVehicle(Vehicle updatedVehicle)
        {
            Vehicle vehicle = uow.VehicleRepository.Get(updatedVehicle.Id, true);
            if (vehicle == null)
            {
                throw new Exception(ErrorMessages.VEHICLE_NOT_FOUND);
            }
            if (vehicle.Race.RaceStatus != RaceStatus.Pending)
            {
                throw new InvalidOperationException(ErrorMessages.VEHICLE_UPDATE_FORBBIDEN);
            }
            vehicle.Update(updatedVehicle);
            uow.SaveChanges();
        }

        public void Start(int raceId)
        {
            Race race = uow.RaceRepository.Get(raceId, true);
            if (race == null || race.RaceStatus != RaceStatus.Pending)
                throw new Exception(ErrorMessages.PENDING_RACE_NOT_FOUND);
            else if (race.Vehicles.Count == 0)
                throw new Exception(ErrorMessages.NO_VEHICLES_IN_RACE);
            race.Start();
            uow.SaveChanges();
        }

        public void Update()
        {
            List<Race> races = uow.RaceRepository.Get(true).Where(r => r.RaceStatus == RaceStatus.Running).ToList();
            foreach (Race race in races)
            {
                race.Update();
            }
            uow.SaveChanges();
        }
    }
}