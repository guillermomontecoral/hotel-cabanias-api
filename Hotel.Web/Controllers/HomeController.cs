using Hotel.LogicaAccesoDatos.RepositoriosEF;
using Hotel.LogicaNegocio.Entidades;
using Hotel.LogicaNegocio.InterfacesRepositorios;
using Hotel.Web.Models;
using LogicaAccesoDatos.RepositoriosEF;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;

namespace Hotel.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepositorioUsuario _repoUsuarios = new RepositorioUsuario();
        private IRepositorioTopesDescripcion _repoTopeDescripcion = new RepositorioTopesDescripcion();


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            string? nombrelog = HttpContext.Session.GetString("email");
            ViewBag.Email = $"{nombrelog}";
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string clave)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(clave))
            {
                ViewBag.LlenarCampos = "Debe completar los campos para poder iniciar sesión";
                return View();                
            }

            try
            {
                _repoUsuarios.ValidarLogin(email, clave);
                HttpContext.Session.SetString("email", email);
                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.msg = ex.Message;
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}