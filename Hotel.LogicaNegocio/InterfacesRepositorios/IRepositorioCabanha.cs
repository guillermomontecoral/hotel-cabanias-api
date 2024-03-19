using System.Collections.Generic;
using LogicaNegocio.Entidades;

namespace Hotel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioCabanha : IRepositorio<Cabanha>
    {

        IEnumerable<Cabanha> BuscarPorTextoEnNombre(string texto);
        IEnumerable<Cabanha> BuscarPorTipo(int? tipo);
        IEnumerable<Cabanha> BuscarPorMaxPer(int? num);
        IEnumerable<Cabanha> BuscarPorHabilitada();

        IEnumerable<Cabanha> Consulta6_OblParteA(decimal? monto);

    }

}

