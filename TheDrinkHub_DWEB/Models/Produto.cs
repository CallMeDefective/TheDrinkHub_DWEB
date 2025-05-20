namespace TheDrinkHub_DWEB.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Produto
    {
        // Propriedade Id (chave primária)
        public int Id { get; set; }

        // Propriedade Nome
        [Required]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        // Propriedade Preço
        [Required]
        [Range(0.01, 10000, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        // Propriedade Descrição
        [StringLength(1000, ErrorMessage = "A descrição deve ter no máximo 1000 caracteres.")]
        public string Descricao { get; set; }

        // Propriedade Imagem (pode ser o caminho para o ficheiro da imagem)
        [StringLength(255)]
        public string Imagem { get; set; }

        // Propriedade IdCategoria (FK - chave estrangeira para a Categoria)
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        // Propriedade Categoria (navegação para a Categoria)
        public Categoria Categoria { get; set; }
    }

}
