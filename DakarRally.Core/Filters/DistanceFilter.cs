using DakarRally.Core.Models;
using DakarRally.Core.Contracts;
using System.Linq;

namespace DakarRally.Core.Filters
{
    class DistanceFilter : ISearchFilter<Vehicle>
    {
        public double Distance { get; set; }
        public IQueryable<Vehicle> ApplyFilter(IQueryable<Vehicle> query)
        {
            return query.Where(q => q.DistanceTraveled == Distance);
        }
    }
}