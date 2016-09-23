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

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="statsRepository"></param>
        public StatsService(IRepository<Product> productRepository)
        {
            this._productRepository = productRepository;
        }

        public List<ProductStat> GetStatsByProduct(int id)
        {
            var stats = _productRepository.Table.Where(x => x.Id == id)
                .SelectMany(x => x.Stats)
                .ToList();

            return stats;
        }
    }
}
