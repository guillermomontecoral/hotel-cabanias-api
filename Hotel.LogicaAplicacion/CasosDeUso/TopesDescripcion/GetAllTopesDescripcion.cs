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
    public class GetAllTopesDescripcion : IGetAllTopesDescripcion
    {
        #region Dependencias inyectadas
        IRepositorioTopesDescripcion _repoTopes;

        public GetAllTopesDescripcion(IRepositorioTopesDescripcion repoTopes)
        {
            _repoTopes = repoTopes;
        }
        #endregion
        IEnumerable<TopesDescripcionDto> IGetAllTopesDescripcion.GetAll()
        {
            var topes = _repoTopes.FindAll();
            if (topes.Count() == 0)
                throw new TopeDescripcionException("No hay topes registrados.");

            var topesDto = MapearTopesDescripcion.ToListTopesDto(topes);
            return topesDto;
        }
    }
}
