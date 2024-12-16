using Blockus_Client.BlockusService;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blockus_Client.Validations
{
    public class AccountValidator
    {

        [Required]
        [StringLength(20, MinimumLength = 3)]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "El nombre de usuario solo debe contener numeros o letras")]
        [UniqueUsername]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        [RegularExpression(@"^[a-zA-z0-9]+$", ErrorMessageResourceName = "Error_specialCharacters", ErrorMessageResourceType = typeof(Properties.Resources))]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public AccountValidator(AccountDTO account, string PasswordConfirmation)
        {
            Username = account.Username;
            Email = account.Email;
            Password = account.Password;
            ConfirmPassword = PasswordConfirmation; 
        }

        public List<ValidationResult> Validate()
        {
            return ValidatorHelper.Validate(this); 
        }
    }
}
