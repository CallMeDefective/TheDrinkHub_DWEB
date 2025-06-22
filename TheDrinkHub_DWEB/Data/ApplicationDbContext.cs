using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheDrinkHub_DWEB.Models;
using TheDrinkHub_DWEB.Models;

namespace TheDrinkHub_DWEB.Data;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    /// <summary>
    /// Tabela de produtos.
    /// </summary>
    public DbSet<Produto> Produtos { get; set; }

        /// <summary>
        /// Tabela de categorias.
        /// </summary>
        public DbSet<Categoria> Categorias { get; set; }

        /// <summary>
        /// Tabela de relacionamento entre produtos e categorias.
        /// </summary>
        public DbSet<ProdutoCategoria> ProdutoCategorias { get; set; }

        /// <summary>
        /// Tabela de encomendas.
        /// </summary>
        public DbSet<Encomenda> Encomendas { get; set; }

        /// <summary>
        /// Tabela de itens de encomenda.
        /// </summary>
        public DbSet<ItemEncomenda> ItensEncomenda { get; set; }

        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }

    /// <summary>
    /// Configurações adicionais do modelo de dados.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da relação muitos-para-muitos entre Produto e Categoria
            modelBuilder.Entity<ProdutoCategoria>()
                .HasKey(pc => new { pc.ProdutoId, pc.CategoriaId });

            modelBuilder.Entity<ProdutoCategoria>()
                .HasOne(pc => pc.Produto)
                .WithMany(p => p.Categorias)
                .HasForeignKey(pc => pc.ProdutoId);

            modelBuilder.Entity<ProdutoCategoria>()
                .HasOne(pc => pc.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(pc => pc.CategoriaId);

        // Relacionamento Encomenda - ApplicationUser (Utilizador)
        modelBuilder.Entity<Encomenda>()
            .HasOne(e => e.Utilizador)
            .WithMany(u => u.Encomendas)  // certifica que ApplicationUser tem ICollection<Encomenda> Encomendas
            .HasForeignKey(e => e.UtilizadorId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relacionamento ItemEncomenda - Encomenda
        modelBuilder.Entity<ItemEncomenda>()
            .HasOne(i => i.Encomenda)
            .WithMany(e => e.Itens)
            .HasForeignKey(i => i.EncomendaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Relacionamento ItemEncomenda - Produto
        modelBuilder.Entity<ItemEncomenda>()
            .HasOne(i => i.Produto)
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}