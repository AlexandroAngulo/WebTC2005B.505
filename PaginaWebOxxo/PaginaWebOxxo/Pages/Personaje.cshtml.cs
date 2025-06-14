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

    public void OnGet(string personaje = null, string track = null)
{
    int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");
    Lider = _context.ObtenerUsuarioPorEmpleados(numEmpleado);

    PersonajeSeleccionado = personaje ?? _context.ObtenerNombrePersonajeEquipado(numEmpleado);

    TrackSeleccionado = track ?? _context.ObtenerNombreTrackEquipado(numEmpleado);
}

    public IActionResult OnPostEquiparPersonaje(string personaje)
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");
    
        int id = _context.ObtenerIdPersonalizacion(personaje);
    
        if (id > 0)
        {
            _context.EquiparPersonaje(numEmpleado, id);
        }

        return RedirectToPage(new { personaje = personaje, track = TrackSeleccionado });
    }
    

    public IActionResult OnPostEquiparTrack(string track)
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");

        int id2 = _context.ObtenerIdPersonalizacionMusica(track);

        if (id2 > 0)
        {
            _context.EquiparTrack(numEmpleado, id2);
        }

        return RedirectToPage("Personaje", new { personaje = PersonajeSeleccionado, track = track });
    }

}
