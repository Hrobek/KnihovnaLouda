using System.ComponentModel.DataAnnotations;

namespace KnihovnaLouda.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Jméno")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Příjmení")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Hesla se neshodují.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
