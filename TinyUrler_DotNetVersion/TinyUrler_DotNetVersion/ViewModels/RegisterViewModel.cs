using System.ComponentModel.DataAnnotations;

namespace TinyUrler_DotNetVersion.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "You must enter a username")]
    [Display(Name = "Username")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Email is required")]
    [Display(Name = "EmailAddress")]
    public string EmailAddress { get; set; }
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    [DataType(DataType.Password)]
    [Display(Name = "ConfirmPassword")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string PasswordConfirm { get; set; }
}