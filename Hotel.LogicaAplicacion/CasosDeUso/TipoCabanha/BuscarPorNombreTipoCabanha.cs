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
    public class BuscarPorNombreTipoCabanha : IBuscarPorNombreTipoCabanha
    {
        #region Dependencias inyectadas
        IRepositorioTipoCabanha _repoTipoCabanha;

        public BuscarPorNombreTipoCabanha(IRepositorioTipoCabanha repoTipoCabanha)
        {
            _repoTipoCabanha = repoTipoCabanha;
        }
        #endregion

        public TipoCabanhaDto BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) throw new TipoCabanhaException("El nombre no puede ser nulo.");


            return MapearTipoCabanha.ToDto(_repoTipoCabanha.BuscarTipoPorNombre(nombre));
        }
    }
}
