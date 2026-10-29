using System.ComponentModel.DataAnnotations;

namespace KoltsegvetesWebApp.Models
{
    public class Tranzakcio
    {
        public int Id { get; set; }
        [Display(Name = "Kategória")]
        public int KategoriaId { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Az összegnek pozitívnak kell lennie.")]
        [Display(Name = "Összeg")]
        public decimal Osszeg { get; set; }
        [Display(Name = "Dátum")]
        public DateTime Datum { get; set; }
        [Display(Name = "Megjegyzés")]
        [StringLength(200, ErrorMessage = "A megjegyzés legfeljebb 200 karakter lehet.")]
        public string? Megjegyzes { get; set; }
    }
}
