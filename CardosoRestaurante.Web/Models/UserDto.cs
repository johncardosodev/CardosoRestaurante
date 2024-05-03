namespace CardosoRestaurante.Web.Models
{
    public class UserDto
    {//Esta class vai conter todos atributos criticos para a autenticação de um utilizador
        public string Id { get; set; } //Id do utilizador
        public string Nome { get; set; } //Nome do utilizador
        public string Email { get; set; } //Email do utilizador
        public string Telemovel { get; set; } //Password do utilizador
    }
}