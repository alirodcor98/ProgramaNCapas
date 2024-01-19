using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class CargaMasivaController : Controller
    {
        // GET: CargaMasiva
        [HttpGet]
        public ActionResult Carga()
        {
            return View();
        }
        /*
        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase txt)
        {
            try
            {
                StreamReader reader = new StreamReader(txt.InputStream);
                string line = reader.ReadLine();
                int ejecucionCarga = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    string[] datos = line.Split('|');
                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Nombre = datos[0].ToString();
                    usuario.ApellidoPaterno = datos[1].ToString();
                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = Convert.ToByte(datos[2].ToString());
                    usuario.ApellidoMaterno = datos[3].ToString();
                    usuario.Password = datos[4].ToString();
                    usuario.FechaNacimiento = Convert.ToDateTime(datos[5].ToString());
                    usuario.Telefono = datos[6].ToString();
                    usuario.Celular = datos[7].ToString();
                    usuario.CURP = datos[8].ToString();
                    usuario.Sexo = "H";
                    usuario.UserName = datos[9].ToString();
                    usuario.Email = datos[10].ToString();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = datos[12].ToString();
                    usuario.Direccion.NumeroExterior = datos[13].ToString();
                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = Convert.ToByte(datos[14].ToString());

                    BL.Usuario.AddEF(usuario);

                    ejecucionCarga++;

                }
                reader.Close();

                if (ejecucionCarga > 1)
                {
                    ViewBag.Mensaje = "Se han insertado los registros";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Mensaje = "Ha ocurrido un error";
                    return PartialView("Modal");
                }
            }
            catch (Exception e)
            {
                ViewBag.Mensaje = "Ha ocurrido una excepcion: " + e.Message;
                return PartialView("Modal");
            }
            
        }

        [HttpGet]
        public ActionResult CargaMasivaExcel()
        {
            return View();
        }
        */
        [HttpPost]
        public ActionResult Carga(HttpPostedFileBase xlsx)
        {
            if (Session["pathExcel"] == null)
            {
                if (xlsx != null)
                {
                    string extensionArchivo = Path.GetExtension(xlsx.FileName).ToLower();
                    string extensionValida = ConfigurationManager.AppSettings["formatoValido"];

                    if (extensionArchivo == extensionValida)
                    {
                        string rutaProyecto = Server.MapPath("~/CargaMasiva/");
                        string filePath = rutaProyecto + Path.GetFileNameWithoutExtension(xlsx.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        if (!System.IO.File.Exists(filePath))
                        {
                            xlsx.SaveAs(filePath);

                            string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filePath;
                            Dictionary<string, object> resultUsuarios = BL.Usuario.LeerExcel(connectionString);
                            bool resultado = (bool)resultUsuarios["Resultado"];

                            if (resultado)
                            {
                                Dictionary<string, object> resultValidacion = BL.Usuario.ValidarExcel((List<ML.Usuario>)resultUsuarios["Objects"]);
                                ML.ResultExcel resultErrores = new ML.ResultExcel();
                                resultErrores.Objects = (List<object>)resultValidacion["Objects"];
                                if (((List<object>)resultValidacion["Objects"]).Count > 0)
                                {
                                    return View(resultErrores);
                                }
                                else
                                {
                                    resultValidacion["Resultado"] = true;
                                    Session["pathExcel"] = filePath;
                                    return View(resultErrores);
                                }
                            }
                            else
                            {
                                ViewBag.Mensaje = (string)resultUsuarios["Excepcion"];
                                return PartialView("Modal");
                            }
                        }
                        else
                        {
                            ViewBag.Mensaje = "Error en la ruta del archivo";
                            return PartialView("Modal");
                        }

                    }
                    else
                    {
                        ViewBag.Mensaje = "Extension del archivo no valida";
                        return PartialView("Modal");
                    }
                }

                else
                {
                    ViewBag.Mensaje = "El archivo es nulo";
                    return PartialView("Modal");
                }
            }
            else
            {
                string filepath = Session["pathExcel"].ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["OleDbConnection"] + filepath;
                Dictionary<string, object> resultUsuarios = BL.Usuario.LeerExcel(connectionString);
                bool resultado = (bool)resultUsuarios["Resultado"];

                if (resultado)
                {
                    foreach(ML.Usuario usuario in (List<ML.Usuario>)resultUsuarios["Objects"])
                    {
                        BL.Usuario.AddEF(usuario);
                    }
                    Session["pathExcel"] = null;
                    ViewBag.Mensaje = "Los datos han sido cargados correctamente";
                    return PartialView("Modal");
                }

                return View();
            }
        }
    }
}