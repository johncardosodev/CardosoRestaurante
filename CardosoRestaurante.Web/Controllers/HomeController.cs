using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Models.Dto;
using CardosoRestaurante.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CardosoRestaurante.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoService _produtoService; // Injeção de dependência do serviço ProdutoService

        public HomeController(IProdutoService produtoService)
        {
            _produtoService = produtoService; // Inicialização do serviço ProdutoService para a classe ProdutoController
        }

        public async Task<IActionResult> Index()
        {
            List<ProdutoDto>? lista = new();// Criação de uma lista de ProdutoDto vazia

            ResponseDto? response = await _produtoService.GetTodosProdutosAsync(); // Obtenção de todos os cupões

            if (response != null && response.Sucesso)
            {
                lista = JsonConvert.DeserializeObject<List<ProdutoDto>>(Convert.ToString(response.Resultado));// Desserialização do resultado da resposta para a lista de ProdutoDto
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return View(lista);
        }

        [Authorize] //Se houve login, o utilizador pode aceder a esta ação
        public async Task<IActionResult> ProdutoDetalhes(int produtoId)
        {
            ProdutoDto? objetoDto = new();// Criação de uma lista de ProdutoDto vazia

            ResponseDto? response = await _produtoService.GetProdutoPorIdAsync(produtoId); // Obtenção de todos os cupões

            if (response != null && response.Sucesso)
            {
                objetoDto = JsonConvert.DeserializeObject<ProdutoDto>(Convert.ToString(response.Resultado));// Desserialização do resultado da resposta para ProdutoDto
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return View(objetoDto);
        }
    }
}