using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Models.Dto;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

namespace CardosoRestaurante.Web.Service
{
    public class ProdutoService : IProdutoService
    {
        private readonly IBaseService _baseService;

        public ProdutoService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApagarProdutoAsync(int produtoId)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.DELETE,
                Url = SD.ProdutoAPIBase + "/api/produto/" + produtoId
            });
        }

        public async Task<ResponseDto?> AtualizarProdutoAsync(ProdutoDto produtoDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.PUT,
                Data = produtoDto, //Aqui estamos a deserializar o objeto ProdutoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.ProdutoAPIBase + "/api/produto"
            });
        }

        public async Task<ResponseDto?> CriarProdutoAsync(ProdutoDto produtoDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = produtoDto, //Aqui estamos a deserializar o objeto ProdutoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.ProdutoAPIBase + "/api/produto"
            });
        }

        public async Task<ResponseDto?> GetProdutoAsync(string produtoNome)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.GET,
                Url = SD.ProdutoAPIBase + "/api/produto/GetProdutoPeloNome/" + produtoNome
            });
        }

        public async Task<ResponseDto?> GetProdutoPorIdAsync(int produtoId)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.GET,
                Url = SD.ProdutoAPIBase + "/api/produto/" + produtoId
            });
        }

        public async Task<ResponseDto?> GetTodosProdutosAsync()
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.GET,
                Url = SD.ProdutoAPIBase + "/api/produto"
            });
        }
    }
}