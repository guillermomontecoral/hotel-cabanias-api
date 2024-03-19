using System.Collections.Generic;
using System;
using LogicaNegocio.Entidades;

namespace Hotel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioMantenimiento : IRepositorio<Mantenimiento>
    {

        IEnumerable<Mantenimiento> MostrarMantenimientosEntreFechas(DateTime? fecha1, DateTime? fecha2, int? idCabanha);

        int ControlarRegistrosPorDia(DateTime? fecha, int idCabanha);

        IEnumerable<Mantenimiento> FindAllMantCabanhas(int? idCabanha);


    }

}

