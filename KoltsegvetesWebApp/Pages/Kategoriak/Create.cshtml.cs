using KoltsegvetesWebApp.Data;
using KoltsegvetesWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoltsegvetesWebApp.Pages.Kategoriak
{
    public class CreateModel : PageModel
    {
        private readonly KoltsegvetesDbContext _context;

        public CreateModel(KoltsegvetesDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Kategoria Kategoria { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Kategoriak.Add(Kategoria);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}