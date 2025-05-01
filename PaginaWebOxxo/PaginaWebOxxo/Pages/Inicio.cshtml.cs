using PaginaWebOxxo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;


namespace PaginaWebOxxo.Pages
{
    public class InicioModel : PageModel
    {
        private readonly DataBaseContext _context;
        public InicioModel(DataBaseContext context)
        {
            _context = context;
        }

        public Estadisticas EstadisticasUsuario { get; set; }

        public Usuarios Usuario {get; set;}
        public void OnGet()
        {
            int numEmpleado = 12345; 
            EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(numEmpleado);
            Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        }

    }
}