using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CardosoRestaurante.Web.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoService _produtoService; // Injeção de dependência do serviço ProdutoService

        public ProdutoController(IProdutoService produtoService)
        {
            _produtoService = produtoService; // Inicialização do serviço ProdutoService para a classe ProdutoController
        }

        public async Task<IActionResult> ProdutoIndex()
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

        [HttpGet]
        public async Task<IActionResult> ProdutoCriar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProdutoCriar(ProdutoDto produtoDto)

        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _produtoService.CriarProdutoAsync(produtoDto); // Criação de um cupão

                if (response != null && response.Sucesso)
                {
                    TempData["success"] = "Produto criado com sucesso!";
                    return RedirectToAction(nameof(ProdutoIndex)); // Redirecionamento para a ação ProdutoIndex
                }
                else
                {
                    TempData["error"] = response?.Mensagem;
                }
            }
            return View(produtoDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProdutoEditar(int produtoId)
        {
            ResponseDto? response = await _produtoService.GetProdutoPorIdAsync(produtoId); // Obtenção de um cupão pelo id

            if (response != null && response.Sucesso)
            {
                ProdutoDto? produtoDto = JsonConvert.DeserializeObject<ProdutoDto>(Convert.ToString(response.Resultado));// Desserialização do resultado da resposta para um ProdutoDto
                return View(produtoDto);
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProdutoEditar(ProdutoDto produtoDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _produtoService.AtualizarProdutoAsync(produtoDto); // Atualização de um cupão

                if (response != null && response.Sucesso)
                {
                    TempData["success"] = "Produto atualizado com sucesso!";
                    return RedirectToAction(nameof(ProdutoIndex)); // Redirecionamento para a ação ProdutoIndex
                }
            }
            return View(produtoDto);
        }

        [HttpGet]
        public async Task<IActionResult> ProdutoApagar(int produtoId)
        {
            ResponseDto? response = await _produtoService.GetProdutoPorIdAsync(produtoId); // Obtenção de um cupão pelo id

            if (response != null && response.Sucesso)
            {
                ProdutoDto? produtoDto = JsonConvert.DeserializeObject<ProdutoDto>(Convert.ToString(response.Resultado));// Desserialização do resultado da resposta para um ProdutoDto
                return View(produtoDto);
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProdutoApagar(ProdutoDto produtoDto)
        {
            ResponseDto? response = await _produtoService.ApagarProdutoAsync(produtoDto.ProdutoId); // Apagar um cupão

            if (response != null && response.Sucesso)
            {
                TempData["success"] = "Produto apagado com sucesso!";
                return RedirectToAction(nameof(ProdutoIndex)); // Redirecionamento para a ação ProdutoIndex
            }
            return NotFound();
        }
    }
}