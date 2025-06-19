using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheDrinkHub_DWEB.Data;
using TheDrinkHub_DWEB.Models;

namespace TheDrinkHub_DWEB.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProdutoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produto
        public async Task<IActionResult> Index()
        {
            var produtos = await _context.Produtos
                .Include(p => p.Categorias)
                .ThenInclude(pc => pc.Categoria)
                .ToListAsync();
            return View(produtos);
        }

        // GET: Produto/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categorias)
                .ThenInclude(pc => pc.Categoria) // importante!
                .FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var viewModel = new ProdutoCreateViewModel
            {
                Categorias = _context.Categorias
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToList()
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoCreateViewModel model)
        {
            ModelState.Remove("Categorias");
            if (ModelState.IsValid)
            {
                var produto = new Produto
                {
                    Nome = model.Nome,
                    Descricao = model.Descricao,
                    Preco = model.Preco,
                    Stock = model.Stock,
                    ImagemUrl = model.ImagemUrl,
                    Categorias = model.SelectedCategorias
                        .Select(categoriaId => new ProdutoCategoria
                        {
                            CategoriaId = categoriaId
                        }).ToList()
                };

                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            model.Categorias = _context.Categorias
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Nome
                }).ToList();

            return View(model);
        }


        // GET: Produto/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            var viewModel = new ProdutoEditViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Stock = produto.Stock,
                ImagemUrl = produto.ImagemUrl,
                SelectedCategorias = produto.Categorias.Select(pc => pc.CategoriaId).ToList(),
                Categorias = await _context.Categorias
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToListAsync()
            };

            return View(viewModel);
        }

        // POST: Produto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProdutoEditViewModel model)
        {
            ModelState.Remove("Categorias");
            if (!ModelState.IsValid)
            {
                model.Categorias = await _context.Categorias
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Nome
                    }).ToListAsync();

                return View(model);
            }

            var produto = await _context.Produtos
                .Include(p => p.Categorias)
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (produto == null)
                return NotFound();

            produto.Nome = model.Nome;
            produto.Descricao = model.Descricao;
            produto.Preco = model.Preco;
            produto.Stock = model.Stock;
            produto.ImagemUrl = model.ImagemUrl;

            // Atualizar categorias
            produto.Categorias.Clear();
            foreach (var categoriaId in model.SelectedCategorias)
            {
                produto.Categorias.Add(new ProdutoCategoria
                {
                    ProdutoId = produto.Id,
                    CategoriaId = categoriaId
                });
            }

            _context.Update(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Produto/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .Include(p => p.Categorias)
                .ThenInclude(pc => pc.Categoria) // importante!
                .FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(Guid id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
