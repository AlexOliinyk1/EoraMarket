using EoraMarketplace.Data.Domain.Goods;
using System.Collections.Generic;

namespace EoraMarketpalce.Web.Models.Goods
{
    public class InventoryProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int SellPrice { get; set; }
        public string ImageUrl { get; set; }

        public List<ProductStat> Stats { get; set; }
    }
}