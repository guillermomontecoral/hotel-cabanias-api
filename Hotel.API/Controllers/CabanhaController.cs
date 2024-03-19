using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Cabanha;
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
    public class CabanhaController : ControllerBase
    {
        #region Dependencias inyectadas
        private IAddCabanha _cuAddCabahna;
        private IDeleteCabanha _cuDeleteCabanha;
        private IGetAllCabanhas _cuGetAllCabanha;
        private IGetByIdCabanha _cuGetByIdCabanha;
        private IUpdateCabanha _cuUpdateCabanha;
        private IBuscarTextoEnNombreCabanha _cuBuscarTextoEnNombreCabanha;
        private IBuscarPorTipoCabanha _cuBuscarPorTipoCabanha;
        private IBuscarPorMaxPersonas _cuBuscarPorMaxPersonas;
        private IBuscarPorHabilitada _cuBuscarPorHabilitada;
        private IConsulta6A _cuConsuta6A;

        public CabanhaController(
            IAddCabanha cuAddCabahna,
            IDeleteCabanha cuDeleteCabanha,
            IGetAllCabanhas cuGetAllCabanha,
            IGetByIdCabanha cuGetByIdCabanha,
            IUpdateCabanha cuUpdateCabanha,
            IBuscarTextoEnNombreCabanha cuBuscarTextoEnNombreCabanha,
            IBuscarPorTipoCabanha cuBuscarPorTipoCabanha,
            IBuscarPorMaxPersonas cuBuscarPorMaxPersonas,
            IBuscarPorHabilitada cuBuscarPorHabilitada,
            IConsulta6A cuConsuta6A)
        {
            _cuAddCabahna = cuAddCabahna;
            _cuDeleteCabanha = cuDeleteCabanha;
            _cuGetAllCabanha = cuGetAllCabanha;
            _cuGetByIdCabanha = cuGetByIdCabanha;
            _cuUpdateCabanha = cuUpdateCabanha;
            _cuBuscarTextoEnNombreCabanha = cuBuscarTextoEnNombreCabanha;
            _cuBuscarPorTipoCabanha = cuBuscarPorTipoCabanha;
            _cuBuscarPorMaxPersonas = cuBuscarPorMaxPersonas;
            _cuBuscarPorHabilitada = cuBuscarPorHabilitada;
            _cuConsuta6A = cuConsuta6A;
        }
        #endregion

        #region Operaciones CRUD
        // GET: api/<CabanhaController>
        /// <summary>
        /// Retorna todas cabañas registradas.
        /// </summary>
        /// <returns>Una lista con todas las cabañas. La lista vacía si no hay cabañas.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabanhaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<CabanhaDto>> Get()
        {
            try
            {
                var cabanhas = _cuGetAllCabanha.GetAll();

                if (cabanhas == null)
                    return NotFound();

                return Ok(cabanhas);
            }
            catch (CabanhaException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<CabanhaController>/5 
        /// <summary>
        /// Retorna una cabaña dado su ID
        /// </summary>
        /// <param name="id">Identificador de lacabaña</param>
        /// <returns>La cabaña encontrada ,de lo contrario null si no existe una cabaña con ese identificador</returns>
        [HttpGet("{id}", Name = "GetByIdCabanha")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoCabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<CabanhaDto> Get(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest("El id no puede ser nulo.");

                var cabanha = _cuGetByIdCabanha.GetById(id);
                return Ok(cabanha);
            }
            catch (CabanhaException ex)
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

        // POST api/<CabanhaController>
        /// <summary>
        /// Registra una nueva cabaña. El nombre debe ser unico
        /// </summary>
        /// <param name="cabanhaDto">Objeto que incluye toda la información para crear una cabaña.</param>
        /// <returns>
        /// Si el alta es exitosa, incluye la url para poder acceder a la cabaña y 
        /// en el body los datos de la cabaña.
        /// </returns>
        /// <remarks>
        /// El ID de la cabaña debe estar en 0, ya que el mismo es un identity y se genera solo, de lo contrario dara error 409.
        /// El numero de habitacion debe ir en 0, por mas que se le ingrese un numero este es autonumerico asi que se le asigna el siguiente numero de la ultima cabaña registrada.
        /// <code>
        ///     Ejemplo de solicitud:
        ///     POST api/cabanha
        ///     Incluir en el body:
        ///     {
        ///         "id": 0,
        ///         "nombre": "nombre cabaña",
        ///         "descripcion": "descripcion cabaña",
        ///         "idTipoCabanha": 1,
        ///         "jacuzzi": true,
        ///         "habilitadaParaReservas": false,
        ///         "numHabitacion": 0,
        ///         "cantMaxPersonas": 6,
        ///         "nombreFoto": "nombreImg_001.png"
        ///      }
        ///      </code>
        /// </remarks>
        /// <response code="201">Retorna la cabaña incluyendo el id que le fue asignado.</response>
        /// <response code="404">La cabaña es nula o hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<CabanhaDto> Post([FromBody] CabanhaDto cabanhaDto)
        {
            if (cabanhaDto == null)
                return BadRequest("Los datos del tipo de cabaña no pueden estar nulos.");

            try
            {
                _cuAddCabahna.Add(cabanhaDto);
                return CreatedAtRoute("GetByIdCabanha", new { id = cabanhaDto.Id }, cabanhaDto);
            }
            catch (CabanhaException ex)
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

        // PUT api/<CabanhaController>/5
        /// <summary>
        /// Modifica los datos de la cabaña dado su ID.
        /// </summary>
        /// <param name="id">Identificador de la cabaña a modificar.</param>
        /// <param name="cabanhaDto">Objeto que guarda toda la informacion a modificar.</param>
        /// <returns>Muestras los datos de la cabaña en caso de exito y su codigo de estado</returns> 
        /// <remarks>
        /// El ID de la cabaña debe coincidir con el ID a buscar.
        /// El numero de habitacion debe ir en 0, por mas que se le ingrese un numero este es autonumerico asi que se le asigna el siguiente numero de la ultima cabaña registrada.
        /// <code>
        ///     Ejemplo de solicitud:
        ///     POST api/cabanha
        ///     Incluir en el body:
        ///     {
        ///         "id": 1,
        ///         "nombre": "nombre cabaña",
        ///         "descripcion": "descripcion cabaña",
        ///         "idTipoCabanha": 1,
        ///         "jacuzzi": true,
        ///         "habilitadaParaReservas": false,
        ///         "numHabitacion": 0,
        ///         "cantMaxPersonas": 6
        ///      }
        ///      </code>
        /// </remarks>
        /// <response code="200">Retorna la cabaña, incluyendo el id que le fue asignado.</response>
        /// <response code="401">Si no esta autorizado.</response>
        /// <response code="404">La cabaña o id es nulo. Hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CabanhaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public ActionResult<CabanhaDto> Put(int? id, [FromBody] CabanhaModificarDto cabanhaDto)
        {
            if (id == null)
                return BadRequest("El id no puede ser nulo.");
            if (cabanhaDto == null)
                return BadRequest("Los datos del tipo de vabaña no pueden ser nulos.");

            try
            {

                _cuUpdateCabanha.Update(id, cabanhaDto);
                return Ok(cabanhaDto);
            }
            catch (CabanhaException ex)
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

        // DELETE api/<CabanhaController>/5
        /// <summary>
        /// Permite eliminar una cabaña dado su ID.
        /// </summary>
        /// <param name="id">Identificador de la cabaña a eliminar</param>
        /// <returns>Retona indicando que el objeto no existe,</returns>        
        /// <response code="204">En caso de eliminarlo con exito</response> 
        /// <response code="401">Si no esta autorizado.</response>
        /// <response code="404">Si la cabaña o id es nulo. Hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return BadRequest("Debe ingresar un id de un tipo de cabaña para poder eliminarlo.");

            try
            {
                _cuDeleteCabanha.Delete(id);
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
        #endregion

        /// <summary>
        /// Busca las cabañas por un texto que exista en su nombre
        /// </summary>
        /// <param name="textoEnNombre">Parametro que se utilizara para buscar las cabañas</param>
        /// <returns>Se mostraran los datos de las cabaña con ese nombre</returns>      
        /// <response code="200">En caso de encontrar cabañas con ese texto en el nombre</response>
        /// <response code="401">Si no esta autorizado.</response>
        /// <response code="404">Si no hay resultados.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabanhaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("nombre/{textoEnNombre}")]
        public ActionResult<IEnumerable<CabanhaDto>> GetByNombre(string textoEnNombre)
        {
            try
            {
                if (textoEnNombre == null)
                    return BadRequest("El nombre no puede ser nulo.");

                var cabanhas = _cuBuscarTextoEnNombreCabanha.BuscarTextoEnNombre(textoEnNombre);
                return Ok(cabanhas);
            }
            catch (CabanhaException ex)
            {
                return NotFound(ex.Message);

                //return BadRequest(ex.Message);
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
        /// Busca las cabañas que esten registradas con un tipo de cabaña predeterminado
        /// </summary>
        /// <param name="idTipoCabanha">Parametro que se utilizara para buscar las cabañas</param>
        /// <returns>Se mostraran los datos de las cabaña con ese nombre</returns>      
        /// <response code="200">En caso de encontrar cabañas con ese tipo de cabaña</response>
        /// <response code="400">Si el id ingresado a buscar es nulo. Hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabanhaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("tipoCabanha/{idTipoCabanha}")]
        public ActionResult<IEnumerable<CabanhaDto>> GetByTiposCabanhas(int? idTipoCabanha)
        {
            try
            {
                if (idTipoCabanha == null)
                    return BadRequest("El id del tipo de cabaña no puede ser nulo.");

                var cabanhas = _cuBuscarPorTipoCabanha.BuscarPorTipo(idTipoCabanha);
                return Ok(cabanhas);
            }
            catch (CabanhaException ex)
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
        /// Busca las cabañas que esten registradas y permitan la cantidad de personas ingresadas.
        /// </summary>
        /// <param name="maxPersonas">Parametro que se utilizara para buscar las cabañas</param>
        /// <returns>Se muestran todas las cabañas que  permitan esa cantidad o más de personas.</returns>      
        /// <response code="200">En caso de encontrar cabañas con esa cantidad o superiror de personas</response>
        /// <response code="400">Si el parametro ingresado es nulo. Hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabanhaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("cantidadHuespedes/{maxPersonas}")]
        public ActionResult<IEnumerable<CabanhaDto>> GetByMaxPersonas(int? maxPersonas)
        {
            try
            {
                if (maxPersonas == null)
                    return BadRequest("El numero no puede ser nulo.");

                var cabanhas = _cuBuscarPorMaxPersonas.BuscarPorMaxPer(maxPersonas);
                return Ok(cabanhas);
            }
            catch (CabanhaException ex)
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
        /// Busca las cabañas que esten registradas y esten habilitadas para la reserva.
        /// </summary>
        /// <returns>Se muestran todas las cabañas habilitadas.</returns>      
        /// <response code="200">En caso de encontrar cabañas habilitadas</response>
        /// <response code="400">Si el parametro ingresado es nulo. Hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabanhaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("habilitadas")]
        public ActionResult<IEnumerable<CabanhaDto>> GetByHabilitadas()
        {
            try
            {
                var cabanhas = _cuBuscarPorHabilitada.BuscarPorHabilitada();
                return Ok(cabanhas);
            }
            catch (CabanhaException ex)
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
        /// Busca las cabañas que esten registradas, habilitadas para reservan, cuenten con jacuzzi y y su costo sea menor al ingresado como parametro
        /// </summary>
        /// <param name="monto">Parametro que se utilizara para buscar por el monto</param>
        /// <returns>Se muestran todas las cabañas habilitadas.</returns>      
        /// <response code="200">En caso de encontrar cabañas habilitadas</response>
        /// <response code="400">Si el parametro ingresado es nulo. Hay errores en los datos.</response>
        /// <response code="500">Error inesperado.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CabanhaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("mostrarPorMonto/{monto}")]
        public ActionResult<IEnumerable<CabanhaDto>> GetByConsulta6A(decimal? monto)
        {
            try
            {
                if (monto == null)
                    throw new CabanhaException("El monto no puede ser nulo.");

                var cabanhas = _cuConsuta6A.Consulta6_OblParteA(monto);
                return Ok(cabanhas);
            }
            catch (CabanhaException ex)
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
    }
}
