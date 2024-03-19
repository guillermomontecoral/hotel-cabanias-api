using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesEntidades
{
	public interface IValidable<T> where T : class
	{
		void ValidarDatos();

	}

}

