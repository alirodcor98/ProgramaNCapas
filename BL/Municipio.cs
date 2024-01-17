using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static Dictionary<string,object> GetByIdEstado(byte idEstado)
        {
            bool resultado = false;
            string excepcion = "";
            ML.Municipio municipio = new ML.Municipio();
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Excepcion", excepcion}, {"Resultado", resultado}, {"Municipio", municipio} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var registro = context.MunicipioGetByIdEstado(idEstado).ToList();
                    if(registro != null)
                    {
                        municipio.Municipios = new List<ML.Municipio>();
                        foreach(var item in registro)
                        {
                            ML.Municipio municipal = new ML.Municipio();
                            municipal.IdMunicipio = item.IdMunicipio;
                            municipal.Nombre= item.Nombre;

                            municipio.Municipios.Add(municipal);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Municipio"] = municipio;
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
