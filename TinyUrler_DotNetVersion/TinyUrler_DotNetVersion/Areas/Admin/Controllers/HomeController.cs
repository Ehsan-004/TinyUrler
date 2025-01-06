using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TinyUrler_DotNetVersion.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(policy:"RequireAdminRole")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}