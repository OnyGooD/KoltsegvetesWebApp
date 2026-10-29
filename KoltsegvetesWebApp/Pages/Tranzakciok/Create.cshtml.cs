using KoltsegvetesWebApp.Data;
using KoltsegvetesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace KoltsegvetesWebApp.Pages.Tranzakciok
{
    public class CreateModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public CreateModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tranzakcio Tranzakcio { get; set; } = new();

        public SelectList KategoriaLista { get; set; } = default!;

        public async Task OnGetAsync()
        {
            await KategoriakBetoltese();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Tranzakcio.Kategoria");

            if (Tranzakcio.Datum.Date > DateTime.Today)
            {
                ModelState.AddModelError("Tranzakcio.Datum", "A dátum nem lehet jövőbeli.");
            }

            if (!ModelState.IsValid)
            {
                await KategoriakBetoltese();
                return Page();
            }

            _context.Tranzakciok.Add(Tranzakcio);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }

        private async Task KategoriakBetoltese()
        {
            var kategoriak = await _context.Kategoriak.OrderBy(k => k.Nev).ToListAsync();
            KategoriaLista = new SelectList(kategoriak, "Id", "Nev");
        }
    }
}