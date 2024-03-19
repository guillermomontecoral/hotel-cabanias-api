using Hotel.LogicaAplicacion.Dtos.TopesDescripcion_Dto;
using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.Entidades.ValueObjects.TopesDescripcion;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.MapeosDtos
{
    public class MapearTopesDescripcion
    {
        internal static TopesDescripcionDto ToDto(TopesDescripcion tipoCabanha)
        {
            return new TopesDescripcionDto()
            {
                Id = tipoCabanha.Id,
                NombreObj = tipoCabanha.NombreTope.Nombre,
                TopeMin = tipoCabanha.Rangos.Min,
                TopeMax = tipoCabanha.Rangos.Max
            };
        }

        internal static TopesDescripcion FromDto(TopesDescripcionDto topesDescripcionDto)
        {
            return new TopesDescripcion()
            {
                Id = topesDescripcionDto.Id,
                NombreTope = new NombreTopeDescripcion(topesDescripcionDto.NombreObj),
                Rangos = new RangoTopesDescripcion(topesDescripcionDto.TopeMin, topesDescripcionDto.TopeMax)
            };
        }


        internal static IEnumerable<TopesDescripcionDto> ToListTopesDto(IEnumerable<TopesDescripcion> topesDescripcion)
        {
            var topesDto = topesDescripcion.Select(t => ToDto(t)).ToList();

            return topesDto;
        }
    }
}
