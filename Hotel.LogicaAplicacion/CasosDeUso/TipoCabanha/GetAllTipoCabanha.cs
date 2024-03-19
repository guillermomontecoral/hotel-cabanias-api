using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TipoCabanha;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.CasosDeUso.TipoCabanha
{
    public class GetAllTipoCabanha : IGetAllTipoCabanha
    {
        #region Dependencias inyectadas
        IRepositorioTipoCabanha _repoTipoCabanha;

        public GetAllTipoCabanha(IRepositorioTipoCabanha repoTipoCabanha)
        {
            _repoTipoCabanha = repoTipoCabanha;
        }
        #endregion
        public IEnumerable<TipoCabanhaDto> GetAll()
        {
            var tiposCabanhas = _repoTipoCabanha.FindAll();
            if (tiposCabanhas == null)
                throw new TipoCabanhaException("La lista se encuentra vacía");

            var tipoCabanhasDto = MapearTipoCabanha.ToListTipoCabanhasDto(tiposCabanhas);
            return tipoCabanhasDto;

        }
    }
}
