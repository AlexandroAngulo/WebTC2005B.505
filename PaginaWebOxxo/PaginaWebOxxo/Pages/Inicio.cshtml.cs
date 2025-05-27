using PaginaWebOxxo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

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
        public Usuarios Usuario { get; set; }
        public int TotalEstrellas { get; set; }

        [BindProperty]
        public int? numEmpleado { get; set; } 

        public void OnGet(int? numEmpleado)
        {

            int empleadoId = numEmpleado ?? HttpContext.Session.GetInt32("numEmpleado") ?? 0;

            HttpContext.Session.SetInt32("numEmpleado", empleadoId);

            EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(empleadoId);
            Usuario = _context.ObtenerUsuarioPorEmpleados(empleadoId);

            var progresos = _context.ObtenerProgresoPorEmpleado(empleadoId);
            TotalEstrellas = progresos.Sum(p => p.Estrellas);
        }
    }
}