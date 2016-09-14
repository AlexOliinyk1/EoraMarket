using System.ComponentModel.DataAnnotations;

namespace EoraMarketplace.Data.Domain.Characters
{
    public class AvatarImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
