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
        const int START_COSTS = 1000;
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
        public ViewResult Create(CreateCharacterVM model)
        {
            Character character = new Character {
                Name = model.Name,
                CreatedAt = DateTime.UtcNow,
                Credits = START_COSTS,
                ImageUrl = model.ImageUrl,
                OwnerId = User.Identity.GetUserId<int>(),
                ClassId = model.SelectedClassId,
                RaceId = model.SelectedRaceId
            };

            character = _characterService.CreateUserCharacter(character);

            return Index();
        }
    }
}