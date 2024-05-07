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
        private readonly IProdutoService _produtoService; // Inje��o de depend�ncia do servi�o ProdutoService
        private readonly ICarrinhoService _carrinhoService; // Inje��o de depend�ncia do servi�o CarrinhoService

        public HomeController(IProdutoService produtoService, ICarrinhoService carrinhoService)
        {
            _produtoService = produtoService; // Inicializa��o do servi�o ProdutoService para a classe ProdutoController
            _carrinhoService = carrinhoService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProdutoDto>? lista = new();// Cria��o de uma lista de ProdutoDto vazia

            ResponseDto? response = await _produtoService.GetTodosProdutosAsync(); // Obten��o de todos os cup�es

            if (response != null && response.Sucesso)
            {
                lista = JsonConvert.DeserializeObject<List<ProdutoDto>>(Convert.ToString(response.Resultado));// Desserializa��o do resultado da resposta para a lista de ProdutoDto
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return View(lista);
        }

        [Authorize] //Se houve login, o utilizador pode aceder a esta a��o
        public async Task<IActionResult> ProdutoDetalhes(int produtoId)
        {
            ProdutoDto? objetoDto = new();// Cria��o de uma lista de ProdutoDto vazia

            ResponseDto? response = await _produtoService.GetProdutoPorIdAsync(produtoId); // Obten��o de todos os cup�es

            if (response != null && response.Sucesso)
            {
                objetoDto = JsonConvert.DeserializeObject<ProdutoDto>(Convert.ToString(response.Resultado));// Desserializa��o do resultado da resposta para ProdutoDto
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return View(objetoDto);
        }

        [Authorize] //Se houve login, o utilizador pode aceder a esta a��o
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

            ResponseDto? response = await _carrinhoService.AtualizaCarrinhoAsync(carrinho); // Obten��o de todos os cup�es

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