using System.ComponentModel.DataAnnotations;

namespace GymComp.Web.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(ErrorMessage = "Please provide a name that will be displayed in the application.")]
        [Display(Name = "Name")]
        public string DisplayName { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }
}
