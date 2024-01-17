using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Paquete
    {
        public int IdPaquete { get; set; }
        public string InstruccionEntrega { get; set; }
        public decimal Peso { get; set; }
        public string DireccionOrigen { get; set; }
        public string DireccionEntrega { get; set; }
        public DateTime FechaEstimadaEntrega { get; set; }
        public string NumeroGuia { get; set; }
        public List<ML.Paquete> Paquetes { get; set; }
    }
}
