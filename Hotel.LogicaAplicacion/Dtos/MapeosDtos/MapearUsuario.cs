using Hotel.LogicaAplicacion.Dtos.Usuario_Dto;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.MapeosDtos
{
    public class MapearUsuario
    {
        internal static UsuarioDto ToDto(Usuario usu)
        {
            return new UsuarioDto()
            {
                Id = usu.Id,
                Email = usu.Email,
                Clave = usu.Clave
            };
        }

        internal static Usuario FromDto(UsuarioDto usuDto)
        {
            return new Usuario()
            {
                Id = usuDto.Id,
                Email = usuDto.Email,
                Clave = usuDto.Clave
            };
        }

        internal static IEnumerable<UsuarioDto> ToListUsuarioDto(IEnumerable<Usuario> usus)
        {
            var ususDto = usus.Select(m => ToDto(m)).ToList();

            return ususDto;
        }
    }
}
