using CardosoRestaurante.Services.AuthAPI.Data;
using CardosoRestaurante.Services.AuthAPI.Models;
using CardosoRestaurante.Services.AuthAPI.Models.DTO;
using CardosoRestaurante.Services.AuthAPI.Service.IService;
using Microsoft.AspNetCore.Identity;

namespace CardosoRestaurante.Services.AuthAPI.Service
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db; //chama a base de dados
        private readonly UserManager<ApplicationUser> _userManager; //chama o utilizador
        private readonly RoleManager<IdentityRole> _roleManager; //chama o role
        private readonly IJwtTokenGenerator _jwtTokenGenerator; //chama o token

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        //EndPoits Login e Registar
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.UserName.ToLower()); //Vai buscar o utilizador pelo username

            bool userValido = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password); //Verifica se a password é válida

            if (user == null || userValido == false) //Se o utilizador não existir ou a password for inválida
            {
                return new LoginResponseDto() { User = null, Token = null }; //Retorna o utilizador e o token a null
            }

            //Se o utilizador existir, gerar JWT token
            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles); //Gera o token

            UserDto userDto = new UserDto() //Se o utilizador existir e a password for válida
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email,
                Telemovel = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto() //Cria um novo token
            {
                User = userDto,
                Token = token //Atribui o token ao utilizador
            };

            return loginResponseDto; //Retorna o
        }

        public async Task<string> Registar(RegistrationRequestDto registrationRequestDto)
        {
            //string username = registrationRequestDto.Email.Substring(0, registrationRequestDto.Email.IndexOf('@')); //Recorta o email até ao @
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                Nome = registrationRequestDto.Nome,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.Telemovel
            };

            try
            {/*A linha de código selecionada cria um novo usuário no sistema usando o objeto _userManager. O método CreateAsync é chamado para criar o usuário com base nos parâmetros fornecidos: o objeto user, que contém as informações do usuário, e a senha fornecida no objeto registrationRequestDto. Essa linha de código é responsável por criar um novo registro de usuário no sistema com as informações fornecidas pelo usuário durante o processo de registro.*/

                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password); //Cria o utilizador com a password e o email do utilizador

                if (result.Succeeded) //Se o utilizador for criado com sucesso
                {
                    // Agora no contexto de um serviço de autenticação, onde após o registro de um novo usuário, é necessário recuperar as informações desse usuário para retorná-las como resposta ao processo de registro. Dessa forma, o código permite que as informações do usuário recém-criado sejam disponibilizadas para uso posterior, como exibição na interface do usuário ou armazenamento em cache.

                    var userBaseDados = _db.ApplicationUsers.First(u => u.UserName == registrationRequestDto.Email); //Vai buscar o utilizador pelo username

                    UserDto userDto = new UserDto()
                    {
                        Id = userBaseDados.Id,
                        Nome = userBaseDados.Nome,
                        Email = userBaseDados.Email,
                        Telemovel = userBaseDados.PhoneNumber
                    };
                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description; //Se não for criado o utilizador retorna a descrição do erro
                }
            }
            catch (Exception)
            {
            }
            return "Erro ao registar o utilizador"; //Se não for criado o utilizador retorna a mensagem de erro
        }

        public async Task<bool> AtribuirFuncao(string email, string roleName)
        {
            //o código busca um usuário no banco de dados com base no endereço de email fornecido. Ele usa o objeto _db.ApplicationUsers para acessar a tabela de usuários e o método FirstOrDefault para encontrar o primeiro usuário cujo endereço de email corresponda ao fornecido.
            var user = _db.ApplicationUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower()); //Vai buscar o utilizador pelo email

            if (user != null)
            {
                //usa o objeto _roleManager e o método RoleExistsAsync para verificar se a função já está cadastrada no sistema. O método GetAwaiter().GetResult() é usado para aguardar a conclusão da verificação.

                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult()) //Se o role não existir
                {
                    //Se a função não existir, o código cria a função usando o objeto _roleManager e o método CreateAsync. Ele cria uma nova instância da classe IdentityRole com o nome da função fornecido e usa o método GetAwaiter().GetResult() para aguardar a conclusão da criação.
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult(); //Cria o role
                }

                //Em seguida, o código usa o objeto _userManager e o método AddToRoleAsync para adicionar o usuário à função especificada. Ele associa o usuário à função usando o método AddToRoleAsync e aguarda a conclusão usando a palavra-chave await.
                await _userManager.AddToRoleAsync(user, roleName); //Adiciona o utilizador ao role

                return true; //Se for bem sucedido
            }
            return false; //Se não for bem sucedido
        }
    }
}