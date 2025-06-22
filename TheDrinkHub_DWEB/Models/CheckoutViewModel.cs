using System;
using System.ComponentModel.DataAnnotations;

namespace TheDrinkHub_DWEB.Models.ViewModels
{
    public class CheckoutViewModel
    {
        [Required(ErrorMessage = "O número do cartão é obrigatório")]
        [RegularExpression(@"^\d{16}$", ErrorMessage = "O cartão deve conter exatamente 16 dígitos numéricos")]
        [Display(Name = "Número do Cartão")]
        public string Cartao { get; set; }

        [Required(ErrorMessage = "A validade é obrigatória")]
        [Display(Name = "Validade")]
        [DataType(DataType.Date)]
        [ValidadeFutura(ErrorMessage = "A validade deve ser uma data futura")]
        public DateTime Validade { get; set; } // Usar DateTime para facilitar validação

        [Required(ErrorMessage = "O CVV é obrigatório")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "O CVV deve conter exatamente 3 dígitos numéricos")]
        [Display(Name = "CVV")]
        public string CVV { get; set; }
    }

    // Validador customizado para data futura
    public class ValidadeFuturaAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime data)
            {
                if (data < DateTime.Today)
                    return new ValidationResult(ErrorMessage ?? "A data deve ser no futuro.");
            }
            return ValidationResult.Success;
        }
    }
}
