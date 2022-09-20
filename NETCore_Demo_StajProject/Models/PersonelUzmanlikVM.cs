using NETCore_Demo_StajProject.DAL;

namespace NETCore_Demo_StajProject.Models
{
    public class PersonelUzmanlikVM
    {
        public Personel Personel { get; set; }
        public PersonelUzmanlik PersonelUzmanlik { get; set; }
        public UzmanlikAlanlari UzmanlikAlanlari { get; set; }
    }
}
