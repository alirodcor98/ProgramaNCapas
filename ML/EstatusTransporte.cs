using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class EstatusTransporte
    {
        public byte IdEstatus { get; set; }
        public string Estatus { get; set; }
        public List<EstatusTransporte> EstatusTransportes { get; set; }

        public EstatusTransporte()
        {

        }
    }
}
