using Microsoft.AspNet.Identity.EntityFramework;
using EoraMarketplace.Data.Domain.Users;
using System.Data.Entity;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Images;
using EoraMarketplace.Data.Domain.Goods;

namespace EoraMarketplace.Data
{
    public class EoraDbContext : IdentityDbContext<User, Role, int, UserLogins, UserRoles, UserClaims>
    {
        public virtual IDbSet<Character> Characters { get; set; }
        public virtual IDbSet<Race> Races { get; set; }
        public virtual IDbSet<Class> Classes { get; set; }

        public virtual IDbSet<MarketProduct> MarketProducts { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
        public virtual IDbSet<ProductStat> ProductStat { get; set; }
        public virtual IDbSet<ProductType> ProductType { get; set; }

        public virtual IDbSet<Picture> Images { get; set; }

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
