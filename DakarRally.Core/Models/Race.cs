using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Core.Models
{
    public class Race
    {
        public int Id { get; private set; }
        public int Year { get; private set; }
        public int UpdateRate { get; private set; }
        public int Distance { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime UpdateTime { get; private set; }
        public RaceStatus RaceStatus { get; private set; }
        public virtual List<Vehicle> Vehicles { get; private set; }

        public Race(int year, int updateRate, int distance)
        {
            Year = year;
            Vehicles = new List<Vehicle>();
            UpdateRate = updateRate;
            RaceStatus = RaceStatus.Pending;
            Distance = distance;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle));
            Vehicles.Add(vehicle);
        }

        public void Start()
        {
            UpdateTime = DateTime.UtcNow;
            StartTime = UpdateTime;
            RaceStatus = RaceStatus.Running;
        }

        public void Update()
        {
            if (!IsFinished())
            {
                foreach (Vehicle vehicle in Vehicles)
                    vehicle.UpdatePosition();
                UpdateTime = UpdateTime.AddSeconds(UpdateRate);
            }
            else
                RaceStatus = RaceStatus.Finished;
        }

        private bool IsFinished()
        {
            List<Vehicle> stillInRaceVehicles = Vehicles.Where(v => !v.HasFinishedRace()).ToList();

            if (stillInRaceVehicles.Count == 0)
            {
                return true;
            }
            return false;
        }
    }

    public enum RaceStatus
    {
        Pending = 0,
        Running = 1,
        Finished = 2
    }
}