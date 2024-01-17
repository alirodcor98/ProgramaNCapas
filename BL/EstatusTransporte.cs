using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EstatusTransporte
    {
        public static Dictionary<string,object> GetAll()
        {
            bool resultado = false;
            string excepcion = "";
            ML.EstatusTransporte estatus = new ML.EstatusTransporte();
            Dictionary<string,object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion}, {"Estatus", estatus} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var lista = context.EstatusTransporteGetAll().ToList();
                    if(lista != null)
                    {
                        estatus.EstatusTransportes = new List<ML.EstatusTransporte>();
                        foreach (var item in lista)
                        {
                            ML.EstatusTransporte status = new ML.EstatusTransporte();
                            status.IdEstatus = item.IdEstatus;
                            status.Estatus = item.Estatus;

                            estatus.EstatusTransportes.Add(status);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Estatus"] = estatus;
                    }
                }
            }
            catch (Exception ex)
            {

                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }
    }
}
