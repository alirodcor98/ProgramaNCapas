using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebApi.Controllers
{
    public class TransporteController : ApiController
    {
        [HttpGet]
        [Route("api/Transporte/GetAll")]
        public IHttpActionResult GetAll()
        {
            Dictionary <string, object> result = BL.Transporte.GetAll();
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Error al obtener los datos");
            }
        }

        [HttpGet]
        [Route("api/Transporte/GetById[idTransporte]")]
        public IHttpActionResult GetById(int idTransporte)
        {
            Dictionary<string, object> result = BL.Transporte.GetById(idTransporte);
            bool resultado = (bool)result["Resultado"];
            if (resultado)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Error al obtener los datos");
            }
        }

        [HttpDelete]
        [Route("api/Transporte/Delete[idTransporte]")]
        public IHttpActionResult Delete(int idTransporte)
        {
            Dictionary<string, object> result = BL.Transporte.Delete(idTransporte);
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
        [Route("api/Transporte/Add")]
        public IHttpActionResult Add([FromBody]ML.Transporte transporte)
        {
            Dictionary<string, object> result = BL.Transporte.Add(transporte);
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

        [HttpPost]
        [Route("api/Transporte/Update")]
        public IHttpActionResult Update([FromBody]ML.Transporte transporte)
        {
            Dictionary<string,object> result = BL.Transporte.Update(transporte);
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
