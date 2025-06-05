using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDrinkHub_DWEB.Data;
using TheDrinkHub_DWEB.Models;

namespace TheDrinkHub_DWEB.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    // GET
    public async Task<IActionResult> Main()
    {
        ViewBag.Categorias = await _context.Categorias.ToListAsync();
        return View(await _context.Produtos.ToListAsync());
    }
    
    // GET
    public async Task<IActionResult> CategoriaMain()
    {
        return View(await _context.Produtos.ToListAsync());
    }
}