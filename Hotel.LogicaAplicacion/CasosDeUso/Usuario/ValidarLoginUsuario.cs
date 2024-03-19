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
    public class ValidarLoginUsuario : IValidarLoginUsuario
    {
        #region Dependencias inyectadas
        IRepositorioUsuario _repoUsuario;

        public ValidarLoginUsuario(IRepositorioUsuario repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }
        #endregion

        public void ValidarLogin(string email, string clave)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(clave))
                throw new Exception("El email o la contraseña no pueden ser nulos");

            _repoUsuario.ValidarLogin(email, clave);
        }
    }
}
