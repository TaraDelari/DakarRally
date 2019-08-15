using DakarRally.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Core.Services
{
    public class LeaderboardGenerator
    {
        public Leaderboard Generate(List<Vehicle> vehicles)
        {
            Leaderboard leaderboard = new Leaderboard();
            vehicles = vehicles.OrderBy(x => x.FinishTime).OrderByDescending(x => x.DistanceTraveled).ToList();
            int position = 1;
            foreach (Vehicle vehicle in vehicles)
            {
                if (position == 1)
                {
                    List<Vehicle> vehicleList = new List<Vehicle>
                    {
                        vehicle
                    };
                    leaderboard.Vehicles[position] = vehicleList;
                    position++;
                }
                else if (vehicle.VehicleStatus == VehicleStatus.Finished && leaderboard.Vehicles[position - 1].First().FinishTime != vehicle.FinishTime)
                {
                    List<Vehicle> vehicleList = new List<Vehicle>
                    {
                        vehicle
                    };
                    leaderboard.Vehicles[position] = vehicleList;
                    position++;
                }
                else if (vehicle.VehicleStatus != VehicleStatus.Finished && leaderboard.Vehicles[position - 1].First().DistanceTraveled != vehicle.DistanceTraveled)
                {
                    List<Vehicle> vehicleList = new List<Vehicle>
                    {
                        vehicle
                    };
                    leaderboard.Vehicles[position] = vehicleList;
                    position++;
                }
                else
                    leaderboard.Vehicles[position - 1].Add(vehicle);

            }
            return leaderboard;

        }
    }
}