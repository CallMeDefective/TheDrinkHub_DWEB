using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheDrinkHub_DWEB.Data;
using TheDrinkHub_DWEB.Models;
using TheDrinkHub_DWEB.Views.Home;

namespace TheDrinkHub_DWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUserStore<ApplicationUser> _userStore;

        public APIController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserStore<ApplicationUser> userStore, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userStore = userStore;
            _context = context;
        }


        // Endpoint para obter todas as categorias
        [HttpGet("categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.Categorias.ToListAsync();
            return Ok(categorias);
        }

        // Endpoint para obter todos os produtos
        [HttpGet("produtos")]
        public async Task<IActionResult> GetProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();
            return Ok(produtos);
        }

        // Endpoint para obter produtos de uma categoria específica
        [HttpGet("categoria/{id}")]
        public async Task<IActionResult> GetProdutosPorCategoria(Guid id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            var produtos = await _context.ProdutoCategorias
                .Where(pc => pc.CategoriaId == id)
                .Include(pc => pc.Produto)
                .Select(pc => pc.Produto)
                .ToListAsync();

            return Ok(produtos);
        }

        // Endpoint para obter o usuário logado
        [HttpGet("perfil")]
        public async Task<IActionResult> GetPerfil([FromQuery] string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
