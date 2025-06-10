using Microsoft.AspNetCore.Identity;

namespace TheDrinkHub_DWEB.Models;
using System;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Representa um utilizador da aplicação com dados adicionais.
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// Email do utilizador.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Password do utilziador.
    /// </summary>
    public string Password { get; set; }
}
