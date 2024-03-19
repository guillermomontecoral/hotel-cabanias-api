using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto;
using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Mantenimiento;
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
    public class MantenimientoController : ControllerBase
    {
        #region Dependencias inyectadas
        private IAddMantenimiento _cuAddMantenimiento;
        private IGetAllMantenimientos _cuGetAllMantenimientos;
        private IFindAllMantenimientosCabanhas _cuFindAllMantenimientosCabanhas;
        private IGetByIdMantenimiento _cuGetByIdMantenimiento;
        private IMantenimientoEntreFechas _cuMantenimientoEntreFechas;
        private IUpdateMantenimiento _cuUpdateMantenimiento;
        private IDeleteMantenimiento _cuDeleteMantenimiento;

        public MantenimientoController(
            IAddMantenimiento cuAddMantenimiento, 
            IGetAllMantenimientos cuGetAllMantenimientos,
            IFindAllMantenimientosCabanhas cuFindAllMantenimientosCabanhas,
            IGetByIdMantenimiento cuGetByIdMantenimiento,
            IMantenimientoEntreFechas cuMantenimientoEntreFechas,
            IUpdateMantenimiento updateMantenimiento,
            IDeleteMantenimiento deleteMantenimiento )
        {
            _cuAddMantenimiento = cuAddMantenimiento;
            _cuGetAllMantenimientos = cuGetAllMantenimientos;
            _cuFindAllMantenimientosCabanhas = cuFindAllMantenimientosCabanhas;
            _cuGetByIdMantenimiento = cuGetByIdMantenimiento;
            _cuMantenimientoEntreFechas = cuMantenimientoEntreFechas;
            _cuUpdateMantenimiento = updateMantenimiento;
            _cuDeleteMantenimiento = deleteMantenimiento;
        }
        #endregion
        // GET: api/<MantenimientoController>
        /// <summary>
        /// Retorna todos los mantenimientos registrados
        /// </summary>
        /// <returns>Una lista con todos los mantenimientos. La lista vacía si no hay mantenimientos.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MantenimientoDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<MantenimientoDto>> Get()
        {
            try
            {
                var mants = _cuGetAllMantenimientos.GetAll();

                return Ok(mants);
            }
            catch (MantenimientoException ex)
            {

                return NotFound(ex.Message);
                //return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<MantenimientoController>/5
        /// <summary>
        /// Retorna un mantenimiento dado su id
        /// </summary>
        /// <param name="id">Identificador del mantenimiento</param>
        /// <returns>Mantenimiento encontrado,de lo contrario si no existe un mantenimiento con ese identificador, se le muestra un mensaje</returns>
        [HttpGet("{id}", Name = "GetByIdMantenimiento")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MantenimientoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<MantenimientoDto> Get(int? id)
        {

            try
            {
                if (id == null)
                    return BadRequest("El id no puede ser nulo.");

                var cabanha = _cuGetByIdMantenimiento.GetdById(id);
                return Ok(cabanha);
            }
            catch (MantenimientoException ex)
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

        /// <summary>
        /// Retorna los mantenimientos que tiene una cabaña
        /// </summary>
        /// <param name="idCabanha">Identificador de la cabaña</param>
        /// <returns>Mantenimiento encontrado,de lo contrario si no existe un mantenimiento con ese identificador, se le muestra un mensaje</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MantenimientoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("mantCabanhas/{idCabanha}")]
        public ActionResult<MantenimientoDto> GetMantCabanhas(int? idCabanha)
        {

            try
            {
                if (idCabanha == null)
                    return BadRequest("El id no puede ser nulo.");

                var cabanha = _cuFindAllMantenimientosCabanhas.GetByIdCabanha(idCabanha);
                return Ok(cabanha);
            }
            catch (MantenimientoException ex)
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

        /// <summary>
        /// Retorna los mantenimientos que tiene una cabaña entre las dos fechas indicadas
        /// </summary>
        /// <param name="f1">Fecha desde donde buscar el mantenimiento</param>
        /// <param name="f2">Fecha hasta donde buscar mantenimiento</param>
        /// <param name="idCab">Identificador de la cabaña para filtrar los mantenimientos</param>
        /// <returns>Mantenimiento encontrado,de lo contrario si no existe un mantenimiento con ese identificador, se le muestra un mensaje</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MantenimientoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("mantenimientosPorFecha/fecha1/{f1}/fecha2/{f2}/cabanha/{idCab}")]
        public ActionResult<MantenimientoDto> GetMantPorFecha(DateTime? f1, DateTime? f2, int? idCab)
        {

            try
            {
                if (f1 == null || f2 == null)
                    return BadRequest("El las fechas no pueden ser nulas.");

                if(idCab == null)
                    return BadRequest("El id de la cabaña no puede ser null.");

                var cabanha = _cuMantenimientoEntreFechas.MostrarLosMantenimientosEntreFechas(f1, f2, idCab);
                return Ok(cabanha);
            }
            catch (MantenimientoException ex)
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

        // POST api/<MantenimientoController>
        /// <summary>
        /// Registra un nuevo mantenimiento
        /// </summary>
        /// <param name="mantDto">Objeto que incluye toda la información para crear el mantenimiento.</param>
        /// <returns>
        /// Si el alta es exitosa, incluye la url para poder acceder al mantenimiento y 
        /// en el body los datos del mantenimiento.
        /// </returns>
        /// <response code="201">Retorna el mantenimiento incluyendo el id que le fue asignado.</response>
        /// <response code="401">Si no esta autorizado.</response>
        /// <response code="404">El mantenimiento es nul o hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MantenimientoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<MantenimientoDto> Post([FromBody] MantenimientoDto mantDto)
        {
            if (mantDto == null)
                return BadRequest("Los datos del tipo de cabaña no pueden estar nulos.");

            try
            {
                _cuAddMantenimiento.Add(mantDto);
                return CreatedAtRoute("GetByIdMantenimiento", new { id = mantDto.Id }, mantDto);
            }
            catch (MantenimientoException ex)
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

        // PUT api/<MantenimientoController>/5
        /// <summary>
        /// Permite modifciar un montenimiento
        /// </summary>
        /// <param name="id">Identificador del mantenimiento</param>
        /// <param name="mant">Contiene los datos modificados del mantenimiento</param>
        /// <returns>Retorna los datos modificados</returns>
        /// <response code="401">Si no esta autorizado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<MantenimientoDto> Put(int? id, [FromBody] MantenimientoDto mant)
        {
            if (id == null)
                return BadRequest("El id no puede ser nulo.");
            if (mant == null)
                return BadRequest("Los datos del tipo de vabaña no pueden ser nulos.");

            try
            {

                _cuUpdateMantenimiento.Update(mant);
                return Ok(mant);
            }
            catch (MantenimientoException ex)
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

        // DELETE api/<MantenimientoController>/5
        /// <summary>
        /// Elimina un mantenimiento
        /// </summary>
        /// <param name="id">Identificador para eliminar el mantenimiento</param>
        /// <response code="401">Si no esta autorizado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest("Debe ingresar un id de un mantenimiento para poder eliminarlo.");

            try
            {
                _cuDeleteMantenimiento.Delete(id);
                return NoContent();
            }
            catch (CabanhaException ex)
            {
                return BadRequest($"Error {ex.Message}");
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
