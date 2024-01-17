using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static Dictionary<string,object> GetByIdPais(byte idPais)
        {
            bool resultado = false;
            string excepcion = "";
            ML.Estado estado = new ML.Estado();
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Excepcion", excepcion}, {"Resultado", resultado}, {"Estado", estado} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var registro = context.EstadoGetByIdPais(idPais).ToList();
                    if(registro != null)
                    {
                        estado.Estados = new List<ML.Estado>();
                        foreach(var item in registro)
                        {
                            ML.Estado state = new ML.Estado();
                            state.idEstado = item.IdEstado;
                            state.Nombre = item.Nombre;

                            estado.Estados.Add(state);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Estado"] = estado;
                    }
                    else
                    {
                        diccionario["Resultado"] = false;
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
