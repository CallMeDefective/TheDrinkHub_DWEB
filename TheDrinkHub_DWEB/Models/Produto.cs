namespace TheDrinkHub_DWEB.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Representa um produto disponível para venda.
    /// </summary>
    public class Produto
    {
        public Produto()
        {
            Categorias = new HashSet<ProdutoCategoria>();
            ItensEncomenda = new HashSet<ItemEncomenda>();
        }

        /// <summary>
        /// Identificador único do produto.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Descrição do produto.
        /// </summary>
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        /// <summary>
        /// Preço do produto.
        /// </summary>
        [Required]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        /// <summary>
        /// Quantidade em stock.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Stock")]
        public int Stock { get; set; }

        /// <summary>
        /// Caminho para a imagem do produto.
        /// </summary>
        [Display(Name = "Imagem")]
        public string? ImagemUrl { get; set; }

        /// <summary>
        /// Relação com as categorias do produto.
        /// </summary>
        public ICollection<ProdutoCategoria> Categorias { get; set; }

        /// <summary>
        /// Itens de encomenda que incluem este produto.
        /// </summary>
        public ICollection<ItemEncomenda> ItensEncomenda { get; set; }
    }

}
