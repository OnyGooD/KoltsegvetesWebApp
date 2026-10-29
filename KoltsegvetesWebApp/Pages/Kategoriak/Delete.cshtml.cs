using KoltsegvetesWebApp.Data;
using KoltsegvetesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoltsegvetesWebApp.Pages.Kategoriak
{
    public class DeleteModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public DeleteModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        public Kategoria Kategoria { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var kategoria = await _context.Kategoriak.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }

            Kategoria = kategoria;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var kategoria = await _context.Kategoriak.FindAsync(id);
            if (kategoria == null)
            {
                return NotFound();
            }

            bool vanTranzakcio = await _context.Tranzakciok.AnyAsync(t => t.KategoriaId == id);
            if (vanTranzakcio)
            {
                Kategoria = kategoria;
                ModelState.AddModelError(string.Empty, "A kategória nem törölhető, mert tartozik hozzá tranzakció.");
                return Page();
            }

            _context.Kategoriak.Remove(kategoria);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}