using CardosoRestaurante.Services.AuthAPI;
using CardosoRestaurante.Services.AuthAPI.Data;
using CardosoRestaurante.Services.AuthAPI.Models;
using CardosoRestaurante.Services.AuthAPI.Service;
using CardosoRestaurante.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Servico de base de dados SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); //Ligacao a base de dados SQL Server
});

/*Essa linha de c�digo configura as op��es de autentica��o JWT (JSON Web Token) na aplica��o. Vamos analisar passo a passo:
1.	builder.Services.Configure<JwtOptions>: Este m�todo � usado para configurar as op��es para um servi�o espec�fico. Neste caso, ele configura as op��es para a classe JwtOptions.
2.	builder.Configuration.GetSection("ApiSettings:JwtOptions"): Isso recupera a se��o de configura��o chamada "ApiSettings:JwtOptions" da configura��o da aplica��o. O arquivo de configura��o geralmente cont�m configura��es e valores que podem ser acessados pela aplica��o.
3.	builder.Services.Configure<JwtOptions>(...): Este m�todo recebe a se��o de configura��o recuperada no passo anterior e a aplica � classe JwtOptions. Ele associa os valores de configura��o �s propriedades da classe JwtOptions, permitindo que a aplica��o utilize essas op��es para autentica��o JWT.
Em resumo, essa linha de c�digo recupera as configura��es de autentica��o JWT do arquivo de configura��o da aplica��o e as aplica � classe JwtOptions, permitindo que a aplica��o utilize essas op��es para autentica��o JWT.*/
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions")); //Configurar as opcoes do JWT ou Em resumo, essa linha de c�digo recupera as configura��es de autentica��o JWT do arquivo de configura��o da aplica��o e as aplica � classe JwtOptions, permitindo que a aplica��o utilize essas op��es para autentica��o JWT.*/

/*O c�digo selecionado adiciona o servi�o de autentica��o e o servi�o de base de dados ao cont�iner de servi�os da aplica��o. Vamos analisar cada parte do c�digo:
1.	builder.Services.AddIdentity<ApplicationUser, IdentityRole>(): Esta linha adiciona o servi�o de autentica��o � aplica��o. O m�todo AddIdentity configura a autentica��o usando a classe ApplicationUser como modelo para os usu�rios e a classe IdentityRole como modelo para os pap�is de usu�rio. Essas classes s�o fornecidas pelo pacote Microsoft.AspNetCore.Identity.
2.	.AddEntityFrameworkStores<AppDbContext>(): Esta linha adiciona o servi�o de base de dados � aplica��o. O m�todo AddEntityFrameworkStores configura o servi�o para usar o AppDbContext como o contexto do banco de dados. Isso permite que a aplica��o armazene e recupere informa��es relacionadas � autentica��o no banco de dados.
3.	.AddDefaultTokenProviders(): Esta linha adiciona o servi�o de tokens � aplica��o. Os tokens s�o usados para fins de autentica��o, como redefini��o de senha e confirma��o de email.
Essas duas linhas de c�digo s�o importantes para configurar a autentica��o e a persist�ncia de dados relacionados � autentica��o na aplica��o. Elas s�o frequentemente usadas em conjunto para fornecer recursos de autentica��o, como registro de usu�rios, login, gerenciamento de senhas e controle de acesso baseado em pap�is.
*/
builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //Adiciona o servico de autenticacao
    .AddEntityFrameworkStores<AppDbContext>() //Adiciona o servico de base de dados
    .AddDefaultTokenProviders(); //Adiciona o servico de tokens

builder.Services.AddControllers();

/*registra o servi�o de gera��o de tokens JWT no cont�iner de servi�os da aplica��o, permitindo que ele seja injetado e usado em outras partes do c�digo.
 * O uso do m�todo AddScoped com a interface IJwtTokenGenerator e a classe JwtTokenGenerator garante que, quando o servi�o for solicitado, uma inst�ncia da classe JwtTokenGenerator ser� criada e compartilhada dentro do mesmo escopo. Isso permite que a aplica��o tenha um controle centralizado sobre a l�gica de gera��o de tokens JWT e facilite a manuten��o e testabilidade do c�digo.
*/
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); //Adiciona o servico de geracao de token

/*A linha de c�digo selecionada adiciona um servi�o de autentica��o � aplica��o.
builder.Services.AddScoped<IAuthService, AuthService>();
�	builder.Services: A propriedade Services do objeto builder representa o cont�iner de servi�os da aplica��o. � onde todos os servi�os s�o registrados e podem ser acessados posteriormente.
�	AddScoped<IAuthService, AuthService>(): O m�todo AddScoped � usado para registrar um servi�o no cont�iner de servi�os com um tempo de vida de escopo. Isso significa que uma inst�ncia do servi�o ser� criada e compartilhada dentro do mesmo escopo durante a vida �til da solicita��o.
�	IAuthService: � a interface que define o contrato para o servi�o de autentica��o. Interfaces s�o usadas para definir um conjunto de m�todos que uma classe deve implementar.
�	AuthService: � a classe concreta que implementa a interface IAuthService. Essa classe cont�m a l�gica real para o servi�o de autentica��o.

Em resumo, essa linha de c�digo registra o servi�o de autentica��o no cont�iner de servi�os da aplica��o, permitindo que ele seja injetado e usado em outras partes do c�digo. O uso do m�todo AddScoped com a interface IAuthService e a classe AuthService garante que, quando o servi�o for solicitado, uma inst�ncia da classe AuthService ser� criada e compartilhada dentro do mesmo escopo. Isso permite que a aplica��o tenha um controle centralizado sobre a l�gica de autentica��o e facilite a manuten��o e testabilidade do c�digo.*/
builder.Services.AddScoped<IAuthService, AuthService>(); //Adiciona o servico de autenticacao

//Adicionaei pelo CupaoAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())//Adicionaei pelo CupaoAPI
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication(); //Autenticacao tem que ser sempre antes de UseAuthorization
app.UseAuthorization();

app.MapControllers();
UpdateMigracoes();

app.Run();

/*O c�digo selecionado � uma fun��o chamada "UpdateMigracoes" que � respons�vel por aplicar as migra��es pendentes em um banco de dados. Vamos analisar o c�digo passo a passo:
1.Primeiro, � criado um novo escopo usando a fun��o "CreateScope" do servi�o "app.Services". Esse escopo permite acessar o banco de dados e executar as migra��es.
2.	Em seguida, � obtido o servi�o "AppDbContext" do escopo usando a fun��o "GetRequiredService". Esse servi�o representa o contexto do banco de dados que foi registrado no container de servi�os.
3.	Depois, � verificado se existem migra��es pendentes para serem aplicadas. Isso � feito chamando a fun��o "GetPendingMigrations" do objeto "Database" do contexto do banco de dados e verificando o n�mero de migra��es pendentes usando a fun��o "Count()".
4.	Se houver migra��es pendentes, a fun��o "Migrate" � chamada no objeto "Database" do contexto do banco de dados. Isso aplica as migra��es pendentes e atualiza o esquema do banco de dados de acordo.
Essa fun��o � �til quando voc� est� usando o Entity Framework Core para gerenciar as migra��es do banco de dados. Ela garante que todas as migra��es pendentes sejam aplicadas automaticamente quando o aplicativo � iniciado. Isso � especialmente �til durante o desenvolvimento, quando voc� est� iterando no esquema do banco de dados e precisa aplicar as altera��es no banco de dados local.*/
void UpdateMigracoes()
{
    using (var scope = app.Services.CreateScope()) //Criar um novo escopo que vai permitir aceder a base de dados e fazer as migracoes
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>(); //Obter o servico da base de dados que foi registado no container
        if (_db.Database.GetPendingMigrations().Count() > 0) //Verificar se existem migracoes pendentes para serem aplicadas
        {
            _db.Database.Migrate(); //Aplicar as migracoes pendentes
        }
    }
}