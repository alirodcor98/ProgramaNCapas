using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class PaqueteController : Controller
    {
        // GET: Paquete
        [HttpGet]
        public ActionResult GetAll()
        {
            Dictionary<string, object> result = BL.Paquete.GetAll();
            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                ML.Paquete paquete = (ML.Paquete)result["Paquete"];
                return View(paquete);
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int idPaquete)
        {
            Dictionary<string, object> result = BL.Paquete.Delete(idPaquete);
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                ViewBag.Mensaje = "Se ha eliminado exitosamente el registro del paquete";
                return PartialView("Modal");
            }
            else
            {
                ViewBag.Mensaje = "Ocurrio un error, el registro del paquete NO fue eliminado";
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult Form(int? idPaquete)
        {
            ML.Paquete paquete = new ML.Paquete();
            if(idPaquete != null)
            {
                Dictionary<string, object> result = BL.Paquete.GetById(idPaquete.Value);
                bool resultado = (bool)result["Resultado"];

                if (resultado)
                {
                    paquete = (ML.Paquete)result["Paquete"];
                    return View(paquete);
                }
                else
                {
                    ViewBag.Mensaje = "Error al consultar información del registro";
                    return PartialView("Modal");
                }
            }
            else
            {
                return View(paquete);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Paquete paquete)
        {
            if(paquete.IdPaquete > 0)
            {
                Dictionary<string, object> result = BL.Paquete.Update(paquete);
                bool resultado = (bool)result["Resultado"];
                if (resultado)
                {
                    ViewBag.Mensaje = "Se actualizo correctamente el registro del paquete";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error, NO se actualizo el registro del paquete";
                    return PartialView("Modal");
                }
            }
            else
            {
                Dictionary<string, object> result = BL.Paquete.Add(paquete);
                bool resultado = (bool)result["Resultado"];
                if (resultado)
                {
                    ViewBag.Mensaje = "Se agrego correctamente el registro del paquete";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error, NO se agrego el registro del paquete";
                    return PartialView("Modal");
                }
            }
        }
    }
}