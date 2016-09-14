﻿using EoraMarketpalce.Web.Common.Constants;
using EoraMarketpalce.Web.Common.Filters;
using EoraMarketpalce.Web.Controllers.Base;
using EoraMarketpalce.Web.Models.Characters;
using EoraMarketplace.Data.Domain.Characters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Controllers
{
    [AccessAuthorize(Roles = AppConsts.UserRoleName)]
    public class CharacterController : AppController
    {
        public ViewResult Index()
        {
            List<Character> chars = GetData();

            return View(new CharactersVM {
                Characters = chars
            });
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView(new CreateCharacterVM());
        }

        [HttpPost]
        public ViewResult Create(CreateCharacterVM model)
        {
            return Index();
        }


        //Todo: remove test data later
        private List<Character> GetData()
        {
            return new List<Character> {
                new Character {
                    Name = "Konan",
                    ImageUrl = "/Content/Test/male_human_p_lg.png",
                    Class = new Class { Name = "Barbarian" },
                    Credits = 500,
                    Race = new Race { Name = "Human" }
                },
                new Character {
                    Name = "Elanor",
                    ImageUrl = "/Content/Test/male_human_p_lg.png",
                    Class = new Class { Name = "Druid" },
                    Credits = 750,
                    Race = new Race { Name = "Godlike" }
                },
                new Character {
                    Name = "Bilbo",
                    ImageUrl = "/Content/Test/male_human_p_lg.png",
                    Class = new Class { Name = " Chanter" },
                    Credits = 800,
                    Race = new Race { Name = "Dwarf" }
                },
            };
        }
    }
}