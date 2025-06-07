using Microsoft.AspNetCore.Identity;

namespace TheDrinkHub_DWEB.Models;
using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Representa um utilizador da aplicação com dados adicionais.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Nome completo do utilizador.
    /// </summary>
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string Nome { get; set; }

    /// <summary>
    /// Data de nascimento do utilizador.
    /// </summary>
    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [DataType(DataType.Date)]
    public DateTime DataNascimento { get; set; }

    /// <summary>
    /// Número de Identificação Fiscal (NIF) do utilizador.
    /// Pode ser nulo.
    /// </summary>
    [StringLength(9, MinimumLength = 9, ErrorMessage = "O NIF deve ter 9 dígitos.")]
    public string? Nif { get; set; }

    /// <summary>
    /// Morada do utilizador.
    /// </summary>
    [Required(ErrorMessage = "A morada é obrigatória.")]
    [StringLength(200, ErrorMessage = "A morada não pode ter mais de 200 caracteres.")]
    public string Morada { get; set; }
}
