using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Common.Filters;
using EoraMarketpalce.Web.Controllers.Base;
using EoraMarketpalce.Web.Models.Characters;
using EoraMarketplace.Data.Domain.Characters;
using EoraMarketplace.Services.Characters;
using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Linq;
using EoraMarketplace.Data.Domain.Users;
using System;
using EoraMarketplace.Data.Domain.Goods;
using Newtonsoft.Json;
using EoraMarketpalce.Web.Models.Goods;
using EoraMarketplace.Services.Stats;
using System.Net;
using System.Collections;
using EoraMarketpalce.Web.Common;

namespace EoraMarketpalce.Web.Controllers
{
    [AccessAuthorize(Roles = AppConsts.UserRoleName)]
    public class CharacterController : AppController
    {
        private ICharacterService _characterService;
        private IStatsService _statsService;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="service"></param>
        /// <param name="statsService"></param>
        public CharacterController(ICharacterService service, IStatsService statsService)
        {
            this._characterService = service;
            this._statsService = statsService;
        }

        public ViewResult Index()
        {
            int userId = User.Identity.GetUserId<int>();

            List<Character> chars = _characterService.GetUserCharacters(userId, 1, 10).ToList();

            return View(new CharactersVM() {
                Characters = chars,
            });
        }

        [HttpGet]
        public PartialViewResult Edit(int id)
        {
            return PartialView(new EditCharacrterViewModel(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public HttpStatusCodeResult Edit(int characterId, string newName)
        {
            if(characterId == 0 || string.IsNullOrEmpty(newName))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Name is required");

            if(newName.Length < 5)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Name must be at least 5 characters long");

            try
            {
                Character uaserChar = _characterService.UpdateCharacterName(User.Identity.GetUserId<int>(), characterId, newName);

                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch(Exception exc)
            {
                AddErrors(exc);
            }

            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Update is failed");
        }

        public ActionResult SelectCharacter(int characterId)
        {
            int userId = User.Identity.GetUserId<int>();
            var character = _characterService.GetUserCharacter(userId, characterId, true);

            ActiveCharacter = new CharacterInfoViewModel {
                Id = character.Id,
                Name = character.Name,
                Class = character.Class.Name,
                Race = character.Race.Name,
                Credits = character.Credits,
                ImageUrl = character.Avatar.ImageUrl
            };

            return RedirectToAction("Index", "Shop");
        }

        public ViewResult View(int id)
        {
            int userId = User.Identity.GetUserId<int>();

            Character character = _characterService.GetUserCharacter(userId, id, true);
            List<Product> inventory = _characterService.GetCharacterInventory(userId, id).Select(x => x.Product).ToList();

            foreach(var item in inventory)
            {
                item.Stats = _statsService.GetStatsByProduct(item.Id);
            }

            CharacterInfoViewModel info = new CharacterInfoViewModel {
                Id = character.Id,
                Name = character.Name,
                Class = character.Class.Name,
                Race = character.Race.Name,
                Credits = character.Credits,
                ImageUrl = character.Avatar.ImageUrl
            };

            CharacterDetailViewModel model = new CharacterDetailViewModel {
                Character = info,
                Inventory = inventory
            };

            return View(model);
        }

        public JsonResult GetActiveCharacter()
        {
            ActiveCharacter.Credits = _characterService.GetCharacterFunds(ActiveCharacter.Id);
            return Json(ActiveCharacter, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetInventory(int characterId)
        {
            int userId = User.Identity.GetUserId<int>();

            List<InventoryProductModel> inventory = _characterService.GetCharacterInventory(userId, characterId)
                .Select(x => new InventoryProductModel {
                    Id = x.Id,
                    ProductId = x.Product.Id,
                    ImageUrl = ImageManager.UrlToHtmlValid(x.Product.Image.ImageUrl),
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    SellPrice = x.Product.SellPrice,
                    Stats = x.Product.Stats
                })
                .ToList();

            return Json(inventory, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public PartialViewResult ActiveCharacterView()
        {
            return PartialView(ActiveCharacter);
        }

        [HttpGet]
        public ViewResult Create()
        {
            CreateCharacterVM model = new CreateCharacterVM {
                Races = _characterService.GetCharactersRaces(),
                Classes = _characterService.GetCharactersClasses()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCharacterVM model)
        {
            if(ModelState.IsValid)
            {
                Character character = new Character {
                    Name = model.Name,
                    CreatedAt = DateTime.UtcNow,
                    Credits = AppConsts.START_COSTS,
                    ImageId = model.ImageId.Value,
                    OwnerId = User.Identity.GetUserId<int>(),
                    ClassId = model.SelectedClassId,
                    RaceId = model.SelectedRaceId
                };

                character = _characterService.CreateUserCharacter(character);

                return RedirectToAction("Index");
            }

            model.Races = _characterService.GetCharactersRaces();
            model.Classes = _characterService.GetCharactersClasses();

            return View(model);
        }

        public JsonResult GetAvatarsByRace(int raceId)
        {
            return Json(_characterService.GetAvatarsByRaceId(raceId), JsonRequestBehavior.AllowGet);
        }
    }
}