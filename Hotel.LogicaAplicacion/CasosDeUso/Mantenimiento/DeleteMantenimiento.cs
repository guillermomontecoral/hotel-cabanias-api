using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Mantenimiento;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.CasosDeUso.Mantenimiento
{
    public class DeleteMantenimiento : IDeleteMantenimiento
    {
        #region Dependencias inyectadas
        IRepositorioMantenimiento _repoMantenimiento;

        public DeleteMantenimiento(
            IRepositorioMantenimiento repoMantenimiento)
        {
            _repoMantenimiento = repoMantenimiento;
        }
        #endregion
        public void Delete(int? id)
        {
            if (id == null)
                throw new CabanhaException("El id del mantenimiento no puede ser nulo.");

            _repoMantenimiento.Delete(id);
        }
    }
}
