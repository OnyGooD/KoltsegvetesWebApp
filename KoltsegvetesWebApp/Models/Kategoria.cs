using System.ComponentModel.DataAnnotations;

namespace KoltsegvetesWebApp.Models
{
    public class Kategoria
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A név megadása kötelező.")]
        [StringLength(60, ErrorMessage = "A név hossza maximum 60 karakter lehet.")]
        [Display(Name = "Név")]
        public string Nev { get; set; }
        [Display(Name = "Típus")]
        public bool Tipus { get; set; } // true = bevétel, false = kiadás
        public List<Tranzakcio> Tranzakciok { get; set; } = new();
    }
}
