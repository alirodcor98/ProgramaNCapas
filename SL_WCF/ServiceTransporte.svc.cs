using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceTransporte" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceTransporte.svc o ServiceTransporte.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceTransporte : IServiceTransporte
    {
        public Result Add(ML.Transporte transporte)
        {
            Dictionary<string, object> diccionario = BL.Transporte.Add(transporte);
            SL_WCF.Result result = new SL_WCF.Result();
            result.Mensaje = (string)diccionario["Excepcion"];
            result.Resultado = (bool)diccionario["Resultado"];
            return result;
        }

        public Result Delete(int idTransporte)
        {
            Dictionary<string, object> diccionario = BL.Transporte.Delete(idTransporte);
            SL_WCF.Result result = new SL_WCF.Result();
            result.Mensaje = (string)diccionario["Excepcion"];
            result.Resultado = (bool)diccionario["Resultado"];
            return result;
        }

        public Result GetAll()
        {
            Dictionary<string, object> diccionario = BL.Transporte.GetAll();
            SL_WCF.Result result = new SL_WCF.Result();
            result.Mensaje = (string)diccionario["Excepcion"];
            result.Resultado = (bool)diccionario["Resultado"];
            ML.Transporte transportes = (ML.Transporte)diccionario["Transporte"];
            result.Objects = transportes.Transportes;
            return result;
        }

        public Result GetById(int idTransporte)
        {
            Dictionary<string, object> diccionario = BL.Transporte.GetById(idTransporte);
            SL_WCF.Result result = new SL_WCF.Result();
            result.Mensaje = (string)diccionario["Excepcion"];
            result.Resultado = (bool)diccionario["Resultado"];
            result.Object = (ML.Transporte)diccionario["Transporte"];
            return result;
        }

        public Result Update(ML.Transporte transporte)
        {
            Dictionary<string, object> diccionario = BL.Transporte.Update(transporte);
            SL_WCF.Result result = new SL_WCF.Result();
            result.Mensaje = (string)diccionario["Excepcion"];
            result.Resultado = (bool)diccionario["Resultado"];
            return result;
        }
    }
}
