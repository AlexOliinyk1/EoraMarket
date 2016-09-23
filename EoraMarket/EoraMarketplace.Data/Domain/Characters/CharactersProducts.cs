using EoraMarketplace.Data.Domain.Goods;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EoraMarketplace.Data.Domain.Characters
{
    public class CharactersProducts
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CharacterId { get; set; }
        [ForeignKey("CharacterId")]
        public Character Character { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
