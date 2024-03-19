using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
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
    public class UpdateTopesDescripcion : IUpdateTopesDescripcion
    {
        #region Dependencias inyectadas
        IRepositorioTopesDescripcion _repoTopes;

        public UpdateTopesDescripcion(IRepositorioTopesDescripcion repoTopes)
        {
            _repoTopes = repoTopes;
        }
        #endregion
        public void Update(int? id, TopesDescripcionDto obj)
        {
            if (id == null)
                throw new TopeDescripcionException("El id del tope es nulo.");

            if (obj == null)
                throw new TopeDescripcionException("Los datos no no pueden ser nulos.");

            var topes = MapearTopesDescripcion.FromDto(obj) ?? throw new TopeDescripcionException("Se devolvio null.");

            _repoTopes.Update(topes);
        }
    }
}
