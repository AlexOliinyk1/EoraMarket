using System.Collections.Generic;
using EoraMarketplace.Data.Domain.Goods;

namespace EoraMarketplace.Services.Stats
{
    /// <summary>
    ///     Service to manage product stats
    /// </summary>
    public interface IStatsService
    {
        /// <summary>
        ///     Get product stats by product id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns></returns>
        List<ProductStat> GetStatsByProduct(int id);
        /// <summary>
        ///     Get stat names that contains namepart
        /// </summary>
        /// <param name="namePart"></param>
        /// <returns></returns>
        List<string> GetStatsNames(string namePart);
    }
}
