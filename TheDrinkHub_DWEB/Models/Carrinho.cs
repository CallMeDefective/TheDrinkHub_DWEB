namespace TheDrinkHub_DWEB.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Carrinho
    {
        // Propriedade Id (chave primária)
        public int Id { get; set; }

        // Propriedade IdProduto (FK - chave estrangeira para o modelo Produto)
        [ForeignKey("Produto")]
        public int IdProduto { get; set; }

        // Propriedade Produto (referência ao modelo Produto)
        public Produto Produto { get; set; }

        // Propriedade IdUtilizador (FK - chave estrangeira para o modelo Utilizador)
        [ForeignKey("Utilizador")]
        public int IdUtilizador { get; set; }

        // Propriedade Utilizador (referência ao modelo Utilizador)
        public Utilizador Utilizador { get; set; }

        // Propriedade Preço (preço do produto no carrinho)
        [Required]
        public decimal Preco { get; set; }

        // Propriedade Quantidade (quantidade do produto no carrinho)
        [Required]
        public int Quantidade { get; set; }
    }

}
