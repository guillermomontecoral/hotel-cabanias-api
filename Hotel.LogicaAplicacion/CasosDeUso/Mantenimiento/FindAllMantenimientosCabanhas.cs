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
    public class FindAllMantenimientosCabanhas : InterfacesCasosDeUsos.Mantenimiento.IFindAllMantenimientosCabanhas
    {
        #region Dependencias inyectadas
        IRepositorioMantenimiento _repoMantenimiento;

        public FindAllMantenimientosCabanhas(IRepositorioMantenimiento repoMantenimiento)
        {
            _repoMantenimiento = repoMantenimiento;
        }
        #endregion
        public IEnumerable<MantenimientoDto> GetByIdCabanha(int? id)
        {
            if (id == null)
                throw new MantenimientoException("El ide no puede ser nulo.");

            var mant = _repoMantenimiento.FindAllMantCabanhas(id);
            if (mant.Count() == 0)
                throw new MantenimientoException($"No existen mantenimientos para la cabaña con id: {id}");

            var mantDto = MapearMantenimiento.ToListMantenimientoDto(mant);

            return mantDto;
        }
    }
}
