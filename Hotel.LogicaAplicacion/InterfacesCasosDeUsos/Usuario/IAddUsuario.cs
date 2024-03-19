using Hotel.LogicaAplicacion.Dtos.Usuario_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Usuario
{
    public interface IAddUsuario
    {
        void Add(UsuarioDto usuarioDto);
    }
}
