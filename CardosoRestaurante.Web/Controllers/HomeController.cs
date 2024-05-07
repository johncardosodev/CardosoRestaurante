using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CardosoRestaurante.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProdutoService _produtoService; // Injeção de dependência do serviço ProdutoService
        private readonly ICarrinhoService _carrinhoService; // Injeção de dependência do serviço CarrinhoService

        public HomeController(IProdutoService produtoService, ICarrinhoService carrinhoService)
        {
            _produtoService = produtoService; // Inicialização do serviço ProdutoService para a classe ProdutoController
            _carrinhoService = carrinhoService;
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

        [Authorize] //Se houve login, o utilizador pode aceder a esta ação
        [HttpPost]
        [ActionName("ProdutoDetalhes")]
        public async Task<IActionResult> ProdutoDetalhes(ProdutoDto produtoDto)
        {
            CarrinhoDto carrinho = new()
            {
                CarrinhoInfo = new CarrinhoInfoDto()
                {
                    UserId = User.Claims.Where(u => u.Type == JwtClaimTypes.Subject).FirstOrDefault()?.Value,
                }
            };

            CarrinhoDetalhesDto carrinhoDetalhesDto = new()
            {
                ProdutoId = produtoDto.ProdutoId,
                Quantidade = produtoDto.Quantidade
            };

            List<CarrinhoDetalhesDto> listaCarrinho = new() { carrinhoDetalhesDto };

            carrinho.CarrinhoDetalhes = listaCarrinho;

            ResponseDto? response = await _carrinhoService.AtualizaCarrinhoAsync(carrinho); // Obtenção de todos os cupões

            if (response != null && response.Sucesso)
            {
                TempData["success"] = "Produto adicionado ao carrinho com sucesso";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return View(produtoDto);
        }
    }
}