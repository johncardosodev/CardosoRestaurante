using AutoMapper;

using CardosoRestaurante.Services.ProdutoAPI.Data;
using CardosoRestaurante.Services.ProdutoAPI.Models;
using CardosoRestaurante.Services.ProdutoAPI.Models.Dto;
using CardosoRestaurante.Services.ProdutoAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CardosoRestaurante.Services.ProdutoAPI.Controllers
{
    [Route("api/produto")] //Este Route é o caminho para aceder ao controller (neste caso é api/produto)
    [ApiController] //Este atributo indica que a classe é um controller API
    public class ProdutoAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response; //Este atribute é para devolver a resposta com o resultado API
        private IMapper _mapper; //Este atributo é do tipo IMapper

        public ProdutoAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper; //O atributo _mapper é igual ao mapper
        }

        // GET: api/ProdutoAPI que devolve todos os produtos
        [HttpGet] //Este atributo indica que o método é um GET
        [SwaggerOperation(Summary = "Obter todos os produtos", Description = "Este endpoint devolve todos os produtos")] //Este atributo é para documentar o endpoint
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Produto> produtos = _db.Produtos.ToList();
                _response.Resultado = _mapper.Map<IEnumerable<ProdutoDto>>(produtos); //Mapear a lista de produtos para uma lista de produtos DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //Get: api/ProdutoAPI/5 que devolve um produto pelo id
        [HttpGet("{id:int}")] //Este atributo indica que o método é um GET e que recebe um parâmetro. Tem que ser igual ao nome do parâmetro
        [SwaggerOperation(Summary = "Obter um produto", Description = "Este endpoint devolve um produto pelo id", OperationId = "GetProdutoById")] //Este atributo é para documentar o endpoint
        public ResponseDto GetProdutoById(int id)
        {
            try
            {
                Produto produto = _db.Produtos.First(p => p.ProdutoId == id);

                _response.Resultado = _mapper.Map<ProdutoDto>(produto); //Mapear o produto para um produto DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //Get: api/ProdutoAPI/GetProdutoPeloNome/ que devolve um produto pelo códigos
        [HttpGet]
        [Route("GetProdutoPeloNome/{nome}")]
        [SwaggerOperation(Summary = "Obter um produto pelo nome", Description = "Este endpoint devolve um produto pelo nome", OperationId = "GetProdutoPeloNome")] //Este atributo é para documentar o endpoint
        public ResponseDto GetProdutoPeloNome(string nome)
        {
            try
            {
                Produto produto = _db.Produtos.First(p => p.Nome.ToLower() == nome.ToLower());

                _response.Resultado = _mapper.Map<ProdutoDto>(produto); //Mapear o produto para um produto DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //POST: api/ProdutoAPI que adiciona um novo produto
        [HttpPost] //Este atributo indica que o método é um POST
        [SwaggerOperation(Summary = "Adicionar um novo produto", Description = "Este endpoint adiciona um novo produto"), Produces("application/json"), Consumes("application/json")] //Este atributo é para documentar o endpoint
        [Authorize(Roles = "ADMINISTRADOR")] //Este atributo indica que é necessário estar autenticado e ter a role Admin para aceder a este método
        public ResponseDto Post([FromBody] ProdutoDto produtoDto) //FromBody indica que o parâmetro vem do corpo do pedido
        {
            try
            {
                Produto produto = _mapper.Map<Produto>(produtoDto); //Mapear o produto DTO para um produto
                _db.Produtos.Add(produto);
                _db.SaveChanges();
                _response.Resultado = _mapper.Map<ProdutoDto>(produto); //Mapear o produto para um produto DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //PUT: api/ProdutoAPI que atualiza um produto
        [HttpPut] //Este atributo indica que o método é um PUT        //
        [SwaggerOperation(Summary = "Atualizar um produto", Description = "Este endpoint atualiza um produto", OperationId = "AtualizarProduto"), Produces("application/json"), Consumes("application/json")] //Este atributo é para documentar o endpoint
        [Authorize(Roles = "ADMINISTRADOR")] //Este atributo indica que é necessário estar autenticado e ter a role Admin para aceder a este método
        public ResponseDto Put([FromBody] ProdutoDto produtoDto) //FromBody indica que o parâmetro vem do corpo do pedido
        {
            try
            {
                Produto produto = _mapper.Map<Produto>(produtoDto); //Mapear o produto DTO para um produto
                _db.Produtos.Update(produto);
                _db.SaveChanges();
                _response.Resultado = _mapper.Map<ProdutoDto>(produto); //Mapear o produto para um produto DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //Delete: api/ProdutoAPI que elimina um produto
        [HttpDelete] //Este atributo indica que o método é um DELETE
        [Route("{produtoId:int}")]   //
        [SwaggerOperation(Summary = "Eliminar um produto", Description = "Este endpoint elimina um produto", OperationId = "EliminarProduto")] //Este atributo é para documentar o endpoint
        [Authorize(Roles = "ADMINISTRADOR")] //Este atributo indica que é necessário estar autenticado e ter a role Admin para aceder a este método
        public ResponseDto Delete(int produtoId) //FromBody indica que o parâmetro vem do corpo do pedido
        {
            try
            {
                Produto produto = _db.Produtos.First(c => c.ProdutoId == produtoId);
                _db.Produtos.Remove(produto);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }
    }
}