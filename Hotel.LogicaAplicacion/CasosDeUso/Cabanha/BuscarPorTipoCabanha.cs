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
    public class BuscarPorTipoCabanha : IBuscarPorTipoCabanha
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;

        public BuscarPorTipoCabanha(IRepositorioCabanha repoCabanha)
        {
            _repoCabanha = repoCabanha;
        }
        #endregion
        public IEnumerable<CabanhaDto> BuscarPorTipo(int? idTipo)
        {
            if (idTipo == null)
                throw new CabanhaException("El id del tipo de cabaña a buscar no puede ser nulo.");

            var cabanhas = _repoCabanha.BuscarPorTipo(idTipo);
            if (cabanhas.Count() == 0)
                throw new CabanhaException($"No existen cabañas registradas con ese tipo de cabaña.");

            var cabanhasDto = MapearCabanha.ToListCabanhasDto(cabanhas);
            return cabanhasDto;
        }
    }
}
