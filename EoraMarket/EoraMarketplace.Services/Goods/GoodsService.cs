using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;
using EoraMarketplace.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EoraMarketplace.Services.Goods
{
    public class GoodsService : IGoodsService
    {
        private readonly IRepository<MarketProduct> _goodsRepository;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="goodsRepository"></param>
        public GoodsService(IRepository<MarketProduct> goodsRepository)
        {
            this._goodsRepository = goodsRepository;
        }

        public List<MarketProduct> GetGoods(Class forClass, int skip, int take)
        {
            return _goodsRepository.Table
                .Where(x => x.Product.Classes.Any(c => c.Name == forClass.Name))
                .OrderBy(x => x.Product.Name)
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}
