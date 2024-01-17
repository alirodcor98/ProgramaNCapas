using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Pais
    {
        public static Dictionary<string,object> GetAll()
        {
            bool resultado = false;
            string excepcion = "";
            ML.Pais pais = new ML.Pais();
            Dictionary<string, object> diccionario = new Dictionary<string, object>(){ { "Resultado", resultado }, {"Excepcion", excepcion}, {"Pais", pais} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var lista = context.PaisGetAll().ToList();
                    if(lista != null)
                    {
                        pais.Paises = new List<ML.Pais>();
                        foreach(var item in lista)
                        {
                            ML.Pais country = new ML.Pais();
                            country.IdPais = item.IdPais;
                            country.Nombre = item.Nombre;

                            pais.Paises.Add(country);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Pais"] = pais;
                    }
                }
            }
            catch (Exception ex)
            {

                diccionario["Excepcion"] = ex.Message;
                diccionario["Resultado"] = false;
            }

            return diccionario;
        } 
    }
}
