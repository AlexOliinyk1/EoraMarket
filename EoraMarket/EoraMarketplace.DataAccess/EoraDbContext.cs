using Microsoft.AspNet.Identity.EntityFramework;
using EoraMarketplace.Data.Domain.Users;
using System.Data.Entity;
using EoraMarketplace.Data.Domain.Characters;

namespace EoraMarketplace.Data
{
    public class EoraDbContext : IdentityDbContext<User, Role, int, UserLogins, UserRoles, UserClaims>
    {
        public virtual IDbSet<Character> Characters { get; set; }
        public virtual IDbSet<Race> Races { get; set; }
        public virtual IDbSet<Class> Classes { get; set; }
        public virtual IDbSet<AvatarImage> Avatars { get; set; }

        public EoraDbContext()
            : base("DevConnection")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRoles>().ToTable("UserRoles");
            modelBuilder.Entity<UserLogins>().ToTable("UserLogins");
            modelBuilder.Entity<UserClaims>().ToTable("UserClaims");
        }
    }
}
