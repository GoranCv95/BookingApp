using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookingApp.Models;

public partial class Smještaj
{
    public int SmještajId { get; set; }

    public string Naziv { get; set; } = null!;

    public string Adresa { get; set; } = null!;

    public int Cijena { get; set; }

    public string Email { get; set; } = null!;
    [DisplayName("Tip smještaja")]
    public int TipSmještajaId { get; set; }

    public virtual ICollection<Recenzija> Recenzijas { get; set; } = new List<Recenzija>();

    public virtual ICollection<Rezervacije> Rezervacijes { get; set; } = new List<Rezervacije>();

    public virtual TipSmještaja? TipSmještaja { get; set; } = null!;
}
