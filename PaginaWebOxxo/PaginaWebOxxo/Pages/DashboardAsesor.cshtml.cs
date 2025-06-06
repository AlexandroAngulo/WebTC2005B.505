using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaWebOxxo.Model;

namespace PaginaWebOxxo.Pages;

public class DashboardAsesorModel : PageModel
{
    private readonly DataBaseContext _context;

    public DashboardAsesorModel(DataBaseContext context)
    {
        _context = context;
    }

    [BindProperty]
    public string asesor { get; set; }

    public string Url { get; set; }

    public void OnGet()
    {
        int numEmpleado = (int)HttpContext.Session.GetInt32("numEmpleado");
        asesor = numEmpleado.ToString();
        Url = ActualizarUrl(asesor);

    }

    private string ActualizarUrl(string asesor)
    {
        return $"https://lookerstudio.google.com/embed/reporting/acd411a9-8da9-4dff-87af-ed879643abf2/page/wg4MF?params=%7B%22asesor_param1%22:%22{asesor}%22%2C%22asesor_param2%22:%22{asesor}%22%2C%22asesor_param3%22:%22{asesor}%22%2C%22asesor_param4%22:%22{asesor}%22%2C%22asesor_param5%22:%22{asesor}%22%2C%22asesor_param6%22:%22{asesor}%22%7D";
    }
}


