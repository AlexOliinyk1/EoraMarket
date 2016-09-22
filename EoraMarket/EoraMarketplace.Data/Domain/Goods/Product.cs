using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Images;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EoraMarketplace.Data.Domain.Goods
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public int ImageId { get; set; }
        public int TypeId { get; set; }

        [ForeignKey("ImageId")]
        public Picture Image { get; set; }

        [ForeignKey("TypeId")]
        public ProductType Type { get; set; }

        public List<ProductStat> Stats { get; set; }
        public List<Class> Classes { get; set; }

        public List<Character> Owners { get; set; }
    }
}
