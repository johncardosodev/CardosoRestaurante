using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Models.DTO;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

namespace CardosoRestaurante.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AtribuirFuncaoAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/atribuirFuncao"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = loginRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/login"
            });
        }

        public async Task<ResponseDto?> RegistarAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/registar"
            });
        }
    }
}