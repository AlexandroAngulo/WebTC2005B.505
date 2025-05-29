using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaWebOxxo.Model;

namespace PaginaWebOxxo.Pages;

public class CapacitacionesModel : PageModel
{
    public List<NivelUsuario> Progresos { get; set; }
    private readonly DataBaseContext _context;

    public CapacitacionesModel(DataBaseContext context)
    {
        _context = context;
    }
    public Usuarios Usuario {get; set;}


    public void OnGet()
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");
        var db = new DataBaseContext();
        Progresos = db.ObtenerProgresoPorEmpleado(numEmpleado);
        Usuario = _context.ObtenerUsuarioPorEmpleados(numEmpleado);
    }

    public string ObtenerPorcentajeBarra(int estrellas)
    {
        return $"{(estrellas / 3.0) * 100}%";
    }

    public List<bool> ObtenerEstadoEstrellas(int estrellas)
    {
        return Enumerable.Range(1, 3).Select(i => i <= estrellas).ToList();
    }
}


