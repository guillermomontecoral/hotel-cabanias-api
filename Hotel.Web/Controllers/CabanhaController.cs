using Hotel.Web.Models;
using LogicaAccesoDatos.RepositoriosEF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Hotel.LogicaAccesoDatos.RepositoriosEF;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Hotel.Web.Controllers
{
    public class CabanhaController : Controller
    {

        //Foto
        private readonly ILogger<CabanhaController> _logger;
        private IWebHostEnvironment _environment;


        private IRepositorioCabanha _repoCabanha = new RepositorioCabanha();
        private IRepositorioTipoCabanha _repoTipoCabanha = new RepositorioTipoCabanha();
        private IRepositorioTopesDescripcion _repoTopeDescripcion = new RepositorioTopesDescripcion();

        public CabanhaController(ILogger<CabanhaController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        // GET: CabanhaController
        public ActionResult Index(string buscarTexto, int? tipoCabanha, int? numMaxPersonas, bool cabHabilitada, bool? reset)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var q = _repoTipoCabanha.FindAll();
            if (!q.Any())
            {
                ViewBag.TipoCabanhas = "No existen tipos de cabañas";
            }
            ViewBag.TipoCabanhas = q;

            try
            {
                IEnumerable<Cabanha> cabanhas = _repoCabanha.FindAll();

                if (!cabanhas.Any())
                {
                    ViewBag.ListaVacia = $"La lista de cabañas se encuentra vacía.";
                    return View();
                }

                if (!string.IsNullOrEmpty(buscarTexto))
                {
                    var buscado = _repoCabanha.BuscarPorTextoEnNombre(buscarTexto);
                    if (!buscado.Any())
                    {
                        ViewBag.ListaVacia = $"No existe una cabaña registrada con el nombre {buscarTexto}";
                        return View();
                    }
                    return View(buscado);
                }

                if (tipoCabanha != null)
                {
                    var buscado = _repoCabanha.BuscarPorTipo(tipoCabanha);
                    if (!buscado.Any())
                    {
                        var obtenerNombre = _repoTipoCabanha.FindById(tipoCabanha);
                        ViewBag.ListaVacia = $"No hay cabañas registradas de tipo {obtenerNombre.Nombre}";
                        return View();
                    }
                    return View(buscado);
                }

                if (numMaxPersonas != null)
                {
                    var buscado = _repoCabanha.BuscarPorMaxPer(numMaxPersonas);
                    if (!buscado.Any())
                    {
                        ViewBag.ListaVacia = $"No hay cabañas para {numMaxPersonas} personas";
                        return View();
                    }
                    return View(buscado);
                }

                if (cabHabilitada)
                {
                    var buscado = _repoCabanha.BuscarPorHabilitada();
                    if (!buscado.Any())
                    {
                        ViewBag.ListaVacia = $"No hay cabañas habilitadas";
                        return View();
                    }
                    return View(buscado);
                }

                if (reset != null)
                {
                    var buscado = _repoCabanha.FindAll();
                    if (!buscado.Any())
                    {
                        ViewBag.ListaVacia = $"No hay cabañas";
                        return View();
                    }
                    return View(buscado);
                }

                return View(cabanhas);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();

            }
        }

        // GET: CabanhaController/Details/5
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var cabanha = _repoCabanha.FindById(id);

            try
            {
                if (cabanha == null)
                {
                    ViewBag.Error = $"No hay una cabaña con el id {id}";
                    return View();
                }

                return View(cabanha);
            }
            catch
            {
                ViewBag.Error = $"No se puede obtener la cabaña con el id {id}";
                return View();
            }
        }

        // GET: CabanhaController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var q = _repoTipoCabanha.FindAll();
            if (q.Count() == 0)
            {
                TempData["TiposCabanhaVacios"] = "No hay tipos cabañas, por eso ha sido redirigido a esta vista para poder agregar un tipo de cabaña.";
                return RedirectToAction("Create", "TipoCabanha");
            }
            ViewBag.TipoCabanhas = q;
            return View();
        }

        // POST: CabanhaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CabanhaViewModel nuevaCabanha, IFormFile imagen)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var q = _repoTipoCabanha.FindAll().OrderBy(t => t.Nombre);
            ViewBag.TipoCabanhas = q;

            try
            {
                if (nuevaCabanha == null)
                {
                    return BadRequest("El tipo de cabaña es nulo, no se puede dar de alta un tipo de cabaña nulo");
                }


                var obtenerTipo = nuevaCabanha.MandarDatosCabanha();
                var valores = _repoTopeDescripcion.FindByNameObject(obtenerTipo.GetType().Name);
                if (valores == null)
                {
                    ViewBag.ErrorTope = $"No hay topes de caracteres registrados, por favor ingrese topes de caracteres y luego puede dar de alta una cabaña.";
                    return View();
                }

                if (nuevaCabanha.Descripcion.Trim().Length < valores.TopeMin || nuevaCabanha.Descripcion.Trim().Length > valores.TopeMax)
                {
                    ViewBag.Error = $"La descripción debe de contener entre {valores.TopeMin} y {valores.TopeMax} caracteres. Usted escribio {nuevaCabanha.Descripcion.Length} caracteres.";
                    return View();
                }

                GuardarImagen(imagen, nuevaCabanha);

                _repoCabanha.Add(nuevaCabanha.MandarDatosCabanha());
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException.InnerException is SqlException)
                {
                    SqlException sql = (SqlException)ex.InnerException.InnerException;
                    if (sql.Number == 2627)
                    {
                        ViewBag.Error = ex.Message;
                        return View();
                    }
                }
                ViewBag.Error = ex.Message;
                return View();

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        private void GuardarImagen(IFormFile imgCabanha, CabanhaViewModel nuevaCabanha)
        {
            if (imgCabanha == null)
                throw new Exception("Debe seleccionar una imagen.");

            if (nuevaCabanha == null)
                throw new Exception("Los datos de la cabaña no pueden estar vacios.");

            // SUBIR LA IMAGEN
            //ruta fisica de wwwroot
            string rutaFisicaWwwRoot = _environment.WebRootPath;
            string nombreImagen;
            if (Path.GetExtension(imgCabanha.FileName) == ".png"
                || Path.GetExtension(imgCabanha.FileName) == ".jpg"
                || Path.GetExtension(imgCabanha.FileName) == ".jpeg")
            {

                nombreImagen = nuevaCabanha.Nombre.Replace(" ", "_") + "_" + nuevaCabanha.Sec + Path.GetExtension(imgCabanha.FileName);
            }
            else
            {
                throw new Exception("Error de formato, el formato de imagen debe ser png, jpg o jpeg.");
            }
            string rutaFisicaFoto = Path.Combine(rutaFisicaWwwRoot, "img", "fotos", nombreImagen);

            //FileStream permite manejar archivos
            try
            {
                //el método using libera los recursos del objeto FileStream al finalizar 
                using (FileStream f = new FileStream(rutaFisicaFoto, FileMode.Create))
                {
                    //si fueran archivos grandes o si fueran varios, deberíamos usar la versión
                    //asincrónica de CopyTo, aquí no es necesario.
                    //sería: await foto.CopyToAsync (f);
                    imgCabanha.CopyTo(f);
                }
                //GUARDAR EL NOMBRE DE LA IMAGEN SUBIDA EN EL OBJETO
                nuevaCabanha.NombreFoto = nombreImagen.ToLower();
                //return true;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        // GET: CabanhaController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.TipoCabanhas = _repoTipoCabanha.FindAll().OrderBy(t => t.Nombre);
            var c = _repoCabanha.FindById(id);
            if (c != null)
                return View(c);

            ViewBag.Error = $"No se puede obtener la cabaña con el id {id}";
            return View();
        }

        // POST: CabanhaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cabanha unaCabanha)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.TipoCabanhas = _repoTipoCabanha.FindAll().OrderBy(t => t.Nombre);

            try
            {
                if (unaCabanha != null)
                {
                    
                    var valores = _repoTopeDescripcion.FindByNameObject(unaCabanha.GetType().Name);
                    if (valores == null)
                    {
                        ViewBag.ErrorTope = $"No hay topes de caracteres registrados, por favor ingrese topes de caracteres y luego puede dar de alta una cabaña.";
                        return View(unaCabanha);
                    }

                    if (unaCabanha.Descripcion.Trim().Length < valores.TopeMin || unaCabanha.Descripcion.Trim().Length > valores.TopeMax)
                    {
                        ViewBag.ErrorTope = $"La descripción debe de contener entre {valores.TopeMin} y {valores.TopeMax} caracteres. Usted escribio {unaCabanha.Descripcion.Length} caracteres.";
                        return View(unaCabanha);
                    }

                    _repoCabanha.Update(unaCabanha);
                    return RedirectToAction("Details", "Cabanha", new { id = unaCabanha.Id });
                }

                return BadRequest();

            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error en la cabaña con el id {id}. {ex.Message}";
                return View();
            }
        }

        // GET: CabanhaController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Cabanha cabanha = _repoCabanha.FindById(id);
            if (cabanha != null)
                return View(cabanha);

            ViewBag.Error = $"No se puede obtener la cabaña con el id {id}";
            return View();
        }

        // POST: CabanhaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Cabanha cabanha)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (cabanha == null)
                    return BadRequest("El id y la cabaña no deben ser nulos");

                _repoCabanha.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error id {id}. {ex.Message}";
                return View();
            }
        }

        // GET: CabanhaController/AgregarFoto/5
        public ActionResult AgregarFoto(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Cabanha cabanha = _repoCabanha.FindById(id);
            if (cabanha != null)
                return View(cabanha);

            ViewBag.Error = $"No se puede obtener la cabaña con el id {id}";
            return View();
        }

        [HttpPost]
        public ActionResult AgregarFoto(int id, CabanhaViewModel nuevaCabanha, IFormFile imagen)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                GuardarImagen(imagen, nuevaCabanha);
                _repoCabanha.Add(nuevaCabanha.MandarDatosCabanha());
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error id {id}. {ex.Message}";
                return View();
            }
        }

    }
}
