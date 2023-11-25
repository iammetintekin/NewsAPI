using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRNews.Entity.Models;

namespace TRNews.Infrastructure.EntityFramework
{
    public class DatabaseContext : IdentityDbContext<User, IdentityRole<int>, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var testUser = new User
            {
                Id = 1,
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                PhoneNumber = "5555555555",
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            testUser.PasswordHash = CreatePasswordHash(testUser, "admin123");

            var adminUserRole = new IdentityRole<int>
            {
                Id = 1,
                Name = "admin",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "ADMIN".ToLower().ToUpper(),
            }; 
            var userRole = new IdentityRole<int>
            {
                Id = 2,
                Name = "user",
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedName = "USER".ToLower().ToUpper(),
            };

            modelBuilder.Entity<IdentityRole<int>>().HasData(adminUserRole, userRole); 

            modelBuilder.Entity<User>().HasData(testUser)
                ;
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });

            base.OnModelCreating(modelBuilder);

        }
        private string CreatePasswordHash(User user, string pass)
        {
            var hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, pass);
        }
        public DbSet<Reporting> Reportings { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
