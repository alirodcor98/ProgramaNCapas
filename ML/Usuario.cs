using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar un nombre")]
        [DataType(DataType.Text, ErrorMessage = "Este campo solo acepta texto")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar el apellido paterno")]
        [DataType(DataType.Text, ErrorMessage = "Este campo solo acepta texto")]
        public string ApellidoPaterno { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public ML.Repartidor Repartidor { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar el apellido materno")]
        [DataType(DataType.Text, ErrorMessage = "Este campo solo acepta texto")]
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar un password")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden, intenta de nuevo")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar una fecha de nacimiento")]
        [DataType(DataType.DateTime, ErrorMessage = "Este campo solo acepta fechas")]
        public DateTime FechaNacimiento { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Este campoco debe contener numeros")]
        [Phone]
        [StringLength(10,MinimumLength=10, ErrorMessage = "La longitud del telefono debe ser 10 digitos")]
        public string Telefono { get; set; }
        [DataType(DataType.PhoneNumber, ErrorMessage = "Este campoco debe contener numeros")]
        [Phone]
        [StringLength(10, MinimumLength=10, ErrorMessage="La longitud del celular debe ser 10 digitos")]
        public string Celular { get; set; }
        [StringLength(18, MinimumLength=18, ErrorMessage="La CURP debe tener 18 caracteres")]
        public string CURP { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Sexo { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Es obligatorio ingresar un Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> Usuarios { get; set; }

        public Usuario() { }

        public Usuario(string nombre, string apellidoPaterno)
        {
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno; 
        }

        public Usuario(string nombre, string apellidoPaterno, byte idRol, string apellidoMaterno, string password, 
            DateTime fechaNacimiento, string telefono, string celular, string curp, string sexo, string userName, 
            string email, byte[] imagen, string calle, string numeroExterior, int idColonia)
        {
            this.Nombre = nombre;
            this.ApellidoPaterno = apellidoPaterno;
            this.ApellidoMaterno = apellidoMaterno;
            this.Password = password;
            this.FechaNacimiento = fechaNacimiento;
            this.Telefono = telefono;
            this.Celular = celular;
            this.CURP = curp;
            this.Sexo = sexo;
            this.UserName = userName;
            this.Email = email;
        }
    }
}
