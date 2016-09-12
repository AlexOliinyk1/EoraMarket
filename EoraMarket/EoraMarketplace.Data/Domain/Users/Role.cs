using Microsoft.AspNet.Identity.EntityFramework;

namespace EoraMarketplace.Data.Domain.Users
{
    /// <summary>
    ///     Role entity for Asp.NET identity users
    /// </summary>
    public class Role : IdentityRole<int, UserRoles>
    {
        public string Description { get; set; }
    }
}
