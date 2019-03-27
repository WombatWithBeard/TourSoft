using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        /// Roles db connector
        /// </summary>
        public DbSet<Role> Roles { get; set; }

        /// <summary>
        /// Roles db connector
        /// </summary>
        public DbSet<UserRole> UserRoles { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
        }
         
        /// <inheritdoc />
        /// <summary>
        /// Default constructor
        /// </summary>
        public DataContext()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Default on model creating, make tabels and seed admin role
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
////            modelBuilder.Entity<User>().ToTable("User");
////            modelBuilder.Entity<Tour>().ToTable("Tour");
////            modelBuilder.Entity<Hotel>().ToTable("Hotel");
//            modelBuilder.Entity<TourPrice>().HasOne(t => t.Tour).WithMany(tp => tp.TourPrices);
//            modelBuilder.Entity<TourPrice>().HasOne(u => u.User).WithMany(tp => tp.TourPrices);
//            modelBuilder.Entity<ExcursionGroup>().HasOne(e => e.Excursion).WithMany(eg => eg.ExcursionGroups); //.ToTable("ExcursionGroup");
//            modelBuilder.Entity<ExcursionGroup>().HasOne(u => u.User).WithMany(eg => eg.ExcursionGroups);
//            modelBuilder.Entity<Excursion>().HasOne(t => t.Tour).WithMany(e => e.Excursions);
//            modelBuilder.Entity<Person>().HasOne(h => h.Hotel).WithMany(p => p.Persons);
////            modelBuilder.Entity<Tour>().ToTable("Tour");

            modelBuilder.Entity<UserRole>().HasKey(ur => new {ur.UserId, ur.RoleId});
            
            modelBuilder.Entity<UserRole>()
                .HasOne<User>(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);
            
            modelBuilder.Entity<UserRole>()
                .HasOne<Role>(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);
            
            var roleId = Guid.NewGuid();
            seedRole(modelBuilder, roleId);

            var userId = Guid.NewGuid();
            seedUser(modelBuilder, userId);

            seedUserRole(modelBuilder,roleId, userId);
            
            base.OnModelCreating(modelBuilder);
        }

        private void seedUserRole(ModelBuilder modelBuilder, Guid roleId, Guid userId)
        {
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole(id: Guid.NewGuid(), 
                    roleId: roleId, 
                    userId: userId
                ));
        }

        private void seedUser(ModelBuilder modelBuilder, Guid userId)
        {
            modelBuilder.Entity<User>().HasData(
                new User(userId, 
                    "admin", 
                    "admin", 
                    123, 
                    "admin", 
                    "admin"
                ));
        }

        private void seedRole(ModelBuilder modelBuilder, Guid roleId)
        {
            modelBuilder.Entity<Role>().HasData(new Role(roleId, "admin", "main role"));
        }


        /// <summary>
        /// GetExcursionGroupsCapacity
        /// </summary>
        /// <param name="excursionId"></param>
        /// <returns></returns>
        public int GetExcursionGroupsCapacity(Guid excursionId)
        {
            var sum = 0;
            
            foreach (var eg in ExcursionGroups.Where(eg => eg.ExcursionId == excursionId))
            {
                //TODO: do it beautiful
                sum += eg.GetCapacity(this);
            }

            return sum;
        }
     }
 }