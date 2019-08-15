using DakarRally.Core.Constants;
using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Core.Services
{
    public class StatsGenerator
    {
        readonly IUnitOfWork uow;

        public StatsGenerator(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public Leaderboard GetRaceLeaderboard(int raceId)
        {
            Race race = uow.RaceRepository.Get(raceId, true);
            if (race == null)
                throw new Exception(ErrorMessages.RACE_NOT_FOUND);

            List<Vehicle> vehicles = race.Vehicles.ToList();
            LeaderboardGenerator generator = new LeaderboardGenerator();
            Leaderboard leaderboard = generator.Generate(vehicles);
            return leaderboard;
        }

        public Leaderboard GetRaceLeaderboard(int raceId, int type)
        {
            Race race = uow.RaceRepository.Get(raceId, true);
            if (race == null)
                throw new Exception(ErrorMessages.RACE_NOT_FOUND);
            if(!Enum.IsDefined(typeof(VehicleType), type))
                throw new Exception(ErrorMessages.VEHICLE_TYPE_FORBBIDEN);

            List<Vehicle> vehicles = race.Vehicles.Where(x => x.VehicleType == (VehicleType)type).ToList();
            LeaderboardGenerator generator = new LeaderboardGenerator();
            Leaderboard leaderboard = generator.Generate(vehicles);
            return leaderboard;
        }

        public RaceStatusStat GetRaceStatus(int raceId)
        {
            Race race = uow.RaceRepository.Get(raceId, true);
            if (race == null)
                throw new Exception(ErrorMessages.RACE_NOT_FOUND);
            RaceStatusStat raceStatus = new RaceStatusStat(race.Id, race.RaceStatus);
            foreach(Vehicle vehicle in race.Vehicles)
            {
                raceStatus.VehiclesByType[vehicle.VehicleType.ToString()] += 1;
                raceStatus.VehiclesByStatus[vehicle.VehicleStatus.ToString()] += 1;
            }
            return raceStatus;     
        }

        public List<Vehicle> GetVehicles(string teamName, string model, string manufactoringDate, string status, string distance, string orderBy = "asc")
        {
            IQueryable<Vehicle> vehiclesQuery = uow.VehicleRepository.Get(true);
            SearchFilterFactory filterFactory = new SearchFilterFactory();
            List<ISearchFilter<Vehicle>> filters = filterFactory.GetFilters(teamName, model, manufactoringDate, status, distance);
            if (filters.Count == 0)
                return new List<Vehicle>();

            foreach(var filter in filters)
            {
                vehiclesQuery = filter.ApplyFilter(vehiclesQuery);
            }
            if (orderBy == "desc")
                vehiclesQuery = vehiclesQuery.OrderByDescending(x => x.Id);

            return vehiclesQuery.ToList();
        }

        public Vehicle GetVehicle(int vehicleId)
        {
            Vehicle vehicle = uow.VehicleRepository.Get(vehicleId, true);
            if (vehicle == null)
            {
                throw new Exception(ErrorMessages.VEHICLE_NOT_FOUND);
            }
            return vehicle;
        }
    }
}