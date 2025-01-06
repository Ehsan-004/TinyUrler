using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyUrler_DotNetVersion.Data;
using TinyUrler_DotNetVersion.Models;

namespace TinyUrler_DotNetVersion.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(policy:"RequireAdminRole")]
public class UrlsController : Controller
{
    private readonly IlinkRepository _context;

    public UrlsController(IlinkRepository context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var allUrls = _context.GetLinks();
        return View(allUrls);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Link link)
    {
        if (!ModelState.IsValid) return View();

        if (_context.CreateLink(link))
        {
            return RedirectToAction("index");
        }

        return View();
    }

    public IActionResult Edit(int id)
    {
        var url = _context.GetById(id);
        return View(url);
    }

    [HttpPost]
    public IActionResult Edit(Link link)
    {
        if (!ModelState.IsValid) return View(link);
        
        _context.UpdateLink(link);
        return RedirectToAction("Index");
    }
    
    public IActionResult Delete(int id)
    {
        var url = _context.GetById(id);
        return View(url);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        _context.DeleteLink(id);
        return RedirectToAction("Index");
    }
}