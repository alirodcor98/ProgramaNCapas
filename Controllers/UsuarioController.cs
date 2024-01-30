using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Management ;
using System.Net.Http;
using System.Configuration;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            if(usuario.Nombre == null)
            {
                usuario.Nombre = "";
            }
            if(usuario.ApellidoPaterno == null)
            {
                usuario.ApellidoPaterno = "";
            }

            /*
            Dictionary<string, object> result = BL.Usuario.GetAllEF(user);
            bool resultado = (bool)result["Resultado"];
            */

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"].ToString());
                var responseTask = client.GetAsync($"Usuario/GetAll?Nombre={usuario.Nombre}&ApellidoPaterno={usuario.ApellidoPaterno}");
                responseTask.Wait();
                List<object> lista = new List<object>();

                var respuesta = responseTask.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    if(readTask.Result.TryGetValue("Usuarios", out object usuarioObject) && usuarioObject != null)
                    {
                        lista = Newtonsoft.Json.JsonConvert.DeserializeObject<List<object>>(usuarioObject.ToString());
                    }
                    usuario.Usuarios = new List<object>();
                    foreach(JObject user in lista)
                    {
                        usuario.Usuarios.Add(user.ToObject<ML.Usuario>());
                    }
                }

                else
                {
                    
                }
            }

                if (usuario.Usuarios != null)
                {
                    //ML.Usuario usuario = (ML.Usuario)result["Usuario"];
                    return View(usuario);
                }

                else
                {
                    return View();
                }
            
        }
        [HttpGet]
        public ActionResult Form(int? idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            Dictionary<string, object> resultRol = BL.Rol.GetAll();
            if (idUsuario != null)
            {
                //Dictionary<string, object> result = BL.Usuario.GetByIdEF(idUsuario.Value);
                //bool resultado = (bool)result["Resultado"];

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"].ToString());
                    var responseTask = client.GetAsync($"Usuario/GetById/{idUsuario}");
                    responseTask.Wait();

                    var respuesta = responseTask.Result;

                    if (respuesta.IsSuccessStatusCode)
                    {
                        var readTask = respuesta.Content.ReadAsAsync<ML.Usuario>();
                        readTask.Wait();
                        if(readTask.Result != null)
                        {
                            usuario = readTask.Result;
                        }
                    }
                }

                    if (usuario.IdUsuario != null)
                    {

                        ML.Rol rol = (ML.Rol)resultRol["Rol"];
                        //usuario.Rol = new ML.Rol();
                        usuario.Rol.Roles = rol.Roles;
                    /*
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                    */
                        Dictionary<string, object> resultEstado = BL.Estado.GetByIdPais((byte)usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                        ML.Estado estado = (ML.Estado)resultEstado["Estado"];
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = estado.Estados;

                        Dictionary<string, object> resultMunicipio = BL.Municipio.GetByIdEstado((byte)usuario.Direccion.Colonia.Municipio.Estado.idEstado);
                        ML.Municipio municipio = (ML.Municipio)resultMunicipio["Municipio"];
                        usuario.Direccion.Colonia.Municipio.Municipios = municipio.Municipios;

                        Dictionary<string, object> resultColonia = BL.Colonia.GetByIdMunicipio((byte)usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        ML.Colonia colonia = (ML.Colonia)resultColonia["Colonia"];
                        usuario.Direccion.Colonia.Colonias = colonia.Colonias;
                    }
                    else
                    {
                        ViewBag.Mensaje = "Ocurrio un error al recuperar la informacion";
                        return PartialView("Modal");
                    }
            }
            else
            {
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                ML.Rol rol = (ML.Rol)resultRol["Rol"];
                usuario.Rol = new ML.Rol();
                usuario.Rol.Roles = rol.Roles;
            }
            Dictionary<string, object> resultPais = BL.Pais.GetAll();
            bool rolCorrect = (bool)resultRol["Resultado"];
            if (rolCorrect == true)
            {

                ML.Pais pais = (ML.Pais)resultPais["Pais"];
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = pais.Paises;

                return View(usuario);
            }
            else
            {
                string excepcion = (string)resultRol["Excepcion"];
                ViewBag.Mensaje = "Ocurrio un error al recuperar la informacion" + excepcion;
                return PartialView("Modal");
            }

        }
        
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["amd"];

                if (file != null)
                {
                    usuario.Imagen = ConvertToBytes(file);
                }

                if (usuario.IdUsuario > 0)
                {
                    //llamar al update
                    //Dictionary<string, object> result = BL.Usuario.UpdateEF(usuario);

                    Dictionary<string, object> result = new Dictionary<string, object>();
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"].ToString());
                        var responseTask = client.PutAsJsonAsync("Usuario/Update", usuario);
                        responseTask.Wait();
                        var respuesta = responseTask.Result;

                        if (respuesta.IsSuccessStatusCode)
                        {
                            var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                            readTask.Wait();
                            result = readTask.Result;
                        }
                        else
                        {
                            var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                            readTask.Wait();
                            result = readTask.Result;
                        }
                    }

                    bool resultado = (bool)result["Resultado"];
                    if (resultado == true)
                    {
                        ViewBag.Mensaje = "Se ha actualizado el registro";
                        return PartialView("Modal");
                    }
                    else
                    {
                        string excepcion = (string)result["Excepcion"];
                        ViewBag.Mensaje = "NO se actualizo el registro: " + excepcion;
                        return PartialView("Modal");
                    }

                }
                
                else
                {
                    //Dictionary<string, object> result = BL.Usuario.AddEF(usuario);
                    Dictionary<string, object> result = new Dictionary<string, object>();
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"].ToString());
                        var responseTask = client.PostAsJsonAsync("Usuario/Add", usuario);
                        responseTask.Wait();
                        var respuesta = responseTask.Result;

                        if (respuesta.IsSuccessStatusCode)
                        {
                            var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                            readTask.Wait();
                            result = readTask.Result;
                        }
                        else
                        {
                            var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                            readTask.Wait();
                            result = readTask.Result;
                        }
                    }

                    bool resultado = (bool)result["Resultado"];
                    if (resultado == true)
                    {
                        ViewBag.Mensaje = "Se ha agregado un nuevo usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        string excepcion = (string)result["Excepcion"];
                        ViewBag.Mensaje = "NO se pudo registrar el nuevo usuario: " + excepcion;
                        return PartialView("Modal");
                    }
                }
            }
            else
            {
                Dictionary<string, object> resultPais = BL.Pais.GetAll();
                Dictionary<string, object> resultRol = BL.Rol.GetAll();

                ML.Rol rol = (ML.Rol)resultRol["Rol"];
                usuario.Rol.Roles = rol.Roles;

                ML.Pais pais = (ML.Pais)resultPais["Pais"];
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = pais.Paises;
                
                return View(usuario);
            }

            
        }
        [HttpGet]
        public ActionResult Delete(int idUsuario)
        {
            //Dictionary<string, object> result = BL.Usuario.DeleteEF(idUsuario);
            Dictionary<string, object> result = new Dictionary<string, object>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApi"].ToString());
                var responseTask = client.DeleteAsync("Usuario/Delete/" + idUsuario);
                responseTask.Wait();
                var respuesta = responseTask.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    result = readTask.Result;
                }
                else
                {
                    var readTask = respuesta.Content.ReadAsAsync<Dictionary<string, object>>();
                    readTask.Wait();
                    result = readTask.Result;
                }
            }
            bool resultado = (bool)result["Resultado"];
            if(resultado == true)
            {
                ViewBag.Mensaje = "Se ha eliminado exitosamente al usuario";
                return PartialView("Modal");
            }
            else
            {
                string excepcion = (string)result["Excepcion"];
                ViewBag.Mensaje = "NO se elimino el registro";
                return PartialView("Modal");
            }
        }

        public JsonResult EstadoGetByIdPais(byte idPais)
        {
            Dictionary<string, object> resultado = BL.Estado.GetByIdPais(idPais);
            ML.Estado estado = (ML.Estado)resultado["Estado"];
            return Json(estado.Estados, JsonRequestBehavior.AllowGet);

        }
       
        public JsonResult MunicipioGetByIdEstado(byte idEstado)
        {
            Dictionary<string, object> resultado = BL.Municipio.GetByIdEstado(idEstado);
            ML.Municipio municipio = (ML.Municipio)resultado["Municipio"];
            return Json(municipio.Municipios, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ColoniaGetByIdMunicipio(byte idMunicipio)
        {
            Dictionary<string,object> resultado = BL.Colonia.GetByIdMunicipio(idMunicipio);
            ML.Colonia colonia = (ML.Colonia)resultado["Colonia"];
            return Json(colonia.Colonias, JsonRequestBehavior.AllowGet);
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