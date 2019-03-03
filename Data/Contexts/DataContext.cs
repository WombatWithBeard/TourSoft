using Microsoft.EntityFrameworkCore;
using ToursSoft.Data.Models;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Data.Contexts
 {
     public class DataContext: DbContext
     {

        public DbSet<Excursion> Excursions { get; set; }
         
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<ExcursionGroup> ExcursionGroups { get; set; }

        public DbSet<TourPrice> TourPrices { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DataContext(DbContextOptions<DataContext> options):base(options)
         {
         }
         
        public DataContext()
         { 
         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Tour>().ToTable("Tour");
            modelBuilder.Entity<Hotel>().ToTable("Hotel");
            modelBuilder.Entity<TourPrice>().ToTable("TourPrice");
            modelBuilder.Entity<ExcursionGroup>().ToTable("ExcursionGroup");
            modelBuilder.Entity<Excursion>().ToTable("Excursion");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Tour>().ToTable("Tour");
        }
    }
 }