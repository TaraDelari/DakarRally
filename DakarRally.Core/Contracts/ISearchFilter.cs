using System.Linq;

namespace DakarRally.Core.Contracts
{
    interface ISearchFilter<T>
    {
        IQueryable<T> ApplyFilter(IQueryable<T> query);
    }
}