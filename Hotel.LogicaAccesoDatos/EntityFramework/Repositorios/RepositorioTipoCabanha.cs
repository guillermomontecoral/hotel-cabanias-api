using LogicaNegocio.Entidades;
using Microsoft.Data.SqlClient;
using Hotel.LogicaNegocio.ExcepcionesEntidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using Microsoft.EntityFrameworkCore;
using Hotel.LogicaNegocio.Entidades.ValueObjects.TipoCabanha;

namespace Hotel.LogicaAccesoDatos.EntityFramework.RepositoriosEF
{
    public class RepositorioTipoCabanha : IRepositorioTipoCabanha
    {
        //private HotelContext _db = new HotelContext();
        //private RepositorioCabanha c = new RepositorioCabanha();

        private HotelContext _db;
        public RepositorioTipoCabanha(HotelContext db)
        {
            _db = db;
        }

        #region Operaciones CRUD
        public void Add(TipoCabanha obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            try
            {
                if (!NombreTipoExiste(obj.Nombre.Value))
                    throw new TipoCabanhaException($"Ya existe un tipo de cabaña con el nombre: {obj.Nombre.Value}");

                obj.Update(obj);
                //obj.ValidarDatos();

                //_db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _db.TipoCabanhas.Add(obj);
                _db.SaveChanges();
            }
            catch (TipoCabanhaException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(TipoCabanha obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            try
            {
                var objOriginal = FindById(obj.Id);

                objOriginal.Nombre = new NombreTipoCabanaha(obj.Nombre.Value);
                objOriginal.Descripcion = new DescripcionTipoCabanha(obj.Descripcion.Descripcion);
                objOriginal.CostoPorHuesped = obj.CostoPorHuesped;
                objOriginal.ValidarDatos();
                //_db.Entry(objOriginal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();

            }
            catch (TipoCabanhaException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                //throw new Exception($"No se pudo dar de alta el tipo de cabanha.");
                throw new Exception(ex.Message);
            }
        }      

        public TipoCabanha FindById(int? id)
        {
            try
            {
                var tipoCabanha = _db.TipoCabanhas.Find(id);

                if (tipoCabanha == null)
                    throw new TipoCabanhaException($"No existe un tipo de cabaña con el id: {id}");

                return tipoCabanha;
            }
            catch (TipoCabanhaException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se puede recuperar el tipo de cabaña con el ID: {id}");
            }
        }

        public IEnumerable<TipoCabanha> FindAll()
        {
            try
            {
                //var tipoCabanha = _db.TipoCabanhas.OrderBy(t => t.Nombre.Value).ToList();
                var tipoCabanha = _db.TipoCabanhas.ToList();
                return tipoCabanha;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(TipoCabanha obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El tipo de cabaña no puede ser nulo");

            try
            {
                _db.TipoCabanhas.Remove(obj);
                //_db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(int? id)
        {
            //TODO - Podria buscarlo directo en la tabla aunque tengo este metodo preguntar que es mejor
            //c.BuscarPorTipo(id);

            try
            {
                var tipoCabanha = FindById(id);
                Delete(tipoCabanha);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        public void Update(int? id, string descripcion, decimal? costoHuesped)
        {
            if (id == null)
                throw new TipoCabanhaException("El id no puede ser nulo.");

            if (string.IsNullOrEmpty(descripcion))
                throw new TipoCabanhaException("La descricpción no puede ser nula.");

            if (costoHuesped < 0 || costoHuesped == null)
                throw new TipoCabanhaException("El costo por huesped no puede ser negativo o ser nulo.");

            try
            {
                var objOriginal = FindById(id);
                objOriginal.Descripcion = new DescripcionTipoCabanha(descripcion.Trim());
                objOriginal.CostoPorHuesped = costoHuesped;
                //objOriginal.Update(descripcion, costoHuesped);
                objOriginal.ValidarDatos();
                //_db.Entry(objOriginal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();

            }
            catch (TipoCabanhaException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                //throw new Exception($"No se pudo dar de alta el tipo de cabanha.");
                throw new Exception(ex.Message);
            }
        }

        public TipoCabanha BuscarTipoPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) throw new TipoCabanhaException("El nombre no puede ser nulo");

            try
            {

                //var tipoCabanha = _db.TipoCabanhas.SingleOrDefault(t => t.Nombre == nombre.Trim()) ?? throw new InvalidOperationException($"No existe un tipo de cabaña con el nombre {nombre}");

                var tipoCabanha = _db.TipoCabanhas
                                        .SingleOrDefault(t => t.Nombre.Value.ToLower().Trim() == 
                                        nombre.ToLower().Trim()) ?? throw new TipoCabanhaException($"No existe un tipo de cabaña con el nombre {nombre}");
                return tipoCabanha;

            }
            catch (TipoCabanhaException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool NombreTipoExiste(string nombre)
        {
            if (string.IsNullOrEmpty(nombre)) throw new ArgumentNullException("El nombre no puede ser nulo");

            try
            {
                if (_db.TipoCabanhas.Where(t => t.Nombre.Value.ToLower().Trim() == nombre.ToLower().Trim())
                                    .FirstOrDefault() != null)
                {
                    return false;
                }

                return true;

            }
            catch (TipoCabanhaException ex)
            {
                throw;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }

}

