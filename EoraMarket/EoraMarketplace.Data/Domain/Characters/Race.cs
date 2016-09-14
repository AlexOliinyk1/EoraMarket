using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EoraMarketplace.Data.Domain.Characters
{
    public class Race
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<AvatarImage> AvailableAvatars { get; set; }
    }
}