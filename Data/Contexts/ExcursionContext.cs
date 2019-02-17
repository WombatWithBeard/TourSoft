using Microsoft.EntityFrameworkCore;

namespace ToursSoft.Data.Models.Contexts
 {
     public class ExcursionContext: DbContext
     {
         public DbSet<Excursion> Excursions { get; set; }
     }
 }