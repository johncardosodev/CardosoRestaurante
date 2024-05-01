using System.ComponentModel.DataAnnotations;

namespace CardosoRestaurante.Web.Models
{
    public class CupaoDto
    {
        public int CupaoId { get; set; }
        [Display(Name = "Código do Cupão")]
        public string CupaoCodigo { get; set; }
        public double Desconto { get; set; }
        [Display(Name = "Valor minimo")]
        public int ValorMinimo { get; set; }
    }
}