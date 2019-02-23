using System;
using Microsoft.EntityFrameworkCore;
using ToursSoft.Data.Models;

namespace ToursSoft.Data.Contexts
 {
     public class DataContext: DbContext
     {
         public DbSet<Excursion> Excursions { get; set; }

         public DbSet<Hotel> Hotels { get; set; }

         public DbSet<Tour> Tours { get; set; }

         public DbSet<ManagersGroup> ManagersGroups { get; set; }

         public DbSet<TourPrice> TourPrices { get; set; }

         public DbSet<User> Users { get; set; }
         
         //public DbSet<LoginModel> LoginModels { get; set; }

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
             modelBuilder.Entity<ManagersGroup>().ToTable("ManagersGroup");
             modelBuilder.Entity<Excursion>().ToTable("Excursion");
         }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//             => optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=tours;username=postgres;password=postgres");

     }
 }