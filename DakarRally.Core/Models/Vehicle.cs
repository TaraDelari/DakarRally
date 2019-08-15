using DakarRally.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DakarRally.Core.Models
{
    public class Vehicle
    {
        public int Id { get; private set; }
        public int MaxSpeed { get; private set; }
        public int RepairTime { get; private set; }
        public int LightMalfunctionProbability { get; private set; }
        public int HeavyMalfunctionProbability { get; private set; }
        public DateTime? FinishTime { get; private set; }
        public string TeamName { get; private set; }
        public string Model { get; private set; }
        public DateTime ManufacturingDate { get; private set; }
        public VehicleType VehicleType { get; private set; }
        public VehicleStatus VehicleStatus { get; private set; }
        public double DistanceTraveled { get; private set; }
        public int RaceId { get; set; }
        public virtual Race Race { get; set; }
        public virtual List<Malfunction> Malfunctions { get; private set; }

        private Vehicle()
        {

        }

        private Vehicle(string teamName, string model, DateTime manufacturingDate, VehicleType vehicleType, int maxSpeed, int repairTime,
            int lightMalfunctionProbability, int heavyMalfunctionProbability)
        {
            TeamName = teamName;
            Model = model;
            ManufacturingDate = manufacturingDate;
            VehicleType = vehicleType;
            MaxSpeed = maxSpeed;
            RepairTime = repairTime;
            LightMalfunctionProbability = lightMalfunctionProbability;
            HeavyMalfunctionProbability = heavyMalfunctionProbability;
            Malfunctions = new List<Malfunction>();
            VehicleStatus = VehicleStatus.Pending;
        }

        public void Update(Vehicle other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            TeamName = other.TeamName;
            Model = other.Model;
            ManufacturingDate = other.ManufacturingDate;

            VehicleFactory factory = GetFactory();
            Vehicle newVehicle = factory.CreateNew(other.VehicleType, other.TeamName, other.Model, other.ManufacturingDate);
            VehicleType = newVehicle.VehicleType;
            MaxSpeed = newVehicle.MaxSpeed;
            HeavyMalfunctionProbability = newVehicle.HeavyMalfunctionProbability;
            LightMalfunctionProbability = newVehicle.LightMalfunctionProbability;
            RepairTime = newVehicle.RepairTime;
        }

        internal void UpdatePosition()
        {
            if (!HasFinishedRace())
            {
                UpdateStatusIfMalfunctionEnded();
                int elapsedTimeSeconds = (int)(Race.UpdateTime - Race.StartTime).GetValueOrDefault().TotalSeconds;
                //Check if full hour has elapsed
                if (elapsedTimeSeconds % 3600 == 0 && VehicleStatus == VehicleStatus.Running)
                {
                    MalfunctionGenerator malfunctionGenerator = new MalfunctionGenerator();
                    Malfunction malfunction = malfunctionGenerator.Generate(LightMalfunctionProbability, HeavyMalfunctionProbability, Race.UpdateTime);

                    if(malfunction != null)
                    {
                        Malfunctions.Add(malfunction);
                        if(malfunction.MalfunctionType == MalfunctionType.Light)
                            VehicleStatus = VehicleStatus.LightMalfunction;
                        else
                            VehicleStatus = VehicleStatus.HeavyMalfunction;
                    }
                    else
                    {
                        VehicleStatus = VehicleStatus.Running;
                        UpdateDistance();
                    }
                }
                else
                {
                    if (!IsMalfunctioning())
                    {
                        VehicleStatus = VehicleStatus.Running;
                        UpdateDistance();
                    }
                }
            }
        }

        public bool HasFinishedRace()
        {
            return VehicleStatus == VehicleStatus.Finished || VehicleStatus == VehicleStatus.HeavyMalfunction;
        }

        private void UpdateStatusIfMalfunctionEnded()
        {
            Malfunction lastLightMalfunction = GetLastLightMalfunction();
            if (lastLightMalfunction != null && lastLightMalfunction.Time.AddHours(RepairTime) < Race.UpdateTime)
            {
                VehicleStatus = VehicleStatus.Running;
            }
        }

        private Malfunction GetLastLightMalfunction()
        {
            if (Malfunctions.Count > 0)
                return Malfunctions.Where(m => m.MalfunctionType == MalfunctionType.Light).OrderByDescending(m => m.Time).FirstOrDefault();
            return null;
        }

        private bool IsMalfunctioning()
        {
            return VehicleStatus == VehicleStatus.HeavyMalfunction || VehicleStatus == VehicleStatus.LightMalfunction;
        }

        private void UpdateDistance()
        {
            double maxSpeedKMSec = MaxSpeed / 3600.0;
            double KMPerInterval = maxSpeedKMSec * Race.UpdateRate;
            double remainingDistance = Race.Distance - DistanceTraveled;

            if (remainingDistance <= KMPerInterval)
            {
                double SecToFinish = remainingDistance / maxSpeedKMSec;
                DistanceTraveled += remainingDistance;
                FinishTime = Race.UpdateTime.AddSeconds(SecToFinish);
                VehicleStatus = VehicleStatus.Finished;
            }
            else
            {
                DistanceTraveled += KMPerInterval;
            }
        }

        public static VehicleFactory GetFactory() => new VehicleFactory();

        public class VehicleFactory
        {
            public Vehicle CreateNew(VehicleType type, string teamName, string model, DateTime manufacturingDate)
            {
                switch (type)
                {
                    case VehicleType.SportsCar:
                        return new Vehicle(teamName, model, manufacturingDate, VehicleType.SportsCar, 140, 5, 12, 2);
                    case VehicleType.TerrainCar:
                        return new Vehicle(teamName, model, manufacturingDate, VehicleType.TerrainCar, 100, 5, 3, 1);
                    case VehicleType.Truck:
                        return new Vehicle(teamName, model, manufacturingDate, VehicleType.Truck, 80, 7, 6, 4);
                    case VehicleType.SportMotorcycle:
                        return new Vehicle(teamName, model, manufacturingDate, VehicleType.SportMotorcycle, 130, 3, 18, 10);
                    case VehicleType.CrossMotorcycle:
                        return new Vehicle(teamName, model, manufacturingDate, VehicleType.CrossMotorcycle, 85, 3, 3, 2);
                    default:
                        throw new InvalidOperationException();
                }
            }

            public Vehicle CreateExisting(int id, VehicleType type, string teamName, string model, DateTime manufacturingDate)
            {
                Vehicle vehicle;
                switch (type)
                {
                    case VehicleType.SportsCar:
                        vehicle = new Vehicle(teamName, model, manufacturingDate, VehicleType.SportsCar, 140, 5, 12, 2)
                        {
                            Id = id
                        };
                        break;
                    case VehicleType.TerrainCar:
                        vehicle = new Vehicle(teamName, model, manufacturingDate, VehicleType.TerrainCar, 100, 5, 3, 1)
                        {
                            Id = id
                        };
                        break;
                    case VehicleType.Truck:
                        vehicle = new Vehicle(teamName, model, manufacturingDate, VehicleType.Truck, 80, 7, 6, 4)
                        {
                            Id = id
                        };
                        break;
                    case VehicleType.SportMotorcycle:
                        vehicle = new Vehicle(teamName, model, manufacturingDate, VehicleType.SportMotorcycle, 130, 3, 18, 10)
                        {
                            Id = id
                        };
                        break;
                    case VehicleType.CrossMotorcycle:
                        vehicle = new Vehicle(teamName, model, manufacturingDate, VehicleType.CrossMotorcycle, 85, 3, 3, 2)
                        {
                            Id = id
                        };
                        break;
                    default:
                        throw new InvalidOperationException();
                }
                return vehicle;
            }
        }
    }

    public enum VehicleType
    {
        SportsCar = 0,
        TerrainCar = 1,
        Truck = 2,
        CrossMotorcycle = 3,
        SportMotorcycle = 4
    }

    public enum VehicleStatus
    {
        Pending = 0,
        Running = 1,
        LightMalfunction = 2,
        HeavyMalfunction = 3,
        Finished = 4
    }
}