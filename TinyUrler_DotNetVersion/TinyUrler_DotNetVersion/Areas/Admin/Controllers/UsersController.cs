using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyUrler_DotNetVersion.Data;
using TinyUrler_DotNetVersion.Models;
using TinyUrler_DotNetVersion.ViewModels;

namespace TinyUrler_DotNetVersion.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(policy:"RequireAdminRole")]
public class UsersController : Controller
{
    private readonly IUserRepository _context;
    private readonly IlinkRepository _linkRepository;

    public UsersController(IUserRepository context, IlinkRepository linkRepository)
    {
        _context = context;
        _linkRepository = linkRepository;
    }

    public IActionResult Index()
    {   
        var users = _context.GetUsers();
        Console.WriteLine(users);
        return View(users);
    }

    public IActionResult UserLinks(string id)
    {
        var user = _context.GetUserById(id);
        var links = _linkRepository.GetUserLinks(user);
        return View(links);
    }
}