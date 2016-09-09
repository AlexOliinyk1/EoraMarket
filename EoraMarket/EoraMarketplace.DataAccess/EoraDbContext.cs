using Microsoft.AspNet.Identity.EntityFramework;
using EoraMarketplace.Data.Domain.Users;
using System.Data.Entity;

namespace EoraMarketplace.Data
{
    class EoraDbContext : IdentityDbContext<User, Role, int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
        }
    }
}
