namespace CardosoRestaurante.Services.AuthAPI
{
    public class JwtOptions
    {
        public string Secret { get; set; } = string.Empty; //Chave secreta para encriptar o token
        public string Issuer { get; set; } = string.Empty; //Emissor do token que é o nosso servidor
        public string Audience { get; set; } = string.Empty; //Audiencia do token que é o nosso servidor
    }
}