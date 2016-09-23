using EoraMarketpalce.Web.Common;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EoraMarketpalce.Web.Models.Goods
{
    public class CreateProductModel
    {
        public int Quantity { get; set; }

        public int Discount { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int SellPrice { get; set; }

        [Required]
        public string Image { get; set; }

        public int TypeId { get; set; }

        public List<ProductStat> Stats { get; set; }
        public List<Class> Classes { get; set; }
    }
}