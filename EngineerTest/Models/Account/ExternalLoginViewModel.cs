using System.ComponentModel.DataAnnotations;

namespace EngineerTest.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
