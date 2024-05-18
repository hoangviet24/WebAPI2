using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DataAnimals.Data
{
    public class AuthDataContext(DbContextOptions<AuthDataContext> options):IdentityDbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var readRoleId = "004c7e80-7dfc-44be-8952-2c7130898655";
            var writeRoleId = "71e282d3-76ca-485e-b094-eff019287fa5";
            base.OnModelCreating(builder);
            var roles = new List<IdentityRole>
            {
                new() {
                    Id = readRoleId,
                    ConcurrencyStamp = readRoleId,
                    Name ="Read",
                    NormalizedName="Read".ToUpper()
                },
                new() {
                    Id = writeRoleId,
                    ConcurrencyStamp = writeRoleId,
                    Name = "Write",
                    NormalizedName = "Write".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
