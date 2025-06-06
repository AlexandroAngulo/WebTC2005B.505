using System;


namespace PaginaWebOxxo.Model
{
    public class Personalizacion
    {
        public Usuarios NumEmpleado { get; set; }

        public int idPersonalizacion { get; set; }

        public string Equipado { get; set; }

        public DateTime FechaCompra { get; set; } = DateTime.Now;


    }
}