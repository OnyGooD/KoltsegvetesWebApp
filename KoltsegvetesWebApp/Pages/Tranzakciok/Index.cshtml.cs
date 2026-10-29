using KoltsegvetesWebApp.Data;
using KoltsegvetesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace KoltsegvetesWebApp.Pages.Tranzakciok
{
    public class IndexModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public IndexModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string? Honap { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? KategoriaId { get; set; }

        public SelectList KategoriaLista { get; set; } = default!;

        public List<Tranzakcio> Tranzakciok { get; set; } = new();

        public async Task OnGetAsync()
        {
            var kategoriak = await _context.Kategoriak.OrderBy(k => k.Nev).ToListAsync();
            KategoriaLista = new SelectList(kategoriak, "Id", "Nev");

            var lekerdezes = _context.Tranzakciok
                .Include(t => t.Kategoria)
                .AsQueryable();

            if (!string.IsNullOrEmpty(Honap) &&
                DateTime.TryParseExact(Honap, "yyyy-MM", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var elsoNap))
            {
                var kovetkezoHonap = elsoNap.AddMonths(1);
                lekerdezes = lekerdezes.Where(t => t.Datum >= elsoNap && t.Datum < kovetkezoHonap);
            }

            if (KategoriaId.HasValue)
            {
                lekerdezes = lekerdezes.Where(t => t.KategoriaId == KategoriaId.Value);
            }

            Tranzakciok = await lekerdezes
                .OrderByDescending(t => t.Datum)
                .ToListAsync();
        }
    }
}