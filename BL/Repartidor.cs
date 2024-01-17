using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Repartidor
    {
        public static Dictionary<string, object> RepartidorAdd(ML.Repartidor repartidor)    
        {
            bool resultado = false;
            string excepcion = "";
            Dictionary<string,object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.RepartidorAdd(repartidor.Usuario.Nombre, repartidor.Usuario.ApellidoPaterno,
                        repartidor.Usuario.Password, repartidor.Usuario.FechaNacimiento, repartidor.Usuario.Telefono,
                        repartidor.Usuario.Imagen, repartidor.Usuario.Sexo.ToString(), repartidor.Usuario.UserName, repartidor.Usuario.Email,
                        repartidor.Transporte.IdTransporte);

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
    
        public static Dictionary<string, object> RepartidorUpdate(ML.Repartidor repartidor)
        {
            bool resultado = false;
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.RepartidorUpdate(repartidor.Usuario.Nombre, repartidor.Usuario.ApellidoPaterno,
                        repartidor.Usuario.Password, repartidor.Usuario.FechaNacimiento, repartidor.Usuario.Telefono,
                        repartidor.Usuario.Imagen, repartidor.Usuario.Sexo.ToString(), repartidor.Usuario.UserName, repartidor.Usuario.Email,
                        repartidor.Transporte.IdTransporte, repartidor.Usuario.IdUsuario);

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
    
        public static Dictionary<string, object> RepartidorDelete(int idUsuario)
        {
            bool resultado = false;
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var filasAfectadas = context.RepartidorDelete(idUsuario);

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
    
        public static Dictionary<string, object> RepartidorGetAll()
        {
            bool resultado = false;
            string excepcion = "";
            ML.Repartidor repartidor = new ML.Repartidor();

            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion}, {"Repartidor", repartidor} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var lista = context.RepartidorGetAll().ToList();

                    if(lista != null)
                    {
                        repartidor.Repartidores = new List<ML.Repartidor>();
                        foreach (var registro in lista)
                        {
                            ML.Repartidor dealer = new ML.Repartidor();
                            dealer.Usuario = new ML.Usuario();
                            dealer.Usuario.IdUsuario = registro.IdUsuario;
                            dealer.Usuario.Imagen = registro.Imagen;
                            dealer.Usuario.Nombre = registro.Nombre;
                            dealer.Usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            dealer.Usuario.Telefono = registro.Telefono;
                            dealer.IdRepartidor = (int)registro.IdRepartidor;
                            dealer.FechaIngreso = (DateTime)registro.FechaIngreso;
                            dealer.Transporte = new ML.Transporte();
                            dealer.Transporte.IdTransporte = (int)registro.IdTransporte;
                            dealer.Transporte.NumeroPlaca = registro.NumeroPlaca;
                            dealer.Transporte.Modelo = registro.Modelo;
                            dealer.Transporte.Marca = registro.Marca;

                            repartidor.Repartidores.Add(dealer);
                        }
                        diccionario["Repartidor"] = repartidor;
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
    
        public static Dictionary<string, object> RepartidorGetById(int idUsuario) 
        {
            bool resultado = false;
            string excepcion = "";
            ML.Repartidor repartidor = new ML.Repartidor();
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion}, {"Repartidor", repartidor} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var registro = context.RepartidorGetById(idUsuario).SingleOrDefault();

                    if (registro != null)
                    {
                        repartidor.Usuario = new ML.Usuario();
                        repartidor.Usuario.IdUsuario = registro.IdUsuario;
                        repartidor.Usuario.Imagen = registro.Imagen;
                        repartidor.Usuario.Nombre = registro.Nombre;
                        repartidor.Usuario.ApellidoPaterno = registro.ApellidoPaterno;
                        repartidor.Usuario.Telefono = registro.Telefono;
                        repartidor.Usuario.Email = registro.Email;
                        repartidor.Usuario.UserName = registro.UserName;
                        repartidor.IdRepartidor = (int)registro.IdRepartidor;
                        repartidor.FechaIngreso = (DateTime)registro.FechaIngreso;
                        repartidor.Transporte = new ML.Transporte();
                        repartidor.Transporte.IdTransporte = (int)registro.IdTransporte;
                        repartidor.Transporte.NumeroPlaca = registro.NumeroPlaca;
                        repartidor.Transporte.Modelo = registro.Modelo;
                        repartidor.Transporte.Marca = registro.Marca;

                        diccionario["Resultado"] = true;
                        diccionario["Repartidor"] = repartidor;
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
