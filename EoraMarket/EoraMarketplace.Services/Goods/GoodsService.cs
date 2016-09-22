using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;
using EoraMarketplace.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;

namespace EoraMarketplace.Services.Goods
{
    public class GoodsService : IGoodsService
    {
        private readonly IRepository<MarketProduct> _goodsRepository;
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Character> _characterRepository;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="goodsRepository"></param>
        /// <param name="classRepository"></param>
        /// <param name="charRepo"></param>
        public GoodsService(IRepository<MarketProduct> goodsRepository, IRepository<Class> classRepository, IRepository<Character> charRepo)
        {
            this._goodsRepository = goodsRepository;
            this._classRepository = classRepository;
            this._characterRepository = charRepo;
        }

        public List<MarketProduct> GetGoods(Class forClass, string searchName, int? minPrice, int? maxPrice, int skip, int take)
        {
            IQueryable<MarketProduct> query = GetFilteredQuery(forClass, searchName, minPrice, maxPrice);

            query = query.Include(x => x.Product)
                .Include(x => x.Product.Image)
                .Include(x => x.Product.Classes)
                .OrderBy(x => x.Product.Name)
                .Skip(skip)
                .Take(take);

            return query.ToList();
        }

        public List<MarketProduct> GetGoods(string searchName, int? minPrice, int? maxPrice, int skip, int take)
        {
            return GetGoods(null, searchName, minPrice, maxPrice, skip, take);
        }

        public MarketProduct CreateProduct(Product product, List<Class> classes, List<ProductStat> stats)
        {
            MarketProduct mProduct = new MarketProduct {
                Discount = 0,
                Quantity = 0,
                Product = product
            };

            var ids = classes.Select(c => c.Id).ToArray();
            mProduct.Product.Classes = _classRepository.Table.Where(x => ids.Any(c => c == x.Id)).ToList();

            mProduct.Product.Stats = stats;

            mProduct = _goodsRepository.Insert(mProduct);

            return mProduct;
        }

        public MarketProduct GetProductDetail(int productId)
        {
            return _goodsRepository.Table.Where(x => x.Id == productId)
                 .Include(x => x.Product)
                 .Include(x => x.Product.Image)
                 .Include(x => x.Product.Classes)
                 .Include(x => x.Product.Stats)
                 .FirstOrDefault();
        }

        public List<string> GetProductNames(string namePart)
        {
            return _goodsRepository.Table.Where(x => x.Product.Name.Contains(namePart))
                .Select(x => x.Product.Name)
                .ToList();
        }

        public int GetGoodsCount(Class forClass, string searchName, int? minPrice, int? maxPrice)
        {
            return GetFilteredQuery(forClass, searchName, minPrice, maxPrice).Count();
        }

        public int GetGoodsCount(string searchName, int? minPrice, int? maxPrice)
        {
            return GetFilteredQuery(null, searchName, minPrice, maxPrice).Count();
        }

        public void BuyProductByCharacter(int userId, int charId, int prodId)
        {
            Character character = _characterRepository.Table.Where(x => x.Id == charId && x.OwnerId == userId)
                .Include(x => x.Inventory)
                .FirstOrDefault();

            if(character == null)
                throw new ObjectNotFoundException("Character not found");

            Product product = _goodsRepository.Table.Where(x => x.ProductId == prodId)
                .Select(x => x.Product)
                .FirstOrDefault();

            if(product == null)
                throw new ObjectNotFoundException("Product not found");

            if(character.Credits < product.Price)
                throw new Exception("Insufficient funds");

            character.Inventory.Add(product);
            character.Credits = character.Credits - product.Price;

            character = _characterRepository.Update(character);
        }

        private IQueryable<MarketProduct> GetFilteredQuery(Class forClass, string searchName, int? minPrice, int? maxPrice)
        {
            IQueryable<MarketProduct> query = _goodsRepository.Table;

            query = query.Where(x => x.Product.Classes.Count() == 0);

            var d = query.Count();

            if(forClass != null)
                query = query.Where(x => x.Product.Classes.Any(c => c.Name == forClass.Name));

            if(!string.IsNullOrEmpty(searchName))
                query = query.Where(x => x.Product.Name.Contains(searchName));

            if(minPrice.HasValue)
                query = query.Where(x => x.Product.Price >= minPrice.Value);

            if(maxPrice.HasValue)
                query = query.Where(x => x.Product.Price <= maxPrice.Value);

            return query;
        }

    }
}
