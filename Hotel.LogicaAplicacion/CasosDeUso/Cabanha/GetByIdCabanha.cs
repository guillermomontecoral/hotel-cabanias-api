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
    public class GetByIdCabanha : IGetByIdCabanha
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;

        public GetByIdCabanha(IRepositorioCabanha repoCabanha)
        {
            _repoCabanha = repoCabanha;
        }


        #endregion
        public CabanhaDto GetById(int? id)
        {
            if (id == null)
                throw new CabanhaException("El ide no puede ser nulo.");

            var cabanha = _repoCabanha.FindById(id);
            if (cabanha == null)
                throw new CabanhaException($"No existe una cabaña con el id {id}");

            var cabanhaDto = MapearCabanha.ToDto(cabanha);

            return cabanhaDto;
        }
    }
}
