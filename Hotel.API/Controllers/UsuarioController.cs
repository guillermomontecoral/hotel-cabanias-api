using Hotel.LogicaAplicacion.Dtos.Usuario_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Cabanha;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Usuario;
using LogicaNegocio.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class UsuarioController : ControllerBase
    {
        

        #region Dependencias inyectadas
        private IAddUsuario _cuAddUsuario;
        private IValidarLoginUsuario _cuValidarLoginUsuario;
        public UsuarioController(
            IAddUsuario cuAddUsuario, 
            IValidarLoginUsuario cuValidarLoginUsuario)
        {
            _cuAddUsuario = cuAddUsuario;
            _cuValidarLoginUsuario = cuValidarLoginUsuario;
        }
        #endregion

        // GET: api/<UsuarioController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}", Name = "GetById")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsuarioController>
        /// <summary>
        /// Registrar un nuevo usuario
        /// </summary>
        /// <param name="usuarioDto">Datos para registrar al usuario</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UsuarioDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public ActionResult<UsuarioDto> Post([FromBody] UsuarioDto usuarioDto)
        {
            try
            {
                _cuAddUsuario.Add(usuarioDto);

                var claveDificil = "Obligatorio2023_PrograMacionM3B_ClaveSuperSecreta15";
                var claveDificilBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));
                var signinCredentials = new SigningCredentials(claveDificilBytes, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "identificadorEmisor",
                    audience: "identificadorAudiencia",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(100),
                    signingCredentials: signinCredentials
                );
                usuarioDto.Token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return CreatedAtRoute("GetById", new { id = usuarioDto.Id }, usuarioDto);
            }
            catch (DbUpdateException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login de usuario
        /// </summary>
        /// <param name="usuario">Datos para ingresar al sistema</param>
        /// <returns>Retorna el login del usario con su token</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<UsuarioDto> Login(UsuarioDto usuario)
        {

            try
            {
                _cuValidarLoginUsuario.ValidarLogin(usuario.Email, usuario.Clave);

                var claveDificil = "Obligatorio2023_PrograMacionM3B_ClaveSuperSecreta15";
                var claveDificilBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(claveDificil));
                var signinCredentials = new SigningCredentials(claveDificilBytes, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: "identificadorEmisor",
                    audience: "identificadorAudiencia",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(100),
                    signingCredentials: signinCredentials
                );
                usuario.Token = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
