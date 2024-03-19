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
    public class DeleteTipoCabanha : IDeleteTipoCabanha
    {
        #region Dependencias inyectadas
        IRepositorioTipoCabanha _repoTipoCabanha;

        public DeleteTipoCabanha(IRepositorioTipoCabanha repoTipoCabanha)
        {
            _repoTipoCabanha = repoTipoCabanha;
        }
        #endregion

        public void Delete(int? id)
        {
            if(id == null)
                throw new TipoCabanhaException("El id del tipo de cabaña no puede ser nulo.");

            _repoTipoCabanha.Delete(id);
        }
    }
}
