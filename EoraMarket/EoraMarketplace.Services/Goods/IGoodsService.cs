using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;
using System.Collections.Generic;

namespace EoraMarketplace.Services.Goods
{
    public interface IGoodsService
    {
        List<Product> GetGoods(Class forClass,int skip, int take);
    }
}
