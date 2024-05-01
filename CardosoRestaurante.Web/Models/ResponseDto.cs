namespace CardosoRestaurante.Web.Models
{
    /// <summary>
    /// A classe ResponseDto é uma classe modelo que é usada para retornar uma resposta ao usuário com novos parâmetros. Ela é tipicamente usada em aplicações web para encapsular os dados que são enviados de volta para o cliente.
    ///
    /// <para>Em resumo, a classe ResponseDto fornece uma maneira padronizada de estruturar e retornar respostas ao usuário, facilitando o tratamento e a comunicação do resultado das operações de maneira consistente.</para>
    /// </summary>
    public class ResponseDto
    {
        //A classe ResponseDto é uma classe modelo que é usada para retornar uma resposta ao usuário com novos parâmetros. Ela é tipicamente usada em aplicações web para encapsular os dados que são enviados de volta para o cliente.
        //
        public object? Resultado { get; set; } //Essa propriedade é do tipo object?, o que significa que ela pode armazenar um objeto de qualquer tipo, incluindo nulo. Ela é usada para armazenar o resultado de uma operação que está sendo realizada. Por exemplo, se você estiver recuperando dados de um banco de dados, a propriedade Resultado pode armazenar os dados recuperados.

        public bool Sucesso { get; set; } = true; //Essa propriedade é do tipo bool e indica se a operação foi bem-sucedida ou não. Por padrão, ela é definida como true, o que significa que a operação é considerada bem-sucedida, a menos que seja definida explicitamente como falsa. Essa propriedade pode ser usada para lidar com condições de erro ou fornecer feedback ao usuário sobre o sucesso de uma operação.
        public string Mensagem { get; set; } = "";//3.	Mensagem: Essa propriedade é do tipo string e é usada para armazenar uma mensagem de erro ou qualquer outra mensagem relevante que precise ser retornada ao usuário. Por padrão, ela é definida como uma string vazia. Se ocorrer um erro durante a operação, você pode definir essa propriedade como uma mensagem de erro apropriada para informar o usuário sobre o problema.
    }
}