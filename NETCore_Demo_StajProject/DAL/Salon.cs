using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_Salon")]
    public class Salon
    {
        [Key]
        public int SalonId { get; set; } 
        public int SalonNo { get; set; }

    }
}
