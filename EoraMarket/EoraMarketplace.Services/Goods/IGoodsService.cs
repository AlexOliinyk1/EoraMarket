using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;
using System.Collections.Generic;

namespace EoraMarketplace.Services.Goods
{
    public interface IGoodsService
    {
        MarketProduct GetProductDetail(int productId);
        List<string> GetProductNames(string namePart);
        List<MarketProduct> GetGoods(Class forClass, string searchName, int? minPrice, int? maxPrice, int skip, int take);
        List<MarketProduct> GetGoods(string searchName, int? minPrice, int? maxPrice, int skip, int take);
        int GetGoodsCount(Class forClass, string searchName, int? minPrice, int? maxPrice);
        int GetGoodsCount(string searchName, int? minPrice, int? maxPrice);
        MarketProduct CreateProduct(Product product, List<Class> classes, List<ProductStat> stats);
        void BuyProductByCharacter(int userId, int charId, int prodId);
    }
}
