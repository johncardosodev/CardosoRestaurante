using AutoMapper;
using CardosoRestaurante.Services.CarrinhoAPI.Models;

namespace CardosoRestaurante.Services.Carrinho
{
    public class MappingConfig
    {
        /// <summary>
        /// O método RegisterMaps é um método estático que configura o mapeamento entre as classes Cupao e CupaoDto.
        /// Este método faz parte da configuração do AutoMapper na aplicação, que é uma biblioteca popular de mapeamento objeto-objeto que pode ajudar a simplificar a transferência de dados entre camadas.
        /// </summary>
        /// <returns>
        /// Retorna uma instância de MapperConfiguration. Esta instância representa a configuração de mapeamento para a aplicação, e pode ser usada para criar instâncias de IMapper, que podem então ser usadas para mapear objetos.
        /// </returns>
        /// <remarks>
        /// O método CreateMap é usado para definir uma configuração de mapeamento entre as classes Cupao e CupaoDto. O método ReverseMap é então chamado para indicar que o mapeamento deve ser bidirecional, ou seja, os objetos podem ser mapeados de Cupao para CupaoDto e de CupaoDto para Cupao.
        /// </remarks>
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CarrinhoDetalhes, CarrinhoDetalhesDto>().ReverseMap();
                config.CreateMap<CarrinhoInfo, CarrinhoInfoDto>().ReverseMap();
            });

            return mappingConfig;
        }
    }
}