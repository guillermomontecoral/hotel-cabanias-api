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
    public class FindByNameObjectTopesDescripcion : IFindByNameObjectTopesDescripcion
    {
        #region Dependencias inyectadas
        IRepositorioTopesDescripcion _repoTopes;

        public FindByNameObjectTopesDescripcion(IRepositorioTopesDescripcion repoTopes)
        {
            _repoTopes = repoTopes;
        }
        #endregion
        public TopesDescripcionDto FindByNameObject(string nombObj)
        {
            if (string.IsNullOrEmpty(nombObj))
                throw new TopeDescripcionException("El nombre no puede ser nulo.");
            var n = _repoTopes.FindByNameObject(nombObj);
            var nDto = MapearTopesDescripcion.ToDto(n);
            return nDto;
        }
    }
}
