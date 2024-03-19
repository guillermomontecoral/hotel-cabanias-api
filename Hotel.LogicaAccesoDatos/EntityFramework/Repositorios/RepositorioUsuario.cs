using LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;

namespace Hotel.LogicaAccesoDatos.EntityFramework.RepositoriosEF
{
    public class RepositorioUsuario : IRepositorioUsuario
    {
        //private HotelContext _db = new HotelContext();

        private HotelContext _db;
        public RepositorioUsuario(HotelContext db)
        {
            _db = db;
        }


        #region Operaciones CRUD
        public void Add(Usuario obj)
        {
            if (obj == null) throw new ArgumentNullException("Erros: El usuario no puede ser nulo");
            obj.ValidarDatos();
            try
            {
                obj.Clave = obj.Clave.Trim();
                obj.Email = obj.Email.Trim().ToLower();

                //_db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                _db.Usuarios.Add(obj);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IEnumerable<Usuario> FindAll()
        {
            return _db.Usuarios.ToList();
        }

        public Usuario FindById(int? id)
        {
            try
            {
                var usuario = _db.Usuarios.Find(id);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se puede recuperar el tipo de cabaña con el ID: {id}");
            }
        }

        public void Update(Usuario obj)
        {
            if (obj == null) throw new ArgumentNullException("El usuario no puede ser nulo");

            Usuario objOriginal = FindByEmail(obj.Email);

            try
            {
                objOriginal.Email = obj.Email.Trim().ToLower();
                objOriginal.Clave = obj.Clave.Trim();
                objOriginal.ValidarDatos();
                _db.Entry(objOriginal).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Delete(int? id)
        {
            try
            {
                var usuario = FindById(id);
                Delete(usuario);

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public void Delete(Usuario obj)
        {
            if (obj == null) throw new ArgumentNullException("Error: El usuario no puede ser nulo");

            try
            {
                _db.Usuarios.Remove(obj);
                _db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        public Usuario FindByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException("Error: El email no puede ser nulo");

            try
            {
                //Despues de los dos ?? el throw saltara si me devuelve que el usuario es null
                var usuario = _db.Usuarios.Where(t => t.Email == email.Trim()).SingleOrDefault() ?? throw new InvalidOperationException($"No existe un usuario registrado con el email {email}");

                return usuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ValidarLogin(string email, string clave)
        {
            var u = FindByEmail(email.ToLower()) ?? throw new ArgumentNullException($"No existe un usuario registrado con el email {email}");
            try
            {
                //validar clave
                if (u.Clave != clave)
                {
                    throw new Exception("La contraseña ingresada es incorrecta");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}

