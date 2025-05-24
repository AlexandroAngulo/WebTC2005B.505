using PaginaWebOxxo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;


namespace PaginaWebOxxo.Pages;

public class EmpleadosModel : PageModel
{

    private readonly DataBaseContext _context;
    public EmpleadosModel(DataBaseContext context)
    {
        _context = context;
    }

    public Usuarios Lider { get; set; }
    public List<Empleados> ListaEmpleados { get; set; }

    public void OnGet()
    {
        int numLider = 54321;

        // Obtener datos del líder
        Lider = _context.ObtenerUsuarioPorEmpleados(numLider);

        // Obtener empleados que dependen del líder
        ListaEmpleados = _context.ObtenerEmpleadosPorLider(numLider);
    }
}
