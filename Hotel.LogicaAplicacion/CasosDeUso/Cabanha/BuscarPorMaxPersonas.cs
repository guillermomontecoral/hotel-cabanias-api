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
    public class BuscarPorMaxPersonas : IBuscarPorMaxPersonas
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;

        public BuscarPorMaxPersonas(IRepositorioCabanha repoCabanha)
        {
            _repoCabanha = repoCabanha;
        }
        #endregion
        public IEnumerable<CabanhaDto> BuscarPorMaxPer(int? num)
        {
            if (num == null)
                throw new CabanhaException("El numero no puede ser nulo.");

            var cabanhas = _repoCabanha.BuscarPorMaxPer(num);
            if (cabanhas.Count() == 0)
                throw new CabanhaException($"No existen cabañas registradas con esa cantidad de huespedes o superior.");

            var cabanhasDto = MapearCabanha.ToListCabanhasDto(cabanhas);
            return cabanhasDto;
        }
    }
}
