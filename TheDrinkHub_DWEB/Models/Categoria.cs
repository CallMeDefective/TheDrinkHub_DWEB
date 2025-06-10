namespace TheDrinkHub_DWEB.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Representa uma categoria de produtos.
    /// </summary>
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new HashSet<ProdutoCategoria>();
        }

        /// <summary>
        /// Identificador único da categoria.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da categoria.
        /// </summary>
        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; } = null!;

        /// <summary>
        /// Descrição da categoria.
        /// </summary>
        [Display(Name = "Descrição")]
        public string? Descricao { get; set; }

        /// <summary>
        /// Relação com os produtos associados.
        /// </summary>
        public ICollection<ProdutoCategoria> Produtos { get; set; }
    }

}
