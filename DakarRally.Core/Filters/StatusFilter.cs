using DakarRally.Core.Models;
using DakarRally.Core.Contracts;
using System.Linq;

namespace DakarRally.Core.Filters
{
    class StatusFilter : ISearchFilter<Vehicle>
    {
        public VehicleStatus Status { get; set; }
        public IQueryable<Vehicle> ApplyFilter(IQueryable<Vehicle> query)
        {
            return query.Where(q => q.VehicleStatus == Status);
        }
    }
}