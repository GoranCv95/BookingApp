using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookingApp.Models;

public partial class Recenzija
{
    public int RecenzijaId { get; set; }
    [DisplayName("Korisnik")]
    public int KorisnikId { get; set; }
    [DisplayName("Smještaj")]
    public int SmještajId { get; set; }

    public decimal Ocjena { get; set; }

    public string? Komentar { get; set; }

    public virtual Korisnici? Korisnik { get; set; } = null!;

    public virtual Smještaj? Smještaj { get; set; } = null!;
}
