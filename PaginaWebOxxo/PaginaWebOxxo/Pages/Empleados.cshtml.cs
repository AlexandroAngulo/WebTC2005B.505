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

        var listaNumeros = ListaEmpleados.Select(e => e.NumEmpleado).ToList();
        HttpContext.Session.SetString("ListaNumEmpleados", JsonSerializer.Serialize(listaNumeros));
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

    public void OnPostEditarLider(int numEmpleado)
    {
        Response.Redirect($"Lider?numEmpleado={numEmpleado}");
    }

    public void OnPostEditarEmpleado(int numEmpleado)
    {
        Response.Redirect($"Lider?numEmpleado={numEmpleado}");
    }
}
