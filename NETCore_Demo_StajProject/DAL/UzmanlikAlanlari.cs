using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_UzmanlıkAlanları")]
    public class UzmanlikAlanlari
    {
        [Key]
        public int UzmanlikId { get; set; }
        public string UzmanlikAd{ get; set; }
    }
}
