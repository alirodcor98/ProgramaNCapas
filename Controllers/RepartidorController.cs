using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class RepartidorController : Controller
    {
        // GET: Repartidor
        [HttpGet]
        public ActionResult GetAll()
        {
            Dictionary<string, object> result = BL.Repartidor.RepartidorGetAll();
            bool resultado = (bool)result["Resultado"];

            if(resultado == true)
            {
                ML.Repartidor repartidor = (ML.Repartidor)result["Repartidor"];
                return View(repartidor);
            }

            else
            {
                string excepcion = (string)result["Excepcion"];
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            ML.Repartidor repartidor = new ML.Repartidor();
            Dictionary<string, object> resultTransporte = BL.Transporte.GetAll();

            ML.Usuario usuario = new ML.Usuario();
            repartidor.Usuario = usuario;

            repartidor.Transporte = new ML.Transporte();

            if (idUsuario != null)
            {
                Dictionary<string, object> result = BL.Repartidor.RepartidorGetById(idUsuario.Value);
                bool resultado = (bool)result["Resultado"];
                Dictionary<string, object> resultUsuario = BL.Usuario.GetByIdEF(idUsuario.Value);
                repartidor.Usuario = (ML.Usuario)resultUsuario["Usuario"];
                if (resultado == true)
                {
                    repartidor = (ML.Repartidor)result["Repartidor"];

                    ML.Transporte transporte = (ML.Transporte)resultTransporte["Transporte"];
                    repartidor.Transporte.Transportes = transporte.Transportes;
                }
                else
                {
                    string excepcion = (string)result["Excepcion"];
                    ViewBag.Mensaje = "Ocurrio una excepcion: " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                ML.Transporte transporte = (ML.Transporte)resultTransporte["Transporte"];
                repartidor.Transporte.Transportes = transporte.Transportes;
            }

            bool correctTransporte = (bool)resultTransporte["Resultado"];
            if(correctTransporte == true)
            {
                return View(repartidor);
            }
            else
            {
                string excepcion = (string)resultTransporte["Excepcion"];
                ViewBag.Mensaje = "Ocurrio una excepcion: " + excepcion;
                return PartialView("Modal");
            }

        }

        [HttpPost]
        public ActionResult Form(ML.Repartidor repartidor)
        {
            HttpPostedFileBase file = Request.Files["amd"];
            if(file != null)
            {
                repartidor.Usuario.Imagen = ConvertToBytes(file);
            }
            
            if(repartidor.Usuario.IdUsuario > 0)
            {
                Dictionary<string, object> result = BL.Repartidor.RepartidorUpdate(repartidor);
                bool resultado = (bool)result["Resultado"];
                if(resultado == true)
                {
                    ViewBag.Mensaje = "Se ha actualizado el registro";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = (string)result["Excepcion"];
                    ViewBag.Mensaje = "Ocurrio una excepcion: " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                Dictionary<string, object> result = BL.Repartidor.RepartidorAdd(repartidor);
                bool resultado = (bool)result["Resultado"];
                if(resultado == true)
                {
                    ViewBag.Mensaje = "Se ha agregado el registro";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = (string)result["Excepcion"];
                    ViewBag.Mensaje = "Ha ocurrido una excepcion: " + excepcion;
                    return PartialView("Modal");
                }
            }
        }

        [HttpGet]
        public ActionResult Delete(int idUsuario) 
        {
            Dictionary<string, object> result = BL.Repartidor.RepartidorDelete(idUsuario);
            bool resultado = (bool)result["Resultado"];

            if(resultado == true)
            {
                ViewBag.Mensaje = "Se ha eliminado exitosamente el registro";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "Error al eliminar el registro: " + excepcion;
                return PartialView("Modal");
            }
        }

        public byte[] ConvertToBytes(HttpPostedFileBase foto)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(foto.InputStream);
            data = reader.ReadBytes((int)foto.ContentLength);
            return data;
        }
    }
}