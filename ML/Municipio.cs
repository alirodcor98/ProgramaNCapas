using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Municipio
    {
        public byte? IdMunicipio { get; set; }
        public string Nombre { get; set; }
        public ML.Estado Estado { get; set; }
        public List<Municipio> Municipios { get; set; }
    }
}
