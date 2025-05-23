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

    public Estadisticas EstadisticasUsuario { get; set; }

    public Usuarios Usuario { get; set; }

    public Contacto contacto { get; set; }
    public Codigopostal codigopostal{ get; set; }
    public void OnGet()
    {
        int numEmpleado = 12345;
        EstadisticasUsuario = _context.ObtenerMonedasPorEmpleado(numEmpleado);
        Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado);


        int codigoPostal = 12345;
        contacto = _context.ObtenerContactoPorEmpleados(numEmpleado);
        codigopostal = _context.ObtenerCodigoPorEmpleados(codigoPostal);

    }

    
    
        

}
