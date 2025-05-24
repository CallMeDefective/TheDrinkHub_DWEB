namespace TheDrinkHub_DWEB.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Utilizador
    {
        // Propriedade Id (chave primária)
        public int Id { get; set; }

        // Propriedade Nome
        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        // Propriedade Email
        [Required]
        [EmailAddress(ErrorMessage = "O email não é válido.")]
        public string Email { get; set; }

        // Propriedade Password
        [Required]
        [StringLength(100, ErrorMessage = "A password deve ter no máximo 100 caracteres.")]
        public string Password { get; set; }

        // Propriedade Data de Nascimento
        [Required]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        // Propriedade Morada
        [StringLength(200, ErrorMessage = "A morada deve ter no máximo 200 caracteres.")]
        public string Morada { get; set; }

        // Propriedade Cartão Bancário
        [StringLength(20, ErrorMessage = "O cartão bancário deve ter no máximo 20 caracteres.")]
        public string CartaoBancario { get; set; }

        // Propriedade para indicar se é Funcionário
        public bool IsFuncionario { get; set; }
    }

}
