using Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto;
using Hotel.LogicaAplicacion.Dtos.MapeosDtos;
using Hotel.LogicaAplicacion.Dtos.TopesDescripcion_Dto;
using Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TopesDescripcion;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.CasosDeUso.TopesDescripcion
{
    public class AddTopesDescripcion : IAddTopesDescripcion
    {
        #region Dependencias inyectadas
        IRepositorioTopesDescripcion _repoTopes;

        public AddTopesDescripcion(IRepositorioTopesDescripcion repoTopes)
        {
            _repoTopes = repoTopes;
        }
        #endregion
        public void Add(TopesDescripcionDto obj)
        {
            if (obj == null)
                throw new TopeDescripcionException("No se puede dar de alta el tope.");

            var nuevoTope= MapearTopesDescripcion.FromDto(obj);
            _repoTopes.Add(nuevoTope);

            obj.Id = nuevoTope.Id;
        }
    }
}
