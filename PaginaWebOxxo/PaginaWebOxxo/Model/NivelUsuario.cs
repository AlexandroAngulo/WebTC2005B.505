using System;

namespace PaginaWebOxxo.Model
{
    public class NivelUsuario
    {
        public int NumEmpleado { get; set; }
        public int IdNivel { get; set; }
        public DateTime FechaIntento { get; set; }
        public int Estrellas { get; set; }
        public int Puntuacion { get; set; }
        public int TiempoNivel { get; set; }
        public string NombreNivel { get; set; }
    }
}