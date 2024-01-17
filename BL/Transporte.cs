using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Transporte
    {
        public static Dictionary<string, object> Add(ML.Transporte transporte)
        {
			bool resultado = false;
			string excepcion = "";
			Dictionary <string,object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion }};
			try
			{
				using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
				{
					int filasAfectadas = context.TransporteAdd(transporte.NumeroPlaca, transporte.Modelo, transporte.Marca, 
						transporte.AnioFabricacion, transporte.Estatus.IdEstatus);
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
		
		public static Dictionary<string, object> Update(ML.Transporte transporte)
		{
			bool resultado = false;
			string excepcion = "";
			Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Tranporte", transporte}, {"Excepcion", excepcion} };
			try
			{
				using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
				{
					int filasAfectadas = context.TransporteUpdate(transporte.NumeroPlaca, 
						transporte.Modelo, transporte.Marca, transporte.AnioFabricacion,
						transporte.Estatus.IdEstatus, transporte.IdTransporte);

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
	
		public static Dictionary<string, object> Delete(int idTransporte)
		{
			bool resultado = false;
			string excepcion = "";
			Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion} };

			try
			{
				using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
				{
					int filasAfectadas = context.TransporteDelete(idTransporte);

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

				diccionario["Excepcion"] = ex.Message;
				diccionario["Resultado"] = false;
			}

			return diccionario;
		}
	
		public static Dictionary<string,object> GetAll()
		{
			bool resultado = false;
			string excepcion = "";
			ML.Transporte transporte = new ML.Transporte();
			Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", resultado }, { "Excepcion", excepcion }, { "Transporte", transporte } };
			using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
			{
				try
				{
					var lista = context.TransporteGetAll().ToList();
					if(lista != null)
					{
						transporte.Transportes = new List<object>();
						foreach(var itemTransporte in lista)
						{
							ML.Transporte transport = new ML.Transporte();
							transport.IdTransporte = itemTransporte.IdTransporte;
							transport.NumeroPlaca = itemTransporte.NumeroPlaca;
							transport.Modelo = itemTransporte.Modelo;
							transport.Marca = itemTransporte.Marca;
							transport.AnioFabricacion = (DateTime)itemTransporte.AnioFabricacion;
							transport.Estatus = new ML.EstatusTransporte();
							transport.Estatus.Estatus = itemTransporte.Estatus;

							transporte.Transportes.Add(transport);
						}
						diccionario["Transporte"] = transporte;
						diccionario["Resultado"] = true;
					}
					else
					{
						diccionario["Resultado"] = false;
					}
				}
				catch (Exception ex)
				{

					diccionario["Resultado"] = false;
					diccionario["Excepcion"] = ex.Message;
				}
			}
				return diccionario;
		}
	
		public static Dictionary<string,object> GetById(int idTransporte)
		{
			bool resultado = false;
			string excepcion = "";
			ML.Transporte transport = new ML.Transporte();
			Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", resultado}, {"Excepcion", excepcion}, {"Transporte", transport} };

			try
			{
				using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
				{
					var elemento = context.TransporteGetById(idTransporte).FirstOrDefault();

					if(elemento != null)
					{
						transport.IdTransporte = elemento.IdTransporte;
						transport.NumeroPlaca = elemento.NumeroPlaca;
						transport.Modelo = elemento.Modelo;
						transport.Marca = elemento.Marca;
						transport.AnioFabricacion = (DateTime)elemento.AnioFabricacion;
						transport.Estatus = new ML.EstatusTransporte();
						transport.Estatus.Estatus = elemento.Estatus;

						diccionario["Resultado"] = true;
						diccionario["Transporte"] = transport;
					}
					else
					{
						diccionario["Resultado"] = false;
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
