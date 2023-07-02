using BookingApp.Models;
using BookingApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookingApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookingAppContext _context;


        public HomeController(ILogger<HomeController> logger, BookingAppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HomeDataViewModel model = new HomeDataViewModel();
            model.UkupnoKorisnika = _context.Korisnicis.Count();
            model.BrojSmještaja = _context.Smještajs.Count();
            model.BrojRecenzija = _context.Recenzijas.Count();
            model.Datum = DateTime.Now.ToString("dd.MM.yyyy.");
            model.DanasnjeRezervacija = _context.Rezervacijes.Where(x => x.DatumRezervacije.Date == DateTime.Now.Date).Count();
            model.listaKorisnika = _context.Korisnicis.OrderByDescending(x => x.KorisničkoIme).Take(5).ToList();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}