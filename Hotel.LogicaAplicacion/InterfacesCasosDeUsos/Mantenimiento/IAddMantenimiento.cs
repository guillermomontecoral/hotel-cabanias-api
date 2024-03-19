using Hotel.LogicaAplicacion.Dtos.Mantenimiento_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Mantenimiento
{
    public interface IAddMantenimiento
    {
        void Add(MantenimientoDto mantenimientoDto);
    }
}
