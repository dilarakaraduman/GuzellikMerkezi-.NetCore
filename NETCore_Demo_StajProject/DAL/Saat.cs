using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_Saat")]
    public class Saat
    {
        [Key]
        public int SaatId { get; set; }
        public TimeSpan SaatNo { get; set; }

    }
}
