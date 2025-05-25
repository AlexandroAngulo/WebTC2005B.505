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


    public void OnGet()
    {

        int numEmpleado = 12345;

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

        if (telefonostr.Length >9 || telefonostr.Length<9)
        {
            ModelState.AddModelError("Telefono", "El número de teléfono es inválido, debe contener 9 dígitos");
        }
        else
        {
            _context.ActualizarTelefono(numEmpleado, Telefono);
        }

            // Recargar todos los datos necesarios para la vista
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(numEmpleado);
        Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
        contacto = _context.ObtenerContactoPorEmpleados(numEmpleado);
        codigopostal = _context.ObtenerCodigoPorEmpleados(codigoPostal);
        
    }
        

}
