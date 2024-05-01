namespace CardosoRestaurante.Web.Utility
{
    public class SD
    {//Classe de utilidade para armazenar constantes Static Data
        public enum APITipo
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public static string CupaoAPIBase { get; set; } //CupaoAPIBase: Essa propriedade do tipo string representa a URL base da API de cupons.
        public static string AuthAPIBase { get; set; } //AuthAPIBase: Essa propriedade do tipo string representa a URL base da API de autenticação.
    }
}