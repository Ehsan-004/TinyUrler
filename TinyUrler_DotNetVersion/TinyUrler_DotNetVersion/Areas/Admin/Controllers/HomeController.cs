using Microsoft.AspNetCore.Mvc;

namespace TinyUrler_DotNetVersion.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}