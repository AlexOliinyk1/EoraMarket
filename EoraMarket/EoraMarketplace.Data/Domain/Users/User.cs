using EoraMarketplace.Data.Domain.Characters;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace EoraMarketplace.Data.Domain.Users
{
    /// <summary>
    ///     User entity for Asp.NET identity
    /// </summary>
    public class User : IdentityUser<int, UserLogins, UserRoles, UserClaims>
    {
        public DateTime CreatedAt { get; set; }

        public ICollection<Character> Characters { get; set; }
    }
}
