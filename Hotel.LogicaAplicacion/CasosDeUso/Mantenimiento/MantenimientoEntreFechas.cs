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
    public class MantenimientoEntreFechas : IMantenimientoEntreFechas
    {
        #region Dependencias inyectadas
        IRepositorioMantenimiento _repoMantenimiento;

        public MantenimientoEntreFechas(IRepositorioMantenimiento repoMantenimiento)
        {
            _repoMantenimiento = repoMantenimiento;
        }
        #endregion
        public IEnumerable<MantenimientoDto> MostrarLosMantenimientosEntreFechas(DateTime? fecha1, DateTime? fecha2, int? idCabanha)
        {
            if (fecha1 == null || fecha2 == null)
                throw new MantenimientoException("Las fechas no pueden ser nulas.");

            if (idCabanha == null)
                throw new MantenimientoException("El id de la cabaña no puede ser null.");

            var mants = _repoMantenimiento.MostrarMantenimientosEntreFechas(fecha1, fecha2, idCabanha);

            var mantsDto = MapearMantenimiento.ToListMantenimientoDto(mants); 
            return mantsDto;
        }
    }
}
