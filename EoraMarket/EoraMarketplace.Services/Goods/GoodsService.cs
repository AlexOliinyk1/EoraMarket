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
        private readonly IRepository<Product> _goodsRepository;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="goodsRepository"></param>
        public GoodsService(IRepository<Product> goodsRepository)
        {
            this._goodsRepository = goodsRepository;
        }

        public List<Product> GetGoods(Class forClass, int skip, int take)
        {
            //  TODO: make class for product
            return _goodsRepository.Table
                //.Where(x => x.Class == forClass.Name)
                .OrderBy(x => x.Name)
                .Skip(skip)
                .Take(take)
                .ToList();
        }
    }
}
