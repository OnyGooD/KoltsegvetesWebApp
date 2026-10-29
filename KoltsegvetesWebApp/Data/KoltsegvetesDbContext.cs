using KoltsegvetesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KoltsegvetesWebApp.Data
{
    public class KoltsegvetesDbContext : DbContext
    {
        public KoltsegvetesDbContext(DbContextOptions<KoltsegvetesDbContext> options) : base(options)
        {
        }

        public DbSet<Kategoria> Kategoriak { get; set; }
        public DbSet<Tranzakcio> Tranzakciok { get; set; }
    }
}
