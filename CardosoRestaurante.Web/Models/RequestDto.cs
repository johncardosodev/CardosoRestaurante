using static CardosoRestaurante.Web.Utility.SD;

namespace CardosoRestaurante.Web.Models
{
    /// <summary>
    ///A classe RequestDto tem o propósito de representar um objeto de transferência de dados(DTO) para uma requisição em um aplicativo web.
    //Esta classe é usada para encapsular todas as informações necessárias para fazer uma requisição a uma API em um único objeto, facilitando o transporte desses dados entre diferentes partes do sistema.
    /// </summary>
    public class RequestDto
    {
        //A classe RequestDto tem o propósito de representar um objeto de transferência de dados(DTO) para uma requisição em um aplicativo web.
        //Um DTO é um objeto que carrega dados entre camadas ou componentes de um sistema.No caso específico dessa classe, ela é usada para encapsular os dados necessários para fazer uma requisição a uma API.

        //Esta classe é usada para encapsular todas as informações necessárias para fazer uma requisição a uma API em um único objeto, facilitando o transporte desses dados entre diferentes partes do sistema.

        /// <summary>
        ///ApiTipo: Essa propriedade do tipo APITipo representa o tipo de requisição que será feita à API, como GET, POST, PUT ou DELETE. O valor padrão é APITipo.GET.
        /// </summary>
        public APITipo ApiTipo { get; set; } = APITipo.GET; //•	ApiTipo: Essa propriedade do tipo APITipo representa o tipo de requisição que será feita à API, como GET, POST, PUT ou DELETE. O valor padrão é APITipo.GET.

        /// <summary>
        /// Url: Essa propriedade do tipo string representa a URL da API que será chamada.O valor padrão é uma string vazia.
        /// </summary>
        public string Url { get; set; } = ""; //Url: Essa propriedade do tipo string representa a URL da API que será chamada.O valor padrão é uma string vazia.

        /// <summary>
        /// Essa propriedade do tipo object representa os dados que serão enviados na requisição. O tipo object permite que qualquer tipo de dado seja atribuído a essa propriedade.
        /// </summary>
        public object Data { get; set; } // Essa propriedade do tipo object representa os dados que serão enviados na requisição. O tipo object permite que qualquer tipo de dado seja atribuído a essa propriedade.

        /// <summary>
        /// Essa propriedade do tipo string representa o token de acesso necessário para autenticar a requisição.
        /// </summary>
        public string AcessoToken { get; set; } // Essa propriedade do tipo string representa o token de acesso necessário para autenticar a requisição.
    }
}