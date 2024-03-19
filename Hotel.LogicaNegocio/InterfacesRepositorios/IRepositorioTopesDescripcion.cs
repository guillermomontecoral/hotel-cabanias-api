using Hotel.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioTopesDescripcion : IRepositorio<TopesDescripcion>
    {
        public TopesDescripcion FindByNameObject(string nombObj);
    }
}
