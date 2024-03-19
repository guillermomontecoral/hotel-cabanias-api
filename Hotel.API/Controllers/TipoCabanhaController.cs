using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Cabanha;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TipoCabanha;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TopesDescripcion;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TipoCabanhaController : ControllerBase
    {
        #region Dependencias inyectadas
        private IAddTipoCabanha _cuAddTipoCabahna;
        private IDeleteTipoCabanha _cuDeleteTipoCabanha;
        private IGetAllTipoCabanha _cuGetAllTipoCabanha;
        private IGetByIdTipoCabanha _cuGetByIdTipoCabanha;
        private IUpdateTipoCabanha _cuUpdateTipoCabanha;
        private IBuscarPorNombreTipoCabanha _cuBuscarPorNombreTipoCabanha;

        //Casos de uso de Cabaña
        private IBuscarPorTipoCabanha _cuBuscarPorTipoCabanha;

        public TipoCabanhaController(
            IAddTipoCabanha cuAddTipoCabahna, 
            IDeleteTipoCabanha cuDeleteTipoCabanha, 
            IGetAllTipoCabanha cuGetAllTipoCabanha, 
            IGetByIdTipoCabanha cuGetByIdTipoCabanha, 
            IUpdateTipoCabanha cuUpdateTipoCabanha,
            IBuscarPorNombreTipoCabanha buscarPorNombreTipoCabanha,
            IBuscarPorTipoCabanha buscarPorTipoCabanha)
        {
            _cuAddTipoCabahna = cuAddTipoCabahna;
            _cuDeleteTipoCabanha = cuDeleteTipoCabanha;
            _cuGetAllTipoCabanha = cuGetAllTipoCabanha;
            _cuGetByIdTipoCabanha = cuGetByIdTipoCabanha;
            _cuUpdateTipoCabanha = cuUpdateTipoCabanha;
            _cuBuscarPorNombreTipoCabanha = buscarPorNombreTipoCabanha;
            _cuBuscarPorTipoCabanha = buscarPorTipoCabanha;
        }
        #endregion


        #region Operaciones CRUD
        // GET: api/<TipoCabanhaController>
        /// <summary>
        /// Retorna todos los tipos de cabañas registrados.
        /// </summary>
        /// <returns>Una lista con todos los tipos de cabaña. La lista vacía si no hay tipos de cabañas.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TipoCabanhaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TipoCabanhaDto>>Get()
        {
            try
            {
                var tipoCabanhas = _cuGetAllTipoCabanha.GetAll();

                if (tipoCabanhas == null)
                    return NotFound();

                return Ok(tipoCabanhas);
            }
            catch (TipoCabanhaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<TipoCabanhaController>/5
        /// <summary>
        /// Retorna un tipo de cabaña dado su ID
        /// </summary>
        /// <param name="id">Identificador del tipo de cabaña</param>
        /// <returns>El tipo de cabaña encontrado de lo contrario null si no existe un tipo de cabaña con ese identificador</returns>
        [HttpGet("{id}", Name="GetByIdTipoCabanha")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoCabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TipoCabanhaDto> Get(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest("El id no puede ser nulo.");

                var tipoCabanha = _cuGetByIdTipoCabanha.GetById(id);                
                return Ok(tipoCabanha);
            }
            catch(TipoCabanhaException ex) 
            {
                return NotFound(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<TipoCabanhaController>
        /// <summary>
        /// Registra un nuevo tipo de cabaña. El nombre debe ser unico
        /// </summary>
        /// <param name="tipoCabanhaDto">Objeto que incluye toda la información para crear un tipo de cabaña.</param>
        /// <returns>
        /// Si el alta es exitosa, incluye la url para poder acceder al tipo de cabaña y 
        /// en el body los datos del tipo de cabaña.
        /// </returns>
        /// <response code="201">Retorna el tipo de cabaña, incluyendo el id que le fue asignado.</response>
        /// <response code="401">Si no esta autorizado.</response>
        /// <response code="404">El tipo de cabaña es nulo o hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TipoCabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<TipoCabanhaDto> Post([FromBody] TipoCabanhaDto tipoCabanhaDto)
        {
            if (tipoCabanhaDto == null)
                return BadRequest("Los datos del tipo de cabaña no pueden estar nulos.");

            try
            {
                _cuAddTipoCabahna.Add(tipoCabanhaDto);
                return CreatedAtRoute("GetByIdTipoCabanha", new { id = tipoCabanhaDto.Id}, tipoCabanhaDto);
            }
            catch(TipoCabanhaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
                //return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<TipoCabanhaController>/5
        /// <summary>
        /// Modifica la descripción y el costo por huesped del tipo de cabaña dado su ID.
        /// </summary>
        /// <param name="id">Identificador del tipo de cabaña a modificar.</param>
        /// <param name="tipoCabanhaDto">Objeto que guarda toda la informacion a modificar.</param>
        /// <returns>Muestras los datos del tipo de cabaña en caso de exito y su codigo de estado</returns>
        /// <response code="200">Retorna el tipo de cabaña, incluyendo el id que le fue asignado.</response>
        /// <response code="404">El tipo de cabaña o id es nulo. Hay errores en los datos.</response> 
        /// <response code="401">Si no esta autorizado.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoCabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<TipoCabanhaDto> PutDescripcionCostoHuesped(int? id, [FromBody] TipoCabanhaEditarDto tipoCabanhaDto)
        {
            if (id == null)
                return BadRequest("El id no puede ser nulo.");
            if (tipoCabanhaDto == null)
                return BadRequest("Los datos del tipo de vabaña no pueden ser nulos.");

            if (id <= 0)
                return BadRequest("Debe ingresar un id mayor a 0");


            try
            {
                _cuUpdateTipoCabanha.Update(id, tipoCabanhaDto);
                return Ok(tipoCabanhaDto);
            }
            catch (TipoCabanhaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<TipoCabanhaController>/5
        /// <summary>
        /// Permite eliminar un tipo de cabaña identificado por su id
        /// </summary>
        /// <param name="id">Identificador del tipo de cabaña a eliminar</param>
        /// <returns>Retona indicando que el objeto no existe,</returns>        
        /// <response code="204">En caso de eliminarlo con exito</response>
        /// <response code="404">Si el tipo de cabaña o id es nulo. Hay errores en los datos.</response>
        /// <response code="401">Si no esta autorizado.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if(id == null)
                return BadRequest("Debe ingresar un id de un tipo de cabaña para poder eliminarlo.");

            try
            {
                var existeRelacion = _cuBuscarPorTipoCabanha.BuscarPorTipo(id);
                if (existeRelacion.Count() > 0)
                    throw new TipoCabanhaException("No se puede eliminar, está asociado con alguna cabaña.");


                _cuDeleteTipoCabanha.Delete(id);
                return NoContent();
            }
            catch (TipoCabanhaException ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500,ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Busca un tipo de cabaña por su nombre
        /// </summary>
        /// <param name="nombre">Parametro que se utilizara para buscar el tipo de cabaña</param>
        /// <returns>Se mostraran los datos del tipo de cabaña con ese nombre</returns>      
        /// <response code="204">En caso de eliminarlo con exito</response>
        /// <response code="400">Si el nombre del tipo de cabaña es nulo. Hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoCabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("nombre/{nombre}")]
        public ActionResult<TipoCabanhaDto> GetByNombre(string nombre)
        {
            try
            {
                if (nombre == null)
                    return BadRequest("El nombre no puede ser nulo.");

                var tipoCabanha = _cuBuscarPorNombreTipoCabanha.BuscarPorNombre(nombre);
                return Ok(tipoCabanha);
            }
            catch (TipoCabanhaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
