using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Paquete
    {
        public static Dictionary<string, object> Add(ML.Paquete paquete)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Excepcion", ""}, {"Resultado", false}};

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.PaqueteAdd(paquete.InstruccionEntrega, paquete.Peso, paquete.DireccionOrigen, paquete.DireccionEntrega);

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
    
        public static Dictionary<string, object> Delete(int idPaquete)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""} };
            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.PaqueteDelete(idPaquete);

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
    
        public static Dictionary<string, object> Update(ML.Paquete paquete)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.PaqueteUpdate(paquete.InstruccionEntrega, paquete.Peso, paquete.DireccionOrigen,
                        paquete.DireccionEntrega, paquete.IdPaquete);

                    if(filasAfectadas > 0)
                    {
                        diccionario["Resultado"] = true;
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
    
        public static Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Paquete", null} };
            ML.Paquete paquete = new ML.Paquete();

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var listaPaquetes = context.PaqueteGetAll().ToList();

                    if(listaPaquetes != null)
                    {
                        paquete.Paquetes = new List<ML.Paquete>();
                        foreach(var registro in listaPaquetes)
                        {
                            ML.Paquete package = new ML.Paquete();
                            package.IdPaquete = registro.IdPaquete;
                            package.InstruccionEntrega = registro.InstruccionEntrega;
                            package.Peso = registro.Peso;
                            package.DireccionOrigen = registro.DireccionOrigen;
                            package.DireccionEntrega = registro.DireccionEntrega;
                            package.FechaEstimadaEntrega = registro.FechaEstimadaEntrega;
                            package.NumeroGuia = registro.NumeroGuia;

                            paquete.Paquetes.Add(package);
                        }

                        diccionario["Resultado"] = true;
                        diccionario["Paquete"] = paquete;
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
    
        public static Dictionary<string, object> GetById(int idPaquete)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Paquete", null} };
            ML.Paquete paquete = new ML.Paquete();

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var registro = context.PaqueteGetById(idPaquete).SingleOrDefault();

                    if(registro != null)
                    {
                        paquete.IdPaquete = registro.IdPaquete;
                        paquete.InstruccionEntrega = registro.InstruccionEntrega;
                        paquete.Peso = registro.Peso;
                        paquete.DireccionOrigen = registro.DireccionOrigen;
                        paquete.DireccionEntrega = registro.DireccionEntrega;
                        paquete.FechaEstimadaEntrega = registro.FechaEstimadaEntrega;
                        paquete.NumeroGuia = registro.NumeroGuia;

                        diccionario["Resultado"] = true;
                        diccionario["Paquete"] = paquete;
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
