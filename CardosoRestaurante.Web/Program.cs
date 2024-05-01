using CardosoRestaurante.Web.Service;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//############################################################################################################ CupaoAPI ############################################################################################################
/*O HttpContextAccessor fornece acesso ao contexto HTTP atual, permitindo que voc� obtenha informa��es
 * sobre a requisi��o atual, como o endere�o IP, cabe�alhos, cookies, entre outros.
 * Nesse caso espec�fico, o servi�o est� sendo adicionado para que seja poss�vel obter o endere�o IP do usu�rio.*/
builder.Services.AddHttpContextAccessor(); // Para obter o endere�o IP do usu�rio atual
/*A linha de c�digo selecionada adiciona um servi�o HttpClient ao cont�iner de servi�os da aplica��o.
 * O HttpClient � uma classe que permite fazer solicita��es HTTP para outras APIs externas.
Ao adicionar esse servi�o, voc� est� configurando a capacidade da sua aplica��o de se comunicar com outras APIs por meio de requisi��es HTTP. Isso � �til quando voc� precisa consumir dados de uma API externa ou enviar dados para ela.
Uma vez que o servi�o HttpClient tenha sido adicionado, voc� pode injet�-lo em outras classes ou componentes da sua aplica��o para realizar opera��es de comunica��o com APIs externas.
Por exemplo, voc� pode usar o HttpClient para fazer solicita��es GET, POST, PUT ou DELETE para uma API e receber as respostas correspondentes.

Essa linha de c�digo � importante para configurar a capacidade de comunica��o da sua aplica��o com APIs externas e � frequentemente usada em cen�rios de integra��o de sistemas ou consumo de servi�os web*/
builder.Services.AddHttpClient(); // Para fazer solicita��es HTTP a outras APIs externas
/*O servi�o HttpClient est� sendo configurado para trabalhar com a interface ICupaoService e a classe CupaoService.
 * Isso significa que o HttpClient ser� usado para fazer solicita��es HTTP para a API relacionada aos cupons de desconto, utilizando a implementa��o fornecida pela classe CupaoService*/
builder.Services.AddHttpClient<ICupaoService, CupaoService>(); // Para fazer solicita��es HTTP � API de cupons de desconto

/*A linha de c�digo selecionada builder.Configuration["ServiceUrls:CupaoAPI"]; est� acessando a configura��o da aplica��o para obter a URL base da API de cupons de desconto.
No ASP.NET Core, a configura��o da aplica��o � armazenada em um objeto chamado Configuration. Esse objeto cont�m uma cole��o de pares chave-valor que representam as configura��es definidas para a aplica��o.
Nesse caso espec�fico, a chave "ServiceUrls:CupaoAPI" est� sendo usada para acessar a URL base da API de cupons de desconto.
Essa chave � definida no arquivo appsettings.json da aplica��o, que � um arquivo de configura��o comum usado para armazenar configura��es da aplica��o.

Ao usar builder.Configuration["ServiceUrls:CupaoAPI"], o c�digo est� buscando o valor correspondente � chave "ServiceUrls:CupaoAPI" na configura��o da aplica��o.

Esse valor representa a URL base da API de cupons de desconto e ser� atribu�do � propriedade SD.CupaoAPIBase.

Essa abordagem permite que a URL base da API de cupons de desconto seja configurada externamente, no arquivo appsettings.json, sem a necessidade de modificar o c�digo fonte. Isso facilita a configura��o e a manuten��o da aplica��o, pois as configura��es podem ser alteradas sem a necessidade de recompilar o c�digo.*/
SD.CupaoAPIBase = builder.Configuration["ServicesUrls:CupaoAPI"]; // Define a URL base da API de cupons de desconto a partir da configura��o da aplica��o
/*Essa linha de c�digo adiciona o BaseService como um servi�o dispon�vel no cont�iner de servi�os da aplica��o, utilizando a interface IBaseService.
Ao adicionar esse servi�o, voc� est� configurando a capacidade da sua aplica��o de enviar solicita��es HTTP.
Isso � �til quando voc� precisa se comunicar com APIs externas ou realizar opera��es de comunica��o com outros servi�os.
O Scoped indica que uma nova inst�ncia do BaseService ser� criada para cada escopo de solicita��o.

Isso significa que cada vez que uma solicita��o HTTP for feita, uma nova inst�ncia do BaseService ser� criada e usada para processar essa solicita��o. Isso garante que cada solicita��o tenha seu pr�prio estado isolado.
Ao adicionar o BaseService como um servi�o no cont�iner de servi�os da aplica��o, voc� pode injet�-lo em outras partes da aplica��o que o requerem.
Isso permite que voc� utilize as funcionalidades fornecidas pelo BaseService em diferentes partes do c�digo, sem precisar criar uma nova inst�ncia toda vez que precisar us�-lo.*/
builder.Services.AddScoped<IBaseService, BaseService>(); // Adiciona o servi�o BaseService ao cont�iner de servi�os da aplica��o para enviar solicita��es HTTP
builder.Services.AddScoped<ICupaoService, CupaoService>(); // Adiciona o servi�o CupaoService ao cont�iner de servi�os da aplica��o para lidar com opera��es relacionadas a cupons de desconto

//############################################################################################################ AuthenticationAPI ############################################################################################################
SD.AuthAPIBase = builder.Configuration["ServicesUrls:AuthAPI"]; // Define a URL base da API de autentica��o a partir da configura��o da aplica��o
builder.Services.AddHttpClient<IAuthService, AuthService>(); // Para fazer solicita��es HTTP � API de autentica��o
builder.Services.AddScoped<IAuthService, AuthService>(); // Adiciona o servi�o AuthService ao cont�iner de servi�os da aplica��o para lidar com opera��es relacionadas � autentica��o

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