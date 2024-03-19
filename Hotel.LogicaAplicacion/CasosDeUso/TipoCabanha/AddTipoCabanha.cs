using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
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
    public class AddTipoCabanha : IAddTipoCabanha
    {

        #region Dependencias inyectadas
        IRepositorioTipoCabanha _repoTipoCabanha;
        IRepositorioTopesDescripcion _repoTopes;

        public AddTipoCabanha(
            IRepositorioTipoCabanha repoTipoCabanha,
            IRepositorioTopesDescripcion repoTopes)
        {
            _repoTipoCabanha = repoTipoCabanha;
            _repoTopes = repoTopes;
        }
        #endregion

        public void Add(TipoCabanhaDto tipoCabanhaDto)
        {
            if (tipoCabanhaDto == null)
                throw new TipoCabanhaException("No se puede dar de alta un tipo de cabaña nulo.");

            var topes = _repoTopes.FindByNameObject("tc");
            if (topes == null)
                throw new TipoCabanhaException("No ha topes para este objeto, debe ingresarlos.");

            if (tipoCabanhaDto.Descripcion.Trim().Length < topes.Rangos.Min || tipoCabanhaDto.Descripcion.Trim().Length > topes.Rangos.Max)
            {
                throw new TipoCabanhaException($"La descripción debe de contener entre {topes.Rangos.Min} y {topes.Rangos.Max} caracteres. Usted escribio {tipoCabanhaDto.Descripcion.Length} caracteres.");
            }

            var nuevoTipoCabanha = MapearTipoCabanha.FromDto(tipoCabanhaDto);
            _repoTipoCabanha.Add(nuevoTipoCabanha);

            //Le devuelvo el id que fue creado al tipo de cabaña al tipoCabanhaDto
            tipoCabanhaDto.Id = nuevoTipoCabanha.Id;

        }
    }
}
