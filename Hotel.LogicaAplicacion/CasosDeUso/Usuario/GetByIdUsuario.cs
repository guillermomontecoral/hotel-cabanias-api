using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.Dtos.Usuario_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Usuario;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.CasosDeUso.Usuario
{
    public class GetByIdUsuario : IGetByIdUsuario
    {
        #region Dependencias inyectadas
        IRepositorioUsuario _repoUsuario;

        public GetByIdUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }
        #endregion
        public UsuarioDto FindById(int? id)
        {
            if (id == null)
                throw new ArgumentNullException("El id no puede ser nulo.");

            var usuario = _repoUsuario.FindById(id);
            var usuarioDto = MapearUsuario.ToDto(usuario);
            return usuarioDto;
        }
    }
}
