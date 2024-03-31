using System.ComponentModel.DataAnnotations;

namespace EventPad.Web.Pages.Profiles.Models;

public class SendEmailModel
{
    [Required(ErrorMessage = "Url is required.")]
    public string FrontendUrl { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Email is invalid.")]
    public string Email { get; set; }
}
