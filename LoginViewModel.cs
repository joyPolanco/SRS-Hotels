using System.ComponentModel.DataAnnotations;

namespace SRS_Hotels.Web.Areas.Auth.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
