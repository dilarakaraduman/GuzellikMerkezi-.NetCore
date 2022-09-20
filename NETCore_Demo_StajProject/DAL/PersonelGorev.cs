using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_PersonelGorev")]
    public class PersonelGorev
    {
        [Key]
        public int PersonelGorevId { get; set; }
        public int GorevId { get; set; }
        public int PersonelId { get; set; }
    }
}