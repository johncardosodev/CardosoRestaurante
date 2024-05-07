using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace CardosoRestaurante.Services.CarrinhoAPI.Utility
{

    /// <summary>
    /// A classe BackendApiAuthenticationHttpClientHandler é responsável por adicionar autenticação aos pedidos HTTP feitos a uma API de backend.
    /// Ela herda da classe DelegatingHandler, que é uma classe base para manipular pedidos HTTP.
    /// </summary>
    /// <remarks>
    /// <para>No construtor da classe, é injetado um objeto IHttpContextAccessor, que permite acessar o contexto HTTP atual.</para>
    /// <para>Isso é necessário para obter o token de acesso necessário para autenticar o pedido.</para>
    /// <para>O método principal desta classe é o SendAsync, que é chamado quando um pedido HTTP é enviado.</para>
    /// <para>Dentro deste método, o token de acesso é obtido do contexto HTTP usando o método GetTokenAsync do IHttpContextAccessor.</para>
    /// <para>O nome do token é especificado como "access_token".</para>
    /// <para>Em seguida, o cabeçalho de autorização do pedido HTTP é configurado para incluir o token de acesso.</para>
    /// <para>Isso é feito atribuindo uma nova instância de AuthenticationHeaderValue ao cabeçalho de autorização.</para>
    /// <para>O tipo de autenticação é definido como "Bearer" e o token de acesso é fornecido como valor.</para>
    /// <para>Por fim, o pedido HTTP é passado para o próximo manipulador na cadeia de manipuladores usando base.SendAsync,</para>
    /// <para>que retorna a resposta do pedido.</para>
    /// <para>Essa classe é útil quando você precisa adicionar autenticação baseada em token a todos os pedidos HTTP feitos a uma API de backend.</para>
    /// <para>Ela garante que cada pedido seja autenticado corretamente antes de ser enviado, adicionando o cabeçalho de autorização com o token de acesso necessário.</para>
    /// </remarks>
    public class BackendApiAuthenticationHttpClientHandler : DelegatingHandler
    {//Necessario implementar o serviço de autenticacao no program.cs
        private readonly IHttpContextAccessor _accessor;

        public BackendApiAuthenticationHttpClientHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _accessor.HttpContext.GetTokenAsync("access_token");

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
