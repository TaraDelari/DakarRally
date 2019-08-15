using DakarRally.Api.DataContracts.DTOs;
using DakarRally.Api.DataContracts.In;
using DakarRally.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static DakarRally.Core.Models.Vehicle;

namespace DakarRally.Api.Adapters
{
    public class VehicleAdapter
    {
        public Vehicle AdaptNew(VehicleRequest vehicleRequest)
        {

            DateTime date = DateTime.Parse(vehicleRequest.ManufacturingDate);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateNew((VehicleType)vehicleRequest.VehicleType, vehicleRequest.TeamName, vehicleRequest.Model, date);
            return vehicle;
        }

        public Vehicle AdaptUpdate(UpdateVehicleRequest vehicleRequest)
        {
            DateTime date = DateTime.Parse(vehicleRequest.ManufacturingDate);
            VehicleFactory factory = GetFactory();
            Vehicle vehicle = factory.CreateExisting(vehicleRequest.Id, (VehicleType)vehicleRequest.VehicleType, vehicleRequest.TeamName, vehicleRequest.Model, date);
            return vehicle;
        }

        public VehicleStatDTO ToVehicleStatDTO(Vehicle vehicle)
        {
            VehicleStatDTO vehicleStat = new VehicleStatDTO()
            {
                Id = vehicle.Id,
                FinishTime = vehicle.FinishTime,
                DistanceTraveled = vehicle.DistanceTraveled,
                VehicleStatus = vehicle.VehicleStatus
            };
            vehicleStat.Malfunctions = vehicle.Malfunctions.Select(malfunction => new MalfunctionStatDTO
            {
                Time = malfunction.Time,
                MalfunctionType = malfunction.MalfunctionType
            }).ToList();
            return vehicleStat;
        }

        public List<VehicleDTO> ToVehicleDTO(List<Vehicle> vehicle)
        {
            List<VehicleDTO> vehicleDTOs = new List<VehicleDTO>();
            foreach(Vehicle vehicleItem in vehicle)
            {
                VehicleDTO vehicleDTO = new VehicleDTO()
                {
                    Id = vehicleItem.Id,
                    TeamName = vehicleItem.TeamName,
                    ManufactoringDate = vehicleItem.ManufacturingDate.ToShortDateString(),
                    Model = vehicleItem.Model,
                    Status = vehicleItem.VehicleStatus.ToString(),
                    VehicleType = vehicleItem.VehicleType.ToString(),
                    Distance = vehicleItem.DistanceTraveled.ToString()
                };
                vehicleDTOs.Add(vehicleDTO);
            }
            return vehicleDTOs;
        }
    }
}