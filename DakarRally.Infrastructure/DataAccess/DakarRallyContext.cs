using DakarRally.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DakarRally.Infrastructure.DataAccess
{
    public class DakarRallyContext : DbContext
    {
        public DakarRallyContext(DbContextOptions<DakarRallyContext> options) : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Malfunction> Malfunctions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureForVehicles(modelBuilder);
            ConfigureForRace(modelBuilder);
            ConfigureForMalfunction(modelBuilder);
        }

        private void ConfigureForVehicles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vehicle>().HasKey(x => x.Id);
            modelBuilder.Entity<Vehicle>().Property(c => c.TeamName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(c => c.Model).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Vehicle>().Property(c => c.VehicleType).IsRequired();

            modelBuilder.Entity<Vehicle>().HasOne(r => r.Race).WithMany(g => g.Vehicles).HasForeignKey(s => s.RaceId).IsRequired();
            modelBuilder.Entity<Vehicle>().HasMany(r => r.Malfunctions).WithOne(v => v.Vehicle).HasForeignKey(s => s.VehicleId);
        }

        private void ConfigureForRace(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Race>().HasKey(x => x.Id);
            modelBuilder.Entity<Race>().Property(c => c.Year).IsRequired();
            modelBuilder.Entity<Race>().Property(c => c.RaceStatus).IsRequired();
        }

        private void ConfigureForMalfunction(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Malfunction>().HasKey(x => x.Id);
            modelBuilder.Entity<Malfunction>().Property(c => c.Time).IsRequired();
            modelBuilder.Entity<Malfunction>().Property(c => c.MalfunctionType).IsRequired();
        }
    }
}