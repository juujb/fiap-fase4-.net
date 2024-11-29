using FIAP.IRRIGACAO.API.Model;
using FIAP.IRRIGACAO.API.Service;
using FIAP.IRRIGACAO.API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FIAP.IRRIGACAO.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Controller genérico para operações CRUD com suporte a paginação")]
    public class CrudController<TViewModel, TEntity, TRegisterViewModel> : ControllerBase
    where TEntity : BaseEntity
    where TViewModel : BaseViewModel
    where TRegisterViewModel : class
    {
        private readonly IGenericService<TViewModel, TEntity, TRegisterViewModel> _service;

        public CrudController(IGenericService<TViewModel, TEntity, TRegisterViewModel> service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getAll")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Obtém todos os registros",
            Description = "Retorna todos os registros da entidade sem paginação."
        )]
        [SwaggerResponse(200, "Lista de registros retornada com sucesso.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public IActionResult GetAll()
        {
            var items = _service.GetAll();
            return Ok(items);
        }

        [HttpGet("paged")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Obtém registros paginados",
            Description = "Retorna os registros da entidade com base nos parâmetros de paginação fornecidos."
        )]
        [SwaggerResponse(200, "Lista de registros paginados retornada com sucesso.")]
        [SwaggerResponse(400, "Os parâmetros de paginação são inválidos.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public IActionResult GetAllPaged(int pageNumber = 1, int pageSize = 10)
        {
            var items = _service.GetAllPaged(pageNumber, pageSize);

            var page = new PagedViewModel<TViewModel>() { 
                Data = items,
                CurrentPage = pageNumber,
                PageSize = pageSize 
            };

            return Ok(page);
        }

        [HttpGet("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Obtém um registro por ID",
            Description = "Retorna o registro correspondente ao ID fornecido."
        )]
        [SwaggerResponse(200, "Registro encontrado com sucesso.")]
        [SwaggerResponse(404, "Registro não encontrado.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public IActionResult GetById(long id)
        {
            var item = _service.FindById(id);
            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(
            Summary = "Cria um novo registro",
            Description = "Adiciona um novo registro com base no modelo fornecido."
        )]
        [SwaggerResponse(201, "Registro criado com sucesso.")]
        [SwaggerResponse(400, "Os dados fornecidos são inválidos.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public IActionResult Create([FromBody] TRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _service.Create(model);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Atualiza um registro existente",
            Description = "Atualiza o registro correspondente ao ID fornecido com os novos dados."
        )]
        [SwaggerResponse(204, "Registro atualizado com sucesso.")]
        [SwaggerResponse(400, "Os dados fornecidos são inválidos ou o ID não corresponde.")]
        [SwaggerResponse(404, "Registro não encontrado.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public IActionResult Update(long id, [FromBody] TRegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(
            Summary = "Exclui um registro",
            Description = "Remove o registro correspondente ao ID fornecido."
        )]
        [SwaggerResponse(200, "Registro excluído com sucesso.")]
        [SwaggerResponse(404, "Registro não encontrado.")]
        [SwaggerResponse(500, "Erro interno no servidor.")]
        public IActionResult Delete(long id)
        {
            var deletedItem = _service.DeleteAndReturn(id);
            if (deletedItem == null)
                return NotFound();

            return Ok(deletedItem);
        }
    }
}