using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using TheDrinkHub_DWEB.Data;
using TheDrinkHub_DWEB.Models;
using TheDrinkHub_DWEB.Views.Home;

namespace TheDrinkHub_DWEB.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserStore<ApplicationUser> _userStore;
    

    
    public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserStore<ApplicationUser> userStore)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _userStore = userStore;
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
    
    
    public IActionResult LoginMain()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginMain(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToAction("Main", "Home");
        }
        else
        {
            ModelState.AddModelError("Email", "Dados incorretos");
        }
        return View(model);
    }

    public IActionResult RegisterMain()
    {
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RegisterMain(RegisterViewModel model)
    {
        var user = Activator.CreateInstance<ApplicationUser>();
        user.Nome = model.Nome;
        user.Morada = model.Morada;
        user.Nif = model.Nif;
        user.DataNascimento = model.DataNascimento;
        user.UserName = model.UserName;
        user.Email = model.Email;
        await _userStore.SetUserNameAsync(user, user.UserName, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return RedirectToAction("LoginMain", "Home");
        }
        return RedirectToAction("RegisterMain", "Home");
    }
}