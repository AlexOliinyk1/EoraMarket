using EoraMarketpalce.Web.Models.Goods;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Services.Characters;
using EoraMarketplace.Services.Goods;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
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
        public List<ProductModel> GetGoods(Class customerClass, int skip, int take)
        {
            int id = User.Identity.GetUserId<int>();

            List<ProductModel> goods = _goodsService.GetGoods(customerClass, skip, take)
                .Select(x => new ProductModel {
                    Name = x.Name,
                    Id = x.Id,
                    Price = x.Price,
                    //Available = x..Quantity,
                    ImageUrl = x.Image.ImageUrl
                })
                .ToList();

            return goods;
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