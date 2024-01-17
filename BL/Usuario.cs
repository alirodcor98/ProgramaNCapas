using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Linq.Expressions;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        //Insertar informacion

        /*
        public static bool Add(ML.Usuario usuario)
        {
            bool result = false;
            try
            {
                //Abrir conexion
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    //query a ejecutar
                    var sentencia = "INSERT INTO Usuario(Nombre, ApellidoPaterno, Edad, EstadoDeNacimiento, Peso) VALUES (@Nombre, @ApellidoPaterno, @Edad, @EstadoDeNacimiento, @Peso)";

                    //estableciendo parametros y dandoles valor
                    SqlParameter[] parametros = new SqlParameter[5];
                    parametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametros[0].Value = usuario.Nombre;
                    parametros[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    parametros[1].Value = usuario.ApellidoPaterno;
                    parametros[2] = new SqlParameter("@Edad", SqlDbType.Int);
                    parametros[2].Value = usuario.Edad;
                    parametros[3] = new SqlParameter("@EstadoDeNacimiento", SqlDbType.VarChar);
                    parametros[3].Value = usuario.EstadoDeNacimiento;
                    parametros[4] = new SqlParameter("@Peso", SqlDbType.Float);
                    parametros[4].Value = usuario.Peso;

                    //objeto que ejecuta la sentencia
                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);

                    //Iniciar conexion
                    command.Connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    //Validando si hubo filas afectadas
                    if(filasAfectadas > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }



                }

            }
            catch (Exception ex)
            {

                result = false;
            }
            return result;
        }
    
        public static bool Delete(int idUsuario)
        {
            bool result = false;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "DELETE FROM Usuario WHERE IdUsuario=@IdUsuario";

                    SqlParameter[] parametros = new SqlParameter[1];
                    parametros[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    parametros[0].Value = idUsuario;

                    //objeto que ejecuta la sentencia
                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);

                    //Iniciar conexion
                    command.Connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    //Validando si hubo filas afectadas
                    if (filasAfectadas > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }

            }
            catch (Exception ex)
            {

                result = false;
            }


            return result;
        }
    
        public static bool Update(int idUsuario,ML.Usuario usuario)
        {
            bool result = false;

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "UPDATE Usuario SET Nombre=@Nombre, ApellidoPaterno=@ApellidoPaterno, Edad=@Edad, EstadoDeNacimiento=@EstadoDeNacimiento, Peso=@Peso WHERE IdUsuario=@IdUsuario";

                    SqlParameter[] parametros = new SqlParameter[6];

                    parametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametros[0].Value = usuario.Nombre;
                    parametros[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    parametros[1].Value = usuario.ApellidoPaterno;
                    parametros[2] = new SqlParameter("@Edad", SqlDbType.Int);
                    parametros[2].Value = usuario.Edad;
                    parametros[3] = new SqlParameter("@EstadoDeNacimiento", SqlDbType.VarChar);
                    parametros[3].Value = usuario.EstadoDeNacimiento;
                    parametros[4] = new SqlParameter("@Peso", SqlDbType.Float);
                    parametros[4].Value = usuario.Peso;
                    parametros[5] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    parametros[5].Value = idUsuario;

                    //objeto que ejecuta la sentencia
                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);

                    //Iniciar conexion
                    command.Connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    //Validando si hubo filas afectadas
                    if (filasAfectadas > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }

                }

            }
            catch (Exception ex)
            {

                result = false;
            }

            return result;
        }
    
        public static ML.Usuario GetAll()
        {
            ML.Usuario user = new ML.Usuario();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "SELECT IdUsuario, Nombre, ApellidoPaterno, Edad, EstadoDeNacimiento, Peso FROM Usuario";

                    //Crear la tabla
                    DataTable tablaUsuario = new DataTable();
                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Connection.Open();
                    //Lee la informacion
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    //Mete la informacion leida en la tabla
                    adapter.Fill(tablaUsuario);
                    //Para la informacion de un datatable a mi modelo (ML.Usuario)
                    if(tablaUsuario.Rows.Count > 0)
                    {
                        user.Usuarios = new List<ML.Usuario>();
                        foreach (DataRow registro in tablaUsuario.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(registro[0].ToString());
                            usuario.Nombre = registro[1].ToString();
                            usuario.ApellidoPaterno = registro[2].ToString();
                            usuario.Edad = byte.Parse(registro[3].ToString());
                            usuario.EstadoDeNacimiento = registro[4].ToString();
                            usuario.Peso = float.Parse(registro[5].ToString());

                            user.Usuarios.Add(usuario);
                        }
                    }
                    else
                    {

                    }

                }

            }
            catch (Exception ex)
            {
                
            }
            return user;
        }
    
        public static ML.Usuario GetById(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "SELECT IdUsuario, Nombre, ApellidoPaterno, Edad, EstadoDeNacimiento, Peso FROM Usuario WHERE IdUsuario = @IdUsuario";

                    SqlParameter[] parametros = new SqlParameter[1];
                    parametros[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    parametros[0].Value = idUsuario;

                    //Crear la tabla
                    DataTable tablaUsuario = new DataTable();
                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);
                    command.Connection.Open();
                    //Lee la informacion
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    //Mete la informacion leida en la tabla
                    adapter.Fill(tablaUsuario);
                    //Para la informacion de un datatable a mi modelo (ML.Usuario)
                    if (tablaUsuario.Rows.Count > 0)
                    {
                        foreach (DataRow registro in tablaUsuario.Rows)
                        {
                            usuario.IdUsuario = int.Parse(registro[0].ToString());
                            usuario.Nombre = registro[1].ToString();
                            usuario.ApellidoPaterno = registro[2].ToString();
                            usuario.Edad = byte.Parse(registro[3].ToString());
                            usuario.EstadoDeNacimiento = registro[4].ToString();
                            usuario.Peso = float.Parse(registro[5].ToString());
                        }
                    }
                    else
                    {

                    }

                }

            }
            catch (Exception ex)
            {

                
            }
               
            
            return usuario;
        }
    
        public static bool AddSP(ML.Usuario usuario)
        {
            bool result = false;

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "UsuarioAdd";
                    SqlParameter[] parametros = new SqlParameter[5];
                    parametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametros[0].Value = usuario.Nombre;
                    parametros[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    parametros[1].Value = usuario.ApellidoPaterno;
                    parametros[2] = new SqlParameter("@Edad", SqlDbType.Int);
                    parametros[2].Value = usuario.Edad;
                    parametros[3] = new SqlParameter("@EstadoDeNacimiento", SqlDbType.VarChar);
                    parametros[3].Value = usuario.EstadoDeNacimiento;
                    parametros[4] = new SqlParameter("@Peso", SqlDbType.Float);
                    parametros[4].Value = usuario.Peso;

                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    if(filasAfectadas > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }

            }
            catch (Exception ex)
            {

                result = false;
            }

            return result;
        }
    
        public static bool DeleteSP(int idUsuario)
        {
            bool result = false;

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "UsuarioDelete";

                    SqlParameter[] parametros = new SqlParameter[1];
                    parametros[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    parametros[0].Value = idUsuario;

                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    if(filasAfectadas > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }

            }
            catch (Exception ex)
            {

                result = false;
            }

            return result;
        }
    
        public static bool UpdateSP(int idUsuario, ML.Usuario usuario)
        {
            bool result = false;

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "UsuarioUpdate";

                    SqlParameter[] parametros = new SqlParameter[6];
                    parametros[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                    parametros[0].Value = usuario.Nombre;
                    parametros[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                    parametros[1].Value = usuario.ApellidoPaterno;
                    parametros[2] = new SqlParameter("@Edad", SqlDbType.Int);
                    parametros[2].Value = usuario.Edad;
                    parametros[3] = new SqlParameter("@EstadoDeNacimiento", SqlDbType.VarChar);
                    parametros[3].Value = usuario.EstadoDeNacimiento;
                    parametros[4] = new SqlParameter("@Peso", SqlDbType.Float);
                    parametros[4].Value = usuario.Peso;
                    parametros[5] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    parametros[5].Value = idUsuario;

                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Connection.Open();
                    int filasAfectadas = command.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {

                result = false;
            }

            return result;
        }
    
        public static ML.Usuario GetAllSP()
        {
            ML.Usuario usuario = new ML.Usuario();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "UsuarioGetAll";

                    DataTable tablaUsuarios = new DataTable();
                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection.Open(); 

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(tablaUsuarios);

                    if(tablaUsuarios.Rows.Count > 0)
                    {
                        usuario.Usuarios = new List<ML.Usuario>();
                        foreach(DataRow registro in tablaUsuarios.Rows)
                        {
                            ML.Usuario user = new ML.Usuario();
                            user.IdUsuario = int.Parse(registro[0].ToString());
                            user.Nombre = registro[1].ToString();
                            user.ApellidoPaterno = registro[2].ToString();
                            user.Edad = byte.Parse(registro[3].ToString());
                            user.EstadoDeNacimiento = registro[4].ToString();
                            user.Peso = float.Parse(registro[5].ToString());

                            usuario.Usuarios.Add(user);
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                
            }

            return usuario;
        }

        public static ML.Usuario GetByIdSP(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.ObtenerConnectionString()))
                {
                    var sentencia = "UsuarioGetById";

                    SqlParameter[] parametros = new SqlParameter[1];
                    parametros[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    parametros[0].Value = idUsuario;

                    DataTable tablaUsuarios = new DataTable();
                    SqlCommand command = new SqlCommand(sentencia, context);
                    command.Parameters.AddRange(parametros);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Connection.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(tablaUsuarios);

                    if(tablaUsuarios.Rows.Count > 0)
                    {
                        foreach(DataRow registro in tablaUsuarios.Rows)
                        {
                            usuario.IdUsuario = int.Parse(registro[0].ToString());
                            usuario.Nombre = registro[1].ToString();
                            usuario.ApellidoPaterno = registro[2].ToString();
                            usuario.Edad = byte.Parse(registro[3].ToString());
                            usuario.EstadoDeNacimiento = registro[4].ToString();
                            usuario.Peso = float.Parse(registro[5].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return usuario;
        }
    */
        public static Dictionary<string,object> AddEF(ML.Usuario usuario)
        {
            bool result = false;
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", result}, { "Excepcion", excepcion } };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.Rol.IdRol, 
                        usuario.ApellidoMaterno, usuario.Password, usuario.FechaNacimiento, usuario.Telefono, usuario.Celular,
                        usuario.CURP, usuario.Sexo.ToString(), usuario.UserName, usuario.Email, usuario.Imagen, usuario.Direccion.Calle,
                        usuario.Direccion.NumeroExterior, usuario.IdUsuario, usuario.Direccion.Colonia.IdColonia);
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
    
        public static Dictionary<string,object> DeleteEF(int idUsuario)
        {
            bool result = false;
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Resultado", result }, { "Excepcion", excepcion } };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioDelete(idUsuario);
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
    
        public static Dictionary<string,object> UpdateEF(ML.Usuario usuario)
        {
            bool result = false;
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", result}, {"Excepcion", excepcion} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    int filasAfectadas = context.UsuarioUpdate(usuario.Nombre, usuario.ApellidoPaterno, usuario.Rol.IdRol,
                        usuario.ApellidoMaterno, usuario.Password, usuario.FechaNacimiento, usuario.Telefono, usuario.Celular,
                        usuario.CURP, usuario.Sexo.ToString(), usuario.UserName, usuario.Email, usuario.Imagen,
                        usuario.Direccion.Calle, usuario.Direccion.NumeroExterior, usuario.IdUsuario, usuario.Direccion.Colonia.IdColonia);
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
    
        public static Dictionary<string, object> GetAllEF(ML.Usuario user)
        {
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { { "Usuario", user }, { "Excepcion", excepcion }, { "Resultado", false } };
            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var lista = context.UsuarioGetAll(user.Nombre, user.ApellidoPaterno).ToList();

                    if(lista != null)
                    {
                        user.Usuarios = new List<object>();
                        foreach (var registro in lista)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = registro.IdUsuario;
                            usuario.Nombre = registro.NombreUsuario;
                            usuario.ApellidoPaterno = registro.ApellidoPaterno;
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.Tipo = registro.Tipo;
                            usuario.ApellidoMaterno = registro.ApellidoMaterno;
                            usuario.Password = registro.Password;
                            usuario.FechaNacimiento = registro.FechaNacimiento;
                            usuario.Telefono = registro.Telefono;
                            usuario.Celular = registro.Celular;
                            usuario.CURP = registro.CURP;
                            usuario.Sexo = registro.Sexo;
                            usuario.UserName = registro.UserName;
                            usuario.Email = registro.Email;
                            usuario.Imagen = registro.Imagen;
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Calle = registro.Calle;
                            usuario.Direccion.NumeroExterior= registro.NumeroExterior;
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Nombre = registro.NombreColonia;
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Nombre = registro.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.NombreEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = registro.NombrePais;

                            user.Usuarios.Add(usuario);
                        }
                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = user;
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

        public static Dictionary<string,object> GetByIdEF(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            string excepcion = "";
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Usuario", usuario}, {"Excepcion", excepcion}, {"Resultado", false} };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var registro = context.UsuarioGetById(idUsuario).Single();

                    if (registro != null)
                    {
                        usuario.IdUsuario = registro.IdUsuario;
                        usuario.Nombre = registro.NombreUsuario;
                        usuario.ApellidoPaterno = registro.ApellidoPaterno;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = (byte)registro.IdRol;
                        usuario.Rol.Tipo = registro.Tipo;
                        usuario.ApellidoMaterno = registro.ApellidoMaterno;
                        usuario.Password = registro.Password;
                        usuario.FechaNacimiento = registro.FechaNacimiento;
                        usuario.Telefono = registro.Telefono;
                        usuario.Celular = registro.Celular;
                        usuario.CURP = registro.CURP;
                        usuario.Sexo = registro.Sexo;
                        usuario.UserName = registro.UserName;
                        usuario.Email = registro.Email;
                        usuario.Imagen = registro.Imagen;
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = (int)registro.IdDireccion;
                        usuario.Direccion.Calle = registro.Calle;
                        usuario.Direccion.NumeroExterior = registro.NumeroExterior;
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = (byte)registro.IdColonia;
                        usuario.Direccion.Colonia.Nombre = registro.NombreColonia;
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = (byte)registro.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = registro.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.idEstado = (byte)registro.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = registro.NombreEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = (byte)registro.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = registro.NombrePais;

                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario;

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
    
        public static Dictionary<string,object> GetByEmail(string email)
        {
            bool resultado = false;
            string excepcion = "";
            ML.Usuario usuario = new ML.Usuario();
            Dictionary<string,object> diccionario = new Dictionary<string, object>() {{"Resultado", resultado}, {"Excepcion", excepcion}, { "Usuario", usuario } };

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var registro = context.UsuarioGetByEmail(email).SingleOrDefault();
                    if(registro != null)
                    {
                        usuario.Email = registro.Email;
                        usuario.Password = registro.Password;

                        diccionario["Resultado"] = true;
                        diccionario["Usuario"] = usuario;
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
        
        public static Dictionary<string, object> LeerExcel(string connectionString)
        {
            ML.Usuario usuario = new ML.Usuario();
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Resultado", false}, {"Excepcion", ""}, {"Objects", null} };

            try
            {
                using (OleDbConnection context = new OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();
                        da.Fill(tableUsuario);

                        if(tableUsuario.Rows.Count > 0)
                        {
                            usuario.Usuarios = new List<object>();

                            foreach(DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario user = new ML.Usuario();

                                user.Nombre = row[0].ToString();
                                user.ApellidoPaterno = row[1].ToString();
                                user.Rol = new ML.Rol();
                                user.Rol.IdRol = Convert.ToByte(row[2].ToString());
                                user.ApellidoMaterno = row[3].ToString();
                                user.Password = row[4].ToString();
                                user.FechaNacimiento = Convert.ToDateTime(row[5].ToString());
                                user.Telefono = row[6].ToString();
                                user.Celular = row[7].ToString();
                                user.CURP = row[8].ToString();
                                user.Sexo = row[9].ToString();
                                user.UserName = row[10].ToString();
                                user.Email = row[11].ToString();
                                user.Direccion = new ML.Direccion();
                                user.Direccion.Calle = row[13].ToString();
                                user.Direccion.NumeroExterior = row[14].ToString();
                                user.Direccion.Colonia = new ML.Colonia();
                                user.Direccion.Colonia.IdColonia = Convert.ToByte(row[15].ToString());

                                usuario.Usuarios.Add(user);
                            }
                            diccionario["Resultado"] = true;
                            diccionario["Objects"] = usuario.Usuarios;
                        }

                        else
                        {
                            diccionario["Resultado"] = false;
                            diccionario["Excepcion"] = "No existen registros en el excel";
                        }
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
        
        public static Dictionary<string, object> ValidarExcel(List<ML.Usuario> usuarios)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>() { {"Excepcion", ""}, {"Resultado", false}, {"Objects", null} };
            ML.ResultExcel resultExcel = new ML.ResultExcel();

            try
            {
                resultExcel.Objects = new List<object>();
                int i = 1;
                foreach(ML.Usuario usuario in usuarios)
                {
                    ML.ResultExcel error = new ML.ResultExcel();
                    error.IdRegistro = i++;

                    if(usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }
                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido paterno  ";
                    }
                    if (usuario.Rol.IdRol == null)
                    {
                        error.Mensaje += "Ingresar el id del rol ";
                    }
                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el apellido materno ";
                    }
                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar el password ";
                    }
                    if (usuario.FechaNacimiento == null)
                    {
                        error.Mensaje += "Ingresar la fecha de nacimiento ";
                    }
                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el telefono ";
                    }
                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresar el celular ";
                    }
                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresar la CURP ";
                    }
                    if (usuario.Sexo == "")
                    {
                        error.Mensaje += "Ingresar el sexo ";
                    }
                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "Ingresar el username ";
                    }
                    if (usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el email ";
                    }
                    if (usuario.Direccion.Calle == "")
                    {
                        error.Mensaje += "Ingresar la calle ";
                    }
                    if (usuario.Direccion.NumeroExterior == "")
                    {
                        error.Mensaje += "Ingresar el numero exterior de la calle ";
                    }
                    if (usuario.Direccion.Colonia.IdColonia == null)
                    {
                        error.Mensaje += "Ingresar el id de la colonia ";
                    }
                    if(error.Mensaje != null)
                    {
                        resultExcel.Objects.Add(error);
                    }
                }
                diccionario["Objects"] = resultExcel.Objects;
            }
            catch (Exception ex)
            {

                diccionario["Resultado"] = false;
                diccionario["Excepcion"] = ex.Message;
            }

            return diccionario;
        }
        /*
        public static bool AddLINQ(ML.Usuario usuario)
        {
            bool resultado = false;

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    DL_EF.Usuario usuarioLINQ = new DL_EF.Usuario();

                    usuarioLINQ.Nombre = usuario.Nombre;
                    usuarioLINQ.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioLINQ.IdRol = usuario.Rol.IdRol;
                    usuarioLINQ.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioLINQ.Password = usuario.Password;
                    usuarioLINQ.FechaNacimiento = usuario.FechaNacimiento;
                    usuarioLINQ.Telefono = usuario.Telefono;
                    usuarioLINQ.Celular = usuario.Celular;
                    usuarioLINQ.CURP = usuario.CURP;
                    usuarioLINQ.Sexo = usuario.Sexo;
                    usuarioLINQ.UserName = usuario.UserName;
                    usuarioLINQ.Email = usuario.Email;

                    context.Usuarios.Add(usuarioLINQ);
                    int filasAfectadas = context.SaveChanges();

                    if(filasAfectadas > 0)
                    {
                        resultado = true;
                    }
                    else
                    {
                        resultado = false;
                    }
                }
            }
            catch (Exception ex)
            {

                resultado = false;
            }

            return resultado;
        }
    
        public static bool DeleteLINQ(int idUsuario)
        {
            bool resultado = false;

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var query = (from Usuario in context.Usuarios
                                 where Usuario.IdUsuario == idUsuario
                                 select Usuario).FirstOrDefault();

                    context.Usuarios.Remove(query);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                resultado = false;
            }

            return resultado;
        }
        
        public static bool UpdateLINQ(ML.Usuario usuario)
        {
            bool resultado = false;

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var query = (from Usuario in context.Usuarios
                                 where Usuario.IdUsuario == usuario.IdUsuario
                                 select Usuario).SingleOrDefault();

                    if(query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.IdRol = usuario.Rol.IdRol;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.Password = usuario.Password;
                        query.FechaNacimiento = usuario.FechaNacimiento;
                        query.Telefono = usuario.Telefono;
                        query.Celular = usuario.Celular;
                        query.Celular = usuario.Celular;
                        query.CURP = usuario.CURP;
                        query.Sexo = usuario.Sexo;
                        query.UserName = usuario.UserName;
                        query.Email = usuario.Email;

                        int filasAfectadas = context.SaveChanges();

                        if(filasAfectadas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                resultado = false;
            }

            return resultado;
        }
    
        public static ML.Usuario GetAllLINQ()
        {
            ML.Usuario usuario = new ML.Usuario();

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var query = (from Usuario in context.Usuarios
                                 join Rol in context.Rols on Usuario.IdRol equals Rol.IdRol
                                 select new {IdUsuario = Usuario.IdUsuario,
                                             Nombre = Usuario.Nombre,
                                             ApellidoPaterno = Usuario.ApellidoPaterno,
                                             Tipo = Rol.Tipo,
                                             ApellidoMaterno = Usuario.ApellidoMaterno,
                                             Password = Usuario.Password,
                                             FechaNacimiento = Usuario.FechaNacimiento,
                                             Telefono = Usuario.Telefono,
                                             Celular = Usuario.Celular,
                                             CURP = Usuario.CURP,
                                             Sexo = Usuario.Sexo,
                                             UserName = Usuario.UserName,
                                             Email = Usuario.Email}).ToList();

                    if(query != null)
                    {
                        usuario.Usuarios = new List<ML.Usuario>();

                        foreach (var registro in query)
                        {
                            ML.Usuario user = new ML.Usuario();
                            user.IdUsuario = registro.IdUsuario;
                            user.Nombre = registro.Nombre;
                            user.ApellidoPaterno = registro.ApellidoPaterno;
                            user.Rol = new ML.Rol();
                            user.Rol.Tipo = registro.Tipo;
                            user.ApellidoMaterno = registro.ApellidoMaterno;
                            user.Password = registro.Password;
                            user.FechaNacimiento = registro.FechaNacimiento;
                            user.Telefono = registro.Telefono;
                            user.Celular = registro.Celular;
                            user.CURP = registro.CURP;
                            user.Sexo = registro.Sexo;
                            user.UserName = registro.UserName;
                            user.Email = registro.Email;

                            usuario.Usuarios.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return usuario;
        }
    
        public static ML.Usuario GetByIdLINQ(int idUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            try
            {
                using (DL_EF.ARodriguezProgramacionNCapasEntities context = new DL_EF.ARodriguezProgramacionNCapasEntities())
                {
                    var query = (from Usuario in context.Usuarios
                                 join Rol in context.Rols on Usuario.IdRol equals Rol.IdRol
                                 where Usuario.IdUsuario == idUsuario
                                 select new
                                 {
                                     IdUsuario = Usuario.IdUsuario,
                                     Nombre = Usuario.Nombre,
                                     ApellidoPaterno = Usuario.ApellidoPaterno,
                                     TipoRol = Rol.Tipo,
                                     ApellidoMaterno = Usuario.ApellidoMaterno,
                                     Password = Usuario.Password,
                                     FechaNacimiento = Usuario.FechaNacimiento,
                                     Telefono = Usuario.Telefono,
                                     Celular = Usuario.Celular,
                                     CURP = Usuario.CURP,
                                     Sexo = Usuario.Sexo,
                                     UserName = Usuario.UserName,
                                     Email = Usuario.Email
                                 }).FirstOrDefault();

                    if(query != null)
                    {
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.Tipo = query.TipoRol;
                        usuario.ApellidoPaterno = query.ApellidoMaterno;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.CURP;
                        usuario.Sexo = query.Sexo;
                        usuario.UserName = query.UserName;
                        usuario.Email = query.Email;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return usuario;
        }*/
    }
}
