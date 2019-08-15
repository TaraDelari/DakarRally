using DakarRally.Core.Models;
using DakarRally.Core.Contracts;
using System.Linq;

namespace DakarRally.Core.Filters
{
    public class ModelFilter : ISearchFilter<Vehicle>
    {
        public string Model { get; set; }
        public IQueryable<Vehicle> ApplyFilter(IQueryable<Vehicle> query)
        {
            return query.Where(q => q.Model == Model);
        }
    }
}