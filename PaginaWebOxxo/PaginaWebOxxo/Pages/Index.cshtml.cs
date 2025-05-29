using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PaginaWebOxxo.Model;

namespace PaginaWebOxxo.Pages;

public class IndexModel : PageModel
{
    private readonly DataBaseContext _context;
    public IndexModel(DataBaseContext context)
    {
        _context = context;
    }
    
    [BindProperty]
    public Login login { get; set; }

    public void OnGet()
    {
        HttpContext.Session.Clear();
    }

    public void OnPostVerificarContraseña()
    {
        var datosLogin = _context.login(login.NumEmpleado);
        if (datosLogin.Contraseña == login.Contraseña)
        {
            HttpContext.Session.SetInt32("numEmpleado", login.NumEmpleado);
            Response.Redirect($"Inicio?numEmpleado={login.NumEmpleado}");
        }
        else
        {

            ModelState.AddModelError("login.Contraseña", "Contraseña o número de empleado incorrectos. Inténtalo de nuevo.");
            
        }
    }
}
