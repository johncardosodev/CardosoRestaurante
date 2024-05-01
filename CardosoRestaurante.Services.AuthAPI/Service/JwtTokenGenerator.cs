using CardosoRestaurante.Services.AuthAPI.Models;
using CardosoRestaurante.Services.AuthAPI.Service.IService;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CardosoRestaurante.Services.AuthAPI.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtOptions _jwtOptions;  //Injeção de dependência que vem do appsettings.json

        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions) //IOptions é uma interface fornecida pelo framework .NET que permite acessar as opções de configuração em um aplicativo.
        {
            _jwtOptions = jwtOptions.Value; //Obter o valor das opções de configuração JwtOptions que foram definidas no arquivo appsettings.json ou em outra fonte de configuração. Essas opções podem incluir informações como a chave secreta, emissor e audiência do token JWT.
        }

        //Aqui iremos configurar o token emissor e secrekey, e auadiance
        public string GenerateToken(ApplicationUser applicationUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //Cria uma instancia jwtSecurityTokenHandler que é responsável por criar e validar tokens

            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret); //Converte a chave secreta para bytes que será usada para encriptar o token

            var claimList = new List<Claim> //Cria uma lista de claims que são informações sobre o usuário que serão armazenadas no token. Neste caso, armazenamos o nome, email e id do usuário
            {
                new Claim(ClaimTypes.Name, applicationUser.Nome), //Claim do nome
                new Claim(ClaimTypes.Email, applicationUser.Email), //Claim do email
                new Claim(ClaimTypes.NameIdentifier, applicationUser.Id) //Claim do id
            };

            var tokenDescriptor = new SecurityTokenDescriptor //Instancia SecurityTokenDescriptor que define as propriedades do token, como tempo de expiração, audiência, emissor e as credenciais de assinatura (chave secreta)
            {
                Audience = _jwtOptions.Audience, //Audiencia do token
                Issuer = _jwtOptions.Issuer, //Emissor do token

                Subject = new ClaimsIdentity(claimList), //Identidade do token
                Expires = DateTime.UtcNow.AddDays(7), //Tempo de expiração do token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor); //	Chama o método CreateToken() do tokenHandler passando o tokenDescriptor como argumento para criar o token JWT.

            return tokenHandler.WriteToken(token); //Retorna o token JWT como uma string usando o método WriteToken() do tokenHandler.
        }
    }
}