using DakarRally.Core.Models;
using System;

namespace DakarRally.Api.DataContracts.DTOs
{
    public class VehicleLeaderboardDTO
    {
        public int Id { get; set; }
        public DateTime? FinishTime { get; set; }
        public double DistanceTraveled { get; set; }
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public VehicleType VehicleType { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
    }
}