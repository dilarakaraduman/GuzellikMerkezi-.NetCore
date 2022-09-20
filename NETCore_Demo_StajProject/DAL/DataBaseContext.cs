using Microsoft.EntityFrameworkCore;

namespace NETCore_Demo_StajProject.DAL
{
    public class DataBaseContext :DbContext
    {
        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<Personel> Personel { get; set; }
        public DbSet<Gorev> Gorev { get; set; }
        public DbSet<PersonelGorev> PersonelGorev { get; set; }
        public DbSet<PersonelUzmanlik> PersonelUzmanlik { get; set; }
        public DbSet<UzmanlikAlanlari> UzmanlikAlanlari { get; set; }
        public DbSet<Operasyonlar> Operasyonlar { get; set; }
        public DbSet<Randevu> Randevu { get; set; }
        public DbSet<Salon> Salon { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DILARA;Database=Dbo_guzellikMerkezi;Trusted_Connection=True;");
        }

    }
}
