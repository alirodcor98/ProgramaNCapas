using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("api/Usuario/GetAll")]
        public IHttpActionResult GetAll(string Nombre, string ApellidoPaterno)
        {

            ML.Usuario usuario = new ML.Usuario(Nombre, ApellidoPaterno);
            if (usuario.Nombre == null)
            {
                usuario.Nombre = "";
            }
            if (usuario.ApellidoPaterno == null)
            {
                usuario.ApellidoPaterno = "";
            }


            Dictionary<string, object> result = BL.Usuario.GetAllEF(usuario);
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                usuario = (ML.Usuario)result["Usuario"];
                return Content(HttpStatusCode.OK, usuario);
            }
            else
            {
                return BadRequest("Error al obtener los datos");
            }
        }

        [HttpGet]
        [Route("api/Usuario/GetById/{idUsuario}")]
        public IHttpActionResult GetById(int idUsuario)
        {
            Dictionary<string, object> result = BL.Usuario.GetByIdEF(idUsuario);
            bool resultado = (bool)result["Resultado"];
            ML.Usuario usuario = new ML.Usuario();
            if (resultado)
            {
                usuario = (ML.Usuario)result["Usuario"];
                return Content(HttpStatusCode.OK, usuario);
            }
            else
            {
                return BadRequest("Error al obtener los datos");
            }
        }

        [HttpDelete]
        [Route("api/Usuario/Delete/{idUsuario}")]
        public IHttpActionResult Delete(int idUsuario)
        {
            Dictionary<string, object> result = BL.Usuario.DeleteEF(idUsuario);
            bool resultado = (bool)result["Resultado"];

            if (resultado)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Error al borrar el registro");
            }
        }

        [HttpPost]
        [Route("api/Usuario/Add")]
        public IHttpActionResult Add([FromBody] ML.Usuario usuario)
        {
            Dictionary<string, object> result = BL.Usuario.AddEF(usuario);
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Error al agregar el registro");
            }
        }

        [HttpPut]
        [Route("api/Usuario/Update")]
        public IHttpActionResult Update([FromBody] ML.Usuario usuario)
        {
            Dictionary<string, object> result = BL.Usuario.UpdateEF(usuario);
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Error al actualizar el registro");
            }
        }

    }
}
