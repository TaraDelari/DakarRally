using DakarRally.Api.DataContracts.DTOs;
using DakarRally.Core.Models;

namespace DakarRally.Api.Adapters
{
    public class RaceStatusAdapter
    {
        public RaceStatusDTO Adapt(RaceStatusStat raceStatus)
        {
            RaceStatusDTO raceStatusDTO = new RaceStatusDTO
            {
                Id = raceStatus.Id,
                Status = raceStatus.Status.ToString(),
                VehiclesByType = raceStatus.VehiclesByType,
                VehiclesByStatus = raceStatus.VehiclesByStatus
            };
            return raceStatusDTO;
        }
    }
}