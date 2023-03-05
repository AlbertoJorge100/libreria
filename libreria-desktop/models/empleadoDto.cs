using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libreria_desktop.models
{
    public class empleadoDto
    {
        public long id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int edad { get; set; }
        public string dui { get; set; }
        public byte activo { get; set; }
    }
}
