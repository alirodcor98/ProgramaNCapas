using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static Dictionary<string, object> GetByIdMunicipio(byte idMunicipio)
        {
            bool resultado = false;
            string excepcion = "";
            ML.Colonia colonia = new ML.Colonia();
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", resultado}, { "Excepcion", excepcion}, { "Colonia", colonia} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var registro = context.ColoniaGetByIdMunicipio(idMunicipio).ToList();
                    if(registro != null)
                    {
                        colonia.Colonias = new List<ML.Colonia>();
                        foreach(var item in registro)
                        {
                            ML.Colonia colon = new ML.Colonia();
                            colon.IdColonia = item.IdColonia;
                            colon.Nombre = item.Nombre;

                            colonia.Colonias.Add(colon);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Colonia"] = colonia;
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
