using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
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
    public class UpdateCabanha : IUpdateCabanha
    {
        #region Dependencias inyectadas
        IRepositorioCabanha _repoCabanha;
        IRepositorioTopesDescripcion _repoTopes;

        public UpdateCabanha(
            IRepositorioCabanha repoCabanha,
            IRepositorioTopesDescripcion repoTopes)
        {
            _repoCabanha = repoCabanha;
            _repoTopes = repoTopes;
        }


        #endregion
        public void Update(int? id, CabanhaModificarDto cabanhaDto)
        {
            if (id == null)
                throw new CabanhaException("El id del tipo de cabaña no puede ser nulo.");

            if (cabanhaDto == null)
                throw new CabanhaException("Los datos del tipo de cabaña a modificar no pueden ser nulos.");

            var topes = _repoTopes.FindByNameObject("cab");
            if (topes == null)
                throw new CabanhaException("No ha topes para este objeto, debe ingresarlos.");

            if (cabanhaDto.Descripcion.Trim().Length < topes.Rangos.Min || cabanhaDto.Descripcion.Trim().Length > topes.Rangos.Max)
            {
                throw new CabanhaException($"La descripción debe de contener entre {topes.Rangos.Min} y {topes.Rangos.Max} caracteres. Usted escribio {cabanhaDto.Descripcion.Length} caracteres.");
            }

            var cabanha = MapearCabanha.FromEditDto(cabanhaDto) ?? throw new CabanhaException("Se devolvio null.");

            _repoCabanha.Update(cabanha);
        }
    }
}
