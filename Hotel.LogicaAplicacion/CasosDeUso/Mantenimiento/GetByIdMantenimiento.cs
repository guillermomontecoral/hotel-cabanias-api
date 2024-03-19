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
    public class GetByIdMantenimiento : IGetByIdMantenimiento
    {
        #region Dependencias inyectadas
        IRepositorioMantenimiento _repoMantenimiento;

        public GetByIdMantenimiento(IRepositorioMantenimiento repoMantenimiento)
        {
            _repoMantenimiento = repoMantenimiento;
        }
        #endregion
        public MantenimientoDto GetdById(int? id)
        {
            if (id == null)
                throw new MantenimientoException("El ide no puede ser nulo.");

            var mant = _repoMantenimiento.FindById(id);
            if (mant == null)
                throw new MantenimientoException($"No existe un mantenimiento con id: {id}");

            var mantDto = MapearMantenimiento.ToDto(mant);

            return mantDto;
        }
    }
}
