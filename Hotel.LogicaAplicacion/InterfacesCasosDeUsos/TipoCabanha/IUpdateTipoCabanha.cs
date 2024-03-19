using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TipoCabanha
{
    public interface IUpdateTipoCabanha
    {
        void Update(int? id, TipoCabanhaDto tipoCabanhaDto);
        void Update(int? id, TipoCabanhaEditarDto tipoCabanhaDto);
    }
}
