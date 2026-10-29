using KoltsegvetesWebApp.Data;
using KoltsegvetesWebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoltsegvetesWebApp.Pages.Kategoriak
{
    public class IndexModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public IndexModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        public List<Kategoria> Kategoriak { get; set; } = new();

        public async Task OnGetAsync()
        {
            Kategoriak = await _context.Kategoriak.ToListAsync();
        }
    }
}