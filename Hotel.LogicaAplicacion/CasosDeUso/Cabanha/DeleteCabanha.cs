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
    public class DeleteCabanha : IDeleteCabanha
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;

        public DeleteCabanha(IRepositorioCabanha repoCabanha)
        {
            _repoCabanha = repoCabanha;
        }


        #endregion
        public void Delete(int? id)
        {
            if (id == null)
                throw new CabanhaException("El id de la cabaña no puede ser nulo.");

            _repoCabanha.Delete(id);
        }
    }
}
