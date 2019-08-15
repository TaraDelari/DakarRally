using DakarRally.Core.Models;
using System.Linq;

namespace DakarRally.Core.Contracts
{
    public interface IRaceRepository
    {
        Race Get(int id, bool includeRelated = true);
        IQueryable<Race> Get(bool includeRelated = true);
        int Insert(Race race);
    }
}