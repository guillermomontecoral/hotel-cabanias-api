using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto;
using Hotel.LogicaNegocio.Entidades.ValueObjects.Mantenimiento;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hotel.LogicaAplicacion.Dtos.MapeosDtos
{
    public class MapearMantenimiento
    {
        internal static MantenimientoDto ToDto(Mantenimiento mant)
        {
            return new MantenimientoDto()
            {
                Id = mant.Id,
                Descripcion = mant.Descripcion.Descripcion,
                RealizadoPor = mant.RealizadoPor.Nombre,
                Fecha = mant.Fecha.Fecha,
                CostoMantenimiento = mant.CostoMantenimiento,
                IdCabanha = mant.IdCabanha
            };
        }

        internal static Mantenimiento FromDto(MantenimientoDto mantDto)
        {
            return new Mantenimiento()
            {
                Id = mantDto.Id,
                Descripcion = new DescripcionMantenimiento(mantDto.Descripcion),
                RealizadoPor = new RealizadoPorMantenimiento(mantDto.RealizadoPor),
                Fecha = new FechaMantenimiento(mantDto.Fecha),
                CostoMantenimiento = mantDto.CostoMantenimiento,
                IdCabanha = mantDto.IdCabanha
            };
        }

        internal static IEnumerable<MantenimientoDto> ToListMantenimientoDto(IEnumerable<Mantenimiento> mants)
        {
            var mantsDto = mants.Select(m => ToDto(m)).ToList();

            return mantsDto;
        }
    }
}
