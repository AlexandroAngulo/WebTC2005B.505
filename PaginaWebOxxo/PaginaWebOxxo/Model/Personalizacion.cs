using System;


namespace PaginaWebOxxo.Model
{
    public class Personalizacion
    {
        public Usuarios NumEmpleado { get; set; }

        public int idPersonalizacion { get; set; }  // FK

        public string Equipado { get; set; }  // Nombre, imagen, ID, etc.

        public DateTime FechaCompra { get; set; } = DateTime.Now;


    }
}