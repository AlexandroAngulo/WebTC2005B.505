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

    public void OnGet()
    {
        int? numEmpleado = HttpContext.Session.GetInt32("numEmpleado");

        if (numEmpleado == null)
        {
            Response.Redirect("/Index");
            return;
        }

        Puesto = _context.ObtenerUsuarioPorEmpleados(numEmpleado.Value);
        Genero = _context.ObtenerUsuarioPorEmpleados(numEmpleado.Value);
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(numEmpleado.Value);
        Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado.Value);
        contacto = _context.ObtenerContactoPorEmpleados(numEmpleado.Value);

        int codigoPostal = 12345;
        codigopostal = _context.ObtenerCodigoPorEmpleados(codigoPostal);
    }

    public void OnPostActualizar()
    {
        int? numEmpleado = HttpContext.Session.GetInt32("numEmpleado");

        if (numEmpleado == null)
        {
            Response.Redirect("/Index");
            return;
        }

        if (string.IsNullOrEmpty(Telefono) || Telefono.Length != 10)
        {
            ModelState.AddModelError("Telefono", "El número de teléfono es inválido, debe contener 10 dígitos");

        }

        _context.ActualizarTelefono(numEmpleado.Value, Telefono);

        // Recargar los datos
        Puesto = _context.ObtenerUsuarioPorEmpleados(numEmpleado.Value);
        Genero = _context.ObtenerUsuarioPorEmpleados(numEmpleado.Value);
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(numEmpleado.Value);
        Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado.Value);
        contacto = _context.ObtenerContactoPorEmpleados(numEmpleado.Value);

        int codigoPostal = 12345;
        codigopostal = _context.ObtenerCodigoPorEmpleados(codigoPostal);
    }
}