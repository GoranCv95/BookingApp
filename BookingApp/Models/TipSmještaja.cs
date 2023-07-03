using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BookingApp.Models;

public partial class TipSmještaja
{
    public int TipSmještajaId { get; set; }
    [DisplayName("Vrsta smještaja")]
    public string TipSmještaja1 { get; set; } = null!;

    public virtual ICollection<Smještaj> Smještajs { get; set; } = new List<Smještaj>();
}
