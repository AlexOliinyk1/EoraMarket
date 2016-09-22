using EoraMarketplace.Data.Domain.Characters;
using System.Collections.Generic;

namespace EoraMarketpalce.Web.Models.Goods
{
    public class GoodsFilter
    {
        public string ProductName { get; set; }

        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }

        public Class ActiveClass { get; set; }

        public int Page { get; set; }
        public int PerPage { get; set; }
    }
}