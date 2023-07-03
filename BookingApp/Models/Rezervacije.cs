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
    [DisplayName("Broj gostiju")]
    public int BrojGostiju { get; set; }
    [DisplayName("Datum rezervacije")]
    public DateTime DatumRezervacije { get; set; }
    [DisplayName("Početak rezervacije")]
    public DateTime PočetakRezervacije { get; set; }
    [DisplayName("Kraj rezervacije")]
    public DateTime KrajRezervacije { get; set; }

    public virtual Korisnici? Korisnik { get; set; } = null!;

    public virtual Smještaj? Smještaj { get; set; } = null!;
}
