using CardosoRestaurante.Services.AuthAPI.Models.DTO;
using CardosoRestaurante.Services.AuthAPI.Service.IService;
using CardosoRestaurante.Services.CupaoAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CardosoRestaurante.Services.AuthAPI.Controllers
{
    [Route("api/auth")] //Rota para aceder a API
    [ApiController] //Significa que é uma API e não um MVC
    public class AuthAPIController : Controller
    {
        private readonly IAuthService _authService; //Interface para o serviço de autenticação
        protected ResponseDto _response; //Objecto para devolver a resposta

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto(); //Instanciar o objecto
        }

        //Configurar ENDPOINT para verificar se a API está a funcionar
        [HttpPost("registar")]
        public async Task<IActionResult> Registar([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var MensagemErro = await _authService.Registar(registrationRequestDto);
            if (!string.IsNullOrEmpty(MensagemErro)) //Se nao houve erro
            {
                _response.Sucesso = false; //Se houver erro o sucesso é falso
                _response.Mensagem = MensagemErro; //Mensagem de erro
                return BadRequest(_response); //Devolve o erro 400 com a mensagem de erro no corpo da resposta
            }
            return Ok(_response); //Se não houver erro devolve o sucesso 200
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var loginResponseDto = await _authService.Login(loginRequestDto);
            if (loginResponseDto.User == null) //Se o user for nulo
            {
                _response.Sucesso = false; //Se houver erro o sucesso é falso
                _response.Mensagem = "User name e/ou password estão errados"; //Mensagem de erro
                return BadRequest(_response); //Devolve o erro 400 com a mensagem de erro no corpo da resposta
            }
            _response.Resultado = loginResponseDto; //Se não houver erro devolve o sucesso 200
            return Ok(_response); //Se não houver erro devolve o sucesso 200
        }

        [HttpPost("atribuirFuncao")]
        public async Task<IActionResult> AtribuirFuncao([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            var atribuirFuncaoValido = await _authService.AtribuirFuncao(registrationRequestDto.Email, registrationRequestDto.Funcao.ToUpper());
            if (!atribuirFuncaoValido) //Se o user for nulo
            {
                _response.Sucesso = false; //Se houver erro o sucesso é falso
                _response.Mensagem = "Erro encontrado"; //Mensagem de erro
                return BadRequest(_response); //Devolve o erro 400 com a mensagem de erro no corpo da resposta
            }
            return Ok(_response); //Se não houver erro devolve o sucesso 200
        }
    }
}