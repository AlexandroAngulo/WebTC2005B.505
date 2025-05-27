using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaWebOxxo.Model;

namespace PaginaWebOxxo.Pages;

public class PersonajeModel : PageModel
{
    private readonly DataBaseContext _context;
    public PersonajeModel(DataBaseContext context)
    {
        _context = context;
    }

    public Usuarios Lider { get; set; }
    public void OnGet()
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");
        // Obtener datos del líder
        Lider = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
    }
}
