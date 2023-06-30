using System;
using System.Collections.Generic;

namespace BookingApp.Models;

public partial class Korisnici
{
    public int KorisnikId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string KorisničkoIme { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string BrojTelefona { get; set; } = null!;

    public virtual ICollection<Recenzija> Recenzijas { get; set; } = new List<Recenzija>();

    public virtual ICollection<Rezervacije> Rezervacijes { get; set; } = new List<Rezervacije>();
}
