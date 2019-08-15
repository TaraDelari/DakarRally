using DakarRally.Api.DataContracts.DTOs;
using DakarRally.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Api.Adapters
{
    public class LeaderboardAdapter
    {
        public LeaderboardDTO Adapt(Leaderboard leaderboard)
        {
            LeaderboardDTO leaderboardDTO = new LeaderboardDTO();
            foreach (KeyValuePair<int, List<Vehicle>> entry in leaderboard.Vehicles)
            {
                leaderboardDTO.Vehicles[entry.Key] = entry.Value.Select(x => FromDomain(x)).ToList();
            }
            return leaderboardDTO;
        }

        public VehicleLeaderboardDTO FromDomain(Vehicle vehicle)
        {
            VehicleLeaderboardDTO vehicleLeaderboard = new VehicleLeaderboardDTO()
            {
                Id = vehicle.Id,
                FinishTime = vehicle.FinishTime,
                DistanceTraveled = vehicle.DistanceTraveled,
                TeamName = vehicle.TeamName,
                Model = vehicle.Model,
                ManufacturingDate = vehicle.ManufacturingDate,
                VehicleType = vehicle.VehicleType,
                VehicleStatus = vehicle.VehicleStatus
            };
            return vehicleLeaderboard;
        }
    }
}