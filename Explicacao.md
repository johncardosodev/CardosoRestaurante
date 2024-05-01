# Solucao
## Passos
	1. Criar um projeto de API
	1. Adicionar pastas 
	1. Alterar o configure startup para abrir automaticamente o front end e API
## Gateaway

## Frontend
MVC application que vai chamar. O frontend vai chamar o backend da API. Temos que colocar request e o response DTOs no frontend.

### Program.cs
1	. Adicionar serviço para obter o endereço da IP do usuário
	1. builder.Services.AddHttpContextAccessor();
1	. Adicionar serviço para fazer solicitações HTTP
	1. builder.Services.AddHttpClient();
1. Adicionar o serviço para fazer solicitaçoes htpp à APIs
	1. builder.Services.AddHttpClient<ICupaoService, CupaoService>();
1. Adicionar a URL base da API
	1. SD.CupaoAPIBase = builder.Configuration["ServiceUrls:CupaoAPI"];
1	. Adicionar o IBaseService ao container servicos para enviar solicitações HTT	
	1. builder.Services.AddScoped<IBaseService, BaseService>();
1	. Adicionar o ICupaoService ao container servicos para enviar solicitações HTT								P
	1.	builder.Services.AddScoped<ICupaoService, CupaoService>();



		
### BootStrapper
1	. Adicionei bootswatch para o estilo do front end. Copiei css e coloquei na pasta wwwroot/css

## Integration
Relacionado to messaging

## Services		
API endPoints will be here


### Progam.cs
 
### Notas nos nuggets
Dentro dos servi;os, é necessario que os mesmos nuggets tem a mesma versão entre os serviços

### Dto
1. Existe propriendades do modelo onde nao queremos passar para a front end e por isso criamos um DTO	
(Data Transfer Object) para passar apenas o que queremos. É uma boa pratica para nao passar informacao desnecessaria numa API
		1. AutoMapper serve para mapear um objeto para outro. Exemplo: mapear um objeto de um modelo para um DTO.
Um DTO (Data Transfer Object) é um objeto simples usado para encapsular dados e enviá-los de um sub-sistema de uma aplicação para outro. DTOs são frequentemente usados no contexto de interfaces remotas, serviços web e APIs para transferir dados entre o cliente e o servidor. 
1. Eles são compostos por propriedades simples e não contêm lógica de negócios. O principal propósito de um DTO é transportar dados através da rede ou camadas de aplicação sem expor o modelo de domínio subjacente ou o esquema de banco de dados ao cliente 
1. 
### AutoMapper
Como mapear um objeto para outro
1	. Criar um arquivo de configuração para o AutoMapper. Exemplo MappingConfig.cs
	1. No Program.cs, adicionar o AutoMapper e o Profile criado. Exemplo: 
	IMapper mapper = MappingConfig.RegisterMaps().CreateMapper(); //Configurar o mapeamento entre as classes Cupao e CupaoDto
	builder.Services.AddSingleton(mapper); //Registar o mapeamento no container de servicos
	builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Registar o AutoMapper no container de servicos
	1. No controller, injetar o IMapper e usar o método Map para mapear o objeto. Exemplo: _mapper.Map<Cupaodto>(cupao)
### ResponseDTO e RequestDTO
São usados para encapsular os dados enviados para e recebidos de um servidor, facilitando a serialização e desserialização desses dados. 
1. RequestDTO: é usado para enviar dados cliente para o servidor. Ele contém as propriedades necessárias para criar um novo objeto no servidor.
1. ResponseDTO: é usado para enviar dados do servidor para o cliente. Ele contém as propriedades necessárias para exibir os dados no cliente

Porque precisamos RepsonseDTO e RequestDTO no frontend?



## Nuggetpackages
Notas
Devemos usar a mesma versao dos nuggets packages dentro do mesmo microserviço. Podemos ter differentes versões em diferentes microserviços.
* AutoMapper serve para mapear objetos de um tipo para outro.
* EntityFrameworkCore serve para acessar o banco de dados.
* EntityFrameworkCore.SqlServer serve para acessar o banco de dados SQL Server.
* EntityFrameworkCore.Tools serve para criar as migrações.
* Swashbuckle.AspNetCore serve para criar a documentação da API.
* microsoft.AspNetCore.OpenApi serve para criar a documentação da API.
* JwtBearer serve para autenticar usuários. 

## Services
IService folder vai conter a interface do serviço
1. IBaseService.cs: Interface que contem um método assyncrono que Envia uma solicitação HTTP para o URI especificado					
1. ICupao.cs  interface segue o Controllador CupaoController, que é responsável por lidar com as solicitações HTTP relacionadas à entidade Cupao

1. BaseService.cs: Classe que implementa a interface IBaseService. Ela contem um //A classe BaseService é uma classe que fornece uma implementação base para enviar solicitações HTTP e receber respostas HTTP. Ela é projetada para ser herdada por outras classes de serviço no namespace CardosoRestaurante.Web.	
1. CupaoService.cs Segue a implementaçao da interface ICupao. A classe CupaoService é uma classe que fornece uma implementação concreta para lidar com as solicitações HTTP relacionadas à entidade Cupao. 

### Notas


## Para cada microserviço
1. Criar um projeto ASP.NET Core Web API
### Program.cs do Microserviço
1	. Fazer a ligação com o banco de dados
1	. Registar o mapeamento fo container de serviços
	1. IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
	1. builder.Services.AddSingleton(mapper);
1	. Adicionar AutoMapper ao container de serviços
	1. builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
1	. Adicionar UpdateMigraçoes() para aplicar as migrações pendentes antes do Run
#### Notas
1. Update migraçoes pendentes. É sempre necessario fazer migracao
	1. Esta função é útil quando você está usando o Entity Framework Core para gerenciar as migrações do banco de dados. Ela garante que todas as migrações pendentes sejam aplicadas automaticamente quando o aplicativo é iniciado. Isso é especialmente útil durante o desenvolvimento, quando você está iterando no esquema do banco de dados e precisa aplicar as alterações no banco de dados local.1.	
		1.Ver exemplo updateMigracoes() que está no CupaoAPI.. 
	1.
### Controllers
Crier um controllerApi para o modelo criado
Criar metodo get para retornar todos os dados do modelo
Criar metodo get para retornar um dado do modelo por id
Dentro di CouponController adicionei um ResponseDTO para retornar a resposta da API
Dentro do metodo Get, retorna o objeto ResponseDTO que contem o objeto CouponDTO


## AuthAPI
### Notas
1. Podemos copiar os nuggets utilizados no CupaoAPI para o AuthAPI
1. Tempos que usar IdentityDbContext para autenticar o usuário e nao DbContext
1. Clocamos public class AppDbContext : IdentityDbContext<IdentityUser>
1. No program.cs:
	1. builder.Services.AddDbContext
	1. app.UseAuthentication(); //Autenticacao tem que ser sempre antes de UseAuthorization
	1. Podemos copiar o Startup.cs do CupaoAPI para o AuthAPI
	1. Adicionar 
		1. builder.Services.AddIdentity<IdentityUser, IdentityRole>() //Adiciona o servico de autenticacao
    .AddEntityFrameworkStores<AppDbContext>() //Adiciona o servico de base de dados
    .AddDefaultTokenProviders();
1. Criar migracao
1. Update database

### JWT token
Sempre que lidamos com autenticacao, lidamos com a criaçao JWT token que é usado para validar um usuário
1. 1. Adicionar nugget Microsoft.AspNetCore.Authentication.JwtBearer

### Registar
#### Notas
Cirar interfaces.
1. Criar Task<string> Registar(RegistrationRequestDto registrationRequestDto);
	1. método Registar é responsável por registrar um novo usuário no sistema.Ele recebe um objeto RegistrationRequestDto como parâmetro, que contém as informações necessárias para criar uma nova conta de usuário.O método retorna uma tarefa assíncrona que representa o processo de registro.
1. Criar Task<string> Login(LoginRequestDto loginRequestDto);
	1. método Login é responsável por autenticar um usuário no sistema. Ele recebe um objeto LoginRequestDto como parâmetro, que contém as credenciais do usuário.O método retorna uma tarefa assíncrona que representa o processo de autenticação.
1.  Task<bool> AtribuirFuncao(string email, string roleName);
	1. método AtribuirFuncao é responsável por atribuir uma função a um usuário no sistema. Ele recebe o email do usuário e o nome da função como parâmetros.O método
1. Criar JwtTokenGenerator
	1.  Este método recebe as informações do usuário, cria um token JWT com base nessas informações e retorna o token gerado como uma string. Esse token pode ser usado para autenticar e autorizar o usuário em uma aplicação.
	
### Program.cs
Adicionar:
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>() //Adiciona o servico de autenticacao
    .AddEntityFrameworkStores<AppDbContext>() //Adiciona o servico de base de dados
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); //Adiciona o servico de geracao de token
builder.Services.AddScoped<IAuthService, AuthService>(); //Adiciona o servico de autenticacao


### DTO dentro do AuthAPI
1. Criar um DTO para o usuario
1	. Criar um DTO para o login
1	. Criar um DTO para o registro
1	. Criar registrationRequestDto
1	. UserDto  

Devemos adicionar JwtOptions no appsettings.json para configurar o token. Leva Secret, Issuer e Audience. Isto ser para
o token ser valido.
1	. Criar classe JwtOptions	
1. Adicionar no AppSettigns.json
1. Adicionar JwtOptions no container de servicos


