using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
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
    public class UpdateMantenimiento : IUpdateMantenimiento
    {
        #region Dependencias inyectadas
        IRepositorioMantenimiento _repoMantenimiento;
        IRepositorioTopesDescripcion _repoTopes;

        public UpdateMantenimiento(
            IRepositorioMantenimiento repoMantenimiento,
            IRepositorioTopesDescripcion repoTopes)
        {
            _repoMantenimiento = repoMantenimiento;
            _repoTopes = repoTopes;
        }
        #endregion

        public void Update(MantenimientoDto obj)
        {
            if (obj == null)
                throw new MantenimientoException("Los datos del tipo de cabaña a modificar no pueden ser nulos.");

            var topes = _repoTopes.FindByNameObject("cab");
            if (topes == null)
                throw new CabanhaException("No ha topes para este objeto, debe ingresarlos.");

            if (obj.Descripcion.Trim().Length < topes.Rangos.Min || obj.Descripcion.Trim().Length > topes.Rangos.Max)
            {
                throw new CabanhaException($"La descripción debe de contener entre {topes.Rangos.Min} y {topes.Rangos.Max} caracteres. Usted escribio {obj.Descripcion.Length} caracteres.");
            }

            var cabanha = MapearMantenimiento.FromDto(obj) ?? throw new CabanhaException("Se devolvio null.");

            _repoMantenimiento.Update(cabanha);
        }
    }
}
