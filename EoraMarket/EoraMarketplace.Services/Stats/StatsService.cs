using System;
using System.Collections.Generic;
using EoraMarketplace.Data.Domain.Goods;
using EoraMarketplace.DataAccess.Repositories;
using System.Linq;

namespace EoraMarketplace.Services.Stats
{
    public class StatsService : IStatsService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductStat> _statRepository;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="productRepository"></param>
        /// <param name="statsRepository"></param>
        public StatsService(IRepository<Product> productRepository, IRepository<ProductStat> statRepository)
        {
            this._productRepository = productRepository;
            this._statRepository = statRepository;
        }

        public List<ProductStat> GetStatsByProduct(int id)
        {
            var stats = _productRepository.Table.Where(x => x.Id == id)
                .SelectMany(x => x.Stats)
                .ToList();

            return stats;
        }

        public List<string> GetStatsNames(string namePart)
        {
            return _statRepository.Table.Where(x => x.StatName.Contains(namePart))
                .Select(x => x.StatName)
                .Distinct()
                .ToList();
        }
    }
}
