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
    public class GetByIdTipoCabanha : IGetByIdTipoCabanha
    {
        #region Dependencias inyectadas
        IRepositorioTipoCabanha _repoTipoCabanha;
         
        public GetByIdTipoCabanha(IRepositorioTipoCabanha repoTipoCabanha)
        {
            _repoTipoCabanha = repoTipoCabanha;
        }
        #endregion
        public TipoCabanhaDto GetById(int? id)
        {
            if (id == null)
                throw new TipoCabanhaException("Debe de ingresar un id, no puede ser nulo.");

            var tipoCabanha = _repoTipoCabanha.FindById(id);

            var tipoCabanhaDto = MapearTipoCabanha.ToDto(tipoCabanha);

            return tipoCabanhaDto;
        }
    }
}
