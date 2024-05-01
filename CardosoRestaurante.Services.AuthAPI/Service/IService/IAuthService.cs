using CardosoRestaurante.Services.AuthAPI.Models.DTO;

namespace CardosoRestaurante.Services.AuthAPI.Service.IService
{/// <summary>
 ///         A interface IAuthService define um contrato para um serviço de autenticação em um aplicativo.Ela contém dois métodos: Registar e Login.
 ///         <para>Ambos os métodos retornam objetos DTO(Data Transfer Object) como resultado.No caso do método Registar, ele retorna um objeto UserDto, que contém informações sobre o usuário registrado.No caso do método Login, ele retorna um objeto LoginResponseDto, que contém informações sobre o resultado da autenticação, como um token de acesso ou uma mensagem de erro.</para>
 ///
 /// <para>Essa interface serve como um contrato para implementações concretas do serviço de autenticação.As classes que implementam essa interface devem fornecer a lógica real para o registro e login de usuários, de acordo com os requisitos específicos do aplicativo.</para>
 /// </summary>
    public interface IAuthService
    {/// <summary>
     /// O método Registar é responsável por registrar um novo usuário no sistema.Ele recebe um objeto RegistrationRequestDto como parâmetro, que contém as informações necessárias para criar uma nova conta de usuário.O método retorna uma tarefa assíncrona que representa o processo de registro.
     /// </summary>
     /// <param name="registrationRequestDto"></param>
     /// <returns></returns>
        Task<string> Registar(RegistrationRequestDto registrationRequestDto);

        /// <summary>
        /// O método Login é responsável por autenticar um usuário no sistema.Ele recebe um objeto LoginRequestDto como parâmetro, que contém as credenciais de login do usuário.O método retorna uma tarefa assíncrona que representa o processo de autenticação.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);

        /// <summary>
        ///O código selecionado é um método chamado AtribuirFuncao que faz parte da classe AuthService. Esse método é responsável por atribuir uma função (role) a um usuário com base no seu endereço de email.
        ///
        /// <para>Importante ressaltar que esse código depende de outras partes do sistema, como o banco de dados, o gerenciador de usuários (UserManager) e o gerenciador de funções (RoleManager). Essas dependências são injetadas no construtor da classe AuthService e são usadas dentro do método AtribuirFuncao para realizar as operações necessárias.</para>
        /// </summary>
        /// <param name="email"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> AtribuirFuncao(string email, string roleName);
    }
}