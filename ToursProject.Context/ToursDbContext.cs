using System.Collections.Generic;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using ToursProject.Context.Models;

namespace ToursProject.Context
{
    public class ToursDbContext : DbContext
    {
        public ToursDbContext() : base("DefaultConnection")
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelComment> HotelComments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ReceivingPoint> ReceivingPoints { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TypeTour> TypeTours { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
