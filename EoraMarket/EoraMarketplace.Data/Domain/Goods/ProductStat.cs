using System.ComponentModel.DataAnnotations;

namespace EoraMarketplace.Data.Domain.Goods
{
    public class ProductStat
    {
        [Key]
        public int Id { get; set; }
        public string StatName { get; set; }
        public int StatValue { get; set; }
    }
}
