using CardosoRestaurante.Services.Carrinho.Models.Dto;
using CardosoRestaurante.Services.Carrinho.Models.DTO;
using CardosoRestaurante.Services.CarrinhoAPI.Service.IService;
using Newtonsoft.Json;

namespace CardosoRestaurante.Services.CarrinhoAPI.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProdutoService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Este método faz uma solicitação GET para a API de produtos, obtém a resposta, verifica se a solicitação foi bem-sucedida e retorna a lista de produtos se for o caso, ou uma lista vazia caso contrário.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProdutoDto>> GetProdutos()
        {
            var cliente = _httpClientFactory.CreateClient("Produto"); //Cria um cliente HTTP para a API de produtos
            var response = await cliente.GetAsync($"api/produto"); //Faz uma solicitação GET para a API de produtos para obter a lista de produtos

            var apiContent = await response.Content.ReadAsStringAsync(); //Lê o conteúdo da resposta da API de produtos
            var respostaObjeto = JsonConvert.DeserializeObject<ResponseDto>(apiContent); //Converte o conteúdo da resposta em um objeto ResponseDto

            if (respostaObjeto.Sucesso)
            {
                var produtos = JsonConvert.DeserializeObject<IEnumerable<ProdutoDto>>(Convert.ToString(respostaObjeto.Resultado)); //Converte o resultado da resposta em uma lista de objetos ProdutoDto
                return produtos; //Retorna a lista de produtos
            }
            else
            {
                return new List<ProdutoDto>();//Retorna uma lista vazia se a resposta não for bem-sucedida
            }
        }
    }
}