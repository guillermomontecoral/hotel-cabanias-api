using Hotel.LogicaAccesoDatos.RepositoriosEF;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using LogicaAccesoDatos.RepositoriosEF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Web.Controllers
{
    public class MantenimientoController : Controller
    {
        private IRepositorioMantenimiento _repoMantenimeinto = new RepositorioMantenimiento();

        private IRepositorioCabanha _repoCabanha = new RepositorioCabanha();
        private IRepositorioTopesDescripcion _repoTopeDescripcion = new RepositorioTopesDescripcion();



        // GET: MantenimientoController
        public ActionResult Index(int id, DateTime? fecha1, DateTime? fecha2, bool? reset)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewBag.Cabanha = _repoCabanha.FindById(id);

            try
            {
                IEnumerable<Mantenimiento> mantenimientos = _repoMantenimeinto.FindAll(id);
                if (!mantenimientos.Any())
                {
                    ViewBag.ListaVacia = $"La lista de mantenimientos se encuentra vacía.";
                    return View();
                }

                if (fecha1 != null && fecha2 != null)
                {
                    var m = _repoMantenimeinto.MostrarMantenimientosEntreFechas(fecha1, fecha2);
                    if (!m.Any())
                    {
                        ViewBag.ListaVacia = $"No hay mantenimientos entre el {fecha1:dd/MM/yyyy} y el {fecha2:dd/MM/yyyy}";
                        return View();
                    }
                    return View(m);
                }

                if (reset != null)
                {
                    var m = _repoMantenimeinto.FindAll(id);
                    if (!m.Any())
                    {
                        ViewBag.ListaVacia = $"No hay mantenimientos";
                        return View();
                    }
                    return View(m);
                }

                return View(mantenimientos);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();

            }

        }

        // GET: MantenimientoController/Details/5
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: MantenimientoController/Create
        public ActionResult Create(int cabanhaId)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var c = _repoCabanha.FindById(cabanhaId);
            ViewBag.Cabanha = c;
            return View();
        }

        // POST: MantenimientoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Mantenimiento m)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var c = _repoCabanha.FindById(m.IdCabanha);
            ViewBag.Cabanha = c;
            try
            {
                if (m == null)
                {
                    return BadRequest("El objeto mantnimiento no puede ser nulo");
                }

                var valores = _repoTopeDescripcion.FindByNameObject(m.GetType().Name);
                if (valores == null)
                {
                    ViewBag.ErrorTope = $"No hay topes de caracteres registrados, por favor ingrese topes de caracteres y luego puede dar de alta un mantenimiento.";
                    return View();
                }

                if (m.Descripcion.Trim().Length < valores.TopeMin || m.Descripcion.Trim().Length > valores.TopeMax)
                {
                    ViewBag.Error = $"La descripción debe de contener entre 10 y 200 caracteres. Usted escribio {m.Descripcion.Length} caracteres.";
                    return View();
                }

                var cantMant = _repoMantenimeinto.ControlarRegistrosPorDia(m.Fecha, m.IdCabanha);
                if (cantMant == 3)
                {
                    ViewBag.Error = $"La cabaña ya cuenta con 3 mantenimientos para el día {m.Fecha.ToString("dd/MM/yyyy")}. Por favor ingrese el mantenimiento en otra fecha.";
                    return View();
                }

                _repoMantenimeinto.Add(m);
                return RedirectToAction("Index", new { id = m.IdCabanha });

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // GET: MantenimientoController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var editarM = _repoMantenimeinto.FindById(id);

            if (editarM != null)
            {
                
                ViewData["Fecha"] = editarM.Fecha;

                return View(editarM);
            }


            ViewBag.Error = $"No se puede obtener el mantenimiento con el id {id}";
            return View();
        }

        // POST: MantenimientoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Mantenimiento m)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (m != null)
                {
                    var valores = _repoTopeDescripcion.FindByNameObject(m.GetType().Name);
                    if (valores == null)
                    {
                        ViewBag.ErrorTope = $"No hay topes de caracteres registrados, por favor ingrese topes de caracteres y luego puede dar de alta una cabaña.";
                        return View(m);
                    }
                    if (m.Descripcion.Trim().Length < valores.TopeMin || m.Descripcion.Trim().Length > valores.TopeMax)
                    {
                        ViewBag.ErrorTope = $"La descripción debe de contener entre 10 y 200 caracteres. Usted escribio {m.Descripcion.Length} caracteres.";
                        return View(m);
                    }

                    _repoMantenimeinto.Update(m);
                    return RedirectToAction("Index", new { id = m.IdCabanha });
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error en el mantenimiento con el id {id}. {ex.Message}";
                return View();
            }
        }

        // GET: MantenimientoController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            Mantenimiento m = _repoMantenimeinto.FindById(id);
            if (m != null)
                return View(m);

            ViewBag.Error = $"No se puede obtener el mantenimiento con el id {id}";
            return View();
        }

        // POST: MantenimientoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Mantenimiento m)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var mantenimiento = _repoMantenimeinto.FindById(id);

            try
            {
                if (mantenimiento == null)
                    return BadRequest("El id y el tipo cabaña no deben ser nulos");

                _repoMantenimeinto.Delete(id);
                return RedirectToAction("Index", new { id = mantenimiento.IdCabanha });
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error id {id}. {ex.Message}";
                return View(mantenimiento);
            }
        }
    }
}
