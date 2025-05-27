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
    public int Telefono { get; set; }
    [BindProperty]
    public Usuarios Usuario { get; set; }
    [BindProperty]
    public Contacto contacto { get; set; }

    [BindProperty]
    public Codigopostal codigopostal { get; set; }

    public Usuarios Genero{ get; set; }
    public Usuarios Puesto{ get; set; }
    public Usuarios Lider { get; set; }


    public void OnGet()
    {

        int numEmpleado = 12345;

        Puesto = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        Genero = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(numEmpleado);
        Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado);

        if (Usuario != null)
        {
            int edad = Usuario.Edad;
        }
        int codigoPostal = 12345;
        contacto = _context.ObtenerContactoPorEmpleados(numEmpleado);
        codigopostal = _context.ObtenerCodigoPorEmpleados(codigoPostal);

    }

    public void OnPostActualizar()
    {
        int numEmpleado = 12345;
        int codigoPostal = 12345;

        

        string telefonostr = Telefono.ToString();

        if (telefonostr.Length >10 || telefonostr.Length<10)
        {
            ModelState.AddModelError("Telefono", "El número de teléfono es inválido, debe contener 10 dígitos");
        }
        else
        {
            _context.ActualizarTelefono(numEmpleado, Telefono);
        }

        // Recargar todos los datos necesarios para la vista
        Puesto = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        Genero = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(numEmpleado);
        Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        contacto = _context.ObtenerContactoPorEmpleados(numEmpleado);
        codigopostal = _context.ObtenerCodigoPorEmpleados(codigoPostal);
        
    }
        

}
