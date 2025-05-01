using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PaginaWebOxxo.Pages;

public class CapacitacionesModel : PageModel
{
    public int Id { get; set; }
    public int EmpleadoId { get; set; }
    public int Minijuego { get; set; }
    public int PuntosObtenidos { get; set;}
    public int EstrellasObtenidas { get; set; }

    public List<(int Minijuego, int EstrellasObtenidas)> Progresos { get; set; }

    public void OnGet()
    {
        Progresos = new List<(int, int)>
        {
            (1, 3), 
            (2, 2), 
            (3, 1)
        };
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


