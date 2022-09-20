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
        public SaatId SaatId { get; set; }
        public int OperasyonId { get; set; }

    }
    public enum SaatId
    {
        _9_00,
        _9_30,
        _10_00,
        _10_30,
        _11_00,
        _11_30,
        _12_00,
        _12_30,
        _13_00,
        _13_30,
            
        _14_00,
        _14_30,
        _15_00,
            
        _15_30,
        _16_00,
        _16_30,
        _17_00,
        _17_30,
        _18_00,
        _18_30,
        _19_00,
    }
}
