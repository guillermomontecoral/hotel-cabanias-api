using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.Entidades.ValueObjects.Cabanha;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.Dtos.MapeosDtos
{
    public class MapearCabanha
    {
        internal static CabanhaDto ToDto(Cabanha cabanha)
        {
            

            return new CabanhaDto()
            {
                Id = cabanha.Id,
                Nombre = cabanha.Nombre.Nombre,
                Descripcion = cabanha.Descripcion.Descripcion,
                IdTipoCabanha = cabanha.IdTipoCabanha,
                NumHabitacion = cabanha.NumHabitacion,
                CantMaxPersonas = cabanha.CantMaxPersonas,
                HabilitadaParaReservas = cabanha.HabilitadaParaReservas,
                Jacuzzi = cabanha.Jacuzzi,
                NombreFoto = cabanha.MisFotos[0].NombreFoto
            };
        }

        internal static Cabanha FromDto(CabanhaDto cabanhaDto)
        {
            return new Cabanha()
            {
                Id = cabanhaDto.Id,
                Nombre = new NombreCabanha (cabanhaDto.Nombre),
                Descripcion = new DescripcionCabanha(cabanhaDto.Descripcion),
                IdTipoCabanha = cabanhaDto.IdTipoCabanha,
                NumHabitacion = cabanhaDto.NumHabitacion,
                CantMaxPersonas = cabanhaDto.CantMaxPersonas,
                HabilitadaParaReservas = cabanhaDto.HabilitadaParaReservas,
                Jacuzzi = cabanhaDto.Jacuzzi,
                MisFotos = new List<Foto>()
                {
                    new Foto()
                    {
                        NombreFoto = cabanhaDto.NombreFoto
                    }
                }
            };
        }

        internal static Cabanha FromEditDto(CabanhaModificarDto cabanhaDto)
        {
            return new Cabanha()
            {
                Id = cabanhaDto.Id,
                Nombre = new NombreCabanha(cabanhaDto.Nombre),
                Descripcion = new DescripcionCabanha(cabanhaDto.Descripcion),
                IdTipoCabanha = cabanhaDto.IdTipoCabanha,
                CantMaxPersonas = cabanhaDto.CantMaxPersonas,
                HabilitadaParaReservas = cabanhaDto.HabilitadaParaReservas,
                Jacuzzi = cabanhaDto.Jacuzzi
            };
        }

        //internal static Cabanha FromDto(int id, string nombre, TipoCabanhaEditarDto tipoCabanhaDto)
        //{
        //    return new Cabanha()
        //    {
        //        Id = id,
        //        Nombre = new NombreTipoCabanaha(nombre),
        //        Descripcion = new DescripcionTipoCabanha(tipoCabanhaDto.Descripcion),
        //        CostoPorHuesped = tipoCabanhaDto.CostoPorHuesped
        //    };
        //}

        internal static IEnumerable<CabanhaDto> ToListCabanhasDto(IEnumerable<Cabanha> cabanhas)
        {
            var cabanhasDto = cabanhas.Select(t => ToDto(t)).ToList();

            return cabanhasDto;
        }
    }
}
