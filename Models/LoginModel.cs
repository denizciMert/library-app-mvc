using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class LoginModel
    {
        [Required]
        public string IdentityInfo { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PasswordInfo { get; set; }
    }
}
