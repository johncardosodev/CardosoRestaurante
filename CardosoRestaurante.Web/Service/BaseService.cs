using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static CardosoRestaurante.Web.Utility.SD;

namespace CardosoRestaurante.Web.Service
{
    /// <summary>
    /// O propósito da classe BaseService é encapsular funcionalidades comuns relacionadas ao envio de solicitações HTTP, como criar uma instância de HttpClient, definir cabeçalhos de solicitação, lidar com diferentes métodos HTTP(GET, POST, PUT,
    /// DELETE) e lidar com diferentes códigos de status HTTP.
    /// <para>A classe tem um construtor que recebe um parâmetro IHttpClientFactory.Esta interface é usada para criar instâncias de HttpClient para enviar solicitações HTTP e receber respostas de um URI.O método EnviarAsync é o principal método 
    /// da classe BaseService.Ele recebe um parâmetro RequestDto e retorna um objeto ResponseDto de forma assíncrona.</para>
    /// </summary>
    /// <remarks>
    /// <para>Dentro deste método, as seguintes etapas são realizadas:</para>
    /// <para>1. Uma instância de HttpClient é criada usando a instância IHttpClientFactory.</para>
    /// <para>2. Uma instância de HttpRequestMessage é criada.</para>
    /// <para>3. Os cabeçalhos de solicitação são definidos, incluindo o cabeçalho "Accept" com o valor "application/json".</para>
    /// <para>4. A URL da solicitação é definida com base na propriedade Url do parâmetro RequestDto.</para>
    /// <para>5. Se a propriedade Data do parâmetro RequestDto não for nula, o conteúdo da solicitação é definido com a representação JSON serializada da propriedade Data.</para>
    /// <para>6. O método HTTP é determinado com base na propriedade ApiTipo do parâmetro RequestDto.</para>
    /// <para>7. A solicitação HTTP é enviada de forma assíncrona usando o método EnviarAsync do HttpClient.</para>
    /// <para>8. O código de status da resposta HTTP é verificado e, com base no código de status, um objeto ResponseDto correspondente é criado e retornado.</para>
    /// <para>9. Se ocorrer uma exceção durante o processo, um objeto ResponseDto é criado com a mensagem de exceção e um valor falso para a propriedade Sucesso.</para>
    /// <para>Em resumo, a classe BaseService fornece uma implementação reutilizável para enviar solicitações HTTP e lidar com respostas,</para>
    /// <para>que pode ser herdada por outras classes de serviço para simplificar a implementação de funcionalidades relacionadas ao HTTP.</para>
    /// </remarks>
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory; // Interface para criar instâncias de HttpClient para enviar solicitações HTTP e receber respostas HTTP de um URI
        private readonly ITokenProvider _tokenProvider; // Interface para fornecer métodos para definir e obter tokens de autenticação

        //Aqui é criado um construtor para a classe BaseService que recebe um parâmetro do tipo IHttpClientFactory com o proposito de criar instâncias de HttpClient
        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory; //Inicializa o campo _httpClientFactory com o valor do parâmetro httpClientFactory
            _tokenProvider = tokenProvider; //Inicializa o campo _tokenProvider com o valor do parâmetro tokenProvider
        }

        public async Task<ResponseDto?> EnviarAsync(RequestDto requestDto, bool comBearer = true)
        {
            try
            {
                HttpClient cliente = _httpClientFactory.CreateClient("CardosoRestauranteAPI"); //Cria uma instância de HttpClient com o nome "CardosoRestauranteAPI"
                HttpRequestMessage message = new();//Cria uma instância de HttpRequestMessage que faz que a solicitação HTTP seja enviada para um URI
                message.Headers.Add("Accept", "application/json"); //Adiciona um cabeçalho à coleção de cabeçalhos de HttpRequestMessage com o nome "Accept" e o valor "application/json"

                //token que manda para a API
                if (comBearer)
                {
                    var token = _tokenProvider.GetToken(); //Obtém o token de autenticação do provedor de token
                    message.Headers.Add("Authorization", $"Bearer {token}"); //Adiciona um cabeçalho à coleção de cabeçalhos de HttpRequestMessage com o nome "Authorization" e o valor
                }

                message.RequestUri = new Uri(requestDto.Url); //Atribui o valor da propriedade Url de requestDto à propriedade RequestUri de message

                if (requestDto.Data != null) //Se houver objeto Data em requestDto
                {//Atualiza o conteúdo da mensagem com o valor da propriedade Data de requestDto serializado em JSON e codificado em UTF-8 com o tipo de mídia "application/json"
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

                    //Encoding.UTF8: This specifies the character encoding to be used when converting the serialized JSON string to bytes. In this case, UTF-8 encoding is used.
                    //"application/json": This specifies the media type of the content. In this case, it is set to "application/json" to indicate that the content is in JSON format.
                }
                HttpResponseMessage? apiResponse = null; //Cria uma variável apiResponse do tipo HttpResponseMessage e atribui-lhe o valor null

                switch (requestDto.ApiTipo) //Verifica o valor da propriedade ApiTipo (GET,PUT,POST) de requestDto e executa o bloco de código correspondente
                {
                    case APITipo.POST:
                        message.Method = HttpMethod.Post; //Atribui o valor HttpMethod.Post à propriedade Method de message
                        break;

                    case APITipo.DELETE:
                        message.Method = HttpMethod.Delete; //Atribui o valor HttpMethod.Delete à propriedade Method de message
                        break;

                    case APITipo.PUT:
                        message.Method = HttpMethod.Put; //Atribui o valor HttpMethod.Put à propriedade Method de message
                        break;

                    default:
                        message.Method = HttpMethod.Get; //Atribui o valor HttpMethod.Get à propriedade Method de message
                        break;
                }

                //Assim que a resposta é recebida, a variável apiResponse é atribuída ao objeto HttpResponseMessage retornado, que contém informações sobre a resposta HTTP, como o código de status, cabeçalhos e conteúdo.
                apiResponse = await cliente.SendAsync(message); //Atribui o valor retornado por cliente.EnviarAsync(message) à variável apiResponse

                switch (apiResponse.StatusCode) //Verifica o valor da propriedade StatusCode de apiResponse e executa o bloco de código correspondente
                {
                    case HttpStatusCode.NotFound:
                        return new() { Sucesso = false, Mensagem = "Nao foi possivel encontrar o recurso" }; //Cria uma nova instância de ResponseDto com os valores Sucesso = false e Mensagem = "Nao foi possivel encontrar o recurso"
                    case HttpStatusCode.Forbidden:
                        return new() { Sucesso = false, Mensagem = "Nao tem permissao para aceder ao recurso" }; //Cria uma nova instância de ResponseDto com os valores Sucesso = false e Mensagem = "Nao tem permissao para aceder ao recurso"
                    case HttpStatusCode.Unauthorized:
                        return new() { Sucesso = false, Mensagem = "Nao tem autorizacao para aceder ao recurso" }; //Cria uma nova instância de ResponseDto com os valores Sucesso = false e Mensagem = "Nao tem autorizacao para aceder ao recurso"
                    case HttpStatusCode.InternalServerError:
                        return new() { Sucesso = false, Mensagem = "Ocorreu um erro interno do servidor" }; //Cria uma nova instância de ResponseDto com os valores Sucesso = false e Mensagem = "Ocorreu um erro interno do servidor"
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync(); //Atribui o valor retornado por apiResponse.Content.ReadAsStringAsync() à variável apiContent
                        var ApiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent); //Atribui o valor retornado por JsonConvert.DeserializeObject<ResponseDto>(apiContent) à variável ApiResponseDto
                        return ApiResponseDto; //Retorna ApiResponseDto
                }
            }
            catch (Exception ex) //Caso ocorra uma exceção
            {
                var dto = new ResponseDto //Cria uma nova instância de ResponseDto
                {
                    Mensagem = ex.Message.ToString(),
                    Sucesso = false
                };
                return dto; //devolve dto
            }
        }
    }
}