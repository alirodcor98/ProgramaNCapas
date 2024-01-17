using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Colonia
    {
        public byte? IdColonia { get; set; }
        public string Nombre { get; set; }
        public ML.Municipio Municipio { get; set; }
        public List<Colonia> Colonias { get; set; }
    }
}
