using System.Collections.Generic;

namespace DakarRally.Api.DataContracts.DTOs
{
    public class LeaderboardDTO
    {
        public Dictionary<int, List<VehicleLeaderboardDTO>> Vehicles { get; set; }

        public LeaderboardDTO()
        {
            Vehicles = new Dictionary<int, List<VehicleLeaderboardDTO>>();
        }
    }
}