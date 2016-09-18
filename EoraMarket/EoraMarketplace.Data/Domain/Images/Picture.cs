using System.ComponentModel.DataAnnotations;

namespace EoraMarketplace.Data.Domain.Images
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvatar { get; set; }
    }
}
