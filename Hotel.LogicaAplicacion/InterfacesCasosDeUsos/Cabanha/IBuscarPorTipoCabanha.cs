using Hotel.LogicaAplicacion.Dtos.Cabanha_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.InterfacesCasosDeUsos.Cabanha
{
    public interface IBuscarPorTipoCabanha
    {
        IEnumerable<CabanhaDto> BuscarPorTipo(int? idTipo);
    }
}
