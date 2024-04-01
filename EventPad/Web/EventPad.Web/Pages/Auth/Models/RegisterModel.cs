using System.ComponentModel.DataAnnotations;

namespace EventPad.Web.Pages.Profiles.Models;

public class RegisterModel
{
    [Required(ErrorMessage = "FirstName is required.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "SecondName is required.")]
    [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
    public string SecondName { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 10 characters long.")]
    [MaxLength(50, ErrorMessage = "Password can not be more than 50 characters long.")]
    public string Password { get; set; }
}
