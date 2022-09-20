using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_Gorev")]
    public class Gorev
    {
        [Key]
        public int GorevId { get; set; }
        public string GorevAd { get; set; }
    }
}
