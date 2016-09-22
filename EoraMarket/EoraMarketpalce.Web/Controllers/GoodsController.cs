using EoraMarketpalce.Web.Common;
using EoraMarketpalce.Web.Common.Extentions;
using EoraMarketpalce.Web.Models.Goods;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Data.Domain.Goods;
using EoraMarketplace.Services.Characters;
using EoraMarketplace.Services.Goods;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace EoraMarketpalce.Web.Controllers
{
    [Authorize]
    public class GoodsController : ApiController
    {
        private readonly ICharacterService _service;
        private readonly IGoodsService _goodsService;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="goodsService"></param>
        public GoodsController(ICharacterService service, IGoodsService goodsService)
        {
            this._service = service;
            this._goodsService = goodsService;
        }

        [HttpGet]
        [Route("api/Goods")]
        public GoodsPagingModel Get([FromUri]GoodsFilter filter)
        {
            List<MarketProduct> goods = new List<MarketProduct>();

            int skip = (filter.Page - 1) * filter.PerPage;

            goods = _goodsService.GetGoods(filter.ActiveClass, filter.ProductName, filter.MinPrice, filter.MaxPrice, skip, filter.PerPage);

            return new GoodsPagingModel {
                Products = goods.Select(x => new ProductModel {
                    Name = x.Product.Name,
                    Id = x.Id,
                    Price = x.Product.Price,
                    Available = x.Quantity,
                    ImageUrl = x.Product.Image.ImageUrl.Replace("~", "")
                }),
                TotalProducts = _goodsService.GetGoodsCount(filter.ActiveClass, filter.ProductName, filter.MinPrice, filter.MaxPrice)
            };
        }

        [HttpGet]
        [Route("api/Goods/Detail/{id}")]
        public MarketProduct GetDetail([FromUri]int id)
        {
            return _goodsService.GetProductDetail(id);
        }

        [HttpGet]
        [Route("api/Goods/GetNames")]
        public string GetProductNames([FromUri]string searchstring)
        {
            var foo = JsonConvert.SerializeObject(_goodsService.GetProductNames(searchstring), new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            return foo;
        }

        [HttpPost]
        [Route("api/Goods/SaveProduct")]
        public HttpResponseMessage SaveProduct(CreateProductModel model)
        {
            ImageSaver saver = ImageSaver.GetSaverInstance();

            string imagePath = saver.SaveImage(model.Image);

            Product product = new Product {
                Name = model.Name,
                Price = model.Price,
                TypeId = model.TypeId,
                Image = new EoraMarketplace.Data.Domain.Images.Picture {
                    IsAvatar = false,
                    ImageUrl = ImageSaver.ToVirtualPath(imagePath)
                },
            };

            _goodsService.CreateProduct(product, model.Classes, model.Stats);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("api/Race/Get")]
        public List<Race> GetRaces()
        {
            return _service.GetCharactersRaces();
        }

        [HttpGet]
        [Route("api/Class/Get")]
        public List<Class> GetClasses()
        {
            return _service.GetCharactersClasses();
        }
    }
}