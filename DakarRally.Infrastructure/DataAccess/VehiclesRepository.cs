using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DakarRally.Infrastructure.DataAccess
{
    public class VehiclesRepository : IVehicleRepository
    {
        readonly DakarRallyContext context;

        public VehiclesRepository(DakarRallyContext context)
        {
            this.context = context;
        }

        public Vehicle Get(int id, bool includeRelated = true)
        {
            IQueryable<Vehicle> vehiclesSource = context.Vehicles;
            if (includeRelated)
                vehiclesSource = IncludeRelated(vehiclesSource);
            Vehicle vehicle = vehiclesSource.SingleOrDefault(x => x.Id == id);
            return vehicle;
        }

        public IQueryable<Vehicle> Get(bool includeRelated = true)
        {
            IQueryable<Vehicle> vehiclesSource = context.Vehicles;
            if (includeRelated)
                vehiclesSource = IncludeRelated(vehiclesSource);
            return vehiclesSource;
        }

        public void Insert(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public void Delete(int id)
        {
            Vehicle vehicle = context.Vehicles.SingleOrDefault(x => x.Id == id);
            context.Remove(vehicle);
        }

        private IQueryable<Vehicle> IncludeRelated(IQueryable<Vehicle> vehiclesStore)
        {
            return vehiclesStore.Include(x => x.Race).Include(x => x.Malfunctions);
        }
    }
}