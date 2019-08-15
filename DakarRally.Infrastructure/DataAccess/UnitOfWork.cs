using DakarRally.Core.Contracts;
using System;

namespace DakarRally.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly DakarRallyContext context;
        VehiclesRepository vehiclesRepository;
        RaceRepository raceRepository;
        public IVehicleRepository VehicleRepository
        {
            get
            {
                if (vehiclesRepository == null)
                    vehiclesRepository = new VehiclesRepository(context);
                return vehiclesRepository;
            }
        }

        public IRaceRepository RaceRepository
        {
            get
            {
                if (raceRepository == null)
                    raceRepository = new RaceRepository(context);
                return raceRepository;
            }
        }

        public UnitOfWork(DakarRallyContext context)
        {
            this.context = context;
        }

        public void SaveChanges()
        {
            try
            {
                context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}