using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using PaginaWebOxxo.Model;

namespace PaginaWebOxxo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DataBaseContext _context;

        public IndexModel(DataBaseContext context)
        {
            _context = context;
        }

        [BindProperty]
        [Required(ErrorMessage = "Debes ingresar tu número de empleado.")]
        public int? NumEmpleado { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Debes ingresar la contraseña.")]
        public string Contraseña { get; set; }

        private Usuarios Usuario { get; set; }

        public void OnGet()
        {
            HttpContext.Session.Clear();
        }

        public IActionResult OnPostVerificarContraseña()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            var datosLogin = _context.login(NumEmpleado.Value);
            if (datosLogin != null && datosLogin.Contraseña == Contraseña)
            {
                HttpContext.Session.SetInt32("numEmpleado", NumEmpleado.Value);
                Usuario = _context.ObtenerUsuarioPorEmpleados(NumEmpleado.Value);
                HttpContext.Session.SetInt32("IdTipoPuesto", Usuario.IdTipoPuesto);
                if (Usuario.IdTipoPuesto == 2)
                {
                    return Redirect($"Lider?numEmpleado={NumEmpleado.Value}");
                }
                else
                {
                    return Redirect($"Inicio?numEmpleado={NumEmpleado.Value}");
                }
            }
            else
            {
                ModelState.AddModelError("Contraseña", "Contraseña o número de empleado incorrectos. Inténtalo de nuevo.");
                return Page();
            }
        }
    }
}