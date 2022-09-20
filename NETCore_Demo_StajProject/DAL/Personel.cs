using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NETCore_Demo_StajProject.DAL

{
        [Table("Tbl_Personel")]
        public class Personel
        {
            [Key]
            public int PersonelId { get; set; }
            public string PersonelAd { get; set; }
            public string PersonelSoyad { get; set; }
            public bool PersonelCinsiyet { get; set; }
            public string PersonelMail { get; set; }
            public string PersonelParola { get; set; }
            public string PersonelTel { get; set; }
            public int? EkiplideriId { get; set; }
        

    }
    
}
