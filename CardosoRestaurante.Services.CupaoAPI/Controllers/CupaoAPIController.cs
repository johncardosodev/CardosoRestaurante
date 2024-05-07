using AutoMapper;
using CardosoRestaurante.Services.CupaoAPI.Data;
using CardosoRestaurante.Services.CupaoAPI.Models;
using CardosoRestaurante.Services.CupaoAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CardosoRestaurante.Services.CupaoAPI.Controllers
{
    [Route("api/cupao")] //Este Route é o caminho para aceder ao controller (neste caso é api/Cupao)
    [ApiController] //Este atributo indica que a classe é um controller API
    [Authorize] //Este atributo indica que é necessário estar autenticado para aceder a este controller
    public class CupaoAPIController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response; //Este atribute é para devolver a resposta com o resultado API
        private IMapper _mapper; //Este atributo é do tipo IMapper

        public CupaoAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper; //O atributo _mapper é igual ao mapper
        }

        // GET: api/CupaoAPI que devolve todos os cupões
        [HttpGet] //Este atributo indica que o método é um GET
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Cupao> cupoes = _db.Cupoes.ToList();
                _response.Resultado = _mapper.Map<IEnumerable<CupaoDto>>(cupoes); //Mapear a lista de cupões para uma lista de cupões DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //Get: api/CupaoAPI/5 que devolve um cupão pelo id
        [HttpGet("{id:int}")] //Este atributo indica que o método é um GET e que recebe um parâmetro. Tem que ser igual ao nome do parâmetro
        public ResponseDto Get(int id)
        {
            try
            {
                Cupao cupao = _db.Cupoes.First(p => p.CupaoId == id);

                _response.Resultado = _mapper.Map<CupaoDto>(cupao); //Mapear o cupão para um cupão DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //Get: api/CupaoAPI/GetCupaoPeloCodigo/ que devolve um cupão pelo código
        [HttpGet]
        [Route("GetCupaoPeloCodigo/{codigo}")]
        public ResponseDto GetCupaoPeloCodigo(string codigo)
        {
            try
            {
                Cupao cupao = _db.Cupoes.First(p => p.CupaoCodigo.ToLower() == codigo.ToLower());

                _response.Resultado = _mapper.Map<CupaoDto>(cupao); //Mapear o cupão para um cupão DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //POST: api/CupaoAPI que adiciona um novo cupão
        [HttpPost] //Este atributo indica que o método é um POST
        [Authorize(Roles = "ADMINISTRADOR")] //Este atributo indica que é necessário estar autenticado e ter a role Admin para aceder a este método
        public ResponseDto Post([FromBody] CupaoDto cupaoDto) //FromBody indica que o parâmetro vem do corpo do pedido
        {
            try
            {
                Cupao cupao = _mapper.Map<Cupao>(cupaoDto); //Mapear o cupão DTO para um cupão
                _db.Cupoes.Add(cupao);
                _db.SaveChanges();
                _response.Resultado = _mapper.Map<CupaoDto>(cupao); //Mapear o cupão para um cupão DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //PUT: api/CupaoAPI que atualiza um cupão
        [HttpPut] //Este atributo indica que o método é um PUT
        [Authorize(Roles = "ADMINISTRADOR")] //Este atributo indica que é necessário estar autenticado e ter a role Admin para aceder a este método
        public ResponseDto Put([FromBody] CupaoDto cupaoDto) //FromBody indica que o parâmetro vem do corpo do pedido
        {
            try
            {
                Cupao cupao = _mapper.Map<Cupao>(cupaoDto); //Mapear o cupão DTO para um cupão
                _db.Cupoes.Update(cupao);
                _db.SaveChanges();
                _response.Resultado = _mapper.Map<CupaoDto>(cupao); //Mapear o cupão para um cupão DTO
            }
            catch (Exception ex)
            {
                _response.Sucesso = false; //Caso haja um erro, o atributo Sucesso é false
                _response.Mensagem = ex.Message; //Caso haja um erro, a mensagem é a mensagem de erro
            }
            return _response;
        }

        //Delete: api/CupaoAPI que elimina um cupão
        [HttpDelete] //Este atributo indica que o método é um DELETE
        [Route("{cupaoId:int}")]
        [Authorize(Roles = "ADMINISTRADOR")] //Este atributo indica que é necessário estar autenticado e ter a role Admin para aceder a este método
        public ResponseDto Delete(int cupaoId) //FromBody indica que o parâmetro vem do corpo do pedido
        {
            try
            {
                Cupao cupao = _db.Cupoes.First(c => c.CupaoId == cupaoId);
                _db.Cupoes.Remove(cupao);
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