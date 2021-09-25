using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage="User Name is required")]
        public string    UserName { get; set; }

        [Required(ErrorMessage="Known As is required")]
        public string    KnownAs { get; set; }

        [Required(ErrorMessage="Gender is required")]
        public string    Gender { get; set; }

        [Required(ErrorMessage="Date Of Birth is required")]
        public DateTime   DateOfBirth { get; set; }

        [Required(ErrorMessage="City is required")]
        public string    City { get; set; }

        [Required(ErrorMessage="Country is required")]
        public string    Country { get; set; }

        [Required(ErrorMessage="Password is required")]
        public string Password { get; set; }
    }
}