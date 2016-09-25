using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;
using System.Collections.Generic;

namespace EoraMarketplace.Services.Goods
{
    /// <summary>
    ///     Service that present possibilities to manage market products
    /// </summary>
    public interface IGoodsService
    {
        /// <summary>
        ///     Get product details by id
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        MarketProduct GetProductDetail(int productId);
        /// <summary>
        ///     Get top 10 product names that contains "namePart"
        /// </summary>
        /// <param name="namePart">Name part for filter</param>
        /// <returns></returns>
        List<string> GetProductNames(string namePart);
        /// <summary>
        ///     Get product list by filters
        /// </summary>
        /// <param name="forClass">User character class</param>
        /// <param name="searchName">Searched product name</param>
        /// <param name="minPrice">Minimal price to search</param>
        /// <param name="maxPrice">Miximal price to search</param>
        /// <param name="skip">Count of products to skip from start of filtered sequence</param>
        /// <param name="take">Count of products to return</param>
        /// <returns></returns>
        List<MarketProduct> GetGoods(Class forClass, string searchName, int? minPrice, int? maxPrice, int skip, int take);
        /// <summary>
        ///     Get product list by filters
        /// </summary>
        /// <param name="searchName">Searched product name</param>
        /// <param name="minPrice">Minimal price to search</param>
        /// <param name="maxPrice">Miximal price to search</param>
        /// <param name="skip">Count of products to skip from start of filtered sequence</param>
        /// <param name="take">Count of products to return</param>
        /// <returns></returns>
        List<MarketProduct> GetGoods(string searchName, int? minPrice, int? maxPrice, int skip, int take);
        /// <summary>
        ///     Get total count of products by filters
        /// </summary>
        /// <param name="forClass">User character class</param>
        /// <param name="searchName">Product name to search</param>
        /// <param name="minPrice">Minimal price to search</param>
        /// <param name="maxPrice">Miximal price to search</param>
        /// <returns></returns>
        int GetGoodsCount(Class forClass, string searchName, int? minPrice, int? maxPrice);
        /// <summary>
        ///     Get total count of products by filters
        /// </summary>
        /// <param name="searchName">Product name to search</param>
        /// <param name="minPrice">Minimal price to search</param>
        /// <param name="maxPrice">Miximal price to search</param>
        /// <returns></returns>
        int GetGoodsCount(string searchName, int? minPrice, int? maxPrice);
        /// <summary>
        ///     Create market product with classes and stats
        /// </summary>
        /// <param name="product">Product to save</param>
        /// <param name="classes">Classes for created product</param>
        /// <param name="stats">Stats for created product</param>
        /// <returns></returns>
        MarketProduct CreateProduct(Product product, List<Class> classes, List<ProductStat> stats);
        /// <summary>
        ///     Buy market product by user character
        /// </summary>
        /// <param name="userId">Id of user that owns character</param>
        /// <param name="charId">Id of user character</param>
        /// <param name="prodId">Id of product to buy</param>
        void BuyProductByCharacter(int userId, int charId, int prodId);
        /// <summary>
        ///     Buy product form inventory by user character
        /// </summary>
        /// <param name="userId">Id of user that owns character</param>
        /// <param name="charId">Id of user character</param>
        /// <param name="prodId">Id of product to sell</param>
        void SellCharacterProduct(int userId, int charId, int prodId);
        /// <summary>
        ///     Remove product from marketplace.
        ///     Mark market product as deleted
        /// </summary>
        /// <param name="productId">Id of product to delete</param>
        void deleteProduct(int productId);
    }
}
