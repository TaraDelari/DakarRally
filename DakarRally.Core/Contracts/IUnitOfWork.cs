namespace DakarRally.Core.Contracts
{
    public interface IUnitOfWork
    {
        IVehicleRepository VehicleRepository { get; }
        IRaceRepository RaceRepository { get; }
        void SaveChanges();
    }
}