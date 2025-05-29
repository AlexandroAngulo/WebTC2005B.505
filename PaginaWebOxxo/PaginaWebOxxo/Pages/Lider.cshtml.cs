using PaginaWebOxxo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace PaginaWebOxxo.Pages;

public class LiderModel : PageModel
{
    private readonly DataBaseContext _context;

    public LiderModel(DataBaseContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Estadisticas EstadisticasUsuario { get; set; }

    [BindProperty]
    public string Telefono { get; set; }

    [BindProperty]
    public Usuarios Usuario { get; set; }

    [BindProperty]
    public Contacto contacto { get; set; }

    [BindProperty]
    public Codigopostal codigopostal { get; set; }



    public Usuarios Genero { get; set; }
    public Usuarios Puesto { get; set; }
    public Usuarios Lider { get; set; }

    [BindProperty]
    public int? numEmpleado { get; set; } 

    public void OnGet(int? numEmpleado)
    {

        int empleadoId;

        if (numEmpleado.HasValue)
        {
            empleadoId = numEmpleado.Value;
        }
        else
        {
            empleadoId = HttpContext.Session.GetInt32("numEmpleado") ?? 0;
            HttpContext.Session.SetInt32("numEmpleado", empleadoId);
        }

        Puesto = _context.ObtenerUsuarioPorEmpleados(empleadoId);
        Genero = _context.ObtenerUsuarioPorEmpleados(empleadoId);
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(empleadoId);
        Usuario = _context.ObtenerUsuarioPorEmpleados(empleadoId);
        contacto = _context.ObtenerContactoPorEmpleados(empleadoId);
        codigopostal = _context.ObtenerCodigoPorEmpleados(empleadoId);

    }

    public void OnPostActualizar(int? numEmpleado)
    {
        int empleadoId = numEmpleado ?? HttpContext.Session.GetInt32("numEmpleado") ?? 0;

        if (!numEmpleado.HasValue)
        {
            HttpContext.Session.SetInt32("numEmpleado", empleadoId);
        }

        if (string.IsNullOrEmpty(Telefono) || Telefono.Length != 10)
        {
            ModelState.AddModelError("Telefono", "El número de teléfono es inválido, debe contener 10 dígitos");
        }

        _context.ActualizarTelefono(empleadoId, Telefono);

        // Recargar los datos del usuario editado
        Puesto = _context.ObtenerUsuarioPorEmpleados(empleadoId);
        Genero = _context.ObtenerUsuarioPorEmpleados(empleadoId);
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(empleadoId);
        Usuario = _context.ObtenerUsuarioPorEmpleados(empleadoId);
        contacto = _context.ObtenerContactoPorEmpleados(empleadoId);
        codigopostal = _context.ObtenerCodigoPorEmpleados(empleadoId);
    }
}