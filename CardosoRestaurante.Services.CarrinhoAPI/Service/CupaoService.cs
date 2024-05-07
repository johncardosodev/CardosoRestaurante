using CardosoRestaurante.Services.Carrinho.Models.DTO;
using CardosoRestaurante.Services.CarrinhoAPI.Service.IService;
using Newtonsoft.Json;

namespace CardosoRestaurante.Services.CarrinhoAPI.Service
{
    public class CupaoService : ICupaoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CupaoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<CupaoDto> GetCupao(string cupaoCodigo)
        {
            var cliente = _httpClientFactory.CreateClient("Cupao"); //Cria um cliente HTTP para a API de produtos
            var response = await cliente.GetAsync($"api/cupao/GetCupaoPeloCodigo/{cupaoCodigo}"); //Faz uma solicitação GET para a API de produtos para obter a lista de produtos

            var apiContent = await response.Content.ReadAsStringAsync(); //Lê o conteúdo da resposta da API de produtos
            var respostaObjeto = JsonConvert.DeserializeObject<ResponseDto>(apiContent); //Converte o conteúdo da resposta em um objeto ResponseDto

            if (respostaObjeto.Sucesso)
            {
                var cupao = JsonConvert.DeserializeObject<CupaoDto>(Convert.ToString(respostaObjeto.Resultado)); //Converte o resultado da resposta em uma lista de objetos ProdutoDto
                return cupao; //Retorna a lista de produtos
            }
            else
            {
                return new CupaoDto();//Retorna uma lista vazia se a resposta não for bem-sucedida
            }
        }
    }
}