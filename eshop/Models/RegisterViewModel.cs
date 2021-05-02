using eshop.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(256)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [StringLength(256)]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [UniqueCharacters(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$",ErrorMessage = RegisterViewModel.ErrorMessagePassword)]
        public string Password { get; set; }
        private const string ErrorMessagePassword = "Password must be at least 8 characters.<BR>" +
            "Password must have at least one non alphanumerical character." +
            "<BR>Password must have at least one digit('0'-'9')." +
            "<BR>Password must have at least one uppercase('A'-'Z').";

        [Required]
        [Compare(nameof(Password),ErrorMessage = "Passwords don't match")]
        public string RepeatedPassword { get; set; }

        public string[] ErrorDuringRegister { get; set; }

    }
}
