using PaginaWebOxxo.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace PaginaWebOxxo.Pages;

public class InstruccionesModel : PageModel
{
    private readonly DataBaseContext _context;


    public InstruccionesModel(DataBaseContext context)
    {
        _context = context;
    }

    public List<InstruccionVideojuego> Instrucciones { get; set; }

    public void OnGet()
    {
        Instrucciones = _context.ObtenerTodasLasInstrucciones();
    }
    public void OnPost()
    {
        Instrucciones = _context.ObtenerTodasLasInstrucciones();
    }
    
    
    
}