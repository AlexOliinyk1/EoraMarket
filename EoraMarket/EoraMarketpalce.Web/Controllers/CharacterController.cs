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

namespace EoraMarketpalce.Web.Controllers
{
    [AccessAuthorize(Roles = AppConsts.UserRoleName)]
    public class CharacterController : AppController
    {
        private ICharacterService _characterService;

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="service"></param>
        public CharacterController(ICharacterService service)
        {
            this._characterService = service;
        }

        public ViewResult Index()
        {
            int userId = User.Identity.GetUserId<int>();

            List<Character> chars = _characterService.GetUserCharacters(userId, 1, 10).ToList();

            return View(new CharactersVM() {
                Characters = chars,
            });
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
            return View(new Character());
        }

        public JsonResult GetActiveCharakter()
        {
            return Json(ActiveCharacter);
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