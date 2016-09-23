using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EoraMarketpalce.Web.Areas.Admin.Models
{
    public class AdminInviteModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email to invite")]
        public string InvitedEmail { get; set; }
    }
}