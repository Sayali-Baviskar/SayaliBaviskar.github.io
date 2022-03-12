using System.ComponentModel.DataAnnotations;

namespace MVC_UI.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 3, ErrorMessage = "Username is not valid")]
        [RegularExpression(@"^(([A-za-z]+[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Username is not valid")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Enter Password")]
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$", ErrorMessage = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }
    }
}
