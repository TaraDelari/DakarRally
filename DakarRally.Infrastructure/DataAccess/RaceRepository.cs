using DakarRally.Core.Contracts;
using DakarRally.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DakarRally.Infrastructure.DataAccess
{
    public class RaceRepository : IRaceRepository
    {
        readonly DakarRallyContext context;

        public RaceRepository(DakarRallyContext context)
        {
            this.context = context;
        }

        public Race Get(int id, bool includeRelated = true)
        {
            IQueryable<Race> raceSource = context.Races;
            if (includeRelated)
                raceSource = IncludeRelated(raceSource);
            Race race = raceSource.SingleOrDefault(x => x.Id == id);
            return race;
        }

        public IQueryable<Race> Get(bool includeRelated = true)
        {
            IQueryable<Race> raceSource = context.Races;
            if (includeRelated)
                raceSource = IncludeRelated(raceSource);
            return raceSource;
        }

        public int Insert(Race race)
        {
            context.Races.Add(race);
            return race.Id;
        }

        private IQueryable<Race> IncludeRelated(IQueryable<Race> raceStore)
        {
            return raceStore.Include(x => x.Vehicles).ThenInclude(m => m.Malfunctions);
        }
    }
}