using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheDrinkHub_DWEB.Models;
using System.Net.Http;
using System.Text.Json;
using TheDrinkHub_DWEB.Views.Home;

namespace TheDrinkHub_DWEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(IHttpClientFactory httpClientFactory, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _httpClient = httpClientFactory.CreateClient("DefaultClient");
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET
        public IActionResult BO()
        {
            return View("Index");
        }
        
        // GET
        public IActionResult Index()
        {
            return RedirectToAction("Main", "Home");
        }
        
        // GET
        public IActionResult LoginMain()
        {
            return View();
        }

        // GET
        public async Task<IActionResult> Main()
        {
            var response = await _httpClient.GetAsync("api/Api/categorias");
            if (response.IsSuccessStatusCode)
            {
                var categorias = await response.Content.ReadFromJsonAsync<List<Categoria>>();
                ViewBag.Categorias = categorias;
            }

            response = await _httpClient.GetAsync("api/Api/produtos");
            if (response.IsSuccessStatusCode)
            {
                var produtos = await response.Content.ReadFromJsonAsync<List<Produto>>();
                return View(produtos);
            }

            return View();
        }

        // GET
        public async Task<IActionResult> CategoriaMain(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Api/categoria/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var produtos = await response.Content.ReadFromJsonAsync<List<Produto>>();
            ViewBag.Categorias = await _httpClient.GetFromJsonAsync<List<Categoria>>("api/Api/categorias");
            return View("CategoriaMain", produtos);
        }

        // GET
        public async Task<IActionResult> PerfilMain()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var response = await _httpClient.GetAsync($"api/Api/perfil?userId={user?.Id}");

                if (!response.IsSuccessStatusCode)
                    return NotFound();

                var perfil = await response.Content.ReadFromJsonAsync<ApplicationUser>();
                return View(perfil);
            }

            return RedirectToPage("/Account/Login", new { area = "Identity" });
        }

        // POST - Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginMain(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: true, lockoutOnFailure: false);
                if (result.Succeeded)
                    return RedirectToAction("Main", "Home");
            }

            ModelState.AddModelError("Email", "Dados incorretos");
            return View(model);
        }

        // GET - Registo
        public IActionResult RegisterMain() => View();

        // POST - Registo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterMain(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction("LoginMain", "Home");

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View(model);
        }

        // POST - Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Main", "Home");
        }
    }
}
