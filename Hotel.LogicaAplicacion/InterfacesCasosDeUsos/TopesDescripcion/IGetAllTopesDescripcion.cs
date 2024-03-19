using Hotel.LogicaAplicacion.Dtos.TopesDescripcion_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TopesDescripcion
{
    public interface IGetAllTopesDescripcion
    {
        IEnumerable<TopesDescripcionDto> GetAll();
    }
}
