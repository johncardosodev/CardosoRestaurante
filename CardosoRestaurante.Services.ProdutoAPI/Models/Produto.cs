using System.ComponentModel.DataAnnotations;

namespace CardosoRestaurante.Services.ProdutoAPI.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Range(1, 1000)]
        public double Preco { get; set; }

        public string Descricao { get; set; }

        [Display(Name = "Porção")]
        public int Porcao { get; set; }

        public string Categoria { get; set; }
        public string ImagemUrl { get; set; }
    }
}