namespace TheDrinkHub_DWEB.Models;

/// <summary>
/// Relação muitos-para-muitos entre produtos e categorias.
/// </summary>
public class ProdutoCategoria
{
    /// <summary>
    /// FK do produto.
    /// </summary>
    public Guid ProdutoId { get; set; }

    /// <summary>
    /// Produto associado.
    /// </summary>
    public Produto Produto { get; set; } = null!;

    /// <summary>
    /// FK da categoria.
    /// </summary>
    public Guid CategoriaId { get; set; }

    /// <summary>
    /// Categoria associada.
    /// </summary>
    public Categoria Categoria { get; set; } = null!;
}