using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

namespace CardosoRestaurante.Web.Service
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly IBaseService _baseService;

        public CarrinhoService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AplicarCupaoAsync(CarrinhoDto carrinhoDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = carrinhoDto, //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.CarrinhoAPIBase + "/api/carrinho/AplicaCupao"
            });
        }

        public async Task<ResponseDto?> AtualizaCarrinhoAsync(CarrinhoDto carrinhoDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = carrinhoDto, //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.CarrinhoAPIBase + "/api/carrinho/CarrinhoAtualiza"
            });
        }

        public async Task<ResponseDto?> GetCarrinhoPeloUserIdAsync(string userId)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.GET,
                Url = SD.CarrinhoAPIBase + "/api/carrinho/GetCarrinho/" + userId
            });
        }

        public async Task<ResponseDto?> RemoverItemCarrinhoAsync(int carrinhoDetalheId)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = carrinhoDetalheId, //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.CarrinhoAPIBase + "/api/carrinho/RemoverCarrinho"
            });
        }

        public async Task<ResponseDto?> EmailCarrinho(CarrinhoDto carrinhoDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = carrinhoDto, //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.CarrinhoAPIBase + "/api/carrinho/EmailCarrinhoRequest"
            });
        }
    }
}