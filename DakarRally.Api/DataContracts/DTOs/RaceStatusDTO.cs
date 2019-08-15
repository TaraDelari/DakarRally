using System.Collections.Generic;

namespace DakarRally.Api.DataContracts.DTOs
{
    public class RaceStatusDTO
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public Dictionary<string, int> VehiclesByStatus { get; set; }
        public Dictionary<string , int> VehiclesByType { get; set; }
    }
}