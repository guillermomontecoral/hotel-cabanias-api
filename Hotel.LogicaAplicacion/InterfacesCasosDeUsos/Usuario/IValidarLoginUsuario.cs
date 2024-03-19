using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Usuario
{
    public interface IValidarLoginUsuario
    {
        void ValidarLogin(string email, string clave);
    }
}
