using System;
using System.Collections.Generic;

#nullable disable

namespace C3xPAWM.Models.Entities
{
    public partial class Indirizzo
    {
        public int IndirizzoId { get; set; }
        public int NegozioId { get; set; }
        public string Regione { get; set; }
        public string Provincia { get; set; }
        public string Citta { get; set; }
        public string Via { get; set; }

        public virtual Negozio Negozio { get; set; }
    }
}
