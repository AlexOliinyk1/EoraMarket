using EoraMarketplace.Data.Domain.Users;
using Microsoft.AspNet.Identity;

namespace EoraMarketpalce.Web.Models.Identity
{
    public class EoraUserManager : UserManager<User, int>
    {
        public EoraUserManager(IUserStore<User, int> store)
            : base(store)
        {
        }
    }
}