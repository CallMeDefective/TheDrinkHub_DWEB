using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TheDrinkHub_DWEB.Models;

public class ProdutoCreateViewModel
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Stock { get; set; }
    public string ImagemUrl { get; set; }

    [Display(Name = "Categorias")]
    public List<Guid> SelectedCategorias { get; set; } = new List<Guid>();

    public List<SelectListItem> Categorias { get; set; }
}
