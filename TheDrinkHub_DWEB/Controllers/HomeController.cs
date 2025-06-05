using Microsoft.AspNetCore.Mvc;

namespace TheDrinkHub_DWEB.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}