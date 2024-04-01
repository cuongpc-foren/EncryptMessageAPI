using System.ComponentModel.DataAnnotations;

namespace MessageAPIViewModel.Authentication
{
    public class SignUpAPIViewModel
    {
        [Required]
        [MinLength(8)]
        public string Username { get; set; }

        [Required]
        [MinLength(10)]
        public string Password { get; set; }

        [Required]
        [MinLength(10)]
        public string RePassword { get; set; }

        // OLD but GOLD
        ////fields
        //public string Username;
        //public string Password;
        //public string RePassword;

        ////getters
        //public string getUsername()
        //{
        //    return this.Username;
        //}

        //public string getPassword()
        //{
        //    return this.Password;
        //}

        //public string getRePassword()
        //{
        //    return this.RePassword;
        //}
        ////setters
        //public string setUsername(string Username)
        //{
        //    return this.Username = Username;
        //}
        //public string setPassword(string Password)
        //{
        //    return this.Password = Password;
        //}
        //public string setRePassword(string RePassword)
        //{
        //    return this.RePassword = RePassword;
        //}

    }
}
