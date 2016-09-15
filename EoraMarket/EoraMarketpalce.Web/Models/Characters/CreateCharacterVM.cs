using EoraMarketplace.Data.Domain.Characters;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Models.Characters
{
    public class CreateCharacterVM
    {
        public List<Race> Races { get; set; }
        public List<Class> Classes { get; set; }

        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public int SelectedRaceId { get; set; }
        public int SelectedClassId { get; set; }

        public IEnumerable<SelectListItem> GetRacesSelectedList()
        {
            return new SelectList(Races, "Id", "Name");
        }

        public IEnumerable<SelectListItem> GetClassesSelectedList()
        {
            return new SelectList(Classes, "Id", "Name");
        }
    }
}