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
        public static string ProdutoAPIBase { get; set; } //ProdutoAPIBase: Essa propriedade do tipo string representa a URL base da API de produtos.
        public static string CarrinhoAPIBase { get; set; } //CarrinhoAPIBase: Essa propriedade do tipo string representa a URL base da API de carrinhos.

        public const string FuncaoAdmin = "Administrador";
        public const string FuncaoUtilizador = "Utilizador";
        public const string TokenCookie = "JWTToken"; //TokenCookie: Essa propriedade do tipo string representa o nome do cookie que armazena o token JWT.
    }
}