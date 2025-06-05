using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TheDrinkHub_DWEB.Models;

/// <summary>
/// Representa uma encomenda feita por um utilizador.
/// </summary>
public class Encomenda
{
    public Encomenda()
    {
        Itens = new HashSet<ItemEncomenda>();
    }

    /// <summary>
    /// Identificador único da encomenda.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// ID do utilizador (Identity).
    /// </summary>
    public string UtilizadorId { get; set; } = null!;

    /// <summary>
    /// Utilizador que fez a encomenda.
    /// </summary>
    public IdentityUser Utilizador { get; set; } = null!;

    /// <summary>
    /// Data da encomenda.
    /// </summary>
    [Display(Name = "Data")]
    public DateTime DataEncomenda { get; set; }

    /// <summary>
    /// Estado atual da encomenda.
    /// </summary>
    [Display(Name = "Estado")]
    public string Estado { get; set; } = "Pendente";

    /// <summary>
    /// Total da encomenda.
    /// </summary>
    [Display(Name = "Total")]
    public decimal Total { get; set; }

    /// <summary>
    /// Itens incluídos na encomenda.
    /// </summary>
    public ICollection<ItemEncomenda> Itens { get; set; }
}