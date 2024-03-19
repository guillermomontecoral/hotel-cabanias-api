using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using Hotel.LogicaNegocio.Entidades.ValueObjects.TipoCabanha;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.MapeosDtos
{
    public class MapearTipoCabanha
    {
        internal static TipoCabanhaDto ToDto(TipoCabanha tipoCabanha)
        {
            return new TipoCabanhaDto()
            {
                Id = tipoCabanha.Id,
                Nombre = tipoCabanha.Nombre.Value,
                Descripcion = tipoCabanha.Descripcion.Descripcion,
                CostoPorHuesped = tipoCabanha.CostoPorHuesped
            };
        }

        internal static TipoCabanha FromDto(TipoCabanhaDto tipoCabanhaDto)
        {
            return new TipoCabanha()
            {
                Id = tipoCabanhaDto.Id,
                Nombre = new NombreTipoCabanaha(tipoCabanhaDto.Nombre),
                Descripcion = new DescripcionTipoCabanha(tipoCabanhaDto.Descripcion),
                CostoPorHuesped = tipoCabanhaDto.CostoPorHuesped
            };
        }

        internal static TipoCabanhaDto FromEditDto(int? id, TipoCabanhaEditarDto tipoCabanhaDto)
        {
            return new TipoCabanhaDto()
            {
                Id = (int)id,
                Descripcion = tipoCabanhaDto.Descripcion,
                CostoPorHuesped = tipoCabanhaDto.CostoPorHuesped
            };
        }

        internal static TipoCabanha FromDto(int id, string nombre, TipoCabanhaEditarDto tipoCabanhaDto)
        {
            return new TipoCabanha()
            {
                Id = id,
                Nombre = new NombreTipoCabanaha(nombre),
                Descripcion = new DescripcionTipoCabanha(tipoCabanhaDto.Descripcion),
                CostoPorHuesped = tipoCabanhaDto.CostoPorHuesped
            };
        }

        internal static IEnumerable<TipoCabanhaDto> ToListTipoCabanhasDto(IEnumerable<TipoCabanha> tipoCabanhas)
        {
            var tiposCabanhasDto = tipoCabanhas.Select(t => ToDto(t)).ToList();

            return tiposCabanhasDto;
        }
    }
}
