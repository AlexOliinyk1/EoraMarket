
using System.ComponentModel.DataAnnotations;

namespace EoraMarketpalce.Web.Models.Characters
{
    public class EditCharacrterViewModel
    {
        public int CharacterId { get; set; }

        [Required]
        [Display(Name = "New Name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string NewName { get; set; }

        /// <summary>
        ///     Ctor.
        /// </summary>
        /// <param name="id"></param>
        public EditCharacrterViewModel(int id)
        {
            CharacterId = id;
        }
    }
}