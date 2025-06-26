using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheDrinkHub_DWEB.Data;
using TheDrinkHub_DWEB.Models;

namespace TheDrinkHub_DWEB.Seeds
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!context.Categorias.Any())
            {
                // Criar categorias
                var categorias = new List<Categoria>
                {
                    new Categoria { Id = Guid.NewGuid(), Nome = "Cerveja", Descricao = "Variedade de cervejas artesanais e comerciais" },
                    new Categoria { Id = Guid.NewGuid(), Nome = "Whisky", Descricao = "Whiskys escoceses, irlandeses e americanos" },
                    new Categoria { Id = Guid.NewGuid(), Nome = "Mais Vendidas", Descricao = "Produtos mais populares entre os clientes" },
                    new Categoria { Id = Guid.NewGuid(), Nome = "Refrigerantes", Descricao = "Bebidas não alcoólicas refrescantes" },
                    new Categoria { Id = Guid.NewGuid(), Nome = "Bebidas Brancas", Descricao = "Vodka, gin e outras bebidas destiladas" }
                };

                await context.Categorias.AddRangeAsync(categorias);
                await context.SaveChangesAsync();

                // Criar produtos
                var produtos = new List<Produto>
                {
                    // Cerveja
                    new Produto { Id = Guid.NewGuid(), Nome = "Super Bock", Preco = 1.20m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/ff1wGS8Jie3FkGxrUAiExQgeL_b60OEs1-3JXChH5Pk/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9kbHB2/aW5ob3MuYWdpbGVj/ZG4uY29tLmJyLzAw/NjkwOV8xLmpwZz92/PTE2Ny05MzUyNDY5/MDM", Descricao = "Cerveja portuguesa" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Sagres", Preco = 1.10m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/B8PIUteO66aSsfyggmo-9dNA2is_qsf9jkMMJJmR2SE/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9odHRw/Mi5tbHN0YXRpYy5j/b20vRF9RX05QXzJY/Xzc5NzA1NS1NTEI0/NDk5NTE0NjQ4OF8w/MjIwMjEtVi1jZXJ2/ZWphLXBvcnR1Z3Vl/c2Etc2FncmVzLWxh/cmdlci0zMzBtbC1r/aXQtMy14LTMzMGwu/d2VicA", Descricao = "Cerveja clássica nacional" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Heineken", Preco = 1.50m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/fAJniDol0iaCU9zPfdaAtmqTx2BrZU_hNO3riVy0ePs/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNTA0/OTA4MDI0L3B0L2Zv/dG8vY2VydmVqYS1s/YWdlci1oZWluZWtl/bi5qcGc_cz02MTJ4/NjEyJnc9MCZrPTIw/JmM9TnVKZTQ0cFlL/UkREZk9jWTVMM0tM/Z2NYVHQ1VFBwM0tB/TFM3Sm1obXhMYz0", Descricao = "Cerveja internacional" },

                    // Whisky
                    new Produto { Id = Guid.NewGuid(), Nome = "Jameson", Preco = 25.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/s8r2BYwRzC9t5DUs4XoLXmUx_-sT9ly2g31Jtn6B7go/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9kZWNh/bnRlcmxpcXVvci5j/by56YS93cC1jb250/ZW50L3VwbG9hZHMv/MjAyMi8xMi9KYW1l/c29uLU9yYW5nZS03/MDBNTC0zMDB4MzAw/LmpwZw", Descricao = "Whisky irlandês suave" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Jack Daniel's", Preco = 30.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/fhIrAAXM3KDPObL2x2nmoJAsN-4QUjs5Uvaxq5VtVqY/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pMC53/cC5jb20vZ29vZHRp/bWVsaXF1b3Jzbnlh/LmNvbS93cC1jb250/ZW50L3VwbG9hZHMv/MjAxOC8wNS9KYWNr/LURhbmllbHMuanBn/P2ZpdD0xMDAwLDEw/MDAmc3NsPTE", Descricao = "Tennessee whiskey" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Johnnie Walker", Preco = 28.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/Pp1kWT-Ii0QmQxDWJsguEFL3R8ufN4OKOQvnJJnUdMY/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly93d3cu/dmlub3JlYS5jb20v/ODIzLWhvbWVfZGVm/YXVsdC9qb2hubmll/LXdhbGtlci1kb3Vi/bGUtYmxhY2suanBn", Descricao = "Blended Scotch whisky" },

                    // Mais Vendidas
                    new Produto { Id = Guid.NewGuid(), Nome = "Vodka Eristoff", Preco = 12.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/DqJVWwSrh3yTdY6E4XiE2QrMwF6RAMBzMlyABiDalyM/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly90b3N0/LmNsL2Nkbi9zaG9w/L2ZpbGVzLzIwVksx/MDE4XzEyMDB4Lmpw/Zz92PTE3Mzg3NDIx/ODg", Descricao = "Vodka popular" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Coca-Cola", Preco = 1.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/TkKr6r29lZ0iYZbOmJFNHmrv5gonW2_xpMn2DPxl3sY/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5nZXR0eWltYWdl/cy5jb20vaWQvNDU4/NTUzMzY1L3B0L2Zv/dG8vY29jYS1jb2xh/LWZyYXNjby1kZS1w/bCVDMyVBMXN0aWNv/LWlzb2xhZG8tZW0t/ZnVuZG8tYnJhbmNv/LmpwZz9zPTYxMng2/MTImdz0wJms9MjAm/Yz12dGE2YWJIZzJw/ZlZQSzJ2Z2Uzd1FZ/b2tXRVc1R1hhT093/SldIbVFueU1JPQ", Descricao = "Refrigerante clássico" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Super Bock Stout", Preco = 1.30m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/Rn7U9wnC2FA_0vEtBsjxfdeh8UiZmqMApjWXsUxiqBA/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbWFn/ZXMuc3F1YXJlc3Bh/Y2UtY2RuLmNvbS9j/b250ZW50L3YxLzYy/ZDZlOTQ4ZTMxZDlm/NjU4Zjg3Yzk0NC8x/Njg0OTM5NjAyOTYz/LVQ5REQ2NVhYMURC/WElPV0NIVUxOLzIz/OC5qcGc", Descricao = "Cerveja preta" },

                    // Refrigerantes
                    new Produto { Id = Guid.NewGuid(), Nome = "Pepsi", Preco = 1.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/qp7bVsCI4ISN50rX9BlGW56KAZ4BYgEME8pS4mj2jaQ/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvMTcx/MjU0NTc0L3Bob3Rv/L3BlcHNpLWNvbGEt/Ym90dGxlLW9uLWJs/dWUtYW5kLXdldC1z/dXJmYWNlLmpwZz9z/PTYxMng2MTImdz0w/Jms9MjAmYz1FWnNl/SHZINy1kMHdEUTNa/enpYdEtNUXR3TWZ2/M1U5RFhJX3l1U0dw/eHRVPQ", Descricao = "Refrigerante cola" },
                    new Produto { Id = Guid.NewGuid(), Nome = "7Up", Preco = 1.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/F1tR5ItTUGpGu8SbgBBAJ20sv1dkJTf2IfM9_AREOXI/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly93d3cu/cG5nbWFydC5jb20v/ZmlsZXMvMjEvN3Vw/LVBORy1JbWFnZS5w/bmc", Descricao = "Refrigerante limão" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Fanta", Preco = 1.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/Z372C7XwEOpCdlUI8klQ3r_488OA-dzdkRaKG30dWk4/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9zaG9w/cml0ZS5uZy93cC1j/b250ZW50L3VwbG9h/ZHMvMjAyMy8wNC9w/NzUuanBn", Descricao = "Refrigerante laranja" },

                    // Bebidas Brancas
                    new Produto { Id = Guid.NewGuid(), Nome = "Gin Bombay", Preco = 18.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/YqdL4zgV2DjToXuHUkVm0mjAlXdvmbfC1T-8Qq0wFNU/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly93d3cu/YWJjLnZpcmdpbmlh/Lmdvdi9saWJyYXJ5/L3Byb2R1Y3QtaW1h/Z2VzL2dpbi9yZWd1/bGFyL2JvbWJheS1z/YXBwaGlyZS1naW4u/anBnP3Jldj1mMzA0/YTI0YjBlZmM0NGYw/ODc1NzdjNDIxYTY5/MDRkNyZsYT1lbiZo/PTQwMCZ3PTQwMCZo/YXNoPTMwMjkwRkNF/QjYyMDgzOEFFQ0VE/NTEyOTJDRTdDNzc4", Descricao = "Gin aromático" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Vodka Absolut", Preco = 15.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/um0ozHUOKoYDcP68ynsa0tmVn3ML5mt3DXYDnsFhDTU/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly93d3cu/cG5ncGxheS5jb20v/d3AtY29udGVudC91/cGxvYWRzLzE0L0Fi/c29sdXQtVm9ka2Et/VHJhbnNwYXJlbnQt/SW1hZ2VzLTIucG5n", Descricao = "Vodka sueca" },
                    new Produto { Id = Guid.NewGuid(), Nome = "Gin Tanqueray", Preco = 20.00m, Stock = 1000, ImagemUrl = "https://imgs.search.brave.com/PEvKIDMCdzf8ig_FSrujdJLLZGHD-zuQgbigYVX_pos/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9pbnRl/cm5ldHdpbmVzLmNv/bS9jZG4vc2hvcC9w/cm9kdWN0cy90YW5x/dWVyYXktZ2luLTlf/OTAweC5qcGc_dj0x/NTUxMzg5NjEw", Descricao = "Gin clássico" }
                };

                await context.Produtos.AddRangeAsync(produtos);
                await context.SaveChangesAsync();

                // Encontrar categorias para associar
                var cervejaCategoria = categorias.First(c => c.Nome == "Cerveja");
                var whiskyCategoria = categorias.First(c => c.Nome == "Whisky");
                var maisVendidasCategoria = categorias.First(c => c.Nome == "Mais Vendidas");
                var refrigerantesCategoria = categorias.First(c => c.Nome == "Refrigerantes");
                var bebidasBrancasCategoria = categorias.First(c => c.Nome == "Bebidas Brancas");

                // Associar produtos manualmente
                var produtoCategorias = new List<ProdutoCategoria>
                {
                    // Cerveja
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Super Bock").Id, CategoriaId = cervejaCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Sagres").Id, CategoriaId = cervejaCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Heineken").Id, CategoriaId = cervejaCategoria.Id },

                    // Whisky
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Jameson").Id, CategoriaId = whiskyCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Jack Daniel's").Id, CategoriaId = whiskyCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Johnnie Walker").Id, CategoriaId = whiskyCategoria.Id },

                    // Mais Vendidas
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Vodka Eristoff").Id, CategoriaId = maisVendidasCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Coca-Cola").Id, CategoriaId = maisVendidasCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Super Bock Stout").Id, CategoriaId = maisVendidasCategoria.Id },

                    // Refrigerantes
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Pepsi").Id, CategoriaId = refrigerantesCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "7Up").Id, CategoriaId = refrigerantesCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Fanta").Id, CategoriaId = refrigerantesCategoria.Id },

                    // Bebidas Brancas
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Gin Bombay").Id, CategoriaId = bebidasBrancasCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Vodka Absolut").Id, CategoriaId = bebidasBrancasCategoria.Id },
                    new ProdutoCategoria { ProdutoId = produtos.First(p => p.Nome == "Gin Tanqueray").Id, CategoriaId = bebidasBrancasCategoria.Id }
                };

                await context.ProdutoCategorias.AddRangeAsync(produtoCategorias);
                await context.SaveChangesAsync();
            }
        }
    }
}
