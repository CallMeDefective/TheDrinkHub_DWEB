using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheDrinkHub_DWEB.Models;

public class ProdutoEditViewModel
{
    public Guid Id { get; set; }

    [Required]
    public string Nome { get; set; }

    public string Descricao { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Preco { get; set; }

    public int Stock { get; set; }

    public string ImagemUrl { get; set; }

    // Para o multiselect
    public List<Guid> SelectedCategorias { get; set; } = new();

    public List<SelectListItem> Categorias { get; set; } = new();
}
