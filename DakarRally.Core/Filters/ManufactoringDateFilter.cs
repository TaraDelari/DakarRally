using DakarRally.Core.Models;
using DakarRally.Core.Contracts;
using System;
using System.Linq;

namespace DakarRally.Core.Filters
{
    class ManufactoringDateFilter : ISearchFilter<Vehicle>
    {
        public DateTime Date { get; set; }
        public IQueryable<Vehicle> ApplyFilter(IQueryable<Vehicle> query)
        {
            return query.Where(q => q.ManufacturingDate == Date);
        }
    }
}