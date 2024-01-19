using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebSockets;
using WebGrease.Css.Ast;

namespace PL_MVC.Controllers
{
    public class TransporteController : Controller
    {
        // GET: Transporte
        [HttpGet]
        public ActionResult GetAll()
        {
            ServiceTransporte.ServiceTransporteClient serviceTransporte = new ServiceTransporte.ServiceTransporteClient();
            var result = serviceTransporte.GetAll();
            //Dictionary<string, object> result = serviceTransporte.GetAll();
            //Dictionary<string, object> result = BL.Transporte.GetAll();
            bool resultado = result.Resultado;
            if(resultado == true)
            {
                ML.Transporte transporte = new ML.Transporte();
                transporte.Transportes = result.Objects.ToList();
                return View(transporte);
            }
            else
            {
                string excepcion = result.Mensaje;
                return View();
            }
        }
        [HttpGet]
        public ActionResult Form(int? idTransporte)
        {
            Dictionary<string, object> resultEstatus = BL.EstatusTransporte.GetAll();
            bool correctEstatus = (bool)resultEstatus["Resultado"];
            ML.EstatusTransporte estatus = (ML.EstatusTransporte)resultEstatus["Estatus"];
            ML.Transporte transporte = new ML.Transporte();
            transporte.Estatus = new ML.EstatusTransporte();
            transporte.Estatus.EstatusTransportes = estatus.EstatusTransportes;
            if(idTransporte != null)
            {
                ServiceTransporte.ServiceTransporteClient serviceTransporte = new ServiceTransporte.ServiceTransporteClient();
                var result = serviceTransporte.GetById(idTransporte.Value);
                //Dictionary<string, object> result = serviceTransporte.GetById(idTransporte.Value);
                //Dictionary<string, object> result = BL.Transporte.GetById(idTransporte.Value);
                bool resultado = result.Resultado;
                if(resultado == true && correctEstatus == true)
                {
                    transporte = (ML.Transporte)result.Object;
                    transporte.Estatus = new ML.EstatusTransporte();
                    transporte.Estatus.EstatusTransportes = estatus.EstatusTransportes;
                    return View(transporte);
                }
                else
                {
                    string excepcion = result.Mensaje;
                    ViewBag.Mensaje = "Ocurrio una excepcion inesperada: " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                return View(transporte);
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Transporte transporte)
        {
            if(transporte.IdTransporte > 0)
            {
                //Update
                ServiceTransporte.ServiceTransporteClient serviceTransporte = new ServiceTransporte.ServiceTransporteClient();
                var result = serviceTransporte.Update(transporte);
                //Dictionary<string, object> result = serviceTransporte.Update(transporte);
                //Dictionary<string, object> result = BL.Transporte.Update(transporte);
                bool resultado = result.Resultado;
                if(resultado == true)
                {
                    ViewBag.Mensaje = "Se ha actualizado correctamente este registro";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = result.Mensaje;
                    ViewBag.Mensaje = "Ha ocurrido una excepcion inesperada: " + excepcion;
                    return PartialView("Modal");
                }
            }
            else
            {
                //Add
                ServiceTransporte.ServiceTransporteClient serviceTransporte = new ServiceTransporte.ServiceTransporteClient();
                var result = serviceTransporte.Add(transporte);
                //Dictionary<string, object> result = serviceTransporte.Add(transporte);
                //Dictionary<string, object> result = BL.Transporte.Add(transporte);
                bool resultado = result.Resultado;
                if(resultado == true)
                {
                    ViewBag.Mensaje = "Se ha agregado el registro correctamente";
                    return PartialView("Modal");
                }
                else
                {
                    string excepcion = result.Mensaje;
                    ViewBag.Mensaje = "Ha ocurrido una excepcion inesperada: " + excepcion;
                    return PartialView("Modal");
                }
            }
        }

        public ActionResult Delete(int idTransporte)
        {
            ServiceTransporte.ServiceTransporteClient serviceTransporte = new ServiceTransporte.ServiceTransporteClient();
            var result = serviceTransporte.Delete(idTransporte);
            //Dictionary<string,object> result = serviceTransporte.Delete(idTransporte);
            //Dictionary<string, object> result = BL.Transporte.Delete(idTransporte);
            bool resultado = result.Resultado;
            if(resultado == true)
            {
                ViewBag.Mensaje = "Se ha borrado el registeo exitosamente";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = result.Mensaje;
                ViewBag.Mensaje = "Ha ocurrido una excepcion inesperada: " + excepcion;
                return PartialView("Modal");
            }
        }
    }
}