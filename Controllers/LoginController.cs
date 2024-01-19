using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Dictionary<string, object> result = BL.Usuario.GetByEmail(email);
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                ML.Usuario usuario = (ML.Usuario)result["Usuario"];
                if(usuario.Password == password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Mensaje = "Credenciales incorrectas";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Mensaje = "Credenciales incorrectas";
                return PartialView("Modal");
            }
        }
    }
}