namespace CardosoRestaurante.Services.Carrinho.Models.DTO
{
    /// <summary>
    ///  O propósito de um ResponseDTO (Data Transfer Object) é encapsular os dados que serão retornados como resposta de uma operação em um serviço ou API. Ele é usado para transmitir informações de volta ao cliente ou consumidor da API de forma estruturada e padronizada.
    ///  <para>Ao retornar um objeto do tipo ResponseDto, o cliente ou consumidor da API pode acessar essas propriedades para obter informações sobre o resultado da operação e tomar as ações apropriadas com base nesses dados. O uso de um ResponseDTO ajuda a padronizar a estrutura das respostas e facilita a comunicação entre o serviço/API e o cliente/consumidor.</para>
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// 1.	Resultado: Essa propriedade é um objeto que pode ser de qualquer tipo e pode ser nulo. Ela é usada para armazenar o resultado da operação realizada pelo serviço ou API.

        /// </summary>
        public object? Resultado { get; set; } //Este atributo é um objecto que pode ser de qualquer tipo e pode ser nulo

        /// <summary>
        /// 2.	Sucesso: Essa propriedade é do tipo booleano e indica se a operação foi bem-sucedida ou não. Por padrão, ela é definida como true, o que significa que a operação foi bem-sucedida. Caso ocorra algum erro, essa propriedade pode ser definida como false.
        /// </summary>
        public bool Sucesso { get; set; } = true; //Campo que indica se a operação foi bem sucedida e por defeito é true

        /// <summary>
        /// 3.	Mensagem: Essa propriedade é do tipo string e é usada para armazenar uma mensagem de erro ou qualquer outra informação relevante sobre a operação. Por padrão, ela é definida como uma string vazia, mas pode ser preenchida com uma mensagem de erro caso ocorra algum problema durante a operação.
        /// </summary>
        public string Mensagem { get; set; } = "";//Atributo que indica a mensagem de erro
    }
}