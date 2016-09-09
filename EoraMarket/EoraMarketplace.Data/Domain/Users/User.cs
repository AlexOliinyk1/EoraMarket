using Microsoft.AspNet.Identity.EntityFramework;

namespace EoraMarketplace.Data.Domain.Users
{
    public class User : IdentityUser<int, IdentityUserLogin<int>, IdentityUserRole<int>, IdentityUserClaim<int>>
    {
    }
}
