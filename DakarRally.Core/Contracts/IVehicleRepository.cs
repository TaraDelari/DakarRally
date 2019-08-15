using DakarRally.Core.Models;
using System.Linq;

namespace DakarRally.Core.Contracts
{
    public interface IVehicleRepository
    {
        Vehicle Get(int id, bool includeRelated = true);
        IQueryable<Vehicle> Get(bool includeRelated = true);
        void Insert(Vehicle account);
        void Delete(int id);
    }
}