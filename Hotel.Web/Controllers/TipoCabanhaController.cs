using Hotel.LogicaAccesoDatos.RepositoriosEF;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using LogicaAccesoDatos.RepositoriosEF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hotel.Web.Controllers
{
    public class TipoCabanhaController : Controller
    {

        private IRepositorioTipoCabanha _repoTipoCabanha = new RepositorioTipoCabanha();
        private IRepositorioCabanha _repoCabanha = new RepositorioCabanha();
        private IRepositorioTopesDescripcion _repoTopeDescripcion = new RepositorioTopesDescripcion();


        // GET: TipoCabanhaController
        public ActionResult Index(string buscarNombre)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                IEnumerable<TipoCabanha> tipoCabanhas = _repoTipoCabanha.FindAll();

                if (!tipoCabanhas.Any())
                {
                    ViewBag.ListaVacia = $"La lista de tipos de cabañas se encuentra vacía.";
                    return View();
                }

                if (!string.IsNullOrEmpty(buscarNombre))
                {
                    var buscado = _repoTipoCabanha.BuscarTipoPorNombre(buscarNombre);
                    
                    if (buscado == null)
                    {
                        ViewBag.ListaVacia = $"No existe registrado un tipo de cabaña con el nombre {buscarNombre}";
                        return View();
                    }

                    var b = new[] { buscado };

                    return View(b);
                }

                return View(tipoCabanhas);

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();

            }
        }

        // GET: TipoCabanhaController/Details/5
        public ActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            TipoCabanha tipoCabanha = _repoTipoCabanha.FindById(id);
            if (tipoCabanha != null)
                return View(tipoCabanha);

            ViewBag.Error = $"No se puede obtener el tipo de cabaña con el id {id}";
            return View();
        }

        // GET: TipoCabanhaController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: TipoCabanhaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoCabanha nuevoTipo)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (nuevoTipo == null)
                {
                    return BadRequest("El tipo de cabaña es nulo, no se puede dar de alta un tipo de cabaña nulo");
                }

                if (!_repoTipoCabanha.NombreTipoExiste(nuevoTipo.Nombre))
                {
                    ViewBag.Error = $"Ya existe un tipo de cabaña con el nombre {nuevoTipo.Nombre}";
                    return View();
                }

                var valores = _repoTopeDescripcion.FindByNameObject(nuevoTipo.GetType().Name);
                if (valores == null)
                {
                    ViewBag.ErrorTope = $"No hay topes de caracteres registrados, por favor ingrese topes de caracteres y luego puede dar de alta un tipo cabaña.";
                    return View();
                }
                if (nuevoTipo.Descripcion.Trim().Length < valores.TopeMin || nuevoTipo.Descripcion.Trim().Length > valores.TopeMax)
                {
                    ViewBag.Error = $"La descripción debe de contener entre 10 y 200 caracteres. Usted escribio {nuevoTipo.Descripcion.Length} caracteres.";
                    return View();
                }

                _repoTipoCabanha.Add(nuevoTipo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(nuevoTipo);
            }
        }

        // GET: TipoCabanhaController/Edit/5
        public ActionResult Edit(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tipoCabanha = _repoTipoCabanha.FindById(id);
            if (tipoCabanha != null)
                return View(tipoCabanha);

            ViewBag.Error = $"No se puede obtener el tipo de cabaña con el id {id}";
            return View();
        }

        // POST: TipoCabanhaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TipoCabanha tipoCabanha)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            try
            {
                if (tipoCabanha != null)
                {
                    var valores = _repoTopeDescripcion.FindByNameObject(tipoCabanha.GetType().Name);
                    if (valores == null)
                    {
                        ViewBag.ErrorTope = $"No hay topes de caracteres registrados, por favor ingrese topes de caracteres y luego puede dar de alta una cabaña.";
                        return View(tipoCabanha);
                    }
                    if (tipoCabanha.Descripcion.Trim().Length < valores.TopeMin || tipoCabanha.Descripcion.Trim().Length > valores.TopeMax)
                    {
                        ViewBag.ErrorTope = $"La descripción debe de contener entre 10 y 200 caracteres. Usted escribio {tipoCabanha.Descripcion.Length} caracteres.";
                        return View(tipoCabanha);
                    }

                    _repoTipoCabanha.Update(tipoCabanha);
                    return RedirectToAction(nameof(Index));
                }

                return BadRequest("Hubo un error");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(tipoCabanha);
            }
        }

        // GET: TipoCabanhaController/Delete/5
        public ActionResult Delete(int id)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var tipoCabanha = _repoTipoCabanha.FindById(id);
            if (tipoCabanha != null)
                return View(tipoCabanha);

            ViewBag.Error = $"No se puede obtener el tipo de cabaña con el id {id}";
            return View();
        }

        // POST: TipoCabanhaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, TipoCabanha tipoCabanha)
        {
            if (HttpContext.Session.GetString("email") == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var t = _repoTipoCabanha.FindById(id);

            try
            {
                if (tipoCabanha == null)
                    return BadRequest("El id y el tipo cabaña no deben ser nulos");

                var existeRelacion = _repoCabanha.BuscarPorTipo(id);
                if (existeRelacion.Count() > 0)
                    throw new Exception("No se puede eliminar, está asociado con alguna cabaña.");

                _repoTipoCabanha.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException e)
            {
                ViewBag.Error = $"Error al eliminar el tipo de cabaña {tipoCabanha.Nombre}. Este error ocurrio ya que se encuentra asociado con alguna cabaña.";
                return View(tipoCabanha);
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error id {id}. {ex.Message}";
                return View(t);
            }
        }
    }
}
