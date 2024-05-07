using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace CardosoRestaurante.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [Authorize]
        public async Task<IActionResult> CarrinhoIndex()
        {
            return View(await CarregarCarrinhoDoUtilizadorLogado());
        }

        private async Task<CarrinhoDto> CarregarCarrinhoDoUtilizadorLogado()
        {
            //Receber o ID do utilizador logado
            var userId = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            ResponseDto? response = await _carrinhoService.GetCarrinhoPeloUserIdAsync(userId); //Aqui estamos a enviar o ID do utilizador logado para o serviço de carrinho para obter o carrinho do utilizador logado.

            if (response != null && response.Sucesso)
            {
                CarrinhoDto carrinhoDto = JsonConvert.DeserializeObject<CarrinhoDto>(Convert.ToString(response.Resultado));
                return carrinhoDto;
            }
            return new CarrinhoDto();
        }

        public async Task<IActionResult> RemoverItem(int carrinhoDetalhesId)
        { //Receber o ID do utilizador logado
            var userId = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            ResponseDto? response = await _carrinhoService.RemoverItemCarrinhoAsync(carrinhoDetalhesId);

            if (response != null && response.Sucesso)
            {
                TempData["success"] = "Item removido do carrinho com sucesso!";
                return RedirectToAction(nameof(CarrinhoIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AplicarCupao(CarrinhoDto carrinho)
        { // o Id do Utilizador já está na view como hidden.
            ResponseDto? response = await _carrinhoService.AplicarCupaoAsync(carrinho); //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.

            if (response != null && response.Sucesso)
            {
                TempData["success"] = "Cupão aplicado com sucesso!";
                return RedirectToAction(nameof(CarrinhoIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RemoverCupao(CarrinhoDto carrinho)
        {
            carrinho.CarrinhoInfo.CupaoCodigo = string.Empty;
            ResponseDto? response = await _carrinhoService.AplicarCupaoAsync(carrinho); //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.

            if (response != null && response.Sucesso)
            {
                TempData["success"] = "Cupão aplicado com sucesso!";
                return RedirectToAction(nameof(CarrinhoIndex));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EmailCarrinho(CarrinhoDto carrinho)
        {
            CarrinhoDto carrinhoUtilizador = await CarregarCarrinhoDoUtilizadorLogado();

            //Receber o Email do utilizador logado
            var userEmail = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Email)?.FirstOrDefault()?.Value;

            carrinhoUtilizador.CarrinhoInfo.Email = userEmail;

            ResponseDto? response = await _carrinhoService.EmailCarrinho(carrinhoUtilizador); //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.

            if (response != null && response.Sucesso)
            {
                TempData["success"] = "Email vai ser processado e enviado!";
                return RedirectToAction(nameof(CarrinhoIndex));
            }
            return View();
        }
    }
}