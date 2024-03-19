﻿using Hotel.LogicaAplicacion.Dtos.TipoCabanha_Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAplicacion.InterfacesCasosDeUsos.TipoCabanha
{
    public interface IGetByIdTipoCabanha
    {
        TipoCabanhaDto GetById(int? id);
    }
}
