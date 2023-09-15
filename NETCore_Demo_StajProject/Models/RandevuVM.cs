using NETCore_Demo_StajProject.DAL;

namespace NETCore_Demo_StajProject.Models
{
    public class RandevuVM
    {
        public Musteri Musteri { get; set; }
        public Randevu Randevu { get; set; }
        public Personel Personel { get; set; }
        public Salon Salon { get; set; }
        public Saat Saat { get; set; }
        //public UzmanlikAlanlari UzmanlikAlanlari { get; set; }
        public Operasyonlar Operasyonlar { get; set; }
      
    }
}
