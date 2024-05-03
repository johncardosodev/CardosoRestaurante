using CardosoRestaurante.Web.Models;

namespace CardosoRestaurante.Web.Service.IService
{
    public interface IAuthService
    {
        /// <summary>
        ///LoginAsync: Este método é semelhante ao método AtribuirFuncaoAsync, mas é usado para fazer login de um usuário. Ele recebe um objeto LoginRequestDto como parâmetro, que contém as credenciais do usuário.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);

        /// <summary>
        ///RegistarAsync: Este método é usado para registrar um novo usuário. Ele recebe um objeto RegistrationRequestDto como parâmetro, que contém as informações do novo usuário.
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        Task<ResponseDto?> RegistarAsync(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        ///AtribuirFuncaoAsync: Este método é usado para atribuir uma função a um usuário. Ele recebe um objeto RegistrationRequestDto como parâmetro, que contém as informações do usuário.
        ///<para>Este objeto é então passado para o método EnviarAsync do _baseService, juntamente com a URL e o tipo de requisição (POST). A URL é construída usando a base da URL da API de autenticação e o endpoint específico para atribuir funções.</para>
        /// </summary>
        /// <param name="registrationRequestDto"></param>
        /// <returns></returns>
        Task<ResponseDto?> AtribuirFuncaoAsync(RegistrationRequestDto registrationRequestDto);
    }
}