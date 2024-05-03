using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;

namespace CardosoRestaurante.Web.Service
{
    /// <summary>
    /// A classe AuthService implementa a interface IAuthService e é responsável por fornecer serviços de autenticação, como login, registro e atribuição de funções.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService) //Dependência de injeção de construtor
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
            }, comBearer: false); //ComBearer é falso porque não é necessário um token de autenticação para fazer login
        }

        public async Task<ResponseDto?> RegistarAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.EnviarAsync(new RequestDto
            {
                ApiTipo = SD.APITipo.POST,
                Data = registrationRequestDto,
                Url = SD.AuthAPIBase + "/api/auth/registar"
            }, comBearer: false); //ComBearer é falso porque não é necessário um token de autenticação para se registrar
        }
    }
}