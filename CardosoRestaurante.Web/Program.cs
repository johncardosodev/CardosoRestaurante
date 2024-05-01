using CardosoRestaurante.Web.Service;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//############################################################################################################ CupaoAPI ############################################################################################################
/*O HttpContextAccessor fornece acesso ao contexto HTTP atual, permitindo que você obtenha informações
 * sobre a requisição atual, como o endereço IP, cabeçalhos, cookies, entre outros.
 * Nesse caso específico, o serviço está sendo adicionado para que seja possível obter o endereço IP do usuário.*/
builder.Services.AddHttpContextAccessor(); // Para obter o endereço IP do usuário atual
/*A linha de código selecionada adiciona um serviço HttpClient ao contêiner de serviços da aplicação.
 * O HttpClient é uma classe que permite fazer solicitações HTTP para outras APIs externas.
Ao adicionar esse serviço, você está configurando a capacidade da sua aplicação de se comunicar com outras APIs por meio de requisições HTTP. Isso é útil quando você precisa consumir dados de uma API externa ou enviar dados para ela.
Uma vez que o serviço HttpClient tenha sido adicionado, você pode injetá-lo em outras classes ou componentes da sua aplicação para realizar operações de comunicação com APIs externas.
Por exemplo, você pode usar o HttpClient para fazer solicitações GET, POST, PUT ou DELETE para uma API e receber as respostas correspondentes.

Essa linha de código é importante para configurar a capacidade de comunicação da sua aplicação com APIs externas e é frequentemente usada em cenários de integração de sistemas ou consumo de serviços web*/
builder.Services.AddHttpClient(); // Para fazer solicitações HTTP a outras APIs externas
/*O serviço HttpClient está sendo configurado para trabalhar com a interface ICupaoService e a classe CupaoService.
 * Isso significa que o HttpClient será usado para fazer solicitações HTTP para a API relacionada aos cupons de desconto, utilizando a implementação fornecida pela classe CupaoService*/
builder.Services.AddHttpClient<ICupaoService, CupaoService>(); // Para fazer solicitações HTTP à API de cupons de desconto

/*A linha de código selecionada builder.Configuration["ServiceUrls:CupaoAPI"]; está acessando a configuração da aplicação para obter a URL base da API de cupons de desconto.
No ASP.NET Core, a configuração da aplicação é armazenada em um objeto chamado Configuration. Esse objeto contém uma coleção de pares chave-valor que representam as configurações definidas para a aplicação.
Nesse caso específico, a chave "ServiceUrls:CupaoAPI" está sendo usada para acessar a URL base da API de cupons de desconto.
Essa chave é definida no arquivo appsettings.json da aplicação, que é um arquivo de configuração comum usado para armazenar configurações da aplicação.

Ao usar builder.Configuration["ServiceUrls:CupaoAPI"], o código está buscando o valor correspondente à chave "ServiceUrls:CupaoAPI" na configuração da aplicação.

Esse valor representa a URL base da API de cupons de desconto e será atribuído à propriedade SD.CupaoAPIBase.

Essa abordagem permite que a URL base da API de cupons de desconto seja configurada externamente, no arquivo appsettings.json, sem a necessidade de modificar o código fonte. Isso facilita a configuração e a manutenção da aplicação, pois as configurações podem ser alteradas sem a necessidade de recompilar o código.*/
SD.CupaoAPIBase = builder.Configuration["ServicesUrls:CupaoAPI"]; // Define a URL base da API de cupons de desconto a partir da configuração da aplicação
/*Essa linha de código adiciona o BaseService como um serviço disponível no contêiner de serviços da aplicação, utilizando a interface IBaseService.
Ao adicionar esse serviço, você está configurando a capacidade da sua aplicação de enviar solicitações HTTP.
Isso é útil quando você precisa se comunicar com APIs externas ou realizar operações de comunicação com outros serviços.
O Scoped indica que uma nova instância do BaseService será criada para cada escopo de solicitação.

Isso significa que cada vez que uma solicitação HTTP for feita, uma nova instância do BaseService será criada e usada para processar essa solicitação. Isso garante que cada solicitação tenha seu próprio estado isolado.
Ao adicionar o BaseService como um serviço no contêiner de serviços da aplicação, você pode injetá-lo em outras partes da aplicação que o requerem.
Isso permite que você utilize as funcionalidades fornecidas pelo BaseService em diferentes partes do código, sem precisar criar uma nova instância toda vez que precisar usá-lo.*/
builder.Services.AddScoped<IBaseService, BaseService>(); // Adiciona o serviço BaseService ao contêiner de serviços da aplicação para enviar solicitações HTTP
builder.Services.AddScoped<ICupaoService, CupaoService>(); // Adiciona o serviço CupaoService ao contêiner de serviços da aplicação para lidar com operações relacionadas a cupons de desconto

//############################################################################################################ AuthenticationAPI ############################################################################################################
SD.AuthAPIBase = builder.Configuration["ServicesUrls:AuthAPI"]; // Define a URL base da API de autenticação a partir da configuração da aplicação
builder.Services.AddHttpClient<IAuthService, AuthService>(); // Para fazer solicitações HTTP à API de autenticação
builder.Services.AddScoped<IAuthService, AuthService>(); // Adiciona o serviço AuthService ao contêiner de serviços da aplicação para lidar com operações relacionadas à autenticação

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();