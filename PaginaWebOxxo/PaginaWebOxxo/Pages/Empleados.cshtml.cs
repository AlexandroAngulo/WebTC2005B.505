using PaginaWebOxxo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Text.Json;

namespace PaginaWebOxxo.Pages;

public class EmpleadosModel : PageModel
{

    private readonly DataBaseContext _context;
    public EmpleadosModel(DataBaseContext context)
    {
        _context = context;
    }

    public Usuarios Lider { get; set; }
    public List<Usuarios> ListaEmpleados { get; set; }

    [BindProperty] public string numLider { get; set; }
    [BindProperty] public string numEmpleado { get; set; }

    public void OnGet()
    {
        int numLider = (int)HttpContext.Session.GetInt32("numEmpleado");

        // Obtener datos del líder
        Lider = _context.ObtenerUsuarioPorEmpleados(numLider);
        Lider.ColorEstatus = ObtenerColorEstatus(Lider.IdEstatus);

        // Obtener empleados que dependen del líder
        ListaEmpleados = _context.ObtenerEmpleadosPorLider(numLider);

        foreach (var empleado in ListaEmpleados)
        {
            empleado.ColorEstatus = ObtenerColorEstatus(empleado.IdEstatus);
        }
    }

    // Asignar colores en base a estatus
    private string ObtenerColorEstatus(int idEstatus)
    {
        return idEstatus switch
        {
            1 => "bg-success",   // Activo
            2 => "bg-warning",   // Inactivo
            3 => "bg-danger",    // Ausente
        };
    }

    public IActionResult OnPostEditarLider(int numEmpleado)
    {
        return Redirect($"Lider?numEmpleado={numEmpleado}");
    }

    public IActionResult OnPostEditarEmpleado(int NumEmpleado)
    {
        return Redirect($"Lider?numEmpleado={NumEmpleado}");
    }

    public IActionResult OnPostAgregarEmpleado(int numLider, int numEmpleado)
    {
        _context.AgregarEmpleado(numLider, numEmpleado);
        return Redirect($"Empleados?");
    }
}
