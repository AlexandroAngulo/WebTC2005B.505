using System;
using Org.BouncyCastle.Asn1.Cms;

namespace PaginaWebOxxo.Model
{
    public class Empleados
    {
        public int NumEmpleado { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public int IdTipoPuesto { get; set; }
        public string Puesto { get; set; }
        public int IdEstatus { get; set; }
        public string ColorEstatus { get; set; }
        public TimeSpan horarioInicio { get; set; }
        public TimeSpan horarioFin{ get; set; }
    }
}