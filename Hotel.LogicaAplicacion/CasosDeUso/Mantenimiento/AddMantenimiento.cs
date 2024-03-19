using Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto;
using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
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
    public class AddMantenimiento : IAddMantenimiento
    {
        #region Dependencias inyectadas
        IRepositorioMantenimiento _repoMantenimiento;
        IRepositorioTopesDescripcion _repoTopes;

        public AddMantenimiento(
            IRepositorioMantenimiento repoMantenimiento,
            IRepositorioTopesDescripcion repoTopes)
        {
            _repoMantenimiento = repoMantenimiento;
            _repoTopes = repoTopes;
        }
        #endregion
        public void Add(MantenimientoDto mantenimientoDto)
        {
             
            if (mantenimientoDto == null)
                throw new MantenimientoException("No se puede dar de alta un tipo de cabaña nulo.");

            var topes = _repoTopes.FindByNameObject("man");
            if (topes == null)
                throw new TipoCabanhaException("No ha topes para este objeto, debe ingresarlos.");

            if (mantenimientoDto.Descripcion.Trim().Length < topes.Rangos.Min || mantenimientoDto.Descripcion.Trim().Length > topes.Rangos.Max)
            {
                throw new TipoCabanhaException($"La descripción debe de contener entre {topes.Rangos.Min} y {topes.Rangos.Max} caracteres. Usted escribio {mantenimientoDto.Descripcion.Length} caracteres.");
            }

            var nuevoMantenimiento = MapearMantenimiento.FromDto(mantenimientoDto);
            _repoMantenimiento.Add(nuevoMantenimiento);

            mantenimientoDto.Id = nuevoMantenimiento.Id;
            //mantenimientoDto.Descripcion = nuevoMantenimiento.Descripcion.Descripcion;
            //mantenimientoDto.RealizadoPor = nuevoMantenimiento.RealizadoPor.Nombre;
            //mantenimientoDto.Fecha = nuevoMantenimiento.Fecha.Fecha;
            //mantenimientoDto.CostoMantenimiento = nuevoMantenimiento.CostoMantenimiento;
            //mantenimientoDto.IdCabanha = nuevoMantenimiento.IdCabanha;
        }
    }
}
