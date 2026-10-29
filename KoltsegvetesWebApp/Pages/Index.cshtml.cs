using KoltsegvetesWebApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoltsegvetesWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public IndexModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        public decimal Bevetel { get; set; }
        public decimal Kiadas { get; set; }
        public decimal Egyenleg { get; set; }

        public async Task OnGetAsync()
        {
            var elsoNap = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            var kovetkezoHonap = elsoNap.AddMonths(1);

            var tranzakciok = await _context.Tranzakciok
                .Include(t => t.Kategoria)
                .Where(t => t.Datum >= elsoNap && t.Datum < kovetkezoHonap)
                .ToListAsync();

            Bevetel = tranzakciok
                .Where(t => t.Kategoria!.Tipus)
                .Sum(t => t.Osszeg);

            Kiadas = tranzakciok
                .Where(t => !t.Kategoria!.Tipus)
                .Sum(t => t.Osszeg);

            Egyenleg = Bevetel - Kiadas;
        }
    }
}