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

         public DbSet<TourPrice> TourPrices { get; set; }

         public DbSet<User> Users { get; set; }

         public DataContext()
         {
         }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
             => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=postgres");

         protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             modelBuilder.Entity<User>().HasData(
                 new User[]
                 {
                     new User(Guid.NewGuid(), "Valeriy", "TourValeriy", 123456789, false), 
                     new User(Guid.NewGuid(), "Sergey", "SergeyInc", 0987665431, false), 
                     new User(Guid.NewGuid(), "Pavel", "PavelExcursions", 565656565, true), 
                 });

             modelBuilder.Entity<Tour>().HasData(
                 new Tour[]
                 {
                     new Tour(Guid.NewGuid(), "Big cats", 60, "Tour where our cats eat you, scumbag"),
                     new Tour(Guid.NewGuid(), "Whales", 120, "You could feed to a whale your child for free"),
                     new Tour(Guid.NewGuid(), "Wombats", 1, "Wombats will do a cubic shit"), 
                 });

             modelBuilder.Entity<Hotel>().HasData(
                 new Hotel[]
                 {
                     new Hotel(Guid.NewGuid(), "SPA RESORT 5 STARS EXCELLENT SHOWER FULL TIME WHORES MOTEL", "Moscow, za garajami street", 7987987),
                     new Hotel(Guid.NewGuid(), "Medium comfort hotel, with full time maniac. Free kittens", "Dominican Republic", 786876), 
                 });
         }
     }
 }