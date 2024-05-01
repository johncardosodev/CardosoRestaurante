using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

namespace CardosoRestaurante.Web.Service
{
    public class CupaoService : ICupaoService
    {
        private readonly IBaseService _baseService;

        public CupaoService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApagarCupaoAsync(int cupaoId)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.DELETE,
                Url = SD.CupaoAPIBase + "/api/cupao/" + cupaoId
            });
        }

        public async Task<ResponseDto?> AtualizarCupaoAsync(CupaoDto cupaoDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.PUT,
                Data = cupaoDto, //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.CupaoAPIBase + "/api/cupao"
            });
        }

        public async Task<ResponseDto?> CriarCupaoAsync(CupaoDto cupaoDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = cupaoDto, //Aqui estamos a deserializar o objeto CupaoDto para JSON e a enviá-lo como parte do corpo da requisição.
                Url = SD.CupaoAPIBase + "/api/cupao"
            });
        }

        public async Task<ResponseDto?> GetCupaoAsync(string cupaoCodigo)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.GET,
                Url = SD.CupaoAPIBase + "/api/cupao/GetCupaoPeloCodigo/" + cupaoCodigo
            });
        }

        public async Task<ResponseDto?> GetCupaoPorIdAsync(int cupaoId)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.GET,
                Url = SD.CupaoAPIBase + "/api/cupao/" + cupaoId
            });
        }

        public async Task<ResponseDto?> GetTodosCupoesAsync()
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.GET,
                Url = SD.CupaoAPIBase + "/api/cupao"
            });
        }
    }
}