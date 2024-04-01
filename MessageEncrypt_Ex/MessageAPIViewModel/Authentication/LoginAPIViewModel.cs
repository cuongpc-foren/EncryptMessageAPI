using System.ComponentModel.DataAnnotations;

namespace MessageAPIViewModel.Authentication
{
    public class LoginAPIViewModel
    {
        [Required]
        [MinLength(8)]
        public string Username { get; set; }

        [Required]
        [MinLength(10)]
        public string Password { get; set; }
    }
}
