using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        public static Dictionary<string, object> GetAll()
        {
            string excepcion = "";
            ML.Rol rol = new ML.Rol();
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Rol", rol }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var objeto = context.RolGetAll().ToList();

                    if (objeto != null)
                    {
                        rol.Roles = new List<ML.Rol>();
                        foreach (var objRol in objeto)
                        {
                            ML.Rol rolobj = new ML.Rol();
                            rolobj.IdRol = objRol.IdRol;
                            rolobj.Tipo = objRol.Tipo;

                            rol.Roles.Add(rolobj);
                        }
                        diccionario["Resultado"] = true;
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
