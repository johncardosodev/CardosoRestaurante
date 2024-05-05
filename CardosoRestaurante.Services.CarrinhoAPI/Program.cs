using AutoMapper;
using CardosoRestaurante.Services.Carrinho;
using CardosoRestaurante.Services.Carrinho.Data;
using CardosoRestaurante.Services.Carrinho.Extensions;
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
//Estas tr�s linhas de c�digo configuram e registram o AutoMapper no container de servi�os da aplica��o, permitindo que ele seja usado para realizar mapeamentos entre as classes Cupao e CupaoDto e outros tipos definidos na aplica��o. Isso facilita a convers�o de dados entre diferentes contextos e simplifica o c�digo que lida com mapeamentos manuais.
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper(); //Configurar o mapeamento entre as classes Cupao e CupaoDto
builder.Services.AddSingleton(mapper); //Registar o mapeamento no container de servicos
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Registar o AutoMapper no container de servicos

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

/*Este trecho de c�digo configura o servi�o SwaggerGen no aplicativo. O SwaggerGen � uma biblioteca que permite gerar automaticamente a documenta��o da API com base nos atributos e configura��es definidos no c�digo. A documenta��o gerada pelo SwaggerGen � exibida em uma interface interativa chamada Swagger UI, que permite explorar e testar os endpoints da API.
A fun��o AddSwaggerGen � usada para adicionar a configura��o do SwaggerGen ao container de servi�os do aplicativo. Dentro dessa fun��o, s�o definidas as configura��es de seguran�a para a API.
A primeira parte do c�digo define um esquema de seguran�a chamado "Bearer". Esse esquema � usado para autentica��o baseada em token JWT. As propriedades do esquema s�o configuradas da seguinte forma:
�	Name: Define o nome do token JWT gerado pelo esquema "Bearer".
�	Description: Define a descri��o do token JWT gerado pelo esquema "Bearer".
�	In: Define a localiza��o do token JWT no cabe�alho da solicita��o.
�	Type: Define o tipo de esquema de seguran�a como "ApiKey".
A segunda parte do c�digo define um requisito de seguran�a para a API. Esse requisito especifica que todos os endpoints da API devem ser autenticados usando o esquema de seguran�a "Bearer". Isso � feito referenciando o esquema de seguran�a definido anteriormente e fornecendo uma lista vazia de escopos necess�rios.
Essa configura��o de seguran�a � importante para proteger os endpoints da API e garantir que apenas usu�rios autenticados e autorizados possam acess�-los. Al�m disso, o SwaggerGen usa essas configura��es de seguran�a para gerar a documenta��o correta dos endpoints protegidos*/
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization", //Nome do token JWT gerado pelo Bearer
        Description = "Escreve 'Bearer Generated-JWT-Token'", //Descri��o do token JWT gerado pelo Bearer
        In = ParameterLocation.Header, //Localiza��o do token JWT no cabe�alho
        Type = SecuritySchemeType.ApiKey, //Tipo de esquema de seguran�a
        Scheme = "Bearer" //Esquema de seguran�a
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
        Title = "Swagger CardosoRestaurante - OpenAPI 3.0",
        Version = "v1",
        Description = "Este � a API do CardosoRestaurante realizada por Jo�o Cardoso"
    });
    option.EnableAnnotations(); //Habilitar anotacoes
});
builder.AddAppAuthetication(); //Adicionar autenticacao ao servico de autenticacao pela classe que est� no Extensions do projeto

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization(); //Tem que ser antes de autoriza��o para garantir que a autentica��o seja feita antes de verificar as permiss�es
app.UseAuthorization();

app.MapControllers();

UpdateMigracoes(); //Aplicar as migracoes pendentes ao iniciar o aplicativo

app.Run();

//############################################################################################################
void UpdateMigracoes()

/*O c�digo selecionado � uma fun��o chamada "UpdateMigracoes" que � respons�vel por aplicar as migra��es pendentes em um banco de dados. Vamos analisar o c�digo passo a passo:
1.Primeiro, � criado um novo escopo usando a fun��o "CreateScope" do servi�o "app.Services". Esse escopo permite acessar o banco de dados e executar as migra��es.
2.	Em seguida, � obtido o servi�o "AppDbContext" do escopo usando a fun��o "GetRequiredService". Esse servi�o representa o contexto do banco de dados que foi registrado no container de servi�os.
3.	Depois, � verificado se existem migra��es pendentes para serem aplicadas. Isso � feito chamando a fun��o "GetPendingMigrations" do objeto "Database" do contexto do banco de dados e verificando o n�mero de migra��es pendentes usando a fun��o "Count()".
4.	Se houver migra��es pendentes, a fun��o "Migrate" � chamada no objeto "Database" do contexto do banco de dados. Isso aplica as migra��es pendentes e atualiza o esquema do banco de dados de acordo.
Essa fun��o � �til quando voc� est� usando o Entity Framework Core para gerenciar as migra��es do banco de dados. Ela garante que todas as migra��es pendentes sejam aplicadas automaticamente quando o aplicativo � iniciado. Isso � especialmente �til durante o desenvolvimento, quando voc� est� iterando no esquema do banco de dados e precisa aplicar as altera��es no banco de dados local.*/

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