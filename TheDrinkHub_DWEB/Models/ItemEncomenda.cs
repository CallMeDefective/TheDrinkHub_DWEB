using System.ComponentModel.DataAnnotations;

namespace TheDrinkHub_DWEB.Models;

/// <summary>
/// Representa um produto específico numa encomenda.
/// </summary>
public class ItemEncomenda
{
    /// <summary>
    /// Identificador único do item.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// FK da encomenda.
    /// </summary>
    public Guid EncomendaId { get; set; }

    /// <summary>
    /// Encomenda associada.
    /// </summary>
    public Encomenda Encomenda { get; set; } = null!;

    /// <summary>
    /// FK do produto.
    /// </summary>
    public Guid ProdutoId { get; set; }

    /// <summary>
    /// Produto associado.
    /// </summary>
    public Produto Produto { get; set; } = null!;

    /// <summary>
    /// Quantidade encomendada.
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    [Display(Name = "Quantidade")]
    public int Quantidade { get; set; }

    /// <summary>
    /// Preço do produto no momento da encomenda.
    /// </summary>
    [Display(Name = "Preço Unitário")]
    public decimal PrecoUnitario { get; set; }
}