using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Models.DTO;

namespace CardosoRestaurante.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        Task<ResponseDto?> RegistarAsync(RegistrationRequestDto registrationRequestDto);

        Task<ResponseDto?> AtribuirFuncaoAsync(RegistrationRequestDto registrationRequestDto);
    }
}