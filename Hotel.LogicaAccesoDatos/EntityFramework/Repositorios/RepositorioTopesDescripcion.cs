using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.LogicaAccesoDatos.EntityFramework.RepositoriosEF
{
    public class RepositorioTopesDescripcion : IRepositorioTopesDescripcion
    {
        //private HotelContext _db = new HotelContext();

        private HotelContext _db;
        public RepositorioTopesDescripcion(HotelContext db)
        {
            _db = db;
        }


        public void Add(TopesDescripcion obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: al agregar un topo de descripción.");

            obj.ValidarDatos();

            try
            {
                _db.TopesDescripciones.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<TopesDescripcion> FindAll()
        {
            try
            {
                var q = _db.TopesDescripciones.ToList();

                return q;
            }
            catch
            {
                throw;
            }
        }

        public TopesDescripcion FindById(int? id)
        {
            try
            {
                var q = _db.TopesDescripciones.Find(id);
                return q;
            }
            catch (TopeDescripcionException ex)
            {
                throw new TopeDescripcionException("No existe ese tope con ese id");
            }
        }

        public void Update(TopesDescripcion obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            var objOriginal = FindById(obj.Id);

            try
            {
                if (objOriginal != null)
                {

                    //objOriginal.Rangos = obj.Rangos;

                    //objOriginal.ValidarDatos();
                    objOriginal.Update(obj);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int? id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TopesDescripcion obj)
        {
            throw new NotImplementedException();
        }


        public TopesDescripcion FindByNameObject(string nombObj)
        {
            try
            {
                //var q = _db.TopesDescripciones.SingleOrDefault(d => d.NombreObj == nombObj);

                var q = _db.TopesDescripciones.SingleOrDefault(d => d.NombreTope.Nombre == nombObj);

                return q;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
