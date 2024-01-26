using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "ServiceOperaciones" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione ServiceOperaciones.svc o ServiceOperaciones.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class ServiceOperaciones : IServiceOperaciones
    {
        public string Saludar(string nombre)
        {
            string saludo = "Hola " + nombre;
            return saludo;
        }

        public int Suma(int numero1, int numero2)
        {
            int suma = numero1 + numero2;
            return suma;
        }

        public int Resta(int numero1, int numero2)
        {
            int resta = numero1 - numero2;
            return resta;
        }

        public int Multiplicacion(int numero1, int numero2)
        {
            int multiplicacion = numero1 * numero2;
            return multiplicacion;
        }

        public int Division(int numero1, int numero2)
        {
            int division = numero1 / numero2;
            return division;
        }
    }
}
