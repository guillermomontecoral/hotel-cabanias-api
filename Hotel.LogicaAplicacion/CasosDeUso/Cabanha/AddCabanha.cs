using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
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
    public class AddCabanha : IAddCabanha
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;
        IRepositorioTopesDescripcion _repoTopes;

        public AddCabanha(
            IRepositorioCabanha repoCabanha, 
            IRepositorioTopesDescripcion repoTopes)
        {
            _repoCabanha = repoCabanha;
            _repoTopes = repoTopes;
        }
        #endregion

        public void Add(CabanhaDto cabanhaDto)
        {
            if (cabanhaDto == null)
                throw new CabanhaException("No se puede dar de alta un tipo de cabaña nulo.");

            var topes = _repoTopes.FindByNameObject("cab");
            if (topes == null)
                throw new CabanhaException("No ha topes para este objeto, debe ingresarlos.");

            if (cabanhaDto.Descripcion.Trim().Length < topes.Rangos.Min || cabanhaDto.Descripcion.Trim().Length > topes.Rangos.Max)
            {
                throw new CabanhaException($"La descripción debe de contener entre {topes.Rangos.Min} y {topes.Rangos.Max} caracteres. Usted escribio {cabanhaDto.Descripcion.Length} caracteres.");
            }

            var nuevoCabanha = MapearCabanha.FromDto(cabanhaDto);
            _repoCabanha.Add(nuevoCabanha);

            //Le devuelvo el id que fue creado al tipo de cabaña al tipoCabanhaDto
            cabanhaDto.Id = nuevoCabanha.Id;
            cabanhaDto.Nombre = nuevoCabanha.Nombre.Nombre;
            cabanhaDto.Descripcion = nuevoCabanha.Descripcion.Descripcion;
            cabanhaDto.IdTipoCabanha = nuevoCabanha.IdTipoCabanha;
            cabanhaDto.Jacuzzi = nuevoCabanha.Jacuzzi;
            cabanhaDto.HabilitadaParaReservas = nuevoCabanha.HabilitadaParaReservas;
            cabanhaDto.NumHabitacion = nuevoCabanha.NumHabitacion;
            cabanhaDto.CantMaxPersonas = nuevoCabanha.CantMaxPersonas;

        }
    }
}
