using Microsoft.AspNetCore.Identity;

namespace TinyUrler_DotNetVersion.Models;

public class AppUser : IdentityUser
{
    public ICollection<Link>? Links { get; set; }
}