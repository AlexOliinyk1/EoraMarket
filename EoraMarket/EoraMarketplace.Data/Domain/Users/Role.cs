using Microsoft.AspNet.Identity.EntityFramework;

namespace EoraMarketplace.Data.Domain.Users
{
    public class Role : IdentityRole<int, IdentityUserRole<int>>
    {
        public string Description { get; set; }
    }
}
