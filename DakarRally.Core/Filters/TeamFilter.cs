using DakarRally.Core.Models;
using DakarRally.Core.Contracts;
using System.Linq;

namespace DakarRally.Core.Filters
{
    public class TeamFilter : ISearchFilter<Vehicle>
    {
        public string TeamName { get; set; }
        public IQueryable<Vehicle> ApplyFilter(IQueryable<Vehicle> query)
        {
            return query.Where(q => q.TeamName == TeamName);
        }
    }
}