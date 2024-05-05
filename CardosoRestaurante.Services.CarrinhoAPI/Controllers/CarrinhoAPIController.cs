using AutoMapper;
using CardosoRestaurante.Services.Carrinho.Data;
using CardosoRestaurante.Services.Carrinho.Models.DTO;
using CardosoRestaurante.Services.CarrinhoAPI.Models;
using CardosoRestaurante.Services.CarrinhoAPI.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardosoRestaurante.Services.CarrinhoAPI.Controllers
{
    [Route("api/carrinho")]
    [ApiController]
    public class CarrinhoAPIController : ControllerBase
    {
        private ResponseDto _response;
        private readonly IMapper _mapper;
        private readonly AppDbContext _db;

        public CarrinhoAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        //Unico endPoint que devolve o carrinho de compras
        [HttpPost("CarrinhoAtualiza")]
        public async Task<ResponseDto> CarrinhoAtualiza(CarrinhoDto carrinhoDto)
        {
            try
            {//O método AsNoTracking() é usado para indicar que as entidades retornadas não serão rastreadas pelo contexto do banco de dados e nao vimos o erro de rastreamento de entidades..«
                var carrinhoInfoBd = await _db.CarrinhoInfos.AsNoTracking().FirstOrDefaultAsync(c => c.UserId == carrinhoDto.CarrinhoInfo.UserId); //

                if (carrinhoInfoBd == null)
                {
                    CarrinhoInfo carrinhoInfo = _mapper.Map<CarrinhoInfo>(carrinhoDto.CarrinhoInfo); //Se não existir, o CarrinhoInfo do CarrinhoDto é mapeado para um novo objeto CarrinhoInfo usando o AutoMapper,

                    _db.CarrinhoInfos.Add(carrinhoInfo); //Adiciona o novo CarrinhoInfo ao contexto do banco de dados

                    await _db.SaveChangesAsync();

                    carrinhoDto.CarrinhoDetalhes.First().CarrinhoInfoId = carrinhoInfo.CarrinhoInfoId; //o CarrinhoInfoId do primeiro CarrinhoDetalhes no CarrinhoDto é definido para o CarrinhoInfoId do novo CarrinhoInfo.

                    _db.CarrinhoDetalhes.Add(_mapper.Map<CarrinhoDetalhes>(carrinhoDto.CarrinhoDetalhes.First())); // o primeiro CarrinhoDetalhes do CarrinhoDto é mapeado para um novo objeto CarrinhoDetalhes usando o AutoMapper

                    await _db.SaveChangesAsync();
                }
                else
                {
                    //Se carrinhoInfoBd não for nulo, então o carrinho já existe
                    //Entao ver se carrinhoDetalhes tem o mesmo produtoId
                    var carrinhoDetalhesDaBd = await _db.CarrinhoDetalhes.AsNoTracking().FirstOrDefaultAsync(        // tenta encontrar um CarrinhoDetalhes existente no banco de dados que corresponda ao
                    c => c.ProdutoId == carrinhoDto.CarrinhoDetalhes.First().ProdutoId &&       //ProdutoId do primeiro CarrinhoDetalhes no CarrinhoDtocompras existente.
                       c.CarrinhoInfoId == carrinhoInfoBd.CarrinhoInfoId);

                    if (carrinhoDetalhesDaBd == null)                                               //Se nenhum CarrinhoDetalhes existente for encontrado
                    {
                        //Criar um novo carrinhoDetalhes
                        carrinhoDto.CarrinhoDetalhes.First().CarrinhoInfoId = carrinhoInfoBd.CarrinhoInfoId; //o CarrinhoInfoId do primeiro CarrinhoDetalhes no CarrinhoDto é definido para o CarrinhoInfoId do CarrinhoInfo existente.
                        _db.CarrinhoDetalhes.Add(_mapper.Map<CarrinhoDetalhes>(carrinhoDto.CarrinhoDetalhes.First())); //4.	Se nenhum CarrinhoDetalhes existente for encontrado, um novo CarrinhoDetalhes é criado, mapeado a partir do primeiro CarrinhoDetalhes no CarrinhoDto usando o AutoMapper,

                        await _db.SaveChangesAsync();
                    }
                    else    //Se um CarrinhoDetalhes existente for encontrado
                    {
                        // Quantidade, CarrinhoInfoId e CarrinhoDetalhesId do primeiro CarrinhoDetalhes no CarrinhoDto são atualizados com os valores do CarrinhoDetalhes existente.
                        carrinhoDto.CarrinhoDetalhes.First().Quantidade += carrinhoDetalhesDaBd.Quantidade;
                        carrinhoDto.CarrinhoDetalhes.First().CarrinhoInfoId = carrinhoDetalhesDaBd.CarrinhoInfoId;
                        carrinhoDto.CarrinhoDetalhes.First().CarrinhoDetalhesId = carrinhoDetalhesDaBd.CarrinhoDetalhesId;

                        _db.CarrinhoDetalhes.Update(_mapper.Map<CarrinhoDetalhes>(carrinhoDto.CarrinhoDetalhes.First())); //O CarrinhoDetalhes existente é atualizado com os valores do primeiro CarrinhoDetalhes no CarrinhoDto, usando o AutoMapper.

                        await _db.SaveChangesAsync();
                    }
                }
                _response.Resultado = carrinhoDto; //O CarrinhoDto é definido como o resultado da operação.
            }
            catch (Exception ex)
            {
                _response.Mensagem = ex.Message.ToString();
                _response.Sucesso = false;
            }

            return _response; //O objeto ResponseDto é retornado.
        }
    }
}