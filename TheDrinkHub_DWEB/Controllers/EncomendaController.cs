using System;
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
    public class EncomendaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EncomendaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Encomenda
        public async Task<IActionResult> Index()
        {
            var encomendas = await _context.Encomendas
                .Include(e => e.Utilizador)
                .Include(e => e.Itens)
                    .ThenInclude(i => i.Produto)
                .ToListAsync();

            return View(encomendas);
        }

        // GET: Encomenda/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
                return NotFound();

            var encomenda = await _context.Encomendas
                .Include(e => e.Utilizador)
                .Include(e => e.Itens)
                    .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (encomenda == null)
                return NotFound();

            return View(encomenda);
        }

        // GET: Encomenda/Create
        public IActionResult Create()
        {
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Encomenda/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UtilizadorId,DataEncomenda,Estado,Total")] Encomenda encomenda)
        {
            if (ModelState.IsValid)
            {
                encomenda.Id = Guid.NewGuid();
                _context.Add(encomenda);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "UserName", encomenda.UtilizadorId);
            return View(encomenda);
        }

        // GET: Encomenda/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
                return NotFound();

            var encomenda = await _context.Encomendas
                .Include(e => e.Itens)
                .ThenInclude(i => i.Produto)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (encomenda == null)
                return NotFound();

            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "UserName", encomenda.UtilizadorId);
            return View(encomenda);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Encomenda encomenda)
        {
            if (id != encomenda.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // Carregar a encomenda original com os itens do banco
                    var encomendaOriginal = await _context.Encomendas
                        .Include(e => e.Itens)
                        .FirstOrDefaultAsync(e => e.Id == id);

                    if (encomendaOriginal == null)
                        return NotFound();

                    // Atualizar os campos simples da encomenda
                    encomendaOriginal.UtilizadorId = encomenda.UtilizadorId;
                    encomendaOriginal.DataEncomenda = encomenda.DataEncomenda;
                    encomendaOriginal.Estado = encomenda.Estado;
                    encomendaOriginal.Total = encomenda.Total;

                    // Atualizar os itens - só quantidade, pois preço e produto não mudam
                    foreach (var itemRecebido in encomenda.Itens)
                    {
                        var itemOriginal = encomendaOriginal.Itens.FirstOrDefault(i => i.Id == itemRecebido.Id);
                        if (itemOriginal != null)
                        {
                            itemOriginal.Quantidade = itemRecebido.Quantidade;
                            _context.Entry(itemOriginal).State = EntityState.Modified;
                        }
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncomendaExists(encomenda.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            ViewData["UtilizadorId"] = new SelectList(_context.Users, "Id", "UserName", encomenda.UtilizadorId);
            return View(encomenda);
        }



        // GET: Encomenda/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
                return NotFound();

            var encomenda = await _context.Encomendas
                .Include(e => e.Utilizador)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (encomenda == null)
                return NotFound();

            return View(encomenda);
        }

        // POST: Encomenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var encomenda = await _context.Encomendas.FindAsync(id);
            if (encomenda != null)
            {
                _context.Encomendas.Remove(encomenda);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool EncomendaExists(Guid id)
        {
            return _context.Encomendas.Any(e => e.Id == id);
        }
    }
}
