using BookingApp.Models;

namespace BookingApp.ViewModels
{
    public class HomeDataViewModel
    {
        public int UkupnoKorisnika { get; set; }
        public int BrojSmještaja { get; set; }
        public int DanasnjeRezervacija { get; set; }
        public int BrojRecenzija { get; set; }
        public string Datum { get; set; }

        public List<Korisnici> listaKorisnika { get; set; }
    }
}
