namespace CardosoRestaurante.Services.CupaoAPI.Models.DTO
{
    public class CupaoDto
    {
        public int CupaoId { get; set; }
        public string CupaoCodigo { get; set; }
        public double Desconto { get; set; }
        public int ValorMinimo { get; set; }
    }
}