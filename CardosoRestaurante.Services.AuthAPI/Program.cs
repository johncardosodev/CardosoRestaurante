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

/*Essa linha de código configura as opções de autenticação JWT (JSON Web Token) na aplicação. Vamos analisar passo a passo:
1.	builder.Services.Configure<JwtOptions>: Este método é usado para configurar as opções para um serviço específico. Neste caso, ele configura as opções para a classe JwtOptions.
2.	builder.Configuration.GetSection("ApiSettings:JwtOptions"): Isso recupera a seção de configuração chamada "ApiSettings:JwtOptions" da configuração da aplicação. O arquivo de configuração geralmente contém configurações e valores que podem ser acessados pela aplicação.
3.	builder.Services.Configure<JwtOptions>(...): Este método recebe a seção de configuração recuperada no passo anterior e a aplica à classe JwtOptions. Ele associa os valores de configuração às propriedades da classe JwtOptions, permitindo que a aplicação utilize essas opções para autenticação JWT.
Em resumo, essa linha de código recupera as configurações de autenticação JWT do arquivo de configuração da aplicação e as aplica à classe JwtOptions, permitindo que a aplicação utilize essas opções para autenticação JWT.*/
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions")); //Configurar as opcoes do JWT ou Em resumo, essa linha de código recupera as configurações de autenticação JWT do arquivo de configuração da aplicação e as aplica à classe JwtOptions, permitindo que a aplicação utilize essas opções para autenticação JWT.*/

/*O código selecionado adiciona o serviço de autenticação e o serviço de base de dados ao contêiner de serviços da aplicação. Vamos analisar cada parte do código:
1.	builder.Services.AddIdentity<ApplicationUser, IdentityRole>(): Esta linha adiciona o serviço de autenticação à aplicação. O método AddIdentity configura a autenticação usando a classe ApplicationUser como modelo para os usuários e a classe IdentityRole como modelo para os papéis de usuário. Essas classes são fornecidas pelo pacote Microsoft.AspNetCore.Identity.
2.	.AddEntityFrameworkStores<AppDbContext>(): Esta linha adiciona o serviço de base de dados à aplicação. O método AddEntityFrameworkStores configura o serviço para usar o AppDbContext como o contexto do banco de dados. Isso permite que a aplicação armazene e recupere informações relacionadas à autenticação no banco de dados.
3.	.AddDefaultTokenProviders(): Esta linha adiciona o serviço de tokens à aplicação. Os tokens são usados para fins de autenticação, como redefinição de senha e confirmação de email.
Essas duas linhas de código são importantes para configurar a autenticação e a persistência de dados relacionados à autenticação na aplicação. Elas são frequentemente usadas em conjunto para fornecer recursos de autenticação, como registro de usuários, login, gerenciamento de senhas e controle de acesso baseado em papéis.
*/
builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //Adiciona o servico de autenticacao
    .AddEntityFrameworkStores<AppDbContext>() //Adiciona o servico de base de dados
    .AddDefaultTokenProviders(); //Adiciona o servico de tokens

builder.Services.AddControllers();

/*registra o serviço de geração de tokens JWT no contêiner de serviços da aplicação, permitindo que ele seja injetado e usado em outras partes do código.
 * O uso do método AddScoped com a interface IJwtTokenGenerator e a classe JwtTokenGenerator garante que, quando o serviço for solicitado, uma instância da classe JwtTokenGenerator será criada e compartilhada dentro do mesmo escopo. Isso permite que a aplicação tenha um controle centralizado sobre a lógica de geração de tokens JWT e facilite a manutenção e testabilidade do código.
*/
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); //Adiciona o servico de geracao de token

/*A linha de código selecionada adiciona um serviço de autenticação à aplicação.
builder.Services.AddScoped<IAuthService, AuthService>();
•	builder.Services: A propriedade Services do objeto builder representa o contêiner de serviços da aplicação. É onde todos os serviços são registrados e podem ser acessados posteriormente.
•	AddScoped<IAuthService, AuthService>(): O método AddScoped é usado para registrar um serviço no contêiner de serviços com um tempo de vida de escopo. Isso significa que uma instância do serviço será criada e compartilhada dentro do mesmo escopo durante a vida útil da solicitação.
•	IAuthService: É a interface que define o contrato para o serviço de autenticação. Interfaces são usadas para definir um conjunto de métodos que uma classe deve implementar.
•	AuthService: É a classe concreta que implementa a interface IAuthService. Essa classe contém a lógica real para o serviço de autenticação.

Em resumo, essa linha de código registra o serviço de autenticação no contêiner de serviços da aplicação, permitindo que ele seja injetado e usado em outras partes do código. O uso do método AddScoped com a interface IAuthService e a classe AuthService garante que, quando o serviço for solicitado, uma instância da classe AuthService será criada e compartilhada dentro do mesmo escopo. Isso permite que a aplicação tenha um controle centralizado sobre a lógica de autenticação e facilite a manutenção e testabilidade do código.*/
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

/*O código selecionado é uma função chamada "UpdateMigracoes" que é responsável por aplicar as migrações pendentes em um banco de dados. Vamos analisar o código passo a passo:
1.Primeiro, é criado um novo escopo usando a função "CreateScope" do serviço "app.Services". Esse escopo permite acessar o banco de dados e executar as migrações.
2.	Em seguida, é obtido o serviço "AppDbContext" do escopo usando a função "GetRequiredService". Esse serviço representa o contexto do banco de dados que foi registrado no container de serviços.
3.	Depois, é verificado se existem migrações pendentes para serem aplicadas. Isso é feito chamando a função "GetPendingMigrations" do objeto "Database" do contexto do banco de dados e verificando o número de migrações pendentes usando a função "Count()".
4.	Se houver migrações pendentes, a função "Migrate" é chamada no objeto "Database" do contexto do banco de dados. Isso aplica as migrações pendentes e atualiza o esquema do banco de dados de acordo.
Essa função é útil quando você está usando o Entity Framework Core para gerenciar as migrações do banco de dados. Ela garante que todas as migrações pendentes sejam aplicadas automaticamente quando o aplicativo é iniciado. Isso é especialmente útil durante o desenvolvimento, quando você está iterando no esquema do banco de dados e precisa aplicar as alterações no banco de dados local.*/
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