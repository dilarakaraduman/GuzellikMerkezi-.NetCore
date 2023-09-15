using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_Randevu")]
    public class Randevu
    {
        [Key]
        public int RandevuId { get; set; }
        public int MusteriId { get; set; }
        public int SalonId { get; set; }
        public int PersonelId { get; set;}
        public DateTime Tarih { get; set; }
        public int saatId { get; set; }
        //public int UzmanlikId { get; set; }
        public int OperasyonId { get; set; }

	}

}
