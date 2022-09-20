using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_Musteri")]
    public class Musteri
    {
        [Key]
        public int MusteriId { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public DateTime MusteriDogumtarihi { get; set; }
        public bool MusteriCinsiyet { get; set; }
        public string MusteriMail { get; set; }
        public string MusteriParola { get; set; }
        public string MusteriTel { get; set; }
      
      
    }

}
