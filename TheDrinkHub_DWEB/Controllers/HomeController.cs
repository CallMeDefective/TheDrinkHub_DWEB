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
    public async Task<IActionResult> CategoriaMain(Guid id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null)
            return NotFound();

        ViewBag.Categorias = await _context.Categorias.ToListAsync();
        ViewBag.CategoriaSelecionada = categoria.Nome;

        var produtos = await _context.ProdutoCategorias
            .Where(pc => pc.CategoriaId == id)
            .Include(pc => pc.Produto)
            .Select(pc => pc.Produto)
            .ToListAsync();

        return View("CategoriaMain", produtos);
    }

}