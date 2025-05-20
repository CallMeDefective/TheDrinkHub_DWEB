namespace TheDrinkHub_DWEB.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome da categoria deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        // Lista de produtos relacionados
        public List<Produto> Produtos { get; set; }
    }

}
