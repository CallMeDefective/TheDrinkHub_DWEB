using System;
using System.ComponentModel.DataAnnotations;

namespace TheDrinkHub_DWEB.Models.ViewModels
{
    public class EditPerfilViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "Máximo de 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [StringLength(9, MinimumLength = 9, ErrorMessage = "O NIF deve ter 9 dígitos.")]
        public string? Nif { get; set; }

        [Required(ErrorMessage = "A morada é obrigatória.")]
        [StringLength(200, ErrorMessage = "Máximo de 200 caracteres.")]
        public string Morada { get; set; }


        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
    }
}

