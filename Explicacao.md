# Solucao
## Passos
	1. Criar um projeto de API
	1. Adicionar pastas 
	1. Alterar o configure startup para abrir automaticamente o front end e API
## Gateaway

## Frontend
MVC application que vai chamar. O frontend vai chamar o backend da API. Temos que colocar request e o response DTOs no frontend.

### Program.cs
1	. Adicionar servi�o para obter o endere�o da IP do usu�rio
	1. builder.Services.AddHttpContextAccessor();
1	. Adicionar servi�o para fazer solicita��es HTTP
	1. builder.Services.AddHttpClient();
1. Adicionar o servi�o para fazer solicita�oes htpp � APIs
	1. builder.Services.AddHttpClient<ICupaoService, CupaoService>();
1. Adicionar a URL base da API
	1. SD.CupaoAPIBase = builder.Configuration["ServiceUrls:CupaoAPI"];
1	. Adicionar o IBaseService ao container servicos para enviar solicita��es HTT	
	1. builder.Services.AddScoped<IBaseService, BaseService>();
1	. Adicionar o ICupaoService ao container servicos para enviar solicita��es HTT								P
	1.	builder.Services.AddScoped<ICupaoService, CupaoService>();



		
### BootStrapper
1	. Adicionei bootswatch para o estilo do front end. Copiei css e coloquei na pasta wwwroot/css

## Integration
Relacionado to messaging

## Services		
API endPoints will be here


### Progam.cs
 
### Notas nos nuggets
Dentro dos servi;os, � necessario que os mesmos nuggets tem a mesma vers�o entre os servi�os

### Dto
1. Existe propriendades do modelo onde nao queremos passar para a front end e por isso criamos um DTO	
(Data Transfer Object) para passar apenas o que queremos. � uma boa pratica para nao passar informacao desnecessaria numa API
		1. AutoMapper serve para mapear um objeto para outro. Exemplo: mapear um objeto de um modelo para um DTO.
Um DTO (Data Transfer Object) � um objeto simples usado para encapsular dados e envi�-los de um sub-sistema de uma aplica��o para outro. DTOs s�o frequentemente usados no contexto de interfaces remotas, servi�os web e APIs para transferir dados entre o cliente e o servidor. 
1. Eles s�o compostos por propriedades simples e n�o cont�m l�gica de neg�cios. O principal prop�sito de um DTO � transportar dados atrav�s da rede ou camadas de aplica��o sem expor o modelo de dom�nio subjacente ou o esquema de banco de dados ao cliente 
1. 
### AutoMapper
Como mapear um objeto para outro
1	. Criar um arquivo de configura��o para o AutoMapper. Exemplo MappingConfig.cs
	1. No Program.cs, adicionar o AutoMapper e o Profile criado. Exemplo: 
	IMapper mapper = MappingConfig.RegisterMaps().CreateMapper(); //Configurar o mapeamento entre as classes Cupao e CupaoDto
	builder.Services.AddSingleton(mapper); //Registar o mapeamento no container de servicos
	builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //Registar o AutoMapper no container de servicos
	1. No controller, injetar o IMapper e usar o m�todo Map para mapear o objeto. Exemplo: _mapper.Map<Cupaodto>(cupao)
### ResponseDTO e RequestDTO
S�o usados para encapsular os dados enviados para e recebidos de um servidor, facilitando a serializa��o e desserializa��o desses dados. 
1. RequestDTO: � usado para enviar dados cliente para o servidor. Ele cont�m as propriedades necess�rias para criar um novo objeto no servidor.
1. ResponseDTO: � usado para enviar dados do servidor para o cliente. Ele cont�m as propriedades necess�rias para exibir os dados no cliente

Porque precisamos RepsonseDTO e RequestDTO no frontend?



## Nuggetpackages
Notas
Devemos usar a mesma versao dos nuggets packages dentro do mesmo microservi�o. Podemos ter differentes vers�es em diferentes microservi�os.
* AutoMapper serve para mapear objetos de um tipo para outro.
* EntityFrameworkCore serve para acessar o banco de dados.
* EntityFrameworkCore.SqlServer serve para acessar o banco de dados SQL Server.
* EntityFrameworkCore.Tools serve para criar as migra��es.
* Swashbuckle.AspNetCore serve para criar a documenta��o da API.
* microsoft.AspNetCore.OpenApi serve para criar a documenta��o da API.
* JwtBearer serve para autenticar usu�rios. 

## Services
IService folder vai conter a interface do servi�o
1. IBaseService.cs: Interface que contem um m�todo assyncrono que Envia uma solicita��o HTTP para o URI especificado					
1. ICupao.cs  interface segue o Controllador CupaoController, que � respons�vel por lidar com as solicita��es HTTP relacionadas � entidade Cupao

1. BaseService.cs: Classe que implementa a interface IBaseService. Ela contem um //A classe BaseService � uma classe que fornece uma implementa��o base para enviar solicita��es HTTP e receber respostas HTTP. Ela � projetada para ser herdada por outras classes de servi�o no namespace CardosoRestaurante.Web.	
1. CupaoService.cs Segue a implementa�ao da interface ICupao. A classe CupaoService � uma classe que fornece uma implementa��o concreta para lidar com as solicita��es HTTP relacionadas � entidade Cupao. 

### Notas


## Para cada microservi�o
1. Criar um projeto ASP.NET Core Web API
### Program.cs do Microservi�o
1	. Fazer a liga��o com o banco de dados
1	. Registar o mapeamento fo container de servi�os
	1. IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
	1. builder.Services.AddSingleton(mapper);
1	. Adicionar AutoMapper ao container de servi�os
	1. builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
1	. Adicionar UpdateMigra�oes() para aplicar as migra��es pendentes antes do Run
#### Notas
1. Update migra�oes pendentes. � sempre necessario fazer migracao
	1. Esta fun��o � �til quando voc� est� usando o Entity Framework Core para gerenciar as migra��es do banco de dados. Ela garante que todas as migra��es pendentes sejam aplicadas automaticamente quando o aplicativo � iniciado. Isso � especialmente �til durante o desenvolvimento, quando voc� est� iterando no esquema do banco de dados e precisa aplicar as altera��es no banco de dados local.1.	
		1.Ver exemplo updateMigracoes() que est� no CupaoAPI.. 
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
1. Tempos que usar IdentityDbContext para autenticar o usu�rio e nao DbContext
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
Sempre que lidamos com autenticacao, lidamos com a cria�ao JWT token que � usado para validar um usu�rio
1. 1. Adicionar nugget Microsoft.AspNetCore.Authentication.JwtBearer

### Registar
#### Notas
Cirar interfaces.
1. Criar Task<string> Registar(RegistrationRequestDto registrationRequestDto);
	1. m�todo Registar � respons�vel por registrar um novo usu�rio no sistema.Ele recebe um objeto RegistrationRequestDto como par�metro, que cont�m as informa��es necess�rias para criar uma nova conta de usu�rio.O m�todo retorna uma tarefa ass�ncrona que representa o processo de registro.
1. Criar Task<string> Login(LoginRequestDto loginRequestDto);
	1. m�todo Login � respons�vel por autenticar um usu�rio no sistema. Ele recebe um objeto LoginRequestDto como par�metro, que cont�m as credenciais do usu�rio.O m�todo retorna uma tarefa ass�ncrona que representa o processo de autentica��o.
1.  Task<bool> AtribuirFuncao(string email, string roleName);
	1. m�todo AtribuirFuncao � respons�vel por atribuir uma fun��o a um usu�rio no sistema. Ele recebe o email do usu�rio e o nome da fun��o como par�metros.O m�todo
1. Criar JwtTokenGenerator
	1.  Este m�todo recebe as informa��es do usu�rio, cria um token JWT com base nessas informa��es e retorna o token gerado como uma string. Esse token pode ser usado para autenticar e autorizar o usu�rio em uma aplica��o.
	
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


