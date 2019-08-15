using System;
using System.Collections.Generic;

namespace DakarRally.Core.Models
{
    public class RaceStatusStat
    {
        public int Id { get; set; }
        public RaceStatus Status { get; set; }
        public Dictionary<string, int> VehiclesByStatus { get; set; }
        public Dictionary<string, int> VehiclesByType { get; set; }

        public RaceStatusStat(int id, RaceStatus status)
        {
            Id = id;
            Status = status;
            VehiclesByType = new Dictionary<string, int>();
            VehiclesByStatus = new Dictionary<string, int>();
            foreach (var type in (VehicleType[])Enum.GetValues(typeof(VehicleType)))
                VehiclesByType[type.ToString()] = 0;
            foreach (var stauts in (VehicleStatus[])Enum.GetValues(typeof(VehicleStatus)))
                VehiclesByStatus[stauts.ToString()] = 0;
        }
    }
}