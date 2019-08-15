using DakarRally.Core.Models;
using System;
using System.Collections.Generic;

namespace DakarRally.Api.DataContracts.DTOs
{
    public class VehicleStatDTO
    {
        public int Id { get; set; }
        public DateTime? FinishTime { get; set; }
        public VehicleStatus VehicleStatus { get; set; }
        public double DistanceTraveled { get; set; }
        public virtual List<MalfunctionStatDTO> Malfunctions { get; set; }
    }
}