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
    public class BuscarTextoEnNombreCabanha : IBuscarTextoEnNombreCabanha
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;

        public BuscarTextoEnNombreCabanha(IRepositorioCabanha repoCabanha)
        {
            _repoCabanha = repoCabanha;
        }
        #endregion
        public IEnumerable<CabanhaDto> BuscarTextoEnNombre(string textoEnNombre)
        {
            if (string.IsNullOrEmpty(textoEnNombre))
                throw new CabanhaException("El texto a buscar no puede ser nulo.");

            var cabanhas = _repoCabanha.BuscarPorTextoEnNombre(textoEnNombre);
            if (cabanhas.Count() == 0)
                throw new CabanhaException($"No existen cabañas que contengan ese texto '{textoEnNombre}' en el nombre.");

            var cabanhasDto = MapearCabanha.ToListCabanhasDto(cabanhas);
            return cabanhasDto;
        }
    }
}
