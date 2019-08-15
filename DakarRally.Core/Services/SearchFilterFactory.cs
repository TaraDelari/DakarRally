using DakarRally.Core.Constants;
using DakarRally.Core.Models;
using DakarRally.Core.Contracts;
using DakarRally.Core.Filters;
using System;
using System.Collections.Generic;

namespace DakarRally.Core.Services
{
    class SearchFilterFactory
    {
        public List<ISearchFilter<Vehicle>> GetFilters(string teamName, string model, string manufactoringDate, string status, string distance)
        {
            List <ISearchFilter<Vehicle>> filters = new List<ISearchFilter<Vehicle>>();
            if (!string.IsNullOrWhiteSpace(teamName))
            {
                TeamFilter teamFilter = new TeamFilter
                {
                    TeamName = teamName
                };
                filters.Add(teamFilter);
            }
            if (!string.IsNullOrWhiteSpace(model))
            {
                ModelFilter modelFilter = new ModelFilter
                {
                    Model = model
                };
                filters.Add(modelFilter);
            }
            if (!string.IsNullOrWhiteSpace(manufactoringDate))
            {
                try
                {
                    DateTime date = DateTime.Parse(manufactoringDate);
                    ManufactoringDateFilter dateFilter = new ManufactoringDateFilter
                    {
                        Date = date
                    };
                    filters.Add(dateFilter);
                }
                catch (Exception)
                {
                    throw new Exception(ErrorMessages.INVALID_DATE);
                }

            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                if(!Enum.IsDefined(typeof(VehicleStatus), status))
                    throw new Exception(ErrorMessages.INVALID_STATUS);
                VehicleStatus vehicleStatus = (VehicleStatus)Enum.Parse(typeof(VehicleStatus), status);
                StatusFilter statusFilter = new StatusFilter
                {
                    Status = vehicleStatus
                };
                filters.Add(statusFilter);
            }
            if (!string.IsNullOrWhiteSpace(distance))
            {
                try
                {
                    double distanceTraveled = Convert.ToDouble(distance);
                    DistanceFilter distanceFilter = new DistanceFilter
                    {
                        Distance = distanceTraveled
                    };
                    filters.Add(distanceFilter);
                }
                catch (Exception)
                {
                    throw new Exception(ErrorMessages.INVALID_DISTANCE);
                }
            }
            return filters;
        }
    }
}