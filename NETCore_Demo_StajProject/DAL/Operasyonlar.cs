using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_Operasyonlar")]
    public class Operasyonlar
    {
        [Key]
        public int OperasyonId { get; set; }
        public string OperasyonAd { get; set; }
        public int Seans_sayisi { get; set; }
        public int Tutar { get; set; }
  
    }


}
