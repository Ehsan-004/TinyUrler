using System.ComponentModel.DataAnnotations;

namespace TinyUrler_DotNetVersion.ViewModels;

public class LoginViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string Username { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}