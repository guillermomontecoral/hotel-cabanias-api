using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.Dtos.Usuario_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Usuario;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.CasosDeUso.Usuario
{
    public class AddUsuario : IAddUsuario
    {
        #region Dependencias inyectadas
        IRepositorioUsuario _repoUsuario;

        public AddUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }
        #endregion

        public void Add(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
                throw new Exception("No se puede dar de alta el usuario.");

            var nuevoUsuario = MapearUsuario.FromDto(usuarioDto);
            _repoUsuario.Add(nuevoUsuario);

            usuarioDto.Id = nuevoUsuario.Id;

        }
    }
}
