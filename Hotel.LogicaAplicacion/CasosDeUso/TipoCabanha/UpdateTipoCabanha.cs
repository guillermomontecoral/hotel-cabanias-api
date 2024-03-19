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
    public class UpdateTipoCabanha : IUpdateTipoCabanha
    {
        #region Dependencias inyectadas
        IRepositorioTipoCabanha _repoTipoCabanha;
        IRepositorioTopesDescripcion _repoTopes;

        public UpdateTipoCabanha(
            IRepositorioTipoCabanha repoTipoCabanha,
            IRepositorioTopesDescripcion repoTopes)
        {
            _repoTipoCabanha = repoTipoCabanha;
            _repoTopes = repoTopes;
        }
        #endregion

        /// <summary>
        /// Modifica todos los datos del tipo de cabaña.
        /// </summary>
        /// <param name="id">Identificador del tipo de cabaña.</param>
        /// <param name="tipoCabanhaDto">Objeto que tiene todos los datos del tipo de cabaña a modificar</param>
        /// <exception cref="TipoCabanhaException">Si hay algún error.</exception>
        public void Update(int? id, TipoCabanhaDto tipoCabanhaDto)
        {
            if (id == null)
                throw new TipoCabanhaException("El id del tipo de cabaña no puede ser nulo.");

            if (tipoCabanhaDto == null)
                throw new TipoCabanhaException("Los datos del tipo de cabaña a modificar no pueden ser nulos.");


            var topes = _repoTopes.FindByNameObject("tc");
            if (topes == null)
                throw new TipoCabanhaException("No ha topes para este objeto, debe ingresarlos.");

            if (tipoCabanhaDto.Descripcion.Trim().Length < topes.Rangos.Min || tipoCabanhaDto.Descripcion.Trim().Length > topes.Rangos.Max)
            {
                throw new TipoCabanhaException($"La descripción debe de contener entre {topes.Rangos.Min} y {topes.Rangos.Max} caracteres. Usted escribio {tipoCabanhaDto.Descripcion.Length} caracteres.");
            }

            var tipoCabanha = MapearTipoCabanha.FromDto(tipoCabanhaDto) ?? throw new TipoCabanhaException("Se devolvio null.");

            tipoCabanha.ValidarDatos();
            _repoTipoCabanha.Update(tipoCabanha);
        }

        /// <summary>
        /// Modifica solamente la descripción y el costo por huesped del tipo de cabaña.
        /// </summary>
        /// <param name="id">Identificador del tipo de cabaña.</param>
        /// <param name="tipoCabanhaDto">Objeto que tiene los datos especificos del tipo de cabaña a modificar</param>
        /// <exception cref="TipoCabanhaException">Si hay algún error.</exception>
        public void Update(int? id, TipoCabanhaEditarDto tipoCabanhaDto)
        {
            if (id == null)
                throw new TipoCabanhaException("El id del tipo de cabaña no puede ser nulo.");

            if (tipoCabanhaDto == null)
                throw new TipoCabanhaException("Los datos del tipo de cabaña a modificar no pueden ser nulos.");


            var topes = _repoTopes.FindByNameObject("tc");
            if (topes == null)
                throw new TipoCabanhaException("No ha topes para este objeto, debe ingresarlos.");

            if (tipoCabanhaDto.Descripcion.Trim().Length < topes.Rangos.Min || tipoCabanhaDto.Descripcion.Trim().Length > topes.Rangos.Max)
            {
                throw new TipoCabanhaException($"La descripción debe de contener entre {topes.Rangos.Min} y {topes.Rangos.Max} caracteres. Usted escribio {tipoCabanhaDto.Descripcion.Length} caracteres.");
            }

            _repoTipoCabanha.Update(id, tipoCabanhaDto.Descripcion, tipoCabanhaDto.CostoPorHuesped);
        }
    }
}
