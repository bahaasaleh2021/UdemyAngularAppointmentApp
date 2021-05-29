using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage="User Name is required")]
        public string    UserName { get; set; }

        [Required(ErrorMessage="Password is required")]
        public string Password { get; set; }
    }
}