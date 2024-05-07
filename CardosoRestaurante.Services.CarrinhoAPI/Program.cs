using AutoMapper;
using CardosoRestaurante.MessageBus;
using CardosoRestaurante.Services.Carrinho;
using CardosoRestaurante.Services.Carrinho.Data;
using CardosoRestaurante.Services.Carrinho.Extensions;
using CardosoRestaurante.Services.CarrinhoAPI.Service;
using CardosoRestaurante.Services.CarrinhoAPI.Service.IService;
using CardosoRestaurante.Services.CarrinhoAPI.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Servico de base de dados SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); //Ligacao a base de dados SQL Server
});

//Configurar o mapeamento entre as classes Cupao e CupaoDto
//Estas três linhas de código configuram e registram o AutoMapper no container de serviços da aplicação, permitindo que ele seja usado para realizar mapeamentos entre as classes Cupao e CupaoDto e outros tipos definidos na aplicação. Isso facilita a conversão de dados entre diferentes contextos e simplifica o código que lida com mapeamentos manuais.
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper(); //Configurar o mapeamento entre as classes Cupao e CupaoDto
builder.Services.AddSingleton(mapper); //Registar o mapeamento no container de servicos
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Registar o AutoMapper no container de servicos

//#### Autenticacao ####
builder.Services.AddHttpContextAccessor();    //Adicionar o HttpContextAccessor ao container de servicos
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>(); //Registar o servico de autenticacao no container de servicos

//###### ProdutoAPI ######
/*Este trecho de código configura um cliente HTTP chamado "Produto" que será usado para se comunicar com um serviço chamado "ProdutoAPI". Vamos analisar o código passo a passo:
1.	A função AddHttpClient é usada para adicionar um cliente HTTP chamado "Produto" ao container de serviços da aplicação. Esse cliente será usado para fazer requisições HTTP para o serviço "ProdutoAPI".
2.	Dentro da função AddHttpClient, é configurado o endereço base do serviço "ProdutoAPI" através da propriedade BaseAddress. O valor dessa propriedade é obtido a partir da configuração da aplicação, mais especificamente da chave "ServicesUrls:ProdutoAPI".
3.	Em seguida, é adicionado um HttpMessageHandler chamado BackendApiAuthenticationHttpClientHandler ao cliente HTTP. Um HttpMessageHandler é responsável por processar as requisições HTTP antes de serem enviadas para o servidor. Nesse caso, o BackendApiAuthenticationHttpClientHandler é usado para adicionar autenticação ao cliente HTTP, garantindo que as requisições sejam feitas de forma segura e autorizada.
Portanto, esse código configura um cliente HTTP chamado "Produto" que será usado para se comunicar com o serviço "ProdutoAPI". O endereço base do serviço é configurado e a autenticação é adicionada ao cliente HTTP para garantir a segurança das requisições.
*/
builder.Services.AddHttpClient("Produto", config =>
    config.BaseAddress = new Uri(builder.Configuration["ServicesUrls:ProdutoAPI"]))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>(); //Configurar o endereco base do servico Produto API

builder.Services.AddScoped<IProdutoService, ProdutoService>(); //Registar o servico de Produto no container de servicos da aplicacao

//###### CupaoAPI ######
/*Este trecho de código configura um cliente HTTP para se comunicar com um serviço chamado "CupaoAPI". Vamos analisar o código passo a passo:
1.	A função AddHttpClient é usada para adicionar um cliente HTTP chamado "Cupao" ao container de serviços da aplicação. Esse cliente será usado para fazer requisições HTTP para o serviço "CupaoAPI".
2.	Dentro da função AddHttpClient, é configurado o endereço base do serviço "CupaoAPI" através da propriedade BaseAddress. O valor dessa propriedade é obtido a partir da configuração da aplicação, mais especificamente da chave "ServicesUrls:CupaoAPI".
3.	Em seguida, é adicionado um HttpMessageHandler chamado BackendApiAuthenticationHttpClientHandler ao cliente HTTP. Um HttpMessageHandler é responsável por processar as requisições HTTP antes de serem enviadas para o servidor. Nesse caso, o BackendApiAuthenticationHttpClientHandler é usado para adicionar autenticação ao cliente HTTP, garantindo que as requisições sejam feitas de forma segura e autorizada.
Portanto, esse código configura um cliente HTTP chamado "Cupao" que será usado para se comunicar com o serviço "CupaoAPI". O endereço base do serviço é configurado e a autenticação é adicionada ao cliente HTTP para garantir a segurança das requisições.
*/
builder.Services.AddHttpClient("Cupao", config =>
    config.BaseAddress = new Uri(builder.Configuration["ServicesUrls:CupaoAPI"]))
    .AddHttpMessageHandler<BackendApiAuthenticationHttpClientHandler>();  //Configurar o endereco base do servico Cupao API

builder.Services.AddScoped<ICupaoService, CupaoService>(); //Registar o servico de Cupao no container de servicos da aplicacao

//###### ServiceBus ######
builder.Services.AddScoped<IMessageBus, MessageBus>(); //Registar o servico de MessageBus no container de servicos da aplicacao

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/*Este trecho de código configura o serviço SwaggerGen no aplicativo. O SwaggerGen é uma biblioteca que permite gerar automaticamente a documentação da API com base nos atributos e configurações definidos no código. A documentação gerada pelo SwaggerGen é exibida em uma interface interativa chamada Swagger UI, que permite explorar e testar os endpoints da API.
A função AddSwaggerGen é usada para adicionar a configuração do SwaggerGen ao container de serviços do aplicativo. Dentro dessa função, são definidas as configurações de segurança para a API.
A primeira parte do código define um esquema de segurança chamado "Bearer". Esse esquema é usado para autenticação baseada em token JWT. As propriedades do esquema são configuradas da seguinte forma:
•	Name: Define o nome do token JWT gerado pelo esquema "Bearer".
•	Description: Define a descrição do token JWT gerado pelo esquema "Bearer".
•	In: Define a localização do token JWT no cabeçalho da solicitação.
•	Type: Define o tipo de esquema de segurança como "ApiKey".
A segunda parte do código define um requisito de segurança para a API. Esse requisito especifica que todos os endpoints da API devem ser autenticados usando o esquema de segurança "Bearer". Isso é feito referenciando o esquema de segurança definido anteriormente e fornecendo uma lista vazia de escopos necessários.
Essa configuração de segurança é importante para proteger os endpoints da API e garantir que apenas usuários autenticados e autorizados possam acessá-los. Além disso, o SwaggerGen usa essas configurações de segurança para gerar a documentação correta dos endpoints protegidos*/
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization", //Nome do token JWT gerado pelo Bearer
        Description = "Escreve 'Bearer Generated-JWT-Token'", //Descrição do token JWT gerado pelo Bearer
        In = ParameterLocation.Header, //Localização do token JWT no cabeçalho
        Type = SecuritySchemeType.ApiKey, //Tipo de esquema de segurança
        Scheme = "Bearer" //Esquema de segurança
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Swagger CardosoRestaurante.CarrinhoAPI - OpenAPI 3.0",
        Version = "v1",
        Description = "Este é a API do CardosoRestaurante realizada por João Cardoso"
    });
    option.EnableAnnotations(); //Habilitar anotacoes
});
builder.AddAppAuthetication(); //Adicionar autenticacao ao servico de autenticacao pela classe que está no Extensions do projeto

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); //Tem que ser antes de autorização para garantir que a autenticação seja feita antes de verificar as permissões
app.UseAuthorization();

app.MapControllers();

UpdateMigracoes(); //Aplicar as migracoes pendentes ao iniciar o aplicativo

app.Run();

//############################################################################################################
void UpdateMigracoes()

/*O código selecionado é uma função chamada "UpdateMigracoes" que é responsável por aplicar as migrações pendentes em um banco de dados. Vamos analisar o código passo a passo:
1.Primeiro, é criado um novo escopo usando a função "CreateScope" do serviço "app.Services". Esse escopo permite acessar o banco de dados e executar as migrações.
2.	Em seguida, é obtido o serviço "AppDbContext" do escopo usando a função "GetRequiredService". Esse serviço representa o contexto do banco de dados que foi registrado no container de serviços.
3.	Depois, é verificado se existem migrações pendentes para serem aplicadas. Isso é feito chamando a função "GetPendingMigrations" do objeto "Database" do contexto do banco de dados e verificando o número de migrações pendentes usando a função "Count()".
4.	Se houver migrações pendentes, a função "Migrate" é chamada no objeto "Database" do contexto do banco de dados. Isso aplica as migrações pendentes e atualiza o esquema do banco de dados de acordo.
Essa função é útil quando você está usando o Entity Framework Core para gerenciar as migrações do banco de dados. Ela garante que todas as migrações pendentes sejam aplicadas automaticamente quando o aplicativo é iniciado. Isso é especialmente útil durante o desenvolvimento, quando você está iterando no esquema do banco de dados e precisa aplicar as alterações no banco de dados local.*/

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