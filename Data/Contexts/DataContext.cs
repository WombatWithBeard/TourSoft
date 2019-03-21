using System;
using Microsoft.EntityFrameworkCore;
using ToursSoft.Data.Models;
using ToursSoft.Data.Models.Users;

namespace ToursSoft.Data.Contexts
 {
     /// <summary>
     /// 
     /// </summary>
     public class DataContext: DbContext
     {
        /// <summary>
        /// Excursions db connector
        /// </summary>
        public DbSet<Excursion> Excursions { get; set; }
         
        /// <summary>
        /// Hotels db connector
        /// </summary>
        public DbSet<Hotel> Hotels { get; set; }

        /// <summary>
        /// Tours db connector
        /// </summary>
        public DbSet<Tour> Tours { get; set; }

        /// <summary>
        /// ExcursionGroups db connector
        /// </summary>
        public DbSet<ExcursionGroup> ExcursionGroups { get; set; }

        /// <summary>
        /// TourPrices db connector
        /// </summary>
        public DbSet<TourPrice> TourPrices { get; set; }

        /// <summary>
        /// Users db connector
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Persons db connector
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
         
        /// <summary>
        /// Default constructor
        /// </summary>
        public DataContext()
         { 
         }

        /// <summary>
        /// Default on model creating, make tabels and seed admin role
        /// </summary>
        /// <param name="modelBuilder"></param>
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

            modelBuilder.Entity<User>().HasData(
                new User(Guid.NewGuid(), 
                    "admin", 
                    "admin", 
                    123, 
                    "admin", 
                    "admin", 
                    "admin"));
        }
    }
 }