using Microsoft.EntityFrameworkCore;

namespace ToursSoft.Data.Models.Contexts
 {
     public class DataContext: DbContext
     {
         public DbSet<Excursion> Excursions { get; set; }

         public DbSet<Hotel> Hotels { get; set; }

         public DbSet<Tour> Tours { get; set; }

         public DbSet<TourPrice> TourPrices { get; set; }

         public DbSet<User> Users { get; set; }
     }
 }