using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookingApp.Models;

public partial class Rezervacije
{
    public int RezervacijaId { get; set; }
    [DisplayName("Korisnik")]
    public int KorisnikId { get; set; }
    [DisplayName("Smještaj")]
    public int SmještajId { get; set; }

    public int BrojGostiju { get; set; }

    public DateTime DatumRezervacije { get; set; }

    public DateTime PočetakRezervacije { get; set; }

    public DateTime KrajRezervacije { get; set; }

    public virtual Korisnici? Korisnik { get; set; } = null!;

    public virtual Smještaj? Smještaj { get; set; } = null!;
}
