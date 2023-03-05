using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreria_desktop.models
{
    public class productosDto
    {        
        public string producto1 { get; set; }
        public decimal precio { get; set; }
        public string descripcion { get; set; }
        public long id_categoria { get; set; }
        public System.DateTime fecha_ingreso { get; set; }
    }
}
