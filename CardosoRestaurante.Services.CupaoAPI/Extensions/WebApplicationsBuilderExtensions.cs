using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CardosoRestaurante.Services.CupaoAPI.Extensions
{
    public static class WebApplicationsBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthetication(this WebApplicationBuilder builder)
        {
            var settings = builder.Configuration.GetSection("ApiSettings"); //Obter as configuracoes da ApiSettings
            var secret = settings.GetValue<string>("Secret"); //Irá buscar a ApiSettings
            var issuer = settings.GetValue<string>("Issuer"); //Irá buscar a ApiSettings
            var audience = settings.GetValue<string>("Audience"); //Irá buscar a ApiSettings
            var key = Encoding.ASCII.GetBytes(secret); //Converte a chave para bytes

            /*Este código configura a autenticação JWT Bearer em um serviço de autenticação de um aplicativo ASP.NET Core. Ele define as configurações de validação do token JWT, permitindo que o aplicativo verifique a autenticidade e a integridade dos tokens JWT recebidos durante as solicitações de autenticação.
            Aqui está uma explicação passo a passo do código:
            1.	A função AddAuthentication é chamada para configurar a autenticação no serviço de autenticação do aplicativo ASP.NET Core.
            2.	Dentro dessa função, são definidos dois esquemas de autenticação: DefaultAuthenticateScheme e DefaultChallengeScheme. Ambos são definidos como JwtBearerDefaults.AuthenticationScheme, indicando que o esquema de autenticação padrão é o JWT Bearer.
            3.	Em seguida, a função AddJwtBearer é chamada para configurar o esquema de autenticação JWT Bearer.
            4.	Dentro dessa função, são definidos os parâmetros de validação do token JWT no objeto TokenValidationParameters. Esses parâmetros incluem:
            •	ValidateIssuerSigningKey: indica se a chave de assinatura do emissor deve ser validada.
            •	IssuerSigningKey: especifica a chave de assinatura do emissor.
            •	ValidateIssuer: indica se o emissor do token deve ser validado.
            •	ValidIssuer: especifica o emissor válido do token.
            •	ValidateAudience: indica se a audiência do token deve ser validada.
            •	ValidAudience: especifica a audiência válida do token.
            5.	Essas configurações de validação garantem que o aplicativo possa verificar se um token JWT é autêntico e válido antes de permitir o acesso a recursos protegidos.
            Em resumo, esse código configura a autenticação JWT Bearer no aplicativo ASP.NET Core, definindo as configurações de validação do token JWT para garantir a segurança e a integridade das solicitações de autenticação.*/
            builder.Services.AddAuthentication(x =>    //Código configura a autenticação JWT Bearer no serviço de autenticação do aplicativo ASP.NET Core, definindo as configurações de validação do token JWT. Isso permite que o aplicativo verifique a autenticidade e a integridade dos tokens JWT recebidos durante as solicitações de autenticação.

            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience
                };
            });

            return builder;
        }
    }
}