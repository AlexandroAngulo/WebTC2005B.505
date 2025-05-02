using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaWebOxxo.Model;

namespace PaginaWebOxxo.Pages;

public class CapacitacionesModel : PageModel
{
    public int EmpleadoId { get; set; } = 1;
    public List<NivelUsuario> Progresos { get; set; }

    public void OnGet()
    {
        var db = new DataBaseContext();
        Progresos = db.ObtenerProgresoPorEmpleado(12345);
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


