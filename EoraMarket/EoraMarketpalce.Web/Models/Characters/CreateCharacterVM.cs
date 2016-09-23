using EoraMarketplace.Data.Domain.Characters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace EoraMarketpalce.Web.Models.Characters
{
    public class CreateCharacterVM
    {
        public List<Race> Races { get; set; }
        public List<Class> Classes { get; set; }

        [Required(ErrorMessage = "Enter character name")]
        [MinLength(5, ErrorMessage = "Name must contains at least 5 symbols")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select character image")]
        public int? ImageId { get; set; }

        [Required(ErrorMessage = "Select character race")]
        public int SelectedRaceId { get; set; }

        [Required(ErrorMessage = "Select character class")]
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