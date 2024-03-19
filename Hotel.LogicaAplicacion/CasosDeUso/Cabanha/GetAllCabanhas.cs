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
    public class GetAllCabanhas : IGetAllCabanhas
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;

        public GetAllCabanhas(IRepositorioCabanha repoCabanha)
        {
            _repoCabanha = repoCabanha;
        }
        #endregion

        IEnumerable<CabanhaDto> IGetAllCabanhas.GetAll()
        {
            var cabanhas = _repoCabanha.FindAll();
            if (cabanhas == null)
                throw new CabanhaException("La lista se encuentra vacía");

            var cabanhasDto = MapearCabanha.ToListCabanhasDto(cabanhas);
            return cabanhasDto;
        }
    }
}
