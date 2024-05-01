namespace CardosoRestaurante.Services.CupaoAPI.Models.DTO
{
    public class ResponseDto
    {
        //O propósito desta classe é devolver um objecto que pode ser de qualquer tipo API
        public object? Resultado { get; set; } //Este atributo é um objecto que pode ser de qualquer tipo e pode ser nulo

        public bool Sucesso { get; set; } = true; //Campo que indica se a operação foi bem sucedida e por defeito é true
        public string Mensagem { get; set; } = "";//Atributo que indica a mensagem de erro
    }
}