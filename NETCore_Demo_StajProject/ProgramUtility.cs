using NETCore_Demo_StajProject.DAL;
using System.Text.Json;

namespace NETCore_Demo_StajProject.Views
{
    public static class ProgramUtility
    {
        public static string musteriSession = ("Musteri");
        public static string personelSession = ("Personel");

        public static Musteri? GetMusteri(HttpContext context)
        {
            var musteriSesion = context.Session.GetString(ProgramUtility.musteriSession);
            if (musteriSesion == null) return null;
            var musteri = JsonSerializer.Deserialize<Musteri>(musteriSesion);
            return musteri;
        }
        public static Personel? GetPersonel(HttpContext context)
        {
            var personelSesion = context.Session.GetString(ProgramUtility.personelSession);
            if (personelSesion == null) return null;
            var personel = JsonSerializer.Deserialize<Personel>(personelSesion);
            return personel;
        }
    }
}
