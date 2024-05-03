using CardosoRestaurante.Web.Service;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;

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

//############################################################################################################ AuthenticationAPI ###############################################################################
SD.AuthAPIBase = builder.Configuration["ServicesUrls:AuthAPI"]; // Define a URL base da API de autenticação a partir da configuração da aplicação
builder.Services.AddHttpClient<IAuthService, AuthService>(); // Para fazer solicitações HTTP à API de autenticação
builder.Services.AddScoped<ITokenProvider, TokenProvider>(); // Adiciona o serviço TokenProvider ao contêiner de serviços da aplicação para lidar com o token JWT
builder.Services.AddScoped<IAuthService, AuthService>(); // Adiciona o serviço AuthService ao contêiner de serviços da aplicação para lidar com operações relacionadas à autenticação

/*A linha de código selecionada está adicionando a autenticação baseada em cookies ao pipeline de solicitação HTTP da aplicação. Isso significa que, quando um usuário faz uma solicitação para a aplicação, o sistema verifica se o usuário está autenticado por meio de cookies.
Ao chamar o método AddAuthentication e passar CookieAuthenticationDefaults.AuthenticationScheme como argumento, você está configurando a autenticação baseada em cookies como o esquema de autenticação padrão da aplicação. Isso significa que os cookies serão usados para autenticar os usuários.
Em seguida, você pode usar o método AddCookie para configurar opções específicas para a autenticação baseada em cookies. No exemplo fornecido, as opções estão sendo configuradas da seguinte maneira:
•	options.ExpireTimeSpan = TimeSpan.FromHours(10);: Define o tempo de expiração dos cookies de autenticação como 10 horas. Isso significa que, após 10 horas, o usuário precisará fazer login novamente.
•	options.LoginPath = "/Auth/Login";: Define o caminho para a página de login da aplicação. Quando um usuário não autenticado tenta acessar uma página protegida, ele será redirecionado para essa página.
•	options.AccessDeniedPath = "/Auth/AccessDenied";: Define o caminho para a página de acesso negado da aplicação. Quando um usuário autenticado tenta acessar uma página para a qual não tem permissão, ele será redirecionado para essa página.
Essa configuração permite que a aplicação utilize a autenticação baseada em cookies para proteger rotas e recursos específicos, garantindo que apenas usuários autenticados tenham acesso a eles.*/
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme) // Configura a autenticação baseada em cookies
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

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
app.UseAuthentication(); //Tem que ser antes de autorização
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();