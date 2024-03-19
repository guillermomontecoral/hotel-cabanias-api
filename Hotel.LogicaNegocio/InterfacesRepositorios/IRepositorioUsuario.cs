using LogicaNegocio.Entidades;

namespace Hotel.LogicaNegocio.InterfacesRepositorios
{
    public interface IRepositorioUsuario : IRepositorio<Usuario>
    {
        Usuario FindByEmail(string email);

        void ValidarLogin(string email, string clave);

    }

}

