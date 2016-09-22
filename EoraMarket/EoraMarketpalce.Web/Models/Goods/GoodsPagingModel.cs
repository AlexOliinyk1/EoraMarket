using System.Collections.Generic;

namespace EoraMarketpalce.Web.Models.Goods
{
    public class GoodsPagingModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public int TotalProducts { get; set; }
    }
}