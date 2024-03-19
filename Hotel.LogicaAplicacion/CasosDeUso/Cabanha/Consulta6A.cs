using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Cabanha;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.CasosDeUso.Cabanha
{
    public class Consulta6A : IConsulta6A
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;

        public Consulta6A(IRepositorioCabanha repoCabanha)
        {
            _repoCabanha = repoCabanha;
        }
        #endregion
        public IEnumerable<CabanhaDto> Consulta6_OblParteA(decimal? monto)
        {
            if (monto == null)
                throw new CabanhaException("El numero no puede ser nulo.");

            var cabanhas = _repoCabanha.Consulta6_OblParteA(monto);
            if (cabanhas.Count() == 0)
                throw new CabanhaException($"No existen cabañas registradas con esa cantidad de huespedes o superior.");

            var cabanhasDto = MapearCabanha.ToListCabanhasDto(cabanhas);
            return cabanhasDto;
        }
    }
}
