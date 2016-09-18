using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EoraMarketplace.Data.Domain.Goods
{
    public class MarketProduct
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }

        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
