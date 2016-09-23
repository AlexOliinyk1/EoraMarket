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
        private readonly IRepository<CharactersProducts> _cpRepository;


        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="goodsRepository"></param>
        /// <param name="classRepository"></param>
        /// <param name="charRepo"></param>
        /// <param name="_cpRepository"></param>
        public GoodsService(IRepository<MarketProduct> goodsRepository, IRepository<Class> classRepository, IRepository<Character> charRepo, IRepository<CharactersProducts> cpRepository)
        {
            this._goodsRepository = goodsRepository;
            this._classRepository = classRepository;
            this._characterRepository = charRepo;
            this._cpRepository = cpRepository;
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

            if(classes != null)
            {
                var ids = classes.Select(c => c.Id).ToArray();
                mProduct.Product.Classes = _classRepository.Table.Where(x => ids.Any(c => c == x.Id)).ToList();
            }

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
            Character character = GetCharacterForUser(charId, userId);

            Product product = GetProduct(prodId);

            if(character.Credits < product.Price)
                throw new Exception("Insufficient funds");

            CharactersProducts buyProd = new CharactersProducts {
                CreatedAt = DateTime.UtcNow,
                CharacterId = character.Id,
                ProductId = product.Id
            };

            character.Inventory.Add(buyProd);
            character.Credits = character.Credits - product.Price;

            character = _characterRepository.Update(character);
        }

        public void SellCharacterProduct(int userId, int charId, int prodId)
        {
            Character character = GetCharacterForUser(charId, userId);

            Product product = GetProduct(prodId);

            if(!character.Inventory.Any(x => x.ProductId.Equals(product.Id)))
                throw new Exception("Current product not found in character inventory.");

            CharactersProducts cp = _cpRepository.Table.FirstOrDefault(x => x.ProductId == product.Id && x.CharacterId == character.Id);

            if(cp == null)
                throw new Exception("Current product not found in character inventory.");

            character.Credits += product.SellPrice;
            character.Inventory.Remove(cp);

            character = _characterRepository.Update(character);
        }

        private IQueryable<MarketProduct> GetFilteredQuery(Class forClass, string searchName, int? minPrice, int? maxPrice)
        {
            IQueryable<MarketProduct> query = _goodsRepository.Table;

            if(forClass != null)
                query = query.Where(x => !x.Product.Classes.Any() || x.Product.Classes.Any(c => c.Name == forClass.Name));

            if(!string.IsNullOrEmpty(searchName))
                query = query.Where(x => x.Product.Name.Contains(searchName));

            if(minPrice.HasValue)
                query = query.Where(x => x.Product.Price >= minPrice.Value);

            if(maxPrice.HasValue)
                query = query.Where(x => x.Product.Price <= maxPrice.Value);

            return query;
        }

        private Character GetCharacterForUser(int charId, int ownerId)
        {
            Character character = _characterRepository.Table.Where(x => x.Id == charId && x.OwnerId == ownerId)
                .Include(x => x.Inventory)
                .FirstOrDefault();

            if(character == null)
                throw new ObjectNotFoundException("Character not found");

            return character;
        }

        private Product GetProduct(int prodId)
        {
            Product product = _goodsRepository.Table.Where(x => x.ProductId == prodId)
                .Select(x => x.Product)
                .FirstOrDefault();

            if(product == null)
                throw new ObjectNotFoundException("Product not found");

            return product;
        }
    }
}
