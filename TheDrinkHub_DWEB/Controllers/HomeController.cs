using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TheDrinkHub_DWEB.Models;
using System.Net.Http;
using System.Text.Json;
using TheDrinkHub_DWEB.Views.Home;
using Microsoft.AspNetCore.Authorization;
using TheDrinkHub_DWEB.Models.ViewModels;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
        [Authorize(Roles = "Admin")]
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


        // GET: Editar perfil
        [Authorize]
        public async Task<IActionResult> EditarPerfil()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            var model = new EditPerfilViewModel
            {
                Nome = user.Nome,
                DataNascimento = user.DataNascimento,
                Nif = user.Nif,
                Morada = user.Morada,
                Email = user.Email
            };

            return View(model);
        }

        // POST: Editar perfil
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarPerfil(EditPerfilViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return NotFound();

            // Atualizar campos básicos
            user.Nome = model.Nome;
            user.DataNascimento = model.DataNascimento;
            user.Nif = model.Nif;
            user.Morada = model.Morada;

            // Atualizar Email se mudou
            if (user.Email != model.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    foreach (var error in setEmailResult.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
                user.UserName = model.Email; // manter UserName sincronizado
            }

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                foreach (var error in updateResult.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(model);
            }

            await _signInManager.RefreshSignInAsync(user);

            TempData["Sucesso"] = "Perfil atualizado com sucesso!";
            return RedirectToAction("PerfilMain");
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

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Nome = model.Nome, Morada = model.Morada, Nif = model.Nif, DataNascimento = model.DataNascimento};
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
