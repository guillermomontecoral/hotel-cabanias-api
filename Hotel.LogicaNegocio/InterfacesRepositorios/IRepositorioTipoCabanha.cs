using LogicaNegocio.Entidades;

namespace Hotel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioTipoCabanha : IRepositorio<TipoCabanha>
    {

        TipoCabanha BuscarTipoPorNombre(string nombre);

        bool NombreTipoExiste(string nombre);

        public void Update(int? id, string descripcion, decimal? costoHuesped);

    }

}

