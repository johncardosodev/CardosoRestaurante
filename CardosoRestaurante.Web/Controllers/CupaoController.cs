using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CardosoRestaurante.Web.Controllers
{
    public class CupaoController : Controller
    {
        private readonly ICupaoService _cupaoService; // Injeção de dependência do serviço CupaoService

        public CupaoController(ICupaoService cupaoService)
        {
            _cupaoService = cupaoService; // Inicialização do serviço CupaoService para a classe CupaoController
        }

        public async Task<IActionResult> CupaoIndex()
        {
            List<CupaoDto>? lista = new();// Criação de uma lista de CupaoDto vazia

            ResponseDto? response = await _cupaoService.GetTodosCupoesAsync(); // Obtenção de todos os cupões

            if (response != null && response.Sucesso)
            {
                lista = JsonConvert.DeserializeObject<List<CupaoDto>>(Convert.ToString(response.Resultado));// Desserialização do resultado da resposta para a lista de CupaoDto
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> CupaoCriar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CupaoCriar(CupaoDto cupaoDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _cupaoService.CriarCupaoAsync(cupaoDto); // Criação de um cupão

                if (response != null && response.Sucesso)
                {
                    TempData["success"] = "Cupão criado com sucesso!";
                    return RedirectToAction(nameof(CupaoIndex)); // Redirecionamento para a ação CupaoIndex
                }
                else
                {
                    TempData["error"] = response?.Mensagem;
                }
            }
            return View(cupaoDto);
        }

        [HttpGet]
        public async Task<IActionResult> CupaoEditar(int cupaoId)
        {
            ResponseDto? response = await _cupaoService.GetCupaoPorIdAsync(cupaoId); // Obtenção de um cupão pelo id

            if (response != null && response.Sucesso)
            {
                CupaoDto? cupaoDto = JsonConvert.DeserializeObject<CupaoDto>(Convert.ToString(response.Resultado));// Desserialização do resultado da resposta para um CupaoDto
                return View(cupaoDto);
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CupaoEditar(CupaoDto cupaoDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _cupaoService.AtualizarCupaoAsync(cupaoDto); // Atualização de um cupão

                if (response != null && response.Sucesso)
                {
                    TempData["success"] = "Cupão atualizado com sucesso!";
                    return RedirectToAction(nameof(CupaoIndex)); // Redirecionamento para a ação CupaoIndex
                }
            }
            return View(cupaoDto);
        }

        [HttpGet]
        public async Task<IActionResult> CupaoApagar(int cupaoId)
        {
            ResponseDto? response = await _cupaoService.GetCupaoPorIdAsync(cupaoId); // Obtenção de um cupão pelo id

            if (response != null && response.Sucesso)
            {
                CupaoDto? cupaoDto = JsonConvert.DeserializeObject<CupaoDto>(Convert.ToString(response.Resultado));// Desserialização do resultado da resposta para um CupaoDto
                return View(cupaoDto);
            }
            else
            {
                TempData["error"] = response?.Mensagem;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CupaoApagar(CupaoDto cupaoDto)
        {
            ResponseDto? response = await _cupaoService.ApagarCupaoAsync(cupaoDto.CupaoId); // Apagar um cupão

            if (response != null && response.Sucesso)
            {
                TempData["success"] = "Cupão apagado com sucesso!";
                return RedirectToAction(nameof(CupaoIndex)); // Redirecionamento para a ação CupaoIndex
            }
            return NotFound();
        }
    }
}