using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDrinkHub_DWEB.Data;
using TheDrinkHub_DWEB.Models;

namespace TheDrinkHub_DWEB.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    
    public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
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
    
    public async Task<IActionResult> PerfilMain()
    {
        if (User.Identity.IsAuthenticated)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
    
            // Passar o objeto user para a vista
            return View(user);
        }
        return RedirectToPage("/Account/Login", new { area = "Identity" });
    }
    
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Main", "Home"); // Redireciona para a página inicial (ou para onde preferir)
    }

}