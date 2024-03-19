using Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto;
using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.TopesDescripcion_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TopesDescripcion;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TopesDescripcionController : ControllerBase
    {
        #region Dependencias inyectadas
        private IAddTopesDescripcion _cuAddTopesDescripcion;
        private IGetAllTopesDescripcion _cuGetAllTopesDescripcion;
        private IUpdateTopesDescripcion _cuUpdateTopesDescripcion;
        public TopesDescripcionController(
            IAddTopesDescripcion addTopesDescripcion,
            IGetAllTopesDescripcion getAllTopesDescripcion, 
            IUpdateTopesDescripcion updateTopesDescripcion
            )
        {
            _cuAddTopesDescripcion = addTopesDescripcion;
            _cuGetAllTopesDescripcion = getAllTopesDescripcion;
            _cuUpdateTopesDescripcion = updateTopesDescripcion;
        }
        #endregion

        // GET: api/<TopesDescripcionController>
        /// <summary>
        /// Devuelve los topes permitidos para la descripcion de caba tipo de objeto
        /// </summary>
        /// <returns>Devuelve los topes actuales</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TopesDescripcionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<TopesDescripcionDto>> Get()
        {
            try
            {
                var topes = _cuGetAllTopesDescripcion.GetAll();


                return Ok(topes);
            }
            catch (TopeDescripcionException ex)
            {

                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<TopesDescripcionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TopesDescripcionController>
        /// <summary>
        /// Crea un nuevo tope de descripción
        /// </summary>
        /// <remarks>
        /// 
        ///     En nombreObj incluir:
        ///      Para Cabaña = cab
        ///      Para Tipo Cabaña = tc
        ///      Para Mantenimiento = man
        /// <code>
        ///     Ejemplo de solicitud:
        ///     POST api/cabanha
        ///     Incluir en el body:
        ///     }
        ///         "id": 0,
        ///         "nombreObj": "string",
        ///         "topeMin": 0,
        ///         "topeMax": 0
        ///     }
        ///      </code>
        /// </remarks>
        /// <param name="tope"></param>
        /// <response code="401">Si no esta autorizado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TopesDescripcionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<TopesDescripcionDto> Post([FromBody] TopesDescripcionDto tope)
        {
            if (tope == null)
                return BadRequest("Los datos del tipo de cabaña no pueden estar nulos.");

            try
            {
                _cuAddTopesDescripcion.Add(tope);
                return CreatedAtRoute("GetByIdMantenimiento", new { id = tope.Id }, tope);
            }
            catch (TopeDescripcionException ex)
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

        // PUT api/<TopesDescripcionController>/5
        /// <summary>
        /// Edita los limites de caracteres de la descripcion
        /// </summary>
        /// <param name="id">Identificador del tope</param>
        /// <param name="tope">Datos a editar, tope minimo y tope maximo</param>
        /// <response code="401">Si no esta autorizado.</response>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TopesDescripcionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Authorize]
        public ActionResult<TopesDescripcionDto> Put(int? id, [FromBody] TopesDescripcionDto tope)
        {
            if (id == null)
                return BadRequest("El id no puede ser nulo.");
            if (tope == null)
                return BadRequest("Los datos no pueden ser nulos.");

            if (id <= 0)
                return BadRequest("Debe ingresar un id mayor a 0");


            try
            {
                _cuUpdateTopesDescripcion.Update(id, tope);
                return Ok(tope);
            }
            catch (TopeDescripcionException ex)
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
