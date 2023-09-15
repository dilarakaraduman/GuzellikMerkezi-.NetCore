using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_Urun")]
    public class Urun
        {
            [Key]
            public int Id { get; set; }
            public string Ad { get; set; }
            public int Fiyat { get; set; }
		    public string? Detay { get; set; }
		    public string ResimAd { get; set; }
		    
	}

}
