using DakarRally.Core.Models;
using System.Collections.Generic;

namespace DakarRally.Core.Models
{
    public class Leaderboard
    {
        public Dictionary<int, List<Vehicle>> Vehicles { get; set; }

        public Leaderboard()
        {
            Vehicles = new Dictionary<int, List<Vehicle>>();
        }
    }
}