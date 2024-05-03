using CardosoRestaurante.Web.Models;
using CardosoRestaurante.Web.Service.IService;
using CardosoRestaurante.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CardosoRestaurante.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider) //Dependência de injeção de construtor
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            ResponseDto responseDto = await _authService.LoginAsync(loginRequestDto); //Enviar resposta para o serviço .

            if (responseDto != null && responseDto.Sucesso) //Se o resuktado for bem suceddido
            {
                LoginResponseDto loginResponseDto =
                    JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Resultado)); //Deserializar o responseDto para um objeto LoginResponseDto

                await SignInUser(loginResponseDto); //Chamar o método SignInUser para autenticar o usuário

                _tokenProvider.SetToken(loginResponseDto.Token); //Definir o token no cookie

                return RedirectToAction("Index", "Home"); //Redirecionar para a página inicials
            }
            else
            {
                //ModelState.AddModelError("CustomError", responseDto.Mensagem); //Adicionar erro ao ModelState
                TempData["error"] = responseDto.Mensagem;
                return View(loginRequestDto);
            }
        }

        [HttpGet]
        public IActionResult Registar()
        {
            //Alimentar SelectListItems no DropDownList
            var funcaoLista = new List<SelectListItem>
            {
                new SelectListItem() { Text = SD.FuncaoAdmin, Value = SD.FuncaoAdmin },
                new SelectListItem() { Text = SD.FuncaoUtilizador, Value = SD.FuncaoUtilizador }
            };

            ViewBag.FuncaoLista = funcaoLista;
            RegistrationRequestDto registrationRequestDto = new RegistrationRequestDto(); //O RegistrationDto para mandar para a view
            return View(registrationRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Registar(RegistrationRequestDto registrationRequestDto)
        {
            ResponseDto resultado = await _authService.RegistarAsync(registrationRequestDto); //Enviar resposta para o serviço .
            ResponseDto? atribuirFuncao;

            if (resultado != null && resultado.Sucesso) //Se o resuktado for bem suceddido
            {
                if (string.IsNullOrEmpty(registrationRequestDto.Funcao)) //Caso a funcao esteja vazia
                {
                    registrationRequestDto.Funcao = SD.FuncaoUtilizador; //Atribui a função de utilizador
                }
                atribuirFuncao = await _authService.AtribuirFuncaoAsync(registrationRequestDto); //Vai atribuir a função

                if (atribuirFuncao != null && atribuirFuncao.Sucesso)  //Se o response for bem sucedido
                {
                    TempData["success"] = "Registo efetuado com sucesso!";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = resultado.Mensagem;
            }

            //Alimentar SelectListItems no DropDownList
            var funcaoLista = new List<SelectListItem>
            {
                new SelectListItem() { Text = SD.FuncaoAdmin, Value = SD.FuncaoAdmin },
                new SelectListItem() { Text = SD.FuncaoUtilizador, Value = SD.FuncaoUtilizador }
            };

            ViewBag.FuncaoLista = funcaoLista;
            return View(registrationRequestDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(); //Fazer logout
            _tokenProvider.ClearToken(); //Limpar o token
            return RedirectToAction("Index", "Home"); //Redirecionar para a página inicial
        }

        /// <summary>
        /// Método SignInUser lê as informações do token JWT recebido após o login, cria uma identidade com base nessas informações e autentica o usuário usando o esquema de autenticação de cookies. Isso permite que o usuário seja reconhecido e autorizado em solicitações futuras.
        /// </summary>
        /// <param name="loginResponseDto"></param>
        /// <returns></returns>
        private async Task SignInUser(LoginResponseDto loginResponseDto)
        {
            var handler = new JwtSecurityTokenHandler(); //JwtSecurityTokenHandler, que é responsável por manipular e ler tokens JWT.

            var jwt = handler.ReadJwtToken(loginResponseDto.Token); //O token JWT é lido usando o handler.ReadJwtToken, passando o token do loginResponseDto.

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme); //Uma nova identidade (ClaimsIdentity) é criada usando o esquema de autenticação de cookies padrão (CookieAuthenticationDefaults.AuthenticationScheme).

            //As informações do usuário são extraídas do token JWT e adicionadas à identidade como reivindicações.
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, //O ID do usuário é adicionado à identidade com base na reivindicação de ID do nome do token JWT.
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, //O nome do usuário é adicionado à identidade com base na reivindicação de nome do token JWT.
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,//O nome do usuário é adicionado à identidade com base na reivindicação de nome do token JWT.
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value)); //A reivindicação de função é adicionada à identidade com base na reivindicação de função do token JWT.

            var principal = new ClaimsPrincipal(identity); //Uma instância de ClaimsPrincipal é criada usando a identidade criada. O ClaimsPrincipal é um contêiner para reivindicações que representam a identidade do usuário autenticado.

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal); //O usuário é autenticado usando o método SignInAsync, que é fornecido pelo HttpContext. O esquema de autenticação de cookies padrão é passado como argumento, juntamente com o principal que representa a identidade do usuário autenticado. Também é possível passar opções adicionais, como a duração do cookie e se ele deve ser persistente ou não para outros controladores
        }
    }
}