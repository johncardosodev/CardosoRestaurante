using System.ComponentModel.DataAnnotations;

namespace CardosoRestaurante.Services.CupaoAPI.Models
{
    public class Cupao
    {
        [Key]
        public int CupaoId { get; set; }

        [Required]
        [Display(Name = "Código do Cupão")]
        public string CupaoCodigo { get; set; }

        [Required]
        public double Desconto { get; set; }

        public int ValorMinimo { get; set; }
    }
}