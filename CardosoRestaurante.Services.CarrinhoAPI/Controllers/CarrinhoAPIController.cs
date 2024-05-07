using AutoMapper;
using CardosoRestaurante.MessageBus;
using CardosoRestaurante.Services.Carrinho.Data;
using CardosoRestaurante.Services.Carrinho.Models.Dto;
using CardosoRestaurante.Services.Carrinho.Models.DTO;
using CardosoRestaurante.Services.CarrinhoAPI.Models;
using CardosoRestaurante.Services.CarrinhoAPI.Models.Dto;
using CardosoRestaurante.Services.CarrinhoAPI.Service.IService;
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
        private IProdutoService _produtoService;
        private ICupaoService _cupaoService;
        private readonly IMessageBus _messageBus; //Vem do ServiceBus
        private IConfiguration _configuration;

        public CarrinhoAPIController(AppDbContext db, IMapper mapper, IProdutoService produtoService, ICupaoService cupaoService, IMessageBus messageBus, IConfiguration configuration)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
            _produtoService = produtoService;
            _cupaoService = cupaoService;
            _messageBus = messageBus;
            _configuration = configuration;
        }

        [HttpGet("GetCarrinho/{userId}")]
        public async Task<ResponseDto> GetCarrinho(string userId)
        {
            try
            {
                CarrinhoDto carrinhoDto = new() //Cria um novo objeto CarrinhoDto
                {
                    CarrinhoInfo = _mapper.Map<CarrinhoInfoDto>(_db.CarrinhoInfos.First(c => c.UserId == userId)) //Mapeia o CarrinhoInfo do banco de dados para um CarrinhoInfoDto e o define como o CarrinhoInfo do CarrinhoDto.
                };

                carrinhoDto.CarrinhoDetalhes = _mapper.Map<IEnumerable<CarrinhoDetalhesDto>>(_db.CarrinhoDetalhes.Where(c => c.CarrinhoInfoId == carrinhoDto.CarrinhoInfo.CarrinhoInfoId)); //Mapa os CarrinhoDetalhes do banco de dados para uma lista de CarrinhoDetalhesDto e define a lista como o CarrinhoDetalhes do CarrinhoDto.

                IEnumerable<ProdutoDto> listaProdutos = await _produtoService.GetProdutos(); //Obtém a lista de produtos da API de produtos.

                foreach (var item in carrinhoDto.CarrinhoDetalhes) //Para cada CarrinhoDetalhes no CarrinhoDetalhes do CarrinhoDto
                {
                    item.produtoDto = listaProdutos.FirstOrDefault(p => p.ProdutoId == item.ProdutoId); //O ProdutoDto do CarrinhoDetalhes é definido como o primeiro ProdutoDto na lista de produtos que corresponde ao ProdutoId do CarrinhoDetalhes.
                    carrinhoDto.CarrinhoInfo.CarrinhoTotal += (item.Quantidade * item.produtoDto.Preco); //O valor total do carrinho é calculado multiplicando a quantidade de cada produto pelo preço do produto e adicionando o resultado ao valor total do carrinho.
                }

                //Verificar se o carrinho tem um cupão
                if (!string.IsNullOrEmpty(carrinhoDto.CarrinhoInfo.CupaoCodigo))
                {
                    CupaoDto cupao = await _cupaoService.GetCupao(carrinhoDto.CarrinhoInfo.CupaoCodigo); //Obtém o cupão da API de cupões

                    if (cupao != null && carrinhoDto.CarrinhoInfo.CarrinhoTotal > cupao.ValorMinimo)
                    {
                        carrinhoDto.CarrinhoInfo.CarrinhoTotal -= cupao.Desconto; //Subtrai o valor do desconto do valor total do carrinho
                        carrinhoDto.CarrinhoInfo.Desconto = cupao.Desconto; //Define o desconto do carrinho como o valor do desconto do cupão

                        _response.Mensagem = "Cupão aplicado com sucesso";
                    }
                }

                _response.Resultado = carrinhoDto; //O CarrinhoDto é definido como o resultado da operação.
            }
            catch (Exception ex)
            {
                _response.Sucesso = false;
                _response.Mensagem = ex.Message;
            }
            return _response;
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

        [HttpPost("RemoverCarrinho")]
        public async Task<ResponseDto> RemoverCarrinho([FromBody] int carrinhoDetalhesId)
        {
            try
            {
                CarrinhoDetalhes carrinhoDetalhes = _db.CarrinhoDetalhes.First(c => c.CarrinhoDetalhesId == carrinhoDetalhesId); //Encontrar o CarrinhoDetalhes no banco de dados que corresponda ao carrinhoDetalhesId fornecido.

                int totalQuantidadeDoCarrinho = _db.CarrinhoDetalhes.Where(c => c.CarrinhoInfoId == carrinhoDetalhes.CarrinhoInfoId).Count(); //Ver quantos CarrinhoDetalhes existem no banco de dados que correspondem ao CarrinhoInfoId do CarrinhoDetalhes encontrado.

                _db.CarrinhoDetalhes.Remove(carrinhoDetalhes); //Remover o CarrinhoDetalhes do contexto do banco de dados

                if (totalQuantidadeDoCarrinho == 1) //Se houver apenas um CarrinhoDetalhes
                {
                    var carrinhoInfoARemover = await _db.CarrinhoInfos
                        .FirstOrDefaultAsync(c => c.CarrinhoInfoId == carrinhoDetalhes.CarrinhoInfoId); //Encontrar o CarrinhoInfo no banco de dados que corresponda ao CarrinhoInfoId do CarrinhoDetalhes encontrado.

                    _db.CarrinhoInfos.Remove(carrinhoInfoARemover); //Remover o CarrinhoInfo do contexto do banco de dados
                }

                await _db.SaveChangesAsync(); //Salvar as alterações no banco de dados

                _response.Resultado = true; //O resultado da operação é definido como verdadeiro se a operação for bem-sucedida.
            }
            catch (Exception ex)
            {
                _response.Mensagem = ex.Message.ToString();
                _response.Sucesso = false;
            }

            return _response; //O objeto ResponseDto é retornado.
        }

        [HttpPost("AplicaCupao")]
        public async Task<object> AplicaCupao([FromBody] CarrinhoDto carrinhoDto)
        {
            try
            {
                var carrinhoBaseDados = await _db.CarrinhoInfos.FirstAsync(c => c.UserId == carrinhoDto.CarrinhoInfo.UserId); //Encontrar o CarrinhoInfo no banco de dados que corresponda ao UserId do CarrinhoInfo no CarrinhoDto.
                carrinhoBaseDados.CupaoCodigo = carrinhoDto.CarrinhoInfo.CupaoCodigo; //Definir o CupaoCodigo do CarrinhoInfo no banco de dados para o CupaoCodigo do CarrinhoInfo no CarrinhoDto.
                _db.CarrinhoInfos.Update(carrinhoBaseDados); //Atualizar o CarrinhoInfo no banco de dados com o novo CupaoCodigo.
                await _db.SaveChangesAsync();
                _response.Resultado = true; //Definir o sucesso da operação como verdadeiro.
            }
            catch (Exception ex)
            {
                _response.Mensagem = ex.ToString();
                _response.Sucesso = false;
            }
            return _response;
        }

        [HttpPost("RemoveCupao")]
        public async Task<object> RemoveCupao([FromBody] CarrinhoDto carrinhoDto)
        {
            try
            {
                var carrinhoBaseDados = await _db.CarrinhoInfos.FirstAsync(c => c.UserId == carrinhoDto.CarrinhoInfo.UserId); //Encontrar o CarrinhoInfo no banco de dados que corresponda ao UserId do CarrinhoInfo no CarrinhoDto.
                carrinhoBaseDados.CupaoCodigo = "";
                _db.CarrinhoInfos.Update(carrinhoBaseDados); //Atualizar o CarrinhoInfo no banco de dados com o novo CupaoCodigo.
                await _db.SaveChangesAsync();
                _response.Resultado = true; //Definir o sucesso da operação como verdadeiro.
            }
            catch (Exception ex)
            {
                _response.Mensagem = ex.ToString();
                _response.Sucesso = false;
            }
            return _response;
        }

        [HttpPost("EmailCarrinhoRequest")]
        public async Task<object> EmailCarrinhoRequest([FromBody] CarrinhoDto carrinhoDto)
        {
            try
            {
                await _messageBus.PublishMessage(carrinhoDto, _configuration.GetValue<string>("TopicAndQueueNames:EmailCarrinho")); //Publicar uma mensagem contendo o CarrinhoDto no tópico ou fila especificado no arquivo de configuração que o método PublishMessage do IMessageBus espera.
                _response.Resultado = true;
            }
            catch (Exception ex)
            {
                _response.Mensagem = ex.ToString();
                _response.Sucesso = false;
            }
            return _response;
        }
    }
}