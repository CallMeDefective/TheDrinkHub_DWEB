﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using TheDrinkHub_DWEB.Data;
using TheDrinkHub_DWEB.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TheDrinkHub_DWEB.Models;
using Microsoft.AspNetCore.Authorization;
using TheDrinkHub_DWEB.Models.ViewModels;

namespace TheDrinkHub_DWEB.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CarrinhoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Carrinho
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var itens = await _context.CarrinhoItens
                .Include(ci => ci.Produto)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            ViewBag.Total = itens.Sum(i => i.Produto.Preco * i.Quantidade);

            return View(itens);
        }

        // POST: /Carrinho/Adicionar/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Adicionar(Guid produtoId, int quantidade)
        {
            var userId = _userManager.GetUserId(User);

            var produto = await _context.Produtos.FindAsync(produtoId);
            if (produto == null || quantidade <= 0 || quantidade > produto.Stock)
            {
                return RedirectToAction("Main", "Home");
            }

            var itemExistente = await _context.CarrinhoItens
                .FirstOrDefaultAsync(c => c.ProdutoId == produtoId && c.UserId == userId);

            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
            }
            else
            {
                var novoItem = new CarrinhoItem
                {
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                    UserId = userId
                };
                _context.CarrinhoItens.Add(novoItem);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        // POST: /Carrinho/Remover/5
        [HttpPost]
        public async Task<IActionResult> Remover(int id)
        {
            var item = await _context.CarrinhoItens.FindAsync(id);
            if (item != null)
            {
                _context.CarrinhoItens.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // POST: /Carrinho/Pagar
        [HttpPost]
        public async Task<IActionResult> Pagar()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var itens = await _context.CarrinhoItens
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            // Aqui normalmente processavas pagamento e geravas encomenda

            _context.CarrinhoItens.RemoveRange(itens);
            await _context.SaveChangesAsync();

            TempData["Sucesso"] = "Compra realizada com sucesso!";
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> FinalizarPagamento(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var itens = await _context.CarrinhoItens
                    .Include(ci => ci.Produto)
                    .Where(ci => ci.UserId == userId)
                    .ToListAsync();

                ViewBag.Total = itens.Sum(i => i.Produto.Preco * i.Quantidade);
                return View("Checkout", model);
            }

            var userIdFinal = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var itensFinal = await _context.CarrinhoItens
                .Include(ci => ci.Produto)
                .Where(ci => ci.UserId == userIdFinal)
                .ToListAsync();

            if (!itensFinal.Any())
            {
                ModelState.AddModelError("", "O carrinho está vazio.");
                return View("Checkout", model);
            }

            // Criar nova encomenda
            var encomenda = new Encomenda
            {
                Id = Guid.NewGuid(),
                UtilizadorId = userIdFinal,
                DataEncomenda = DateTime.Now,
                Estado = "Pendente",
                Total = itensFinal.Sum(i => i.Produto.Preco * i.Quantidade),
                Itens = new List<ItemEncomenda>()
            };

            // Processa cada item do carrinho
            foreach (var item in itensFinal)
            {
                if (item.Quantidade > item.Produto.Stock)
                {
                    ModelState.AddModelError("", $"Stock insuficiente para o produto {item.Produto.Nome}.");
                    ViewBag.Total = itensFinal.Sum(i => i.Produto.Preco * i.Quantidade);
                    return View("Checkout", model);
                }

                // Adiciona item à encomenda
                encomenda.Itens.Add(new ItemEncomenda
                {
                    Id = Guid.NewGuid(),
                    EncomendaId = encomenda.Id,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.Produto.Preco
                });

                // Diminui stock
                item.Produto.Stock -= item.Quantidade;
                _context.Produtos.Update(item.Produto);
            }

            // Adiciona encomenda e remove itens do carrinho
            _context.Encomendas.Add(encomenda);
            _context.CarrinhoItens.RemoveRange(itensFinal);

            await _context.SaveChangesAsync();

            TempData["Mensagem"] = "Pagamento efetuado e encomenda criada com sucesso!";
            return RedirectToAction("Index", "Home");
        }



        [Authorize]
        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var itens = await _context.CarrinhoItens
                .Include(ci => ci.Produto)
                .Where(ci => ci.UserId == userId)
                .ToListAsync();

            ViewBag.Total = itens.Sum(i => i.Produto.Preco * i.Quantidade);

            var model = new CheckoutViewModel();
            return View(model);
        }


    }
}
