using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace NETCore_Demo_StajProject.DAL
{
    [Table("Tbl_PersonelUzmanlik")]
    public class PersonelUzmanlik
    {
        [Key]
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public int UzmanlikId {get;set; }
    }
}
