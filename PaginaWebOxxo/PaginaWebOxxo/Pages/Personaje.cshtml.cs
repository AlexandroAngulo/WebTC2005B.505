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
    public string PersonajeSeleccionado { get; set; }
    public string TrackSeleccionado { get; set; }
    public void OnGet()
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");
        Lider = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
    }

    public IActionResult OnPostEquiparPersonaje(string personaje)
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");
    
        int id = _context.ObtenerIdPersonalizacion(personaje, "Personaje");
    
        if (id > 0)
        {
            _context.EquiparPersonaje(numEmpleado, id);
        }
    
        return RedirectToPage("Personaje", new { personaje = personaje, track = TrackSeleccionado });
    }
    

    public IActionResult OnPostEquiparTrack(string track)
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");

        int id2 = _context.ObtenerIdPersonalizacionM(track, "Musica");

        if (id2 > 0)
        {
            _context.EquiparTrack(numEmpleado, id2);
        }

        return RedirectToPage("Personaje", new { personaje = PersonajeSeleccionado, track = track });
    }

}
