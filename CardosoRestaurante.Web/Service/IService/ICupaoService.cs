using CardosoRestaurante.Web.Models;

namespace CardosoRestaurante.Web.Service.IService
{
    /// <summary>
    /// Esta interface ICupaoService define um contrato para as classes que a implementam, garantindo que elas forneçam métodos para interagir com a entidade Cupao.
    ///<para>A interface segue o Controllador CupaoController, que é responsável por lidar com as solicitações HTTP relacionadas à entidade Cupao.</para>
    /// </summary>
    public interface ICupaoService
    {//Esta interface ICupaoService define um contrato para as classes que a implementam, garantindo que elas forneçam métodos para interagir com a entidade Cupao.
        //A interface segue o Controllador CupaoController, que é responsável por lidar com as solicitações HTTP relacionadas à entidade Cupao.
        Task<ResponseDto?> GetCupaoAsync(string cupaoCodigo);

        Task<ResponseDto?> GetTodosCupoesAsync();

        Task<ResponseDto?> GetCupaoPorIdAsync(int cupaoId);

        Task<ResponseDto?> CriarCupaoAsync(CupaoDto cupaoDto);

        Task<ResponseDto?> AtualizarCupaoAsync(CupaoDto cupaoDto);

        Task<ResponseDto?> ApagarCupaoAsync(int cupaoId);
    }
}