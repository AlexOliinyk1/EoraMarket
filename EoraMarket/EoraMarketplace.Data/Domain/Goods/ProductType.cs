using System.ComponentModel.DataAnnotations;

namespace EoraMarketplace.Data.Domain.Goods
{
    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
