using System.Collections.Generic;
using EoraMarketplace.Data.Domain.Goods;

namespace EoraMarketplace.Services.Stats
{
    public interface IStatsService
    {
        List<ProductStat> GetStatsByProduct(int id);
        List<string> GetStatsNames(string namePart);
    }
}
