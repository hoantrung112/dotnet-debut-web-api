using Microsoft.EntityFrameworkCore;
using System;

namespace DebutWebAPI.Models
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<SmartHome> SmartHomes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<SmartHomeCitizen> SmartHomeCitizens { get; set; }
        public DbSet<SmartHomeOwner> SmartHomeOwners { get; set; }
        public DbSet<RoomOwner> RoomOwners { get; set; }
        public DbSet<RoomDevice> RoomDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // configure DB entities' relationship
            modelBuilder.Entity<SmartHomeCitizen>().HasKey(
                sc => new { sc.SmarHomeId, sc.CitizenId });

            modelBuilder.Entity<SmartHomeOwner>().HasKey(
                so => new { so.SmarHomeId, so.OwnerId });

            modelBuilder.Entity<RoomOwner>().HasKey(
                ro => new { ro.RoomId, ro.OwnerId });

            modelBuilder.Entity<RoomDevice>().HasKey(
                rd => new { rd.RoomId, rd.DeviceId });

            // Seed Citizen Table
            modelBuilder.Entity<Citizen>().HasData(new Citizen()
            {
                CitizenId = 1,
                Username = "nhtrung",
                FullName = "Nguyen Hoan Trung",
                Email = "nhtrung.1102@gmail.com",
                DOB = new DateTime(2001, 2, 11),
                Gender = Gender.Male,
                PhoneNumber = "0123456789",
                IdCardNumber = "001002003004"
            });
        }
    }
}
