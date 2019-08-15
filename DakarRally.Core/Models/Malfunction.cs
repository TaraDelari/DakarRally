using System;

namespace DakarRally.Core.Models
{
    public class Malfunction
    {
        public int Id { get; private set; }
        public DateTime Time { get; private set; }
        public int VehicleId { get; private set; }
        public virtual Vehicle Vehicle { get; set; }
        public MalfunctionType MalfunctionType { get; private set; }

        private Malfunction()
        {

        } 

        public Malfunction(MalfunctionType malfunctionType, DateTime time)
        {
            MalfunctionType = malfunctionType;
            Time = time;
        }
    }

    public enum MalfunctionType
    {
        Light = 0,
        Heavy = 1
    }
}