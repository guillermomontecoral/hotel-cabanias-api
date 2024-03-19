using LogicaAccesoDatos.RepositoriosEF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Web.Controllers
{
    public class UsuarioController : Controller
    {

        private IRepositorioUsuario _repoUsuarios = new RepositorioUsuario();
        // GET: UsuarioController
        public ActionResult Index()
        {
            try
            {
                PrecargarUsuarios();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException ex)
            {
                TempData["ErrorUsuarios"] = $"Se ha producido un error. {ex.InnerException.Message} Verifique la base de datos.";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                //ViewBag.Error = ex.Message;
                TempData["ErrorUsuarios"] = ex.Message;
                return RedirectToAction("Index", "Home");
            }

        }

        private void PrecargarUsuarios()
        {
            Usuario usu_1 = new Usuario("d@d.com", "123Abc");
            Usuario usu_2 = new Usuario("e@e.com", "123Abc");
            Usuario usu_3 = new Usuario("f@f.com", "123Abc");
            Usuario usu_4 = new Usuario("g@g.com", "123Abc");
            Usuario usu_5 = new Usuario("g@g.com", "123Abc"); //Tira error
            Usuario usu_6 = new Usuario("dd.d", "123Abc"); //Tira error

            
            _repoUsuarios.Add(usu_1);
            _repoUsuarios.Add(usu_2);
            _repoUsuarios.Add(usu_3);
            _repoUsuarios.Add(usu_4);
            _repoUsuarios.Add(usu_5);
            _repoUsuarios.Add(usu_6);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
