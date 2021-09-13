using BookingRoomAPI.Domain.Models;
using BookingRoomAPI.Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace BookingRoomAPI.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Room> Rooms { get; set; }

        #region Protected Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            LoadingSeed(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected static void LoadingSeed(ModelBuilder modelBuilder)
        {
            new RoomSeed().Seed(modelBuilder.Entity<Room>());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Created_At") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Created_At").CurrentValue = DateTime.Now;
                    entry.Property("Active").CurrentValue = true;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Updated_At").CurrentValue = DateTime.Now;
                    entry.Property("Updated_At").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        #endregion
    }
}
