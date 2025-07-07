using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management2.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminGuid = "b07e14cd-b7e6-4360-afb3-4511799dd221";
            var userGuid = "2a8a7edc-02e9-4fe4-bad5-26552904b912";
            var roles = new List<IdentityRole>()
            {
                new IdentityRole()
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = adminGuid,
                    ConcurrencyStamp = adminGuid
                },

                new IdentityRole()
                {
                    Name = "User",
                    NormalizedName = "USER",
                    Id = userGuid,
                    ConcurrencyStamp = userGuid
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId = "1d2289e4-6d4d-4568-87cf-2a63542ee054";

            var adminUser = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "Admin",
                Email = "admin@mail.com",
                NormalizedUserName = "ADMIN",
                NormalizedEmail = "admin@mail.com",
                EmailConfirmed = true,
                SecurityStamp = adminUserId,
                ConcurrencyStamp = adminUserId,
                PasswordHash = "AQAAAAIAAYagAAAAEO142DSwAe9cTEvJJsyssCrdNL738MEuFJpkfbXvPmXGTgXZsWztW839Reh/cmHVMg=="
            };
            
            //adminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(adminUser, "admin1234");
            //Console.WriteLine(adminUser.PasswordHash);
            builder.Entity<IdentityUser>().HasData(adminUser);

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>()
                {
                    RoleId = adminGuid,
                    UserId = adminUserId
                    
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}
