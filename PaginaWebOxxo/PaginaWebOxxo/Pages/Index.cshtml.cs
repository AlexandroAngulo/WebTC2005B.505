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
    public Login login { get; set; }
    public void OnGet()
    {

    }
}
