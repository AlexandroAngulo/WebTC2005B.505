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
    [BindProperty]
    public Login login { get; set; }
    public List<Usuarios> ListaEmpleados { get; set; }

    public void OnGet()
    {
        int numEmpleado = login.NumEmpleado;
        // Obtener datos del líder
        Lider = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        Lider.ColorEstatus = ObtenerColorEstatus(Lider.IdEstatus);

        // Obtener empleados que dependen del líder
        ListaEmpleados = _context.ObtenerEmpleadosPorLider(numEmpleado);

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

    public void OnPostEditarLider()
    {
        Response.Redirect($"Lider?numEmpleado={Lider.NumEmpleado}");
    }

    public void OnPostEditarEmpleado(int numEmpleado)
    {
        Response.Redirect($"Lider?numEmpleado={numEmpleado}");
    }
}
