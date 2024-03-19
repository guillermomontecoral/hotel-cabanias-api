using Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto;
using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
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
    public class GetAllMantenimientos : IGetAllMantenimientos
    {
        #region Dependencias inyectadas
        IRepositorioMantenimiento _repoMantenimiento;

        public GetAllMantenimientos(IRepositorioMantenimiento repoMantenimiento)
        {
            _repoMantenimiento = repoMantenimiento;
        }
        #endregion
        public IEnumerable<MantenimientoDto> GetAll()
        {
            var mants = _repoMantenimiento.FindAll();
            if (mants.Count() == 0)
                throw new MantenimientoException("La lista de mantenimientos esta vacia.");

            var mantsDto = MapearMantenimiento.ToListMantenimientoDto(mants);
            return mantsDto;
        }
    }
}
