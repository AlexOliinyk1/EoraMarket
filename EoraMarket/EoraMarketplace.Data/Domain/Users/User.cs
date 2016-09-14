using EoraMarketplace.Data.Domain.Characters;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace EoraMarketplace.Data.Domain.Users
{
    /// <summary>
    ///     User entity for Asp.NET identity
    /// </summary>
    public class User : IdentityUser<int, UserLogins, UserRoles, UserClaims>
    {
        public ICollection<Character> Characters { get; set; }
    }
}
