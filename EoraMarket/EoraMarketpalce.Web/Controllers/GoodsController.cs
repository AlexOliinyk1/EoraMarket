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
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

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
            MarketProduct product = _goodsService.GetProductDetail(id);

            product.Product.Image.ImageUrl = ImageManager.UrlToHtmlValid(product.Product.Image.ImageUrl);

            return product;
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
            ImageManager saver = ImageManager.GetSaverInstance();

            string imagePath = saver.SaveImage(model.Image);

            Product product = new Product {
                Name = model.Name,
                Price = model.Price,
                TypeId = model.TypeId,
                Image = new EoraMarketplace.Data.Domain.Images.Picture {
                    IsAvatar = false,
                    ImageUrl = ImageManager.ToVirtualPath(imagePath)
                },
            };

            _goodsService.CreateProduct(product, model.Classes, model.Stats);

            return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("api/Goods/BuyProduct")]
        public HttpResponseMessage BuyProduct([FromBody]BuyModel buy)
        {
            if(RequestContext.Principal.IsAdmin())
                return new HttpResponseMessage(System.Net.HttpStatusCode.BadRequest);

            int userId = RequestContext.Principal.Identity.GetUserId<int>();

            try
            {
                _goodsService.BuyProductByCharacter(userId, buy.CharId, buy.ProdId);
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch(System.Exception exc)
            {
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, exc.Message);
            }
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