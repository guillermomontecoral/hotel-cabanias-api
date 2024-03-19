using System;
using System.Collections.Generic;

namespace Hotel.LogicaNegocio.InterfacesRepositorios
{
	public interface IRepositorio<T> where T : class
	{
		void Add(T obj);

		void Update(T obj);

		T FindById(int? id);

		IEnumerable<T> FindAll();

		void Delete(int? id);

		void Delete(T obj);



    }

}

