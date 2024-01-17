using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Transporte
    {
        public int IdTransporte { get; set; }
        public string NumeroPlaca { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public DateTime AnioFabricacion { get; set; }
        public EstatusTransporte Estatus { get; set; }
        public List<object> Transportes { get; set; }
        
        public Transporte()
        {

        }
    }
}
